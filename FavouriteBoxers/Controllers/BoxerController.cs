using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FavouriteBoxers.Data;
using FavouriteBoxers.Models;
using Microsoft.AspNetCore.Authorization;

namespace FavouriteBoxers.Controllers
{
    public class BoxerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoxerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boxer
        public async Task<IActionResult> Index()
        {
            return _context.Boxer != null ?
                        View(await _context.Boxer.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Boxer'  is null.");
        }

        // GET: Boxer/ShowSearchForm 
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Boxer/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            var boxer = await _context.Boxer.Where(boxer => boxer.Full_Name.ToLower().Contains(SearchPhrase.ToLower())).ToListAsync();

            Console.WriteLine("The searched boxer " + SearchPhrase + " " + boxer);

            return View("Index", boxer);
        }

        // GET: Boxer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Boxer == null)
            {
                return NotFound();
            }

            var boxer = await _context.Boxer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boxer == null)
            {
                return NotFound();
            }

            return View(boxer);
        }

        // GET: Boxer/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boxer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Full_Name,Alias,Nationality")] Boxer boxer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boxer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boxer);
        }

        [Authorize]
        // GET: Boxer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Boxer == null)
            {
                return NotFound();
            }

            var boxer = await _context.Boxer.FindAsync(id);
            if (boxer == null)
            {
                return NotFound();
            }
            return View(boxer);
        }

        // POST: Boxer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Full_Name,Alias,Nationality")] Boxer boxer)
        {
            if (id != boxer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boxer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoxerExists(boxer.Id))
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
            return View(boxer);
        }

        // GET: Boxer/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Boxer == null)
            {
                return NotFound();
            }

            var boxer = await _context.Boxer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boxer == null)
            {
                return NotFound();
            }

            return View(boxer);
        }

        // POST: Boxer/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Boxer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Boxer'  is null.");
            }
            var boxer = await _context.Boxer.FindAsync(id);
            if (boxer != null)
            {
                _context.Boxer.Remove(boxer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoxerExists(int id)
        {
            return (_context.Boxer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
