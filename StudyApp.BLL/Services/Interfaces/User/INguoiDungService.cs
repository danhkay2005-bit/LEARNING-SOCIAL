using StudyApp.DTO.Requests.NguoiDung;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Services.Interfaces.User
{
    public enum LoginResult { Success, InvalidCredentials, UserNotFound }
    public enum RegisterResult { Success, UsernameExists, Fail }

    public interface INguoiDungService
    {
        LoginResult Login(DangNhapRequest request);
        RegisterResult Register(DangKyNguoiDungRequest request);
    }



}
