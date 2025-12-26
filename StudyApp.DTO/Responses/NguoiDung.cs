using StudyApp.DTO.Commons;
using StudyApp.DTO.Entities;
using StudyApp.DTO.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.DTO.Responses
{
    public class DangKyNguoiDungResponse: DtoKetQuaCoBan
    {
       public NguoiDungDTO? NguoiDung { get; set; }
    }

    public class DangNhapNguoiDungResponse: DtoKetQuaCoBan
    {
       public NguoiDungDTO? NguoiDung { get; set; }
       public string? Token { get; set; }
    }

    public class  DoiMatKhauNguoiDungResponse: DtoKetQuaCoBan
    {
        
    }
}
