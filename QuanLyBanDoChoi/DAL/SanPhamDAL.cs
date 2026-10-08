using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanDoChoi.DTO;

namespace QuanLyBanDoChoi.DAL
{
    public class SanPhamDAL
    {
        /// <summary>
        /// Lấy danh sách sản phẩm (Hỗ trợ lọc chỉ lấy sản phẩm đang kinh doanh)
        /// </summary>
        public List<SanPhamDTO> LayDanhSach(bool chiLayConBan = false)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            string query = @"SELECT sp.MaSP, sp.MaLoai, l.TenLoai, sp.TenSP, sp.DoTuoi, 
                                    sp.TenXuatXu, sp.Hang, sp.GiaNhap, sp.DonGia, 
                                    sp.TonKho, sp.HinhAnh, sp.TrangThai,
                                    ISNULL(STUFF((
                                        SELECT ', ' + nt.TenNenTang
                                        FROM SanPham_NenTang spnt
                                        JOIN NenTang nt ON spnt.MaNenTang = nt.MaNenTang
                                        WHERE spnt.MaSP = sp.MaSP
                                        FOR XML PATH('')
                                    ), 1, 2, ''), N'Tại quầy') AS KenhDangBan
                             FROM SanPham sp
                             LEFT JOIN LoaiDoChoi l ON sp.MaLoai = l.MaLoai
                             WHERE (@ChiLayConBan = 0 OR sp.TrangThai = 1)
                             ORDER BY sp.MaSP DESC";

            using (SqlConnection conn = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ChiLayConBan", chiLayConBan ? 1 : 0);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(DocSanPhamTuReader(reader));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy chi tiết thông tin một sản phẩm theo mã
        /// </summary>
        public SanPhamDTO LayChiTiet(string maSP)
        {
            SanPhamDTO sp = null;
            string query = @"SELECT sp.MaSP, sp.MaLoai, l.TenLoai, sp.TenSP, sp.DoTuoi, 
                                    sp.TenXuatXu, sp.Hang, sp.GiaNhap, sp.DonGia, 
                                    sp.TonKho, sp.HinhAnh, sp.TrangThai,
                                    ISNULL(STUFF((
                                        SELECT ', ' + nt.TenNenTang
                                        FROM SanPham_NenTang spnt
                                        JOIN NenTang nt ON spnt.MaNenTang = nt.MaNenTang
                                        WHERE spnt.MaSP = sp.MaSP
                                        FOR XML PATH('')
                                    ), 1, 2, ''), N'Tại quầy') AS KenhDangBan
                             FROM SanPham sp
                             LEFT JOIN LoaiDoChoi l ON sp.MaLoai = l.MaLoai
                             WHERE sp.MaSP = @MaSP";

            using (SqlConnection conn = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP.Trim());
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sp = DocSanPhamTuReader(reader);
                    }
                }
            }

            return sp;
        }

        /// <summary>
        /// Lấy DataTable sản phẩm phục vụ nạp DataSource cho Controls
        /// </summary>
        public DataTable LayDataTable()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT sp.MaSP AS [Mã SP], sp.TenSP AS [Tên sản phẩm], 
                                    l.TenLoai AS [Danh mục], sp.DoTuoi AS [Độ tuổi], 
                                    sp.DonGia AS [Giá bán], sp.GiaNhap AS [Giá nhập], 
                                    sp.TonKho AS [Tồn kho], sp.Hang AS [Hãng], 
                                    sp.TenXuatXu AS [Xuất xứ],
                                    ISNULL(STUFF((
                                        SELECT ', ' + nt.TenNenTang
                                        FROM SanPham_NenTang spnt
                                        JOIN NenTang nt ON spnt.MaNenTang = nt.MaNenTang
                                        WHERE spnt.MaSP = sp.MaSP
                                        FOR XML PATH('')
                                    ), 1, 2, ''), N'Tại quầy') AS [Kênh bán]
                             FROM SanPham sp
                             LEFT JOIN LoaiDoChoi l ON sp.MaLoai = l.MaLoai
                             WHERE sp.TrangThai = 1
                             ORDER BY sp.MaSP DESC";

            using (SqlConnection conn = Database.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            return dt;
        }

        /// <summary>
        /// Kiểm tra mã sản phẩm đã tồn tại trong CSDL hay chưa
        /// </summary>
        public bool KiemTraTonTai(string maSP)
        {
            string query = "SELECT COUNT(1) FROM SanPham WHERE MaSP = @MaSP";

            using (SqlConnection conn = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP.Trim());
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        /// <summary>
        /// Thêm sản phẩm mới
        /// </summary>
        public bool Them(SanPhamDTO sp)
        {
            string query = @"INSERT INTO SanPham (MaSP, MaLoai, TenSP, DoTuoi, TenXuatXu, Hang, GiaNhap, DonGia, TonKho, HinhAnh, TrangThai)
                             VALUES (@MaSP, @MaLoai, @TenSP, @DoTuoi, @TenXuatXu, @Hang, @GiaNhap, @DonGia, @TonKho, @HinhAnh, @TrangThai)";

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, conn, tran);
                        cmd.Parameters.AddWithValue("@MaSP", sp.MaSP.Trim());
                        cmd.Parameters.AddWithValue("@MaLoai", string.IsNullOrEmpty(sp.MaLoai) ? (object)DBNull.Value : sp.MaLoai.Trim());
                        cmd.Parameters.AddWithValue("@TenSP", sp.TenSP.Trim());
                        cmd.Parameters.AddWithValue("@DoTuoi", string.IsNullOrEmpty(sp.DoTuoi) ? (object)DBNull.Value : sp.DoTuoi.Trim());
                        cmd.Parameters.AddWithValue("@TenXuatXu", string.IsNullOrEmpty(sp.TenXuatXu) ? (object)DBNull.Value : sp.TenXuatXu.Trim());
                        cmd.Parameters.AddWithValue("@Hang", string.IsNullOrEmpty(sp.Hang) ? (object)DBNull.Value : sp.Hang.Trim());
                        cmd.Parameters.AddWithValue("@GiaNhap", sp.GiaNhap);
                        cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
                        cmd.Parameters.AddWithValue("@TonKho", sp.TonKho);
                        cmd.Parameters.AddWithValue("@HinhAnh", string.IsNullOrEmpty(sp.HinhAnh) ? (object)DBNull.Value : sp.HinhAnh.Trim());
                        cmd.Parameters.AddWithValue("@TrangThai", sp.TrangThai ? 1 : 0);

                        int inserted = cmd.ExecuteNonQuery();

                        // Nếu có thông tin kênh bán (DanhSachKenhBan), đồng bộ vào bảng liên kết SanPham_NenTang
                        if (inserted > 0 && !string.IsNullOrWhiteSpace(sp.DanhSachKenhBan))
                        {
                            // DanhSachKenhBan lưu dạng "Tên nền tảng[, Tên nền tảng]..."
                            string[] parts = sp.DanhSachKenhBan.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var raw in parts)
                            {
                                string tenNenTang = raw.Trim();
                                if (string.IsNullOrEmpty(tenNenTang)) continue;

                                // Tìm MaNenTang tương ứng theo TenNenTang
                                string sel = "SELECT MaNenTang FROM NenTang WHERE TenNenTang = @TenNenTang";
                                using (SqlCommand cmdSel = new SqlCommand(sel, conn, tran))
                                {
                                    cmdSel.Parameters.AddWithValue("@TenNenTang", tenNenTang);
                                    object ma = cmdSel.ExecuteScalar();
                                    if (ma != null && ma != DBNull.Value)
                                    {
                                        string maNenTang = ma.ToString().Trim();
                                        string ins = "INSERT INTO SanPham_NenTang (MaSP, MaNenTang) VALUES (@MaSP, @MaNenTang)";
                                        using (SqlCommand cmdIns = new SqlCommand(ins, conn, tran))
                                        {
                                            cmdIns.Parameters.AddWithValue("@MaSP", sp.MaSP.Trim());
                                            cmdIns.Parameters.AddWithValue("@MaNenTang", maNenTang);
                                            try { cmdIns.ExecuteNonQuery(); } catch { /* Ignore if duplicate key */ }
                                        }
                                    }
                                }
                            }
                        }

                        tran.Commit();
                        return inserted > 0;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm (Đã bổ sung cập nhật TrangThai)
        /// </summary>
        public bool CapNhat(SanPhamDTO sp)
        {
            string query = @"UPDATE SanPham 
                             SET MaLoai = @MaLoai,
                                 TenSP = @TenSP,
                                 DoTuoi = @DoTuoi,
                                 TenXuatXu = @TenXuatXu,
                                 Hang = @Hang,
                                 GiaNhap = @GiaNhap,
                                 DonGia = @DonGia,
                                 TonKho = @TonKho,
                                 HinhAnh = @HinhAnh,
                                 TrangThai = @TrangThai
                             WHERE MaSP = @MaSP";

            using (SqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(query, conn, tran);
                        cmd.Parameters.AddWithValue("@MaSP", sp.MaSP.Trim());
                        cmd.Parameters.AddWithValue("@MaLoai", string.IsNullOrEmpty(sp.MaLoai) ? (object)DBNull.Value : sp.MaLoai.Trim());
                        cmd.Parameters.AddWithValue("@TenSP", sp.TenSP.Trim());
                        cmd.Parameters.AddWithValue("@DoTuoi", string.IsNullOrEmpty(sp.DoTuoi) ? (object)DBNull.Value : sp.DoTuoi.Trim());
                        cmd.Parameters.AddWithValue("@TenXuatXu", string.IsNullOrEmpty(sp.TenXuatXu) ? (object)DBNull.Value : sp.TenXuatXu.Trim());
                        cmd.Parameters.AddWithValue("@Hang", string.IsNullOrEmpty(sp.Hang) ? (object)DBNull.Value : sp.Hang.Trim());
                        cmd.Parameters.AddWithValue("@GiaNhap", sp.GiaNhap);
                        cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
                        cmd.Parameters.AddWithValue("@TonKho", sp.TonKho);
                        cmd.Parameters.AddWithValue("@HinhAnh", string.IsNullOrEmpty(sp.HinhAnh) ? (object)DBNull.Value : sp.HinhAnh.Trim());
                        cmd.Parameters.AddWithValue("@TrangThai", sp.TrangThai ? 1 : 0);

                        int updated = cmd.ExecuteNonQuery();

                        // Cập nhật lại bảng liên kết SanPham_NenTang: xóa cũ, thêm mới
                        if (updated > 0)
                        {
                            string del = "DELETE FROM SanPham_NenTang WHERE MaSP = @MaSP";
                            using (SqlCommand cmdDel = new SqlCommand(del, conn, tran))
                            {
                                cmdDel.Parameters.AddWithValue("@MaSP", sp.MaSP.Trim());
                                cmdDel.ExecuteNonQuery();
                            }

                            if (!string.IsNullOrWhiteSpace(sp.DanhSachKenhBan))
                            {
                                string[] parts = sp.DanhSachKenhBan.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var raw in parts)
                                {
                                    string tenNenTang = raw.Trim();
                                    if (string.IsNullOrEmpty(tenNenTang)) continue;

                                    string sel = "SELECT MaNenTang FROM NenTang WHERE TenNenTang = @TenNenTang";
                                    using (SqlCommand cmdSel = new SqlCommand(sel, conn, tran))
                                    {
                                        cmdSel.Parameters.AddWithValue("@TenNenTang", tenNenTang);
                                        object ma = cmdSel.ExecuteScalar();
                                        if (ma != null && ma != DBNull.Value)
                                        {
                                            string maNenTang = ma.ToString().Trim();
                                            string ins = "INSERT INTO SanPham_NenTang (MaSP, MaNenTang) VALUES (@MaSP, @MaNenTang)";
                                            using (SqlCommand cmdIns = new SqlCommand(ins, conn, tran))
                                            {
                                                cmdIns.Parameters.AddWithValue("@MaSP", sp.MaSP.Trim());
                                                cmdIns.Parameters.AddWithValue("@MaNenTang", maNenTang);
                                                try { cmdIns.ExecuteNonQuery(); } catch { }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        tran.Commit();
                        return updated > 0;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Xóa sản phẩm (Mặc định xóa mềm bằng cách đặt TrangThai = 0)
        /// </summary>
        public bool Xoa(string maSP, bool xoaMem = true)
        {
            string query = xoaMem
                ? "UPDATE SanPham SET TrangThai = 0 WHERE MaSP = @MaSP"
                : "DELETE FROM SanPham WHERE MaSP = @MaSP";

            using (SqlConnection conn = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP.Trim());

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo từ khóa (Mã, Tên, Hãng, Xuất xứ)
        /// </summary>
        public List<SanPhamDTO> TimKiem(string tuKhoa)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();

            string query = @"SELECT sp.MaSP, sp.MaLoai, l.TenLoai, sp.TenSP, sp.DoTuoi, 
                                    sp.TenXuatXu, sp.Hang, sp.GiaNhap, sp.DonGia, 
                                    sp.TonKho, sp.HinhAnh, sp.TrangThai,
                                    ISNULL(STUFF((
                                        SELECT ', ' + nt.TenNenTang
                                        FROM SanPham_NenTang spnt
                                        JOIN NenTang nt ON spnt.MaNenTang = nt.MaNenTang
                                        WHERE spnt.MaSP = sp.MaSP
                                        FOR XML PATH('')
                                    ), 1, 2, ''), N'Tại quầy') AS KenhDangBan
                             FROM SanPham sp
                             LEFT JOIN LoaiDoChoi l ON sp.MaLoai = l.MaLoai
                             WHERE sp.TrangThai = 1
                               AND (sp.MaSP LIKE @Keyword 
                                    OR sp.TenSP LIKE @Keyword 
                                    OR sp.Hang LIKE @Keyword 
                                    OR sp.TenXuatXu LIKE @Keyword 
                                    OR l.TenLoai LIKE @Keyword)
                             ORDER BY sp.MaSP DESC";

            using (SqlConnection conn = Database.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + tuKhoa.Trim() + "%");
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(DocSanPhamTuReader(reader));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Hàm bổ trợ chuyển đổi dữ liệu từ SqlDataReader sang SanPhamDTO
        /// </summary>
        private SanPhamDTO DocSanPhamTuReader(SqlDataReader reader)
        {
            return new SanPhamDTO
            {
                MaSP = reader["MaSP"].ToString().Trim(),
                MaLoai = reader["MaLoai"] != DBNull.Value ? reader["MaLoai"].ToString().Trim() : string.Empty,
                TenLoai = ColumnExists(reader, "TenLoai") && reader["TenLoai"] != DBNull.Value ? reader["TenLoai"].ToString().Trim() : string.Empty,
                TenSP = reader["TenSP"].ToString().Trim(),
                DoTuoi = reader["DoTuoi"] != DBNull.Value ? reader["DoTuoi"].ToString().Trim() : string.Empty,
                TenXuatXu = reader["TenXuatXu"] != DBNull.Value ? reader["TenXuatXu"].ToString().Trim() : string.Empty,
                Hang = reader["Hang"] != DBNull.Value ? reader["Hang"].ToString().Trim() : string.Empty,
                GiaNhap = reader["GiaNhap"] != DBNull.Value ? Convert.ToDecimal(reader["GiaNhap"]) : 0,
                DonGia = reader["DonGia"] != DBNull.Value ? Convert.ToDecimal(reader["DonGia"]) : 0,
                TonKho = reader["TonKho"] != DBNull.Value ? Convert.ToInt32(reader["TonKho"]) : 0,
                HinhAnh = reader["HinhAnh"] != DBNull.Value ? reader["HinhAnh"].ToString().Trim() : string.Empty,
                TrangThai = ColumnExists(reader, "TrangThai") && reader["TrangThai"] != DBNull.Value ? Convert.ToBoolean(reader["TrangThai"]) : true,
                DanhSachKenhBan = ColumnExists(reader, "KenhDangBan") && reader["KenhDangBan"] != DBNull.Value ? reader["KenhDangBan"].ToString().Trim() : "Tại quầy"
            };
        }

        private bool ColumnExists(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}