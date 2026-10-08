using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanDoChoi.BUS;
using QuanLyBanDoChoi.DTO;

namespace QuanLyBanDoChoi.GUI
{
    public partial class frmThemSP : Form
    {
        // Khởi tạo các tầng nghiệp vụ BUS
        private readonly SanPhamBUS _sanPhamBUS = new SanPhamBUS();
        private readonly LoaiDoChoiBUS _loaiDoChoiBUS = new LoaiDoChoiBUS();

        // Đối tượng sản phẩm khi mở form ở chế độ Sửa (null nếu là Thêm mới)
        private SanPhamDTO _sanPhamCanSua = null;

        // Đường dẫn file ảnh gốc và tên file ảnh chuẩn hóa được lưu trong thư mục Images
        private string _selectedImagePath = string.Empty;
        private string _tenHinhAnh = string.Empty;

        public frmThemSP()
        {
            InitializeComponent();
            InitForm();
        }

        public frmThemSP(SanPhamDTO sp)
        {
            InitializeComponent();
            _sanPhamCanSua = sp;
            InitForm();
        }

        private void InitForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.Load += frmThemSP_Load;

            // Bỏ gán sự kiện btnLuu ở đây vì Designer.cs đã gán rồi
            // Placeholder ô Tên sản phẩm khi thêm mới
            txtTenSP.Enter += (s, e) =>
            {
                if (txtTenSP.Text == "Nhập tên sản phẩm...")
                {
                    txtTenSP.Text = "";
                    txtTenSP.ForeColor = Color.Black;
                }
            };
            txtTenSP.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTenSP.Text))
                {
                    txtTenSP.Text = "Nhập tên sản phẩm...";
                    txtTenSP.ForeColor = Color.DarkGray;
                }
            };
        }

        private void frmThemSP_Load(object sender, EventArgs e)
        {
            // Tải danh mục loại đồ chơi từ CSDL thông qua LoaiDoChoiBUS (Đã hỗ trợ L001 -> L008)
            LoadDanhMucLoai();

            // Nạp danh sách độ tuổi tiêu chuẩn
            cboDoTuoi.Items.Clear();
            cboDoTuoi.Items.AddRange(new object[] { "0-3 tuổi", "3-6 tuổi", "6-12 tuổi", "Trên 12 tuổi" });
            if (cboDoTuoi.Items.Count > 0) cboDoTuoi.SelectedIndex = 1;

            // Nạp danh sách Kênh Bán Hàng vào ComboBox cboKenhBan
            if (cboKenhBan != null)
            {
                cboKenhBan.Items.Clear();
                cboKenhBan.Items.AddRange(new object[] {
                    "Tại quầy",
                    "Shopee",
                    "TikTok Shop",
                    "Lazada",
                    "Tại quầy, Shopee",
                    "Tại quầy, TikTok Shop",
                    "Tất cả các kênh"
                });
                if (cboKenhBan.Items.Count > 0) cboKenhBan.SelectedIndex = 0;
            }

            // Cấu hình các NumericUpDown
            numTonKho.Minimum = 0;
            numTonKho.Maximum = 999999;
            numTonKho.Value = 0;

            numDonGia.Minimum = 0;
            numDonGia.Maximum = 1000000000;
            numDonGia.Increment = 5000;
            numDonGia.Value = 0;

            // Nếu đang ở chế độ Chỉnh sửa sản phẩm -> điền dữ liệu cũ vào các ô
            if (_sanPhamCanSua != null)
            {
                lblTitle.Text = "✏️ Cập Nhật Thông Tin Sản Phẩm";
                lblSubTitle.Text = $"Chỉnh sửa thông tin mã SKU: {_sanPhamCanSua.MaSP}";
                this.Text = "Cập Nhật Sản Phẩm - " + _sanPhamCanSua.MaSP;

                txtMaSP.Text = _sanPhamCanSua.MaSP;
                txtMaSP.ReadOnly = true;
                txtMaSP.BackColor = Color.FromArgb(240, 240, 240);

                txtTenSP.Text = _sanPhamCanSua.TenSP;
                txtTenSP.ForeColor = Color.Black;

                txtHang.Text = _sanPhamCanSua.Hang ?? "";
                txtXuatXu.Text = _sanPhamCanSua.TenXuatXu ?? "";

                if (!string.IsNullOrEmpty(_sanPhamCanSua.MaLoai))
                {
                    cboDanhMuc.SelectedValue = _sanPhamCanSua.MaLoai;
                }

                if (!string.IsNullOrEmpty(_sanPhamCanSua.DoTuoi))
                {
                    cboDoTuoi.SelectedItem = _sanPhamCanSua.DoTuoi;
                }

                // Điền Kênh bán cũ vào cboKenhBan
                if (cboKenhBan != null)
                {
                    string kenhCu = _sanPhamCanSua.DanhSachKenhBan ?? "";
                    if (!string.IsNullOrEmpty(kenhCu))
                    {
                        // Tìm index của item khớp với giá trị cũ
                        int index = cboKenhBan.Items.IndexOf(kenhCu);
                        if (index >= 0)
                        {
                            cboKenhBan.SelectedIndex = index;
                        }
                        else
                        {
                            // Nếu không tìm thấy, đặt về index 0
                            cboKenhBan.SelectedIndex = 0;
                        }
                    }
                }

                numDonGia.Value = _sanPhamCanSua.DonGia >= 0 ? _sanPhamCanSua.DonGia : 0;
                numTonKho.Value = _sanPhamCanSua.TonKho >= 0 ? _sanPhamCanSua.TonKho : 0;

                if (_sanPhamCanSua.TrangThai)
                {
                    radDangKinhDoanh.Checked = true;
                }
                else
                {
                    radNgungKinhDoanh.Checked = true;
                }

                _tenHinhAnh = _sanPhamCanSua.HinhAnh ?? "";

                // Nạp ảnh hiện tại (nếu có) bằng MemoryStream để không khóa file
                LoadHinhAnhHienTai(_tenHinhAnh);
            }
        }

        /// <summary>
        /// Nạp ảnh an toàn qua MemoryStream
        /// </summary>
        private void LoadHinhAnhHienTai(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            try
            {
                string path1 = Path.Combine(Application.StartupPath, "Images", fileName);
                string path2 = Path.Combine(Application.StartupPath, @"..\..\Images", fileName);
                string fullPath = File.Exists(path1) ? path1 : (File.Exists(path2) ? path2 : null);

                if (fullPath != null && File.Exists(fullPath))
                {
                    byte[] bytes = File.ReadAllBytes(fullPath);
                    using (var ms = new MemoryStream(bytes))
                    {
                        if (picHinhAnh.Image != null) picHinhAnh.Image.Dispose();
                        picHinhAnh.Image = Image.FromStream(ms);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Nạp danh sách loại đồ chơi vào ComboBox cboDanhMuc (Đã hỗ trợ fallback L001 -> L008)
        /// </summary>
        private void LoadDanhMucLoai()
        {
            try
            {
                var dsLoai = _loaiDoChoiBUS.LayDanhSach();
                if (dsLoai != null && dsLoai.Count > 0)
                {
                    cboDanhMuc.DataSource = dsLoai;
                    cboDanhMuc.DisplayMember = "TenLoai";
                    cboDanhMuc.ValueMember = "MaLoai";
                }
                else
                {
                    cboDanhMuc.DataSource = null;
                    cboDanhMuc.Items.Clear();
                    cboDanhMuc.Items.AddRange(new object[] { "L001", "L002", "L003", "L004", "L005", "L006", "L007", "L008" });
                    cboDanhMuc.SelectedIndex = 0;
                }
            }
            catch
            {
                cboDanhMuc.DataSource = null;
                cboDanhMuc.Items.Clear();
                cboDanhMuc.Items.AddRange(new object[] { "L001", "L002", "L003", "L004", "L005", "L006", "L007", "L008" });
                cboDanhMuc.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 1. Tải & Quản lý Hình Ảnh
        /// </summary>
        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn hình ảnh cho sản phẩm";
                ofd.Filter = "Tệp hình ảnh (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg;*.jpeg;*.png;*.bmp|PNG Image (*.png)|*.png|JPEG Image (*.jpg;*.jpeg)|*.jpg;*.jpeg|Tất cả tệp (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _selectedImagePath = ofd.FileName;

                        byte[] imageBytes = File.ReadAllBytes(_selectedImagePath);

                        if (picHinhAnh.Image != null)
                        {
                            picHinhAnh.Image.Dispose();
                            picHinhAnh.Image = null;
                        }

                        using (var ms = new MemoryStream(imageBytes))
                        {
                            picHinhAnh.Image = Image.FromStream(ms);
                        }

                        string extension = Path.GetExtension(_selectedImagePath);
                        if (string.IsNullOrEmpty(extension)) extension = ".jpg";
                        string fileName = $"SP_{DateTime.Now:yyyyMMddHHmmss}{extension}";

                        string appImagesDir = Path.Combine(Application.StartupPath, "Images");
                        if (!Directory.Exists(appImagesDir))
                        {
                            Directory.CreateDirectory(appImagesDir);
                        }
                        string targetPath = Path.Combine(appImagesDir, fileName);
                        File.WriteAllBytes(targetPath, imageBytes);

                        try
                        {
                            string projectDir = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."));
                            if (Directory.Exists(projectDir))
                            {
                                string projectImagesDir = Path.Combine(projectDir, "Images");
                                if (!Directory.Exists(projectImagesDir))
                                {
                                    Directory.CreateDirectory(projectImagesDir);
                                }
                                File.WriteAllBytes(Path.Combine(projectImagesDir, fileName), imageBytes);
                            }
                        }
                        catch { /* Bỏ qua nếu không có quyền ghi */ }

                        _tenHinhAnh = fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 2. Xử lý Lưu Sản Phẩm (Thêm mới hoặc Cập nhật)
        /// </summary>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Tạm thời vô hiệu hóa nút bấm để tránh người dùng hoặc hệ thống kích hoạt 2 lần
            btnLuu.Enabled = false;

            try
            {
                // Validate Tên sản phẩm
                string tenSP = txtTenSP.Text.Trim();
                if (string.IsNullOrWhiteSpace(tenSP) || tenSP == "Nhập tên sản phẩm...")
                {
                    MessageBox.Show("Tên sản phẩm không được để trống!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenSP.Focus();
                    btnLuu.Enabled = true; // Mở lại nút
                    return;
                }

                // Validate Mã SKU sản phẩm
                string maSP = txtMaSP.Text.Trim();
                if (string.IsNullOrWhiteSpace(maSP))
                {
                    MessageBox.Show("Mã sản phẩm (SKU) không được để trống!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaSP.Focus();
                    btnLuu.Enabled = true;
                    return;
                }
                if (maSP.Length > 10)
                {
                    MessageBox.Show("Mã sản phẩm không được vượt quá 10 ký tự!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaSP.Focus();
                    btnLuu.Enabled = true;
                    return;
                }

                // Validate Loại danh mục
                string maLoai = cboDanhMuc.SelectedValue != null ? cboDanhMuc.SelectedValue.ToString() : cboDanhMuc.Text.Trim();
                if (string.IsNullOrWhiteSpace(maLoai))
                {
                    MessageBox.Show("Vui lòng chọn danh mục loại sản phẩm!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboDanhMuc.Focus();
                    btnLuu.Enabled = true;
                    return;
                }

                // Validate Đơn giá bán
                decimal donGia = numDonGia.Value;
                if (donGia < 0)
                {
                    MessageBox.Show("Đơn giá bán phải lớn hơn hoặc bằng 0!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numDonGia.Focus();
                    btnLuu.Enabled = true;
                    return;
                }

                // Validate Số lượng tồn kho
                if (numTonKho.Value < 0)
                {
                    MessageBox.Show("Số lượng tồn kho không được âm!", "Cảnh Báo Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numTonKho.Focus();
                    btnLuu.Enabled = true;
                    return;
                }
                int tonKho = (int)numTonKho.Value;

                string doTuoi = cboDoTuoi.SelectedItem != null ? cboDoTuoi.SelectedItem.ToString() : cboDoTuoi.Text.Trim();
                string hang = txtHang.Text.Trim();
                string xuatXu = txtXuatXu.Text.Trim();
                bool trangThai = radDangKinhDoanh.Checked;

                // Lấy giá trị Kênh bán hàng từ ComboBox
                string kenhBan = cboKenhBan != null && cboKenhBan.SelectedItem != null ? 
                    cboKenhBan.SelectedItem.ToString().Trim() : "Tại quầy";


                // Đóng gói đối tượng SanPhamDTO
                SanPhamDTO sp = new SanPhamDTO
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    MaLoai = maLoai,
                    DoTuoi = doTuoi,
                    Hang = hang,
                    TenXuatXu = xuatXu,
                    GiaNhap = _sanPhamCanSua != null ? _sanPhamCanSua.GiaNhap : 0,
                    DonGia = donGia,
                    TonKho = tonKho,
                    HinhAnh = !string.IsNullOrEmpty(_tenHinhAnh) ? _tenHinhAnh : (_sanPhamCanSua?.HinhAnh ?? ""),
                    TrangThai = trangThai,
                    DanhSachKenhBan = kenhBan
                };

                bool ketQua;
                string error;

                if (_sanPhamCanSua == null)
                {
                    // Chế độ Thêm mới
                    ketQua = _sanPhamBUS.Them(sp, out error);
                    if (ketQua)
                    {
                        MessageBox.Show("Thêm sản phẩm mới thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Chế độ Cập nhật (Sửa)
                    ketQua = _sanPhamBUS.CapNhat(sp, out error);
                    if (ketQua)
                    {
                        MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (ketQua)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(error, "Lỗi Khi Lưu Sản Phẩm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnLuu.Enabled = true; // Mở lại nút nếu lưu thất bại
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnLuu.Enabled = true;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnThemDongBo_Click(object sender, EventArgs e)
        {
            btnLuu_Click(sender, e);
        }
    }
}