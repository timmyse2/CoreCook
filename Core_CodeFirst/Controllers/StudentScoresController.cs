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
    public class StudentScoresController : Controller
    {
        private readonly ComicDbContext _context;

        public StudentScoresController(ComicDbContext context)
        {
            _context = context;
        }

        // GET: StudentScores
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentScore.ToListAsync());
        }

        // GET: StudentScores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentScore = await _context.StudentScore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentScore == null)
            {
                return NotFound();
            }

            return View(studentScore);
        }

        // GET: StudentScores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Chinese,English,Math,Total")] StudentScore studentScore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentScore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentScore);
        }

        // GET: StudentScores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentScore = await _context.StudentScore.FindAsync(id);
            if (studentScore == null)
            {
                return NotFound();
            }
            return View(studentScore);
        }

        // POST: StudentScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Chinese,English,Math,Total")] StudentScore studentScore)
        {
            if (id != studentScore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentScoreExists(studentScore.Id))
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
            return View(studentScore);
        }

        // GET: StudentScores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentScore = await _context.StudentScore
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentScore == null)
            {
                return NotFound();
            }

            return View(studentScore);
        }

        // POST: StudentScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentScore = await _context.StudentScore.FindAsync(id);
            _context.StudentScore.Remove(studentScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentScoreExists(int id)
        {
            return _context.StudentScore.Any(e => e.Id == id);
        }
    }
}
