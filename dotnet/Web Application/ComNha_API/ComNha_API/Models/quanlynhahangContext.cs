using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComNha_API.Models
{
    public partial class quanlynhahangContext : DbContext
    {
        public quanlynhahangContext()
        {
        }

        public quanlynhahangContext(DbContextOptions<quanlynhahangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banan> Banans { get; set; } = null!;
        public virtual DbSet<Chitietgoimon> Chitietgoimons { get; set; } = null!;
        public virtual DbSet<Chitietphieunhap> Chitietphieunhaps { get; set; } = null!;
        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Hoivien> Hoiviens { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Lichhen> Lichhens { get; set; } = null!;
        public virtual DbSet<Monan> Monans { get; set; } = null!;
        public virtual DbSet<Nguyenvatlieu> Nguyenvatlieus { get; set; } = null!;
        public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; } = null!;
        public virtual DbSet<Nhanvien> Nhanviens { get; set; } = null!;
        public virtual DbSet<Nhommonan> Nhommonans { get; set; } = null!;
        public virtual DbSet<Phieugoimon> Phieugoimons { get; set; } = null!;
        public virtual DbSet<Phieunhap> Phieunhaps { get; set; } = null!;
        public virtual DbSet<Taikhoan> Taikhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string SQL_Server_ConnectionString = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetSection("ConnectionStrings")["SQLServerConnection"];
                // Lưu Connection String này vào file appconfig
                optionsBuilder.UseSqlServer(SQL_Server_ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banan>(entity =>
            {
                entity.HasKey(e => e.MaBanAn)
                    .HasName("PK__banan__E2673DF2600F2234");

                entity.ToTable("banan");

                entity.Property(e => e.MaBanAn).HasMaxLength(10);

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.TinhTrang).HasMaxLength(20);
            });

            modelBuilder.Entity<Chitietgoimon>(entity =>
            {
                entity.HasKey(e => new { e.MaMonAn, e.MaOrder })
                    .HasName("PK__chitietg__D4124FCA7AA42AB8");

                entity.ToTable("chitietgoimon");

                entity.Property(e => e.MaMonAn).HasMaxLength(10);

                entity.Property(e => e.MaOrder).HasMaxLength(10);

                entity.HasOne(d => d.MaMonAnNavigation)
                    .WithMany(p => p.Chitietgoimons)
                    .HasForeignKey(d => d.MaMonAn)
                    .HasConstraintName("FK_ChiTietGoiMon_MonAn");

                entity.HasOne(d => d.MaOrderNavigation)
                    .WithMany(p => p.Chitietgoimons)
                    .HasForeignKey(d => d.MaOrder)
                    .HasConstraintName("FK_ChiTietGoiMon_PhieuGoiMon");
            });

            modelBuilder.Entity<Chitietphieunhap>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("chitietphieunhap");

                entity.Property(e => e.MaNguyenVatLieu).HasMaxLength(10);

                entity.Property(e => e.MaPhieuNhap).HasMaxLength(10);

                entity.HasOne(d => d.MaNguyenVatLieuNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaNguyenVatLieu)
                    .HasConstraintName("FK_ChiTietPhieuNhap_NguyenVatLieu");

                entity.HasOne(d => d.MaPhieuNhapNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaPhieuNhap)
                    .HasConstraintName("FK_ChiTietPhieuNhap_PhieuNhap");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__hoadon__835ED13B53696C95");

                entity.ToTable("hoadon");

                entity.Property(e => e.MaHoaDon).HasMaxLength(10);

                entity.Property(e => e.ChietKhau).HasDefaultValueSql("((0))");

                entity.Property(e => e.MaHoiVien).HasMaxLength(20);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.MaOrder).HasMaxLength(10);

                entity.Property(e => e.ThoiGianThanhToan)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TinhTrang).HasDefaultValueSql("('0')");

                entity.Property(e => e.TongTien).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.MaHoiVienNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.MaHoiVien)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HoaDon_HoiVien");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HoaDon_KhachHang");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HoaDon_MaNhanVien");

                entity.HasOne(d => d.MaOrderNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.MaOrder)
                    .HasConstraintName("FK_HoaDon_PhieuGoiMon");
            });

            modelBuilder.Entity<Hoivien>(entity =>
            {
                entity.HasKey(e => e.SoDienThoai)
                    .HasName("PK__hoivien__0389B7BC02A07E9F");

                entity.ToTable("hoivien");

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.MatKhau).HasMaxLength(100);

                entity.Property(e => e.QuyenLoi).HasMaxLength(100);

                entity.Property(e => e.TenHoiVien).HasMaxLength(50);
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__khachhan__88D2F0E528279A01");

                entity.ToTable("khachhang");

                entity.Property(e => e.DiaChi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenKhachHang).HasMaxLength(50);
            });

            modelBuilder.Entity<Lichhen>(entity =>
            {
                entity.HasKey(e => e.MaLichHen)
                    .HasName("PK__lichhen__150F264F1FC97220");

                entity.ToTable("lichhen");

                entity.Property(e => e.MaLichHen).HasMaxLength(10);

                entity.Property(e => e.MaBanAn).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.NgayHen).HasColumnType("date");

                entity.Property(e => e.NhuCau).HasMaxLength(600);

                entity.Property(e => e.TinhTrang).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.MaBanAnNavigation)
                    .WithMany(p => p.Lichhens)
                    .HasForeignKey(d => d.MaBanAn)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LichHen_BanAn");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.Lichhens)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK_LichHen_KhachHang");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Lichhens)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LichHen_NhanVien");
            });

            modelBuilder.Entity<Monan>(entity =>
            {
                entity.HasKey(e => e.MaMonAn)
                    .HasName("PK__monan__B1171625BE72E219");

                entity.ToTable("monan");

                entity.Property(e => e.MaMonAn).HasMaxLength(10);

                entity.Property(e => e.DonVi).HasMaxLength(50);

                entity.Property(e => e.HinhAnh).HasMaxLength(100);

                entity.Property(e => e.MaNhom).HasMaxLength(10);

                entity.Property(e => e.MoTa).HasMaxLength(500);

                entity.Property(e => e.MoTaNgan).HasMaxLength(100);

                entity.Property(e => e.TenMonAn).HasMaxLength(50);

                entity.HasOne(d => d.MaNhomNavigation)
                    .WithMany(p => p.Monans)
                    .HasForeignKey(d => d.MaNhom)
                    .HasConstraintName("FK_MonAn_NhomMonAn");
            });

            modelBuilder.Entity<Nguyenvatlieu>(entity =>
            {
                entity.HasKey(e => e.MaNguyenVatLieu)
                    .HasName("PK__nguyenva__23D814DDEABBDC4D");

                entity.ToTable("nguyenvatlieu");

                entity.Property(e => e.MaNguyenVatLieu).HasMaxLength(10);

                entity.Property(e => e.DonVi).HasMaxLength(50);

                entity.Property(e => e.TenNguyenVatLieu).HasMaxLength(50);

                entity.Property(e => e.TinhTrang).HasMaxLength(50);
            });

            modelBuilder.Entity<Nhacungcap>(entity =>
            {
                entity.HasKey(e => e.MaNhaCungCap)
                    .HasName("PK__nhacungc__53DA9205116F41F9");

                entity.ToTable("nhacungcap");

                entity.Property(e => e.MaNhaCungCap).HasMaxLength(10);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.MoTaNhaCungCap).HasMaxLength(50);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenNhaCungCap).HasMaxLength(50);
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__nhanvien__77B2CA47C6D85B2F");

                entity.ToTable("nhanvien");

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.ChucVu).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.SoDienThoai).HasMaxLength(20);

                entity.Property(e => e.TenNhanVien).HasMaxLength(100);
            });

            modelBuilder.Entity<Nhommonan>(entity =>
            {
                entity.HasKey(e => e.MaNhom)
                    .HasName("PK__nhommona__234F91CD12B2BBFA");

                entity.ToTable("nhommonan");

                entity.Property(e => e.MaNhom).HasMaxLength(10);

                entity.Property(e => e.TenNhom).HasMaxLength(50);
            });

            modelBuilder.Entity<Phieugoimon>(entity =>
            {
                entity.HasKey(e => e.MaOrder)
                    .HasName("PK__phieugoi__50559EF762A28A9A");

                entity.ToTable("phieugoimon");

                entity.Property(e => e.MaOrder).HasMaxLength(10);

                entity.Property(e => e.GhiChuMonAn).HasMaxLength(600);

                entity.Property(e => e.MaBanAn).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.HasOne(d => d.MaBanAnNavigation)
                    .WithMany(p => p.Phieugoimons)
                    .HasForeignKey(d => d.MaBanAn)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PhieuGoiMon_BanAn");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Phieugoimons)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_PhieuGoiMon_NhanVien");
            });

            modelBuilder.Entity<Phieunhap>(entity =>
            {
                entity.HasKey(e => e.MaPhieuNhap)
                    .HasName("PK__phieunha__1470EF3BD764DBFC");

                entity.ToTable("phieunhap");

                entity.Property(e => e.MaPhieuNhap).HasMaxLength(10);

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.MaNhaCungCap).HasMaxLength(10);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.NgayNhapHang).HasColumnType("datetime");

                entity.HasOne(d => d.MaNhaCungCapNavigation)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.MaNhaCungCap)
                    .HasConstraintName("FK_PhieuNhap_NhaCungCap");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Phieunhaps)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_PhieuNhap_NhanVien");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.TenTaiKhoan)
                    .HasName("PK__taikhoan__B106EAF9EF4A8E3C");

                entity.ToTable("taikhoan");

                entity.Property(e => e.TenTaiKhoan).HasMaxLength(50);

                entity.Property(e => e.MaNhanVien).HasMaxLength(10);

                entity.Property(e => e.MatKhau).HasMaxLength(255);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Taikhoans)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_TaiKhoan_NhanVien");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
