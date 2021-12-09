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

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public void Create(DiemCachLy diemCachLy) {
            var context = HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            context.sqlInsertDiemCachLy(diemCachLy);
        }
    }
}