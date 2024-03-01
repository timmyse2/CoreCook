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
    public class BlogsController : Controller
    {
        private readonly BlogDbContext _context;

        public BlogsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var blogDbContext = _context.Blogs.Include(b => b.User);

            //< h5 class="text-danger" id="bg">@TempData["action_msg"]</h5>

            if (GetMySession("IsAdmin") == "YES")
            {
                ViewData["IsAdmin"] = "YES";
                ViewData["UserAccount"] = HttpContext.Request.Cookies["UserAccount"];
            }

            return View(await blogDbContext.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["action_msg"] = "找不到找不到";
                return RedirectToAction("Index");
                //return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.User)
                ////.Include(b=>b.Post) //:: timmy tried to include post
                .FirstOrDefaultAsync(m => m.BlogId == id);


            if (blog == null)
            {
                TempData["action_msg"] = "找不到";
                return RedirectToAction("Index");
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            //::
            if (GetMySession("IsAdmin") != "YES")
            {
                TempData["action_msg"] = "權限不足, 請登入";

                return RedirectToAction("Login", "Comics");
                //return RedirectToAction("Index");
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            //::add            
            //var SelectList_withName = new SelectList(_context.Users, "Id", "UserName", blog.UserId);
            var SelectList_withName = new SelectList(_context.Users, "Id", "UserName");
            ViewData["UserId_withName"] = SelectList_withName;
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,BlogName,Url,UserId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blog);
                await _context.SaveChangesAsync();
                TempData["action_msg"] = "Created";
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", blog.UserId);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //::
            if (GetMySession("IsAdmin") != "YES")
            {
                TempData["action_msg"] = "權限不足, 請登入";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", blog.UserId);

            //::set name in view
            //ViewData["UserName"] = new SelectList(_context.Users, "UserName", "UserName", blog.UserId);
            //ViewData["UserName"] = new SelectList(_context.Users, "UserName", blog.UserId.ToString());
            //ViewData["UserName"] = new SelectList(_context.Users, blog.UserId.ToString(), "UserName");

            var SelectList_withName = new SelectList(_context.Users, "Id", "UserName", blog.UserId);            
            ViewData["UserId_withName"] = SelectList_withName;
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,BlogName,Url,UserId")] Blog blog)
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,BlogName,Url,UserId")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    TempData["action_msg"] = "Edited";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", blog.UserId);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //::
            if (GetMySession("IsAdmin") != "YES")
            {
                TempData["action_msg"] = "權限不足, 請登入";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            TempData["action_msg"] = "Removed";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
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
