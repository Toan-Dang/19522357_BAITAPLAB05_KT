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

    public class CongNhansController : Controller {

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult List(int sotrieuchung) {
            var context = HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            return View(context.sqlListByTimeCongNhan(sotrieuchung));
        }

        public IActionResult DiemCachLy() {
            var context = HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            return View(context.sqlListDiemCachLy());
        }

        public IActionResult List2(string MaDiemCachLy) {
            var context = HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            return View(context.sqlListDiemCachLy(MaDiemCachLy));
        }

        public IActionResult Delete(int id) {
            var context = HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            context.sqlXoaCongNhan(id);
            return RedirectToAction(nameof(List2));
        }

        public IActionResult Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            var context = HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;

            return View(context.infoCongNhan(id));
        }
    }
}