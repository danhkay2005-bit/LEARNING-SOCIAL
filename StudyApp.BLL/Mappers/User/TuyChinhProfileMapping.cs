using AutoMapper;
using StudyApp.DAL.Entities.User;
using StudyApp.DTO.Enums;
using StudyApp.DTO.Requests.NguoiDung;
using StudyApp.DTO.Responses.NguoiDung;
using static StudyApp.BLL.Mappers.MappingHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyApp.BLL.Mappers.User
{
    public class TuyChinhProfileMapping: Profile
    {
        public TuyChinhProfileMapping()
        {
            CreateMap<TuyChinhProfile, TuyChinhProfileResponse>();
        }
    }
}
