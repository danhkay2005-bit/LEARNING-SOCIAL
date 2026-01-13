using StudyApp.DTO.Requests.Learn;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IQuestionEditor
    {
        ChiTietTheRequest GetQuestionData();
        void SetQuestionData(ChiTietTheRequest data);
    }
}
