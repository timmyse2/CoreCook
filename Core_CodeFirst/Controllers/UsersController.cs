using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_CodeFirst.Models;

namespace Core_CodeFirst.Controllers
{
    public class UsersController : Controller
    {
        private readonly BlogDbContext _context;

        public UsersController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Users.ToListAsync());

            var ctx = _context.Users.Include(m => m.Blogs).ToListAsync();       //:: include another model

            if (GetMySession("IsAdmin") == "YES")
            {
                ViewData["IsAdmin"] = "YES";
                ViewData["UserAccount"] = HttpContext.Request.Cookies["UserAccount"];
            }

            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(m=>m.Blogs) //:: include another model
                //.Include(m => m.vt_Posts) //:: include another model
                .FirstOrDefaultAsync(m => m.Id == id);

            //var user = await _context.Users                
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            //::
            if (GetMySession("IsAdmin") != "YES")
            {
                TempData["td_serverMessage"] = "權限不足, 請登入";
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData["td_serverMessage"] = "已執行建立";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //::
            if (GetMySession("IsAdmin") != "YES")            
            {
                TempData["td_serverMessage"] = "權限不足, 請登入";
                return RedirectToAction("Index");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        TempData["td_serverMessage"] = "更新時發生錯誤";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["td_serverMessage"] = "已更新";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //::
            if (GetMySession("IsAdmin") != "YES")            
            {
                TempData["td_serverMessage"] = "權限不足";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["td_serverMessage"] = "已執行刪除";
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        //::<Timmy>
        public string GetMySession(string key)
        {
            string _str = null;
            try
            {
                byte[] bv = null;
                //Microsoft.AspNetCore.Http.HttpContext hp = new Microsoft.AspNetCore.Http.HttpContext();s
                HttpContext.Session.TryGetValue(key, out bv);
                //::byte[] to string
                _str = System.Text.Encoding.Default.GetString(bv);
            }
            catch
            {
            }
            return _str;
        }
    }
}


