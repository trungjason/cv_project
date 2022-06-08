--
-- Database: quanlynhahang
--
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'quanlynhahang')
	BEGIN
		CREATE DATABASE quanlynhahang
    END
GO
    USE quanlynhahang;
GO
-- --------------------------------------------------------

--
-- Table structure for table banan
--

CREATE TABLE banan (
  MaBanAn nvarchar(10) NOT NULL PRIMARY KEY,
  TinhTrang nvarchar(20) NOT NULL,
  SucChua int NOT NULL,
  GhiChu nvarchar(50) NOT NULL
);

--
-- Dumping data for table banan
--

INSERT INTO banan (MaBanAn, TinhTrang, SucChua, GhiChu) VALUES
(N'BA001', N'Trống', 4, ''),
(N'BA002', N'Trống', 4, ''),
(N'BA003', N'Trống', 8, ''),
(N'BA004', N'Trống', 8, ''),
(N'BA005', N'Trống', 10, ''),
(N'BA006', N'Trống', 10, ''),
(N'BA007', N'Trống', 12, ''),
(N'BA008', N'Trống', 14, ''),
(N'BA009', N'Trống', 18, ''),
(N'BA010', N'Trống', 20, ''),
(N'BA011', N'Trống', 20, ''),
(N'BA012', N'Trống', 20, '');

-- --------------------------------------------------------

--
-- Table structure for table chitietgoimon
--

CREATE TABLE chitietgoimon (
  MaMonAn nvarchar(10) NOT NULL ,
  MaOrder nvarchar(10) NOT NULL ,
  SoLuongMonAn int NOT NULL,
  primary key (MaMonAn, MaOrder)
);

-- --------------------------------------------------------

--
-- Table structure for table chitietphieunhap
--

CREATE TABLE chitietphieunhap (
  MaNguyenVatLieu nvarchar(10) NOT NULL,
  MaPhieuNhap nvarchar(10) NOT NULL,
  SoLuongNhap int NOT NULL
);

-- --------------------------------------------------------

--
-- Table structure for table hoadon
--

CREATE TABLE hoadon (
  MaHoaDon nvarchar(10) NOT NULL PRIMARY KEY,
  ThoiGianThanhToan datetime NOT NULL DEFAULT GETDATE(),
  TongTien float DEFAULT 0,
  ChietKhau float DEFAULT 0,
  TinhTrang int NOT NULL DEFAULT '0',
  MaKhachHang int DEFAULT NULL,
  MaHoiVien nvarchar(20) DEFAULT NULL,
  MaNhanVien nvarchar(10) DEFAULT NULL,
  MaOrder nvarchar(10) NOT NULL
);

--
-- Dumping data for table hoadon
--

INSERT INTO hoadon (MaHoaDon, ThoiGianThanhToan, TongTien, ChietKhau, TinhTrang, MaKhachHang, MaHoiVien, MaNhanVien, MaOrder) VALUES
('HD001', '2021-12-26 17:14:08', 600000, 0, 1, NULL, NULL, NULL, 'PGM002'),
('HD002', '2021-12-27 23:23:56', 300000, 0, 1, NULL, '159', NULL, 'PGM004'),
('HD003', '2021-12-27 23:26:46', 300000, 0, 1, NULL, NULL, NULL, 'PGM005'),
('HD004', '2021-12-27 23:26:55', 300000, 0, 1, NULL, '159', NULL, 'PGM006'),
('HD005', '2021-12-28 10:15:17', 300000, 0, 1, NULL, NULL, NULL, 'PGM007'),
('HD006', '2021-12-28 10:15:25', 300000, 0, 1, NULL, '159', NULL, 'PGM008'),
('HD007', '2021-12-28 10:16:27', 430000, 0, 1, NULL, '159', NULL, 'PGM010'),
('HD008', '2021-12-28 10:21:56', 300000, 0, 1, NULL, NULL, NULL, 'PGM011'),
('HD009', '2021-12-28 10:22:01', 300000, 0, 1, NULL, '159', NULL, 'PGM012'),
('HD010', '2022-01-09 21:49:23', 4500000, 90000, 1, NULL, '159', 'NV001', 'PGM013'),
('HD012', '2022-01-09 21:49:04', 300000, 6000, 1, NULL, '123', 'NV001', 'PGM015'),
('HD013', '2022-01-09 21:47:59', 400000, 8000, 1, NULL, NULL, 'NV001', 'PGM016'),
('HD014', '2022-01-09 21:49:42', 1800000, 144000, 1, NULL, '159', 'NV001', 'PGM017'),
('HD015', '2022-01-09 21:50:12', 1800000, 36000, 1, NULL, NULL, 'NV001', 'PGM018'),
('HD016', '2022-01-09 21:56:48', 770000, 0, 1, NULL, NULL, 'NV001', 'PGM019'),
('HD017', '2022-01-09 21:58:04', 30000, 2400, 1, NULL, '159', 'NV001', 'PGM021'),
('HD018', '2022-01-09 22:07:32', 30000, 0, 1, NULL, NULL, 'NV001', 'PGM022'),
('HD019', '2022-01-09 21:59:00', 0, 0, 0, NULL, NULL, NULL, 'PGM023');

-- --------------------------------------------------------

--
-- Table structure for table hoivien
--

CREATE TABLE hoivien (
  TenHoiVien nvarchar(50)  NOT NULL,
  SoDienThoai nvarchar(20)  NOT NULL PRIMARY KEY,
  TongSoTienThanhToan float NOT NULL,
  Email nvarchar(100)  NOT NULL,
  MatKhau nvarchar(100)  NOT NULL,
  DiaChi nvarchar(100)  NOT NULL,
  QuyenLoi nvarchar(100)  NOT NULL
);

--
-- Dumping data for table hoivien
--

INSERT INTO hoivien (TenHoiVien, SoDienThoai, TongSoTienThanhToan, Email, MatKhau, DiaChi, QuyenLoi) VALUES
(N'Tăng Kiến Trung', N'0159987654', 1, N'trungtkt159@gmail.com', '$2a$12$jxJtmS2W/nl2Ui/leG9yS.zzNn7WsoNMln6QigppREluZG77r8srK', '', N'không có'),
(N'Nguyễn Trường Anh', N'0344883919', 2000000, '', '$2a$12$e4CU5c0ADO7AxJAAv2cgZOfnhBWdEjD7EFkpXISSKb19fbi2NdFWW', N'Bình Dương', ''),
(N'Trương Tuấn Thịnh', N'0987654321', 0, N'thinhtruong@wibu.com.vn', '$2a$12$OXMNUAMXGNrSuT9Ah2qdcen0aoi9UefqWvLdKxIvnlfLEbkuJyQNa', '', ''),
(N'Trung', N'123', 300000, N'trung@gmail.com.vn', '$2a$12$PvCGOdpiiEwqG/HQu/hTTexgyQyMa8Nf4LOBmquOAyaboMVOSCoHi',  '43/13 ', 'VIP'),
(N'Trung', N'159', 7660000, N'trung@gmail.com', '$2a$12$xaHCvOF/SVUhk6eLZ5Id2.OnjAU0iOx/wV5oqW1rOo943n6EMa8qq', '', '');

-- --------------------------------------------------------

--
-- Table structure for table khachhang
--

CREATE TABLE khachhang (
  MaKhachHang int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  SoDienThoai nvarchar(20)  NOT NULL,
  TenKhachHang nvarchar(50)  NOT NULL,
  DiaChi nvarchar(50)  DEFAULT NULL,
  Email nvarchar(50)  DEFAULT NULL
) ;

--
-- Dumping data for table khachhang
--

INSERT INTO khachhang (SoDienThoai, TenKhachHang, DiaChi, Email) VALUES
(N'0344883919', N'Nguyễn Trường Anh', N'202/3E, Huỳnh Văn Bánh, Quận 11, Tp.HCM', N'truonganh@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table lichhen
--

CREATE TABLE lichhen (
  MaLichHen nvarchar(10)  NOT NULL PRIMARY KEY,
  NhuCau nvarchar(600)  DEFAULT NULL,
  SoLuongKhach int NOT NULL,
  ThoiGian time NOT NULL,
  NgayHen date NOT NULL,
  TinhTrang int DEFAULT '0',
  MaKhachHang int NOT NULL,
  MaNhanVien nvarchar(10)  DEFAULT NULL,
  MaBanAn nvarchar(10)  DEFAULT NULL
) ;

--
-- Dumping data for table lichhen
--

INSERT INTO lichhen (MaLichHen, NhuCau, SoLuongKhach, ThoiGian, NgayHen, TinhTrang, MaKhachHang, MaNhanVien, MaBanAn) VALUES
(N'LH001', N'Ăn cưới', 10, '13:13:11', '2021-12-09', '1', 1, 'NV001', 'BA003');

-- --------------------------------------------------------

--
-- Table structure for table monan
--

CREATE TABLE monan (
  MaMonAn nvarchar(10)  NOT NULL PRIMARY KEY,
  TenMonAn nvarchar(50)  NOT NULL,
  DonGia float NOT NULL,
  DonVi nvarchar(50)  NOT NULL,
  MoTa nvarchar(500)  NOT NULL,
  MoTaNgan nvarchar(100)  NOT NULL,
  HinhAnh nvarchar(100)  NOT NULL,
  MaNhom nvarchar(10)  NOT NULL
) ;

--
-- Dumping data for table monan
--

INSERT INTO monan (MaMonAn, TenMonAn, DonGia, DonVi, MoTa, MoTaNgan, HinhAnh, MaNhom) VALUES
('MA001', N'Lẩu thái', 300000, N'VNĐ', N'Lẩu được nấu từ các nguyên vật liệu tươi ngon nhất, được chọn lọc trong ngày, tạo nên nước lẩu thơm ', N'Phù hợp cho 2-3 người ăn. Nước lẩu đậm vị, thơm ngon.', 'lau1.jpg', 'NMA001'),
('MA002', N'Lẩu cua đồng', 350000, N'VNĐ', N'Lẩu cua đồng, thơm ngon, được nấu từ cua đồng giao trong ngày.', N'Phù hợp cho 2-3 người ăn.', 'lau2.jpg', 'NMA001'),
('MA003', N'Lẩu dê', 300000, N'VNĐ', N'Lẩu được hầm trong hơn 24h, các nguyên liệu được chọn lọc từ các chú dê núi, đảm bảo hương vị thơm n', N'Phù hợp cho 2-3 người ăn.', 'lau3.jpg', 'NMA001'),
('MA004', N'Lẩu bò', 350000, N'VNĐ', N'Lẩu bò thơm ngon, nước hầm đậm vị, được ninh từ xương bò với thời gian hơn 24h.', N'Phù hợp cho 2-3 người ăn.', 'lau4.jpg', 'NMA001'),
('MA005', N'Lẩu mắm', 300000, N'VNĐ', N'Lẩu mắm thơm ngon tuyệt vời.', N'Phù hợp cho 2-3 người ăn.', 'lau5.jpg', 'NMA001'),
('MA006', N'Lẩu gà', 300000, N'VNĐ', N'Lẩu gà thơm ngon, bổ dưỡng, rất phù hợp cho người cần tẩm bổ.', N'Phù hợp cho 2-3 người ăn.', 'lau6.jpg', 'NMA001'),
('MA007', N'Lẩu nấm', 350000, N'VNĐ', N'Lẩu nấm với nước dùng ngọt ngào, nấm giòn và tươi.', N'Phù hợp cho 2-3 người ăn.', 'lau7.jpg', 'NMA001'),
('MA008', N'Lẩu cá kèo', 350000, N'VNĐ', N'Cá kèo được chọn lọc và sử dụng trong ngày, đảm bảo độ tươi ngon và chắc thịt.', N'Phù hợp cho 2-3 người ăn.', 'lau8.jpg', 'NMA001'),
('MA009', N'Lẩu cá thác lác', 300000, N'VNĐ', N'Nước lẩu ngọt, đậm vị, cá thác lác tươi ngon.', N'Phù hợp cho 2-3 người ăn.', 'lau9.jpg', 'NMA001'),
('MA010', N'Lẩu bò nhúng dấm', 350000, N'VNĐ', N'Bò nhúng dấm thơm ngon, thịt bò tươi mềm.', N'Phù hợp cho 2-3 người ăn.', 'lau10.jpg', 'NMA001'),
('MA011', N'Cút nướng', 100000, N'VNĐ', N'Cút nướng thơm ngon, nhấm mồi hết ý.', N'Một suất gồm 4 con cút nướng.', 'nuong1.jpg', 'NMA002'),
('MA012', N'Gà nướng', 220000, N'VNĐ', N'Gà nướng nguyên con, đậm vị.', N'Gà nướng với khối lượng 1.5kg/con.', 'nuong2.jpg', 'NMA002'),
('MA013', N'Tôm nướng muối ớt', 100000, N'VNĐ', N'Thịt tôm chắc, tươi ngon, kết hợp với muối ớt cay nồng tạo hương vị lôi cuốn.', N'Mỗi phần gồm 8 con tôm.', 'nuong3.jpg', 'NMA002'),
('MA014', N'Bạch tuộc nướng', 350000, N'VNĐ', N'Bạch tuộc được chế biến tươi sống, đảm bảo chất lượng thơm ngon.', N'Phần gồm 1 con bạch tuộc to.', 'nuong4.jpg', 'NMA002'),
('MA015', N'Thịt xiên nướng', 50000, N'VNĐ', N'Thịt xiên kết hợp rau củ mang đến hương vị thơm ngon, cân bằng giữa thịt và rau.', N'Mỗi phần gồm 2 xiên thịt.', 'nuong5.jpg', 'NMA002'),
('MA016', N'Chân gà nướng', 75000, N'VNĐ', N'Chân gà nướng thấm vị, thích hợp cho nhâm nhi với bia.', N'Mỗi phần gồm 4 chân gà.', 'nuong6.jpg', 'NMA002'),
('MA017', N'Sườn nướng', 150000, N'VNĐ', N'Sườn nướng chuẩn vị BBQ, ngon ngỡ ngàn.\r\n', N'Mỗi phần gồm 2 cây sườn đậm vị.', 'nuong7.jpg', 'NMA002'),
('MA018', N'Cá lóc nướng', 230000, N'VNĐ', N'Được chế biến từ nguyên liệu tươi sống, giữ nguyên hương vị ngọt của thịt cá.', N'Mỗi phần gồm 1 con cá 1,5kg và rau ăn kèm', 'nuong8.jpg', 'NMA002'),
('MA019', N'Gỏi gà xé phay', 60000, N'VNĐ', N'Nguyên liệu tươi ngon, thịt gà chắc và ngọt.', N'Phù hợp cho 1-2 người ăn.', 'goi1.jpg', 'NMA003'),
('MA020', N'Nộm hoa chuối bắp bò', 75000, N'VNĐ', N'Bắp bò ngon kết hợp với hoa chuối tươi tạo nên một món ăn chuẩn ngon.', N'Phù hợp cho 1-2 người ăn.', 'goi2.jpg', 'NMA003'),
('MA021', N'Gỏi đu đủ tai heo - tôm', 60000, N'VNĐ', N'Tai heo giai ngon kết hơp với đu đủ giòn giòn và thịt tôm ngọt, chắc thịt.', N'Phù hợp cho 1-2 người ăn.', 'goi3.jpg', 'NMA003'),
('MA022', N'Gỏi càng cua thịt bò', 60000, N'VNĐ', N'Rau càng cua chua ngọt kết hợp với thịt bò mềm ngon.', N'Phù hợp cho 1-2 người ăn.', 'goi4.jpg', 'NMA003'),
('MA023', N'Gỏi ngó sen tôm thịt', 60000, N'VNĐ', N'Thơm ngon chuẩn vị.', N'Phù hợp cho 1-2 người ăn.', 'goi5.jpg', 'NMA003'),
('MA024', N'Nộm củ hủ dừa', 60000, N'VNĐ', N'Vị giòn của củ hủ dừa kết hợp với thịt tôm tươi tạo nên vị ngon tinh tế.', N'Phù hợp cho 1-2 người ăn.', 'goi6.jpg', 'NMA003'),
('MA025', N'Gỏi cuốn tôm thịt', 70000, N'VNĐ', N'Gỏi cuốn thơm ngon kết hợp hài hòa giữa thịt heo cùng tôm, bún và rau, đi đôi với nước chấm tương đe', N'Phần ăn gồm 10 cuốn.', 'goi7.jpg', 'NMA003'),
('MA026', N'Gỏi cuốn bì', 60000, N'VNĐ', N'Bì giòn ngon kết hợp với nước mắm mặn ngọt tạo nên hương vị tuyệt vời.', N'Mỗi phần gồm 10 cuốn.', 'goi8.jpg', 'NMA003'),
('MA027', N'Nem nướng cuốn bánh hỏi', 100000, N'VNĐ', N'Nem nướng thơm ngon chuẩn bị, cuốn cùng bánh hỏi kèm thêm rau, chấm nước mắm chua ngọt tạo hương vị ', N'Phù hợp cho 2-3 người ăn.', 'cuon1.jpg', 'NMA004'),
('MA028', N'Thịt nướng cuốn bánh tráng', 100000, N'VNĐ', N'Thịt nướng thơm ngon, cuốn với bánh tráng và rau, chấm vào nước mắm tạo hương vị cực đỉnh.', N'Phù hợp cho 2-3 người ăn.', 'cuon2.jpg', 'NMA004'),
('MA029', N'Bánh xèo tôm thịt', 50000, N'VNĐ', N'Bánh xèo mềm giai, cùng với hương thơm của thịt và tôm.', N'Phù hợp cho 1-2 người ăn.', 'cuon3.jpg', 'NMA004'),
('MA030', N'Chả giò tôm thịt', 50000, N'VNĐ', N'Chả giò vỏ giòn bên ngoài, kết hợp với thịt heo và tôm bằm nhuyễn bên trong, tạo hương vị thơm ngon.', N'Mỗi phần gồm 6 chiếc chả giò.', 'cuon4.jpg', 'NMA004'),
('MA031', N'Bò bía mặn', 50000, N'VNĐ', N'Sự kết hợp hài hòa giữa bún, rau và lạp xưởng, chấm với nước chấm đặc biệt của quan tạo hương vị tuy', N'Mỗi phần gồm 15 chiếc.', 'cuon5.jpg', 'NMA004'),
('MA032', N'Thịt luộc cuốn bánh tráng', 70000, N'VNĐ', N'Thịt luộc ngon kết hợp với bún và rau, chấm với nước mắm mang lại hương vị bùng nổ.', N'Phù hợp cho 2-3 người ăn.', 'cuon6.jpg', 'NMA004'),
('MA033', N'Khoai tây chiên', 30000, N'VNĐ', N'Khoai tây từ Đà Lạt, đảm bảo vị ngon khi thưởng thức.', N'Phù hợp 1-2 người ăn.', 'annhe1.jpg', 'NMA005'),
('MA034', N'Cá viên chiên', 30000, N'VNĐ', N'Cá viên chiên đủ loại.', N'Phù hợp cho 1-2 người ăn.', 'annhe2.jpg', 'NMA005'),
('MA035', N'Gà rán', 50000, N'VNĐ', N'Gà rán giòn ngon.', N'Mỗi phần gồm 2 đuồi.', 'annhe3.jpg', 'NMA005'),
('MA036', N'Chuối chiên', 20000, N'VNĐ', N'Chuối chiên vỏ giòn, chuối ngọt bên trong.', N'Mỗi phần gồm 4 trái.', 'annhe4.jpg', 'NMA005'),
('MA037', N'Cánh gà chiên nước mắm', 75000, N'VNĐ', N'Cánh gà chiên giòn kết hợp với vị nước mắm đậm đà, ngọt ngọt, cay cay, tạo nên hương vị tuyệt vời.', N'Mỗi phần gồm 4 cánh.', 'annhe5.jpg', 'NMA005');

-- --------------------------------------------------------

--
-- Table structure for table nguyenvatlieu
--

CREATE TABLE nguyenvatlieu (
  MaNguyenVatLieu nvarchar(10)  NOT NULL PRIMARY KEY,
  TenNguyenVatLieu nvarchar(50)  NOT NULL,
  DonGia float NOT NULL,
  DonVi nvarchar(50)  NOT NULL,
  TinhTrang nvarchar(50)  NOT NULL,
  SoLuongTonKho int NOT NULL DEFAULT 0
) ;

--
-- Dumping data for table nguyenvatlieu
--

INSERT INTO nguyenvatlieu (MaNguyenVatLieu, TenNguyenVatLieu, DonGia, DonVi, TinhTrang, SoLuongTonKho) VALUES
('NVL001', N'Bàn ăn 4 chỗ', 100000, N'Chiếc', N'New 99%', 0),
('NVL002', N'Muỗng ăn bằng bạc', 200000, N'Chiếc', N'Bạc fake chợ bà chiễu', 0),
('NVL003', N'Nồi đất đựng lẩu', 100000, N'Nồi', N'New 100% mới nung', 0),
('NVL004', N'Xiên nướng thịt', 30000, N'Xiên', N'New 100%', 0),
('NVL005', N'Gà nguyên con', 350000, N'Con', N'Gà nguyên con làm sạch tươi', 0),
('NVL006', N'Bò phi lê', 150000, N'kg', N'Thịt bò phi lê mềm ngọt mọng nước', 0),
('NVL007', N'Dú dê', 300000, N'Cái', N'Dú dê tươi dùng làm dú dê nướng', 0),
('NVL008', N'Cá lóc', 150000, N'Con', N'Cá lóc tươi sống', 0);

-- --------------------------------------------------------

--
-- Table structure for table nhacungcap
--

CREATE TABLE nhacungcap (
  MaNhaCungCap nvarchar(10)  NOT NULL PRIMARY KEY,
  MoTaNhaCungCap nvarchar(50)  NOT NULL,
  TenNhaCungCap nvarchar(50)  NOT NULL,
  DiaChi nvarchar(100)  NOT NULL,
  SoDienThoai nvarchar(20)  NOT NULL
) ;

INSERT INTO nhacungcap (MaNhaCungCap, MoTaNhaCungCap, TenNhaCungCap, DiaChi, SoDienThoai) VALUES
('NCC001', N'Chuyên cung cấp các loại khăn trải bàn, bàn ghế, v', N'Thịnh Nội Thất', N'Số 10 Hàng Mã', '06503858404 '),
('NCC002', N'Chuyên cung cấp các loại hải sản tươi sống', N'Vũ Hải Sản', N'20/4 Vũng Tàu', '0902653319'),
('NCC003', N'Chuyên cung cấp thịt các loại gia súc như Bò, Gà..', N'Trung Gia Súc', N'20/10/23/15 Tây Bắc', '01688918002'),
('NCC004', N'Chuyên cung cấp các loại rau củ quả tươi', N'Anh Thực Vật', N'25 Đà Lạt', '0123456786');

-- --------------------------------------------------------

--
-- Table structure for table nhanvien
--

CREATE TABLE nhanvien (
  MaNhanVien nvarchar(10)  NOT NULL PRIMARY KEY,
  TenNhanVien nvarchar(100)  NOT NULL,
  SoDienThoai nvarchar(20)  NOT NULL,
  Luong float NOT NULL,
  ChucVu nvarchar(50)  NOT NULL,
  DiaChi nvarchar(100)  NOT NULL,
  Email nvarchar(100)  NOT NULL
) ;

--
-- Dumping data for table nhanvien
--

INSERT INTO nhanvien (MaNhanVien, TenNhanVien, SoDienThoai, Luong, ChucVu, DiaChi, Email) VALUES
('NV001', N'Tăng Kiến Trung', '0123456789', 1000000000, N'Quản Lý Nhà Hàng', N'20/10/1 Trần Định Trọng Huyện Ba Đình Hà Nội', N'51900718@student.tdtu.edu.vn'),
('NV004', N'Nguyễn Võ Hoàng Vũ', '0456', 0, N'Thu Ngân', N'20/10/1 Trần Định Trọng Huyện Ba Đình Hà Nội', N'vu@gmail.com'),
('NV005', N'Nguyễn Trường Anh', '0123', 0, N'Lễ Tân', N'20/10/1 Trần Định Trọng Huyện Ba Đình Hà Nội', N'anh@gmail.com'),
('NV006', N'Trương Thịnh', '0789', 0, N'Phục Vụ', N'20/10/1 Trần Định Trọng Huyện Ba Đình Hà Nội', N'thinh@gmail.com');


-- --------------------------------------------------------

--
-- Table structure for table nhommonan
--

CREATE TABLE nhommonan (
  MaNhom nvarchar(10)  NOT NULL PRIMARY KEY,
  TenNhom nvarchar(50)  NOT NULL
) ;

--
-- Dumping data for table nhommonan
--

INSERT INTO nhommonan (MaNhom, TenNhom) VALUES
('NMA001', N'Lẩu'),
('NMA002', N'Nướng'),
('NMA003', N'Gỏi'),
('NMA004', N'Cuốn'),
('NMA005', N'Ăn nhẹ');

-- --------------------------------------------------------

--
-- Table structure for table phieugoimon
--

CREATE TABLE phieugoimon (
  MaOrder nvarchar(10)  NOT NULL PRIMARY KEY,
  GhiChuMonAn nvarchar(600)  NOT NULL,
  MaBanAn nvarchar(10)  DEFAULT NULL,
  MaNhanVien nvarchar(10)  DEFAULT NULL
) ;

--
-- Dumping data for table phieugoimon
--

INSERT INTO phieugoimon (MaOrder, GhiChuMonAn, MaBanAn, MaNhanVien) VALUES
('PGM001', '', 'BA002', 'NV001'),
('PGM002', '', 'BA002', 'NV001'),
('PGM003', '', 'BA003', 'NV001'),
('PGM004', '', 'BA008', 'NV001'),
('PGM005', '', 'BA012', 'NV001'),
('PGM006', '', 'BA012', 'NV001'),
('PGM007', '', 'BA003', 'NV001'),
('PGM008', '', 'BA002', 'NV001'),
('PGM009', '', 'BA006', 'NV001'),
('PGM010', '', 'BA010', 'NV001'),
('PGM011', '', 'BA002', 'NV001'),
('PGM012', '', 'BA003', 'NV001'),
('PGM013', '', 'BA001', 'NV001'),
('PGM014', '', 'BA005', 'NV001'),
('PGM015', '', 'BA009', 'NV001'),
('PGM016', '', 'BA008', 'NV001'),
('PGM017', '', 'BA001', 'NV001'),
('PGM018', '', 'BA003', 'NV001'),
('PGM019', '', 'BA005', 'NV001'),
('PGM020', '', 'BA006', 'NV001'),
('PGM021', '', 'BA002', 'NV001'),
('PGM022', '', 'BA003', 'NV001'),
('PGM023', '', 'BA005', 'NV001');

-- --------------------------------------------------------

--
-- Table structure for table phieunhap
--

CREATE TABLE phieunhap (
  MaPhieuNhap nvarchar(10)  NOT NULL PRIMARY KEY,
  NgayNhapHang datetime NOT NULL,
  TongGiaNhap float NOT NULL,
  GhiChu nvarchar(100)  NOT NULL,
  MaNhaCungCap nvarchar(10)  NOT NULL,
  MaNhanVien nvarchar(10)  NOT NULL
) ;

INSERT INTO phieunhap (MaPhieuNhap, NgayNhapHang, TongGiaNhap, GhiChu, MaNhaCungCap, MaNhanVien) VALUES
('PN001', '2022-01-09 22:23:45', 500000, N'Giao lẹ lẹ', 'NCC001', 'NV001');

-- --------------------------------------------------------

--
-- Table structure for table taikhoan
--

CREATE TABLE taikhoan (
  TenTaiKhoan nvarchar(50)  NOT NULL PRIMARY KEY,
  MatKhau nvarchar(255)  NOT NULL,
  MaNhanVien nvarchar(10)  NOT NULL
) ;

--
-- Dumping data for table taikhoan
--

INSERT INTO taikhoan (TenTaiKhoan, MatKhau, MaNhanVien) VALUES
('admin', '$2a$06$quSQWNFP4nBh3Q/OPHrw8.0rlTOxwX2bV.2KzD0hgTNo2ArVK6/lC', 'NV001'),
('anh@gmail.com', '$2a$06$2DB.nd2gO6fGg5peWVYYDeeRTNTyFDci2X3T6P.HmXJl9Bpkb.kR6', 'NV005'),
('thinh@gmail.com', '$2a$06$Db2WaNVeIYcCEUbuBTjJFuoPF7k90mPdc4xKBbCs3FrYmtF02i56i', 'NV006'),
('vu@gmail.com', '$2a$06$3mPV0uUqJF6REjc.LYX3h.zbbzsfHwdr.H2BGTeIqF3PBWCBuR0/2', 'NV004');

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table khachhang
--

--
-- Constraints for dumped tables
--

--
-- Constraints for table chitietgoimon
--
ALTER TABLE chitietgoimon
  ADD CONSTRAINT FK_ChiTietGoiMon_MonAn FOREIGN KEY (MaMonAn) REFERENCES monan (MaMonAn) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_ChiTietGoiMon_PhieuGoiMon FOREIGN KEY (MaOrder) REFERENCES phieugoimon (MaOrder) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table chitietphieunhap
--
ALTER TABLE chitietphieunhap
  ADD CONSTRAINT FK_ChiTietPhieuNhap_NguyenVatLieu FOREIGN KEY (MaNguyenVatLieu) REFERENCES nguyenvatlieu (MaNguyenVatLieu) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_ChiTietPhieuNhap_PhieuNhap FOREIGN KEY (MaPhieuNhap) REFERENCES phieunhap (MaPhieuNhap) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table hoadon
--
ALTER TABLE hoadon
  ADD CONSTRAINT FK_HoaDon_HoiVien FOREIGN KEY (MaHoiVien) REFERENCES hoivien (SoDienThoai) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES khachhang (MaKhachHang) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_HoaDon_PhieuGoiMon FOREIGN KEY (MaOrder) REFERENCES phieugoimon (MaOrder) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_HoaDon_MaNhanVien FOREIGN KEY (MaNhanVien) REFERENCES nhanvien (MaNhanVien) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table lichhen
--
ALTER TABLE lichhen
  ADD CONSTRAINT FK_LichHen_BanAn FOREIGN KEY (MaBanAn) REFERENCES banan (MaBanAn) ON DELETE CASCADE ON UPDATE CASCADE;

 ALTER TABLE lichhen
  ADD CONSTRAINT FK_LichHen_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES khachhang (MaKhachHang) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE lichhen
  ADD CONSTRAINT FK_LichHen_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES nhanvien (MaNhanVien) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table monan
--
ALTER TABLE monan
  ADD CONSTRAINT FK_MonAn_NhomMonAn FOREIGN KEY (MaNhom) REFERENCES nhommonan (MaNhom) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table phieugoimon
--
ALTER TABLE phieugoimon
  ADD CONSTRAINT FK_PhieuGoiMon_BanAn FOREIGN KEY (MaBanAn) REFERENCES banan (MaBanAn) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_PhieuGoiMon_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES nhanvien (MaNhanVien) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table phieunhap
--
ALTER TABLE phieunhap
  ADD CONSTRAINT FK_PhieuNhap_NhaCungCap FOREIGN KEY (MaNhaCungCap) REFERENCES nhacungcap (MaNhaCungCap) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES nhanvien (MaNhanVien) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table taikhoan
--
ALTER TABLE taikhoan
  ADD CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES nhanvien (MaNhanVien) ON DELETE CASCADE ON UPDATE CASCADE;
