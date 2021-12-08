using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _BAITAPLAB05_KT.Data;
using _BAITAPLAB05_KT.Models;

namespace _BAITAPLAB05_KT.Controllers
{
    public class CongNhansController : Controller
    {
        private readonly AppDbContext _context;

        public CongNhansController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CongNhans
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CongNhan.Include(c => c.DiemCachLy);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CongNhans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congNhan = await _context.CongNhan
                .Include(c => c.DiemCachLy)
                .FirstOrDefaultAsync(m => m.MaCongNhan == id);
            if (congNhan == null)
            {
                return NotFound();
            }

            return View(congNhan);
        }

        // GET: CongNhans/Create
        public IActionResult Create()
        {
            ViewData["MaDiemCachLy"] = new SelectList(_context.DiemCachLy, "MaDiemCachLy", "MaDiemCachLy");
            return View();
        }

        // POST: CongNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCongNhan,TenCongNhan,GioiTinh,NamSinh,NuocVe,MaDiemCachLy")] CongNhan congNhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(congNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDiemCachLy"] = new SelectList(_context.DiemCachLy, "MaDiemCachLy", "MaDiemCachLy", congNhan.MaDiemCachLy);
            return View(congNhan);
        }

        // GET: CongNhans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congNhan = await _context.CongNhan.FindAsync(id);
            if (congNhan == null)
            {
                return NotFound();
            }
            ViewData["MaDiemCachLy"] = new SelectList(_context.DiemCachLy, "MaDiemCachLy", "MaDiemCachLy", congNhan.MaDiemCachLy);
            return View(congNhan);
        }

        // POST: CongNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaCongNhan,TenCongNhan,GioiTinh,NamSinh,NuocVe,MaDiemCachLy")] CongNhan congNhan)
        {
            if (id != congNhan.MaCongNhan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(congNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongNhanExists(congNhan.MaCongNhan))
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
            ViewData["MaDiemCachLy"] = new SelectList(_context.DiemCachLy, "MaDiemCachLy", "MaDiemCachLy", congNhan.MaDiemCachLy);
            return View(congNhan);
        }

        // GET: CongNhans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var congNhan = await _context.CongNhan
                .Include(c => c.DiemCachLy)
                .FirstOrDefaultAsync(m => m.MaCongNhan == id);
            if (congNhan == null)
            {
                return NotFound();
            }

            return View(congNhan);
        }

        // POST: CongNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var congNhan = await _context.CongNhan.FindAsync(id);
            _context.CongNhan.Remove(congNhan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongNhanExists(string id)
        {
            return _context.CongNhan.Any(e => e.MaCongNhan == id);
        }
    }
}
