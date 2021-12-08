using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _BAITAPLAB05_KT.Models {

    public class CongNhan {

        [Key]
        public string MaCongNhan { get; set; }

        public string TenCongNhan { get; set; }
        public bool GioiTinh { get; set; }
        public int NamSinh { get; set; }
        public string NuocVe { get; set; }
        public string MaDiemCachLy { get; set; }

        [ForeignKey("MaDiemCachLy")]
        public virtual DiemCachLy DiemCachLy { get; set; }

        public virtual ICollection<CN_TC> CN_TCs { get; set; }
    }
}