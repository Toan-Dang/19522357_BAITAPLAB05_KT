using Microsoft.EntityFrameworkCore;
using _BAITAPLAB05_KT.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace _BAITAPLAB05_KT.Data {

    public class AppDbContext {
        public string _Conn { get; set; }

        public AppDbContext(string _conn) {
            this._Conn = _conn;
        }

        private SqlConnection GetConnection() {
            return new SqlConnection(_Conn);
        }

        public int sqlInsertDiemCachLy(DiemCachLy diemCachLy) {
            using (SqlConnection conn = GetConnection()) {
                conn.Open();
                var str = "insert into DiemCachLy values(@madiemcachly, @tendiemcachly, @diachi)";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("madiemcachly", diemCachLy.MaDiemCachLy);
                cmd.Parameters.AddWithValue("tendiemcachly", diemCachLy.TenDiemCachLy);
                cmd.Parameters.AddWithValue("diachi", diemCachLy.DiaChi);
                return (cmd.ExecuteNonQuery());
            }
        }

        public List<object> sqlListDiemCachLy(string maDiemCachLy) {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection()) {
                conn.Open();
                string str = @"Select MaCongNhan, TenCongNhan from CongNhan where MaDiemCachLy = @value;";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("value", maDiemCachLy);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(new {
                            TenCongNhan = reader["TenCongNhan"].ToString(),
                            MaCongNhan = reader["MaCongNhan"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public object infoCongNhan(int? id) {
            object o = new object();
            using (SqlConnection conn = GetConnection()) {
                conn.Open();
                string str = @"Select top(1) MaCongNhan, TenCongNhan, GioiTinh, NamSinh, NuocVe from CongNhan where MaCongNhan = @value";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("value", id);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        o = new {
                            MaCongNhan = reader["MaCongNhan"].ToString(),
                            TenCongNhan = reader["TenCongNhan"].ToString(),
                            GioiTinh = Convert.ToInt32(reader["GioiTinh"]),
                            NamSinh = Convert.ToInt32(reader["NamSinh"]),
                            NuocVe = reader["NuocVe"].ToString()
                        };
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return o;
        }

        public List<object> sqlListDiemCachLy() {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection()) {
                conn.Open();
                string str = @"Select MaDiemCachLy, TenDiemCachLy from DiemCachLy;";
                SqlCommand cmd = new SqlCommand(str, conn);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(new {
                            TenDiem = reader["TenDiemCachLy"].ToString(),
                            MaDiemCachLy = reader["MaDiemCachLy"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<object> sqlListByTimeCongNhan(int sotrieuchung) {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection()) {
                conn.Open();
                string str = @"select cn.TenCongNhan, cn.NamSinh, cn.NuocVe, COUNT(*) as SoTrieuChung
                                from CongNhan cn join CN_TC tc on cn.MaCongNhan = tc.MaCongNhan
                                group by  cn.TenCongNhan, cn.NamSinh, cn.NuocVe
                                having Count(*) >= @SoLanInput";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("SoLanInput", sotrieuchung);
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        list.Add(new {
                            TenCongNhan = reader["TenCongNhan"].ToString(),
                            NamSinh = Convert.ToInt32(reader["NamSinh"]),
                            NuocVe = reader["NuocVe"].ToString(),
                            SoTrieuChung = Convert.ToInt32(reader["SoTrieuChung"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<object> sqlXoaCongNhan(int macongnhan) {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection()) {
                conn.Open();
                string str = @"delete from congnhan where MaCongNhan = @MaCongNhan";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("MaCongNhan", macongnhan);
                using (var reader = cmd.ExecuteReader()) {
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }
    }
}