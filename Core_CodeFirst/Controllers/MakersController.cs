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
    public class MakersController : Controller
    {
        private readonly ComicDbContext _context;

        public MakersController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: Makers
        public async Task<IActionResult> Index()
        {
            //::get session
            byte[] bv = null;
            HttpContext.Session.TryGetValue("IsAdmin", out bv);
            string _str = null;
            if (bv != null) _str = System.Text.Encoding.Default.GetString(bv);
            //::set admin
            //if (HttpContext.Request.Cookies["IsAdmin"] == "YES")
            if (_str == "YES")
            {
                ViewData["IsAdmin"] = "YES";
            }

            return View(await _context.Makers.ToListAsync());
        }

        // GET: Makers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["action_msg"] = "No record | 無記錄";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            //::
            byte[] bv = null;
            HttpContext.Session.TryGetValue("IsAdmin", out bv);
            string _str = null;
            if (bv != null) _str = System.Text.Encoding.Default.GetString(bv);
            //if (HttpContext.Request.Cookies["IsAdmin"] == "YES")
            if(_str == "YES")
            {
                ViewData["IsAdmin"] = "YES";
            }

            var maker = await _context.Makers
                .FirstOrDefaultAsync(m => m.MakerId == id);
            if (maker == null)
            {
                TempData["action_msg"] = "No record";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            return View(maker);
        }

        // GET: Makers/Create
        public IActionResult Create()
        {

            //::login check
            //use session
            byte[] bv = null;
            HttpContext.Session.TryGetValue("IsAdmin", out bv);
            string _str = null;
            if (bv != null) _str = System.Text.Encoding.Default.GetString(bv);
            //if (HttpContext.Request.Cookies["IsAdmin"] != "YES")
            if (_str != "YES")
            {
                TempData["action_msg"] = "You have no power here | 權限不足請登入";
                //return RedirectToAction("Index");
                return RedirectToAction("login", "comics");
                //return Content("You have no power here!");
            }

            return View();
        }

        // POST: Makers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MakerId,UserName,Image")] Maker maker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maker);
                await _context.SaveChangesAsync();
                TempData["action_msg"] = "Created";
                return RedirectToAction(nameof(Index));
            }
            return View(maker);
        }

        // GET: Makers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["action_msg"] = "No record";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            //::login check
            //use session
            byte[] bv = null;            
            HttpContext.Session.TryGetValue("IsAdmin", out bv);
            string _str = null;
            if (bv != null) _str = System.Text.Encoding.Default.GetString(bv);
            //if (HttpContext.Request.Cookies["IsAdmin"] != "YES")
            if(_str != "YES")
            {
                TempData["action_msg"] = "You have no power here | 權限不足請登入";
                return RedirectToAction("Index");
                //return Content("You have no power here!");
            }

            var maker = await _context.Makers.FindAsync(id);
            if (maker == null)
            {
                TempData["action_msg"] = "No record";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }
            return View(maker);
        }

        // POST: Makers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MakerId,UserName,Image")] Maker maker)
        {
            if (id != maker.MakerId)
            {
                TempData["action_msg"] = "No record";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MakerExists(maker.MakerId))
                    {
                        TempData["action_msg"] = "No record";
                        return RedirectToAction(nameof(Index));
                        //return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["action_msg"] = "Updated";
                return RedirectToAction(nameof(Index));
            }
            return View(maker);
        }

        // GET: Makers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                TempData["action_msg"] = "No record";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            //::login check
            byte[] bv = null;
            HttpContext.Session.TryGetValue("IsAdmin", out bv);
            string _str = null;
            if (bv != null) _str = System.Text.Encoding.Default.GetString(bv);
            //if (HttpContext.Request.Cookies["IsAdmin"] != "YES")
            if(_str != "YES")
            {
                TempData["action_msg"] = "You have no power here | 權限不足請登入";
                return RedirectToAction("Index");
                //return Content("You have no power here!");
            }

            var maker = await _context.Makers
                .FirstOrDefaultAsync(m => m.MakerId == id);
            if (maker == null)
            {
                TempData["action_msg"] = "No record";
                return RedirectToAction(nameof(Index));
                //return NotFound();
            }

            return View(maker);
        }

        // POST: Makers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //::login check
            byte[] bv = null;
            HttpContext.Session.TryGetValue("IsAdmin", out bv);
            string _str = null;
            if (bv != null) _str = System.Text.Encoding.Default.GetString(bv);
            //if (HttpContext.Request.Cookies["IsAdmin"] != "YES")
            if (_str != "YES")
            {
                TempData["action_msg"] = "You have no power here | 權限不足請登入";
                return RedirectToAction("Index");
                //return Content("You have no power here!");
            }


            var maker = await _context.Makers.FindAsync(id);
            _context.Makers.Remove(maker);
            await _context.SaveChangesAsync();
            TempData["action_msg"] = "Deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool MakerExists(int id)
        {
            return _context.Makers.Any(e => e.MakerId == id);
        }
    }
}
