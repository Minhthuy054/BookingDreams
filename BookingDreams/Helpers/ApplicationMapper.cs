﻿using AutoMapper;
using BookingDreams.Data;
using BookingDreams.Models;

namespace BookingDreams.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper() {
            CreateMap<KhachSan, KhachSanModel>().ReverseMap();
            //CreateMap<ChucVu, ChucVuModel>().ReverseMap();
            CreateMap<DatPhong, DatPhongModel>().ReverseMap();
            CreateMap<KhachHang, KhachHangModel>().ReverseMap();
            //CreateMap<PhanQuyen, PhanQuyenModel>().ReverseMap();
            CreateMap<Phong, PhongModel>().ReverseMap();
            //CreateMap<Role, RoleModel>().ReverseMap();
           // CreateMap<TaiKhoan, TaiKhoanModel>().ReverseMap();
            CreateMap<TinhThanh, TinhThanhModel>().ReverseMap();
            CreateMap<DanhGia, DanhGiaModel>().ReverseMap();
            CreateMap<MaGiamGia, MaGiamGiaModel>().ReverseMap();
        }
    }
}
