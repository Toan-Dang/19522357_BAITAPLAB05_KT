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

    public class DiemCachLiesController : Controller {
        private readonly AppDbContext _context;

        public DiemCachLiesController(AppDbContext context) {
            _context = context;
        }

        // GET: DiemCachLies
        public IActionResult Index() {
            ViewData["Cachly"] = new SelectList(_context.Set<DiemCachLy>(), "MaDiemCachLy", "TenDiemCachLy");
            return View();
        }

        [HttpPost]
        public async IActionResult Select([FromBody] string id) {
            var cn = await _context.CongNhan.Include(p => p.DiemCachLy).Where(p => p.MaDiemCachLy == id).ToListAsync();
            return View();
        }

        // GET: DiemCachLies/Details/5
        public async Task<IActionResult> Details(string id) {
            if (id == null) {
                return NotFound();
            }

            var diemCachLy = await _context.DiemCachLy
                .FirstOrDefaultAsync(m => m.MaDiemCachLy == id);
            if (diemCachLy == null) {
                return NotFound();
            }

            return View(diemCachLy);
        }

        // GET: DiemCachLies/Create
        public IActionResult Create() {
            return View();
        }

        // POST: DiemCachLies/Create To protect from overposting attacks, enable the specific
        // properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDiemCachLy,TenDiemCachLy,DiaChi")] DiemCachLy diemCachLy) {
            if (ModelState.IsValid) {
                _context.Add(diemCachLy);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(diemCachLy);
        }

        // GET: DiemCachLies/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var diemCachLy = await _context.DiemCachLy.FindAsync(id);
            if (diemCachLy == null) {
                return NotFound();
            }
            return View(diemCachLy);
        }

        // POST: DiemCachLies/Edit/5 To protect from overposting attacks, enable the specific
        // properties you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaDiemCachLy,TenDiemCachLy,DiaChi")] DiemCachLy diemCachLy) {
            if (id != diemCachLy.MaDiemCachLy) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(diemCachLy);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!DiemCachLyExists(diemCachLy.MaDiemCachLy)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(diemCachLy);
        }

        // GET: DiemCachLies/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var diemCachLy = await _context.DiemCachLy
                .FirstOrDefaultAsync(m => m.MaDiemCachLy == id);
            if (diemCachLy == null) {
                return NotFound();
            }

            return View(diemCachLy);
        }

        // POST: DiemCachLies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var diemCachLy = await _context.DiemCachLy.FindAsync(id);
            _context.DiemCachLy.Remove(diemCachLy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiemCachLyExists(string id) {
            return _context.DiemCachLy.Any(e => e.MaDiemCachLy == id);
        }
    }
}