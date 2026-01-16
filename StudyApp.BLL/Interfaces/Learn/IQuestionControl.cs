using System;
using System.Collections.Generic;
using System.Text;

namespace StudyApp.BLL.Interfaces.Learn
{
    public interface IQuestionControl
    {
        // Kiểm tra xem người dùng đã chọn đúng/sai chưa
        bool IsCorrect { get; }

        // Hiển thị đáp án đúng/sai (đổi màu nút xanh/đỏ) sau khi người dùng nhấn "Kiểm tra"
        void ShowResult();
    }
}
