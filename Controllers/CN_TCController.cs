using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _BAITAPLAB05_KT.Data;
using _BAITAPLAB05_KT.Models;
using System.Data.SqlClient;

namespace _BAITAPLAB05_KT.Controllers {

    public class CN_TCController : Controller {
        private readonly AppDbContext _context;

        public CN_TCController(AppDbContext context) {
            _context = context;
        }

        // GET: CN_TC
        public async Task<IActionResult> Index() {
            var appDbContext = _context.CN_TC.Include(c => c.CongNhan).Include(c => c.TrieuChung);
            return View(await appDbContext.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Select([FromBody] int select) {
            List<CongNhan> list = new List<CongNhan>();
            List<string> macn = new List<string>();
            using (SqlConnection conn = new SqlConnection("Server=TOANDANG\\TOANDANG; Database=covid19; Trusted_Connection=True; MultipleActiveResultSets=true")) {
                conn.Open();
                string str = @"select a.MaCongNhan from cn_tc a join congnhan b
                                    on a.MaCongNhan = b.MaCongNhan
                                    group by  a.MaCongNhan having count(a.MaCongNhan) >= @soluong";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("soluong", select);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        macn.Add(reader["MaCongNhan"].ToString());
                    }
                    reader.Close();
                }
                conn.Close();
            }
            foreach (var item in macn) {
                var congnhan = await _context.CongNhan.Where(p => p.MaCongNhan == item).FirstAsync();
                list.Add(congnhan);
            }

            return Json(new {
                newUrl = Url.Action("Details", "CN_TC", new { id = list })
            });
        }

        // GET: CN_TC/Details/5
        public async Task<IActionResult> Details(List<CongNhan> id) {
            return View(id);
        }

        // GET: CN_TC/Create
        public IActionResult Create() {
            ViewData["MaCongNhan"] = new SelectList(_context.Set<CongNhan>(), "MaCongNhan", "TenCongNhan");
            ViewData["MaTrieuChung"] = new SelectList(_context.TrieuChung, "MaTrieuChung", "TenTrieuChung");
            return View();
        }

        // POST: CN_TC/Create To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaCongNhan,MaTrieuChung")] CN_TC cN_TC) {
            if (ModelState.IsValid) {
                _context.Add(cN_TC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCongNhan"] = new SelectList(_context.Set<CongNhan>(), "MaCongNhan", "MaCongNhan", cN_TC.MaCongNhan);
            ViewData["MaTrieuChung"] = new SelectList(_context.TrieuChung, "MaTrieuChung", "MaTrieuChung", cN_TC.MaTrieuChung);
            return View(cN_TC);
        }

        // GET: CN_TC/Edit/5
        public async Task<IActionResult> Edit(string id) {
            if (id == null) {
                return NotFound();
            }

            var cN_TC = await _context.CN_TC.FindAsync(id);
            if (cN_TC == null) {
                return NotFound();
            }
            ViewData["MaCongNhan"] = new SelectList(_context.Set<CongNhan>(), "MaCongNhan", "MaCongNhan", cN_TC.MaCongNhan);
            ViewData["MaTrieuChung"] = new SelectList(_context.TrieuChung, "MaTrieuChung", "MaTrieuChung", cN_TC.MaTrieuChung);
            return View(cN_TC);
        }

        // POST: CN_TC/Edit/5 To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaCongNhan,MaTrieuChung")] CN_TC cN_TC) {
            if (id != cN_TC.MaCongNhan) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(cN_TC);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CN_TCExists(cN_TC.MaCongNhan)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCongNhan"] = new SelectList(_context.Set<CongNhan>(), "MaCongNhan", "MaCongNhan", cN_TC.MaCongNhan);
            ViewData["MaTrieuChung"] = new SelectList(_context.TrieuChung, "MaTrieuChung", "MaTrieuChung", cN_TC.MaTrieuChung);
            return View(cN_TC);
        }

        // GET: CN_TC/Delete/5
        public async Task<IActionResult> Delete(string id) {
            if (id == null) {
                return NotFound();
            }

            var cN_TC = await _context.CN_TC
                .Include(c => c.CongNhan)
                .Include(c => c.TrieuChung)
                .FirstOrDefaultAsync(m => m.MaCongNhan == id);
            if (cN_TC == null) {
                return NotFound();
            }

            return View(cN_TC);
        }

        // POST: CN_TC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id) {
            var cN_TC = await _context.CN_TC.FindAsync(id);
            _context.CN_TC.Remove(cN_TC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CN_TCExists(string id) {
            return _context.CN_TC.Any(e => e.MaCongNhan == id);
        }
    }
}