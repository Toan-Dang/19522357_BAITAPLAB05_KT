using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _BAITAPLAB05_KT.Data;
using _BAITAPLAB05_KT.Models;

namespace _BAITAPLAB05_KT.Controllers {

    public class TrieuChungsController : Controller {
        private readonly AppDbContext _context;

        public TrieuChungsController(AppDbContext context) {
            _context = context;
        }

        // GET: TrieuChungs
        public async Task<IActionResult> Index() {
            return View(await _context.TrieuChung.ToListAsync());
        }

        // GET: TrieuChungs/Details/5
        public async Task<IActionResult> Details(string id) {
            if (id == null) {
                return NotFound();
            }

            var trieuChung = await _context.TrieuChung
                .FirstOrDefaultAsync(m => m.MaTrieuChung == id);
            if (trieuChung == null) {
                return NotFound();
            }

            return View(trieuChung);
        }

        // GET: TrieuChungs/Create
        public IActionResult Create() {
            return View();
        }

        // POST: TrieuChungs/Create To protect from overposting attacks, enable the specific
        // properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTrieuChung,TenTrieuChung")] TrieuChung trieuChung) {
            if (ModelState.IsValid) {
                _context.Add(trieuChung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trieuChung);
        }

        // GET: TrieuChungs/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var trieuChung = await _context.TrieuChung.FindAsync(id);
            if (trieuChung == null) {
                return NotFound();
            }
            return View(trieuChung);
        }

        // POST: TrieuChungs/Edit/5 To protect from overposting attacks, enable the specific
        // properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTrieuChung,TenTrieuChung")] TrieuChung trieuChung) {
            if (id != trieuChung.MaTrieuChung) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(trieuChung);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!TrieuChungExists(trieuChung.MaTrieuChung)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trieuChung);
        }

        // GET: TrieuChungs/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var trieuChung = await _context.TrieuChung
                .FirstOrDefaultAsync(m => m.MaTrieuChung == id);
            if (trieuChung == null) {
                return NotFound();
            }

            return View(trieuChung);
        }

        // POST: TrieuChungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var trieuChung = await _context.TrieuChung.FindAsync(id);
            _context.TrieuChung.Remove(trieuChung);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrieuChungExists(string id) {
            return _context.TrieuChung.Any(e => e.MaTrieuChung == id);
        }
    }
}