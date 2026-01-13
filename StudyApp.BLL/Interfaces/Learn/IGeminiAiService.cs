using StudyApp.DTO.Responses.Learn;

public interface IGeminiAiService
{
    // Sinh ảnh hoặc nội dung giải thích từ Flashcard
    Task<LogsGenerateAiResponse> GenerateImageForFlashcardAsync(int maThe, Guid maNguoiDung);
}