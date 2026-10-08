using QuanLyBanDoChoi.BUS;
using QuanLyBanDoChoi.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanDoChoi.GUI
{
    public partial class UC_BanHang : UserControl
    {
        private bool rdTTTMDaChon;
        private bool rdTTCKDaChon;
        // Khai báo đối tượng xử lý logic (đổi tên SanPhamBUS theo đúng tên class của bạn)
        private SanPhamBUS sanPhamBUS = new SanPhamBUS();
        public UC_BanHang()
        {
            InitializeComponent();
        }

        // Normalize fonts for this control and child controls to fix inconsistent font issues
        private void NormalizeFonts()
        {
            Font appFont = new Font("Times New Roman", 10F, FontStyle.Regular);
            this.Font = appFont;
            ApplyFontRecursive(this, appFont);
        }

        private void ApplyFontRecursive(Control parent, Font font)
        {
            if (parent == null) return;

            foreach (Control c in parent.Controls)
            {
                try
                {
                    c.Font = font;
                }
                catch { }

                if (c is DataGridView dgv)
                {
                    try
                    {
                        dgv.DefaultCellStyle.Font = font;
                        dgv.ColumnHeadersDefaultCellStyle.Font = new Font(font.FontFamily, font.Size, FontStyle.Bold);
                    }
                    catch { }
                }

                // recurse
                if (c.HasChildren)
                    ApplyFontRecursive(c, font);
            }
        }
        private void UC_BanHang_Load(object sender, EventArgs e)
        {
            // Ensure consistent font across controls
            NormalizeFonts();

            rdTTTM.FlatStyle = FlatStyle.Flat;
            rdTTCK.FlatStyle = FlatStyle.Flat;
            cbbHinhThuc.SelectedItem = "_Tất cả_";

            // Đăng ký sự kiện tự đánh số STT khi nạp xong dữ liệu
            dgvThongTinDoChoi.DataBindingComplete += dgvThongTinDoChoi_DataBindingComplete;

            CapNhatBoLoc();
            DatLaiNgay();
            ApDungBoLoc();
            CauHinhGiaoDienBang();
            LoadDanhSachDoChoiTuDatabase();
        }
        private void dgvThongTinDoChoi_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CapNhatSTT();
        }
        private void CapNhatBoLoc()
        {
            string hinhThuc = cbbHinhThuc.Text.Trim();

            Color mauKhoa = Color.FromArgb(190, 190, 190);
            Color mauBinhThuong = Color.Black;

            if (hinhThuc == "_Tất cả_")
            {
                // Radio vẫn Enabled để mình điều khiển màu
                rdTTTM.Enabled = true;
                rdTTCK.Enabled = true;

                // Khóa nền tảng thật
                cbbNenTangLS.Enabled = false;

                // Màu nhạt
                rdTTTM.ForeColor = mauKhoa;
                rdTTCK.ForeColor = mauKhoa;
                lblTT.ForeColor = mauKhoa;
                lblNT.ForeColor = mauKhoa;

                // Không chọn gì
                rdTTTM.Checked = false;
                rdTTCK.Checked = false;

                rdTTTM.Text = "Tiền mặt";
            }
            else if (hinhThuc == "Tại quầy")
            {
                rdTTTM.Enabled = true;
                rdTTCK.Enabled = true;

                cbbNenTangLS.Enabled = false;

                rdTTTM.ForeColor = mauBinhThuong;
                rdTTCK.ForeColor = mauBinhThuong;

                lblTT.ForeColor = mauBinhThuong;
                lblNT.ForeColor = mauKhoa;

                rdTTTM.Text = "Tiền mặt";
            }
            else if (hinhThuc == "Online")
            {
                rdTTTM.Enabled = true;
                rdTTCK.Enabled = true;

                cbbNenTangLS.Enabled = true;

                rdTTTM.ForeColor = mauBinhThuong;
                rdTTCK.ForeColor = mauBinhThuong;

                lblTT.ForeColor = mauBinhThuong;
                lblNT.ForeColor = mauBinhThuong;

                rdTTTM.Text = "Thu hộ";
            }
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            // Chọn màu viền và độ dày của viền (ở đây để độ dày là 3 pixel)
            int borderWidth = 2;
            using (Pen pColor = new Pen(Color.Black, borderWidth))
            {
                // Vẽ khung viền đè lên Panel
                e.Graphics.DrawRectangle(pColor, 0, 0, p.ClientRectangle.Width, p.ClientRectangle.Height);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            // Chọn màu viền và độ dày của viền (ở đây để độ dày là 3 pixel)
            int borderWidth = 2;
            using (Pen pColor = new Pen(Color.Black, borderWidth))
            {
                // Vẽ khung viền đè lên Panel
                e.Graphics.DrawRectangle(pColor, 0, 0, p.ClientRectangle.Width , p.ClientRectangle.Height );
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            // Chọn màu viền và độ dày của viền (ở đây để độ dày là 3 pixel)
            int borderWidth = 2;
            using (Pen pColor = new Pen(Color.Black, borderWidth))
            {
                // Vẽ khung viền đè lên Panel
                e.Graphics.DrawRectangle(pColor, 0, 0, p.ClientRectangle.Width , p.ClientRectangle.Height );
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            // Chọn màu viền và độ dày của viền (ở đây để độ dày là 3 pixel)
            int borderWidth = 2;
            using (Pen pColor = new Pen(Color.Black, borderWidth))
            {
                // Vẽ khung viền đè lên Panel
                e.Graphics.DrawRectangle(pColor, 0, 0, p.ClientRectangle.Width , p.ClientRectangle.Height );
            }
        }
        private void DatLaiNgay()
        {
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = " ";

            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = " ";
        }
       


        private void cbbHinhThuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatBoLoc();
        }

        private void rdTTTM_Click(object sender, EventArgs e)
        {
            if (cbbHinhThuc.Text.Trim() == "_Tất cả_")
            {
                rdTTTM.Checked = false;
                return;
            }

            if (rdTTTMDaChon)
                rdTTTM.Checked = false;

            rdTTTMDaChon = false;
        }

        private void rdTTTM_MouseDown(object sender, MouseEventArgs e)
        {
            rdTTTMDaChon = rdTTTM.Checked;
        }

        private void rdTTCK_MouseDown(object sender, MouseEventArgs e)
        {
            rdTTCKDaChon = rdTTCK.Checked;
        }

        private void rdTTCK_Click(object sender, EventArgs e)
        {
            if (cbbHinhThuc.Text.Trim() == "_Tất cả_")
            {
                rdTTCK.Checked = false;
                return;
            }

            if (rdTTCKDaChon)
                rdTTCK.Checked = false;

            rdTTCKDaChon = false;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

            Panel p = sender as Panel;
            // Chọn màu viền và độ dày của viền (ở đây để độ dày là 3 pixel)
            int borderWidth = 2;
            using (Pen pColor = new Pen(Color.Black, borderWidth))
            {
                // Vẽ khung viền đè lên Panel
                e.Graphics.DrawRectangle(pColor, 0, 0, p.ClientRectangle.Width, p.ClientRectangle.Height);
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            // Chọn màu viền và độ dày của viền (ở đây để độ dày là 3 pixel)
            int borderWidth = 2;
            using (Pen pColor = new Pen(Color.Black, borderWidth))
            {
                // Vẽ khung viền đè lên Panel
                e.Graphics.DrawRectangle(pColor, 0, 0, p.ClientRectangle.Width, p.ClientRectangle.Height);
            }
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            dtpTuNgay.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            dtpDenNgay.CustomFormat = "dd/MM/yyyy";
        }
        private void CauHinhGiaoDienBang()
        {
            // 1. Tắt tự động sinh cột
            dgvThongTinDoChoi.AutoGenerateColumns = false;

            // 2. Ánh xạ tên thuộc tính khớp chính xác với SanPhamDTO
            dgvThongTinDoChoi.Columns["colMaSP"].DataPropertyName = "MaSP";
            dgvThongTinDoChoi.Columns["colTenSP"].DataPropertyName = "TenSP";

            // Kiểm tra trong SanPhamDTO: nếu là TenLoai hoặc TenDanhMuc thì đổi lại tương ứng
            dgvThongTinDoChoi.Columns["colDanhMuc"].DataPropertyName = "TenLoai";

            dgvThongTinDoChoi.Columns["colDonGia"].DataPropertyName = "DonGia";

            // Theo SanPhamBUS: tên thuộc tính là TenXuatXu và TonKho
            dgvThongTinDoChoi.Columns["colXuatXu"].DataPropertyName = "TenXuatXu";
            dgvThongTinDoChoi.Columns["colTonKho"].DataPropertyName = "TonKho";

            dgvThongTinDoChoi.Columns["colDoTuoi"].DataPropertyName = "DoTuoi";

            // 3. Định dạng hiển thị tiền tệ cho Đơn giá (VD: 300,000)
            dgvThongTinDoChoi.Columns["colDonGia"].DefaultCellStyle.Format = "N0";
        }


        private void LoadDanhSachDoChoiTuDatabase()
        {
            try
            {
                // Gọi hàm lấy danh sách từ CSDL (trả về List<SanPhamDTO> hoặc DataTable)
                var dsSanPham = sanPhamBUS.LayDanhSach(chiLayConBan: true);

                // Gán dữ liệu vào DataGridView
                dgvThongTinDoChoi.DataSource = null;
                dgvThongTinDoChoi.DataSource = dsSanPham;

                // Cập nhật lại cột STT
                CapNhatSTT();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu từ Database: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatSTT()
        {
            for (int i = 0; i < dgvThongTinDoChoi.Rows.Count; i++)
            {
                dgvThongTinDoChoi.Rows[i].Cells["colSTT"].Value = (i + 1).ToString();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
                string tuKhoa = txtTimKiem.Text.Trim();

                // Lọc bỏ chuỗi Watermark / Placeholder nếu có
                if (tuKhoa.StartsWith("Tìm tên") || tuKhoa.StartsWith("Tìm kiếm"))
                {
                    tuKhoa = string.Empty;
                }

                // Gọi hàm tìm kiếm từ BUS
                var dsKetQua = sanPhamBUS.TimKiem(tuKhoa);

                // Gán lại DataSource (DataBindingComplete sẽ tự động gọi CapNhatSTT)
                dgvThongTinDoChoi.DataSource = null;
                dgvThongTinDoChoi.DataSource = dsKetQua;
            //}
            //catch (Exception ex)
            //{
            //    // Tránh throw lỗi ra UI khi người dùng đang gõ nhanh
            //}
        }

        private void dgvThongTinDoChoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void cboXuatXu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApDungBoLoc();
        }
        private void ApDungBoLoc()
        {
            var dsGoc = sanPhamBUS.LayDanhSach(chiLayConBan: true);
            if (dsGoc == null) return;

            // 1. Đọc thông tin từ ô tìm kiếm
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            if (tuKhoa.StartsWith("tìm tên") || tuKhoa.StartsWith("tìm kiếm"))
            {
                tuKhoa = string.Empty;
            }

            // 2. Đọc thông tin chọn từ 2 ComboBox
            string xuatXuChon = cboXuatXu.SelectedItem?.ToString() ?? "_Tất cả xuất xứ_";
            string doTuoiChon = cboDoTuoi.SelectedItem?.ToString() ?? "_Tất cả độ tuổi_";

            // 3. Lọc danh sách kết hợp 3 điều kiện bằng LINQ
            var dsKetQua = dsGoc.Where(sp =>
                // Điều kiện 1: Tìm theo Tên, Mã hoặc Loại sản phẩm
                (string.IsNullOrEmpty(tuKhoa) ||
                 (!string.IsNullOrEmpty(sp.TenSP) && sp.TenSP.ToLower().Contains(tuKhoa)) ||
                 (!string.IsNullOrEmpty(sp.MaSP) && sp.MaSP.ToLower().Contains(tuKhoa)) ||
                 (!string.IsNullOrEmpty(sp.TenLoai) && sp.TenLoai.ToLower().Contains(tuKhoa))) &&

                // Điều kiện 2: Lọc theo Xuất xứ
                (xuatXuChon == "_Tất cả xuất xứ_" ||
                 (!string.IsNullOrEmpty(sp.TenXuatXu) && sp.TenXuatXu.Equals(xuatXuChon, StringComparison.OrdinalIgnoreCase))) &&

                // Điều kiện 3: Lọc theo Độ tuổi
                (doTuoiChon == "_Tất cả độ tuổi_" ||
                 (!string.IsNullOrEmpty(sp.DoTuoi) && sp.DoTuoi.Equals(doTuoiChon, StringComparison.OrdinalIgnoreCase)))
            ).ToList();

            // 4. Cập nhật lại DataSource lên DataGridView
            dgvThongTinDoChoi.DataSource = null;
            dgvThongTinDoChoi.DataSource = dsKetQua;
        }

        private void cboDoTuoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApDungBoLoc();
        }
    }
}
