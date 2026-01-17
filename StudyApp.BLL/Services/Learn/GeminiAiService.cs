using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudyApp.BLL.Interfaces.Learn;
using StudyApp.DAL.Data;
using StudyApp.DAL.Entities.Learn;
using StudyApp.DTO.Responses.Learn;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration; // Required for MediaTypeHeaderValue

public class GeminiAiService : IGeminiAiService
{
    private readonly LearningDbContext _context;
    private readonly UserDbContext _userContext;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    // RECOMMENDATION: Move this to appsettings.json
    private readonly string? _geminiApiKey;

    public GeminiAiService(LearningDbContext context, UserDbContext userContext, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _userContext = userContext;
        _mapper = mapper;
        _httpClient = new HttpClient();

        _geminiApiKey = configuration["AiSettings:GeminiApiKey"];
    }

    public async Task<LogsGenerateAiResponse> GenerateImageForFlashcardAsync(int maThe, Guid maNguoiDung)
    {
        const int phiAi = 5;

        // 1. Kiểm tra User & Kim cương
        var user = await _userContext.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung);
        if (user == null || (user.KimCuong ?? 0) < phiAi)
            throw new Exception("Bạn không đủ kim cương (cần 5 💎)!");

        // 2. Lấy nội dung Flashcard
        var the = await _context.TheFlashcards.FindAsync(maThe);
        if (the == null) throw new Exception("Thẻ không tồn tại.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 3. Trừ kim cương
            user.KimCuong -= phiAi;
            _userContext.NguoiDungs.Update(user);
            await _userContext.SaveChangesAsync();

            // 4. Gọi API để lấy ảnh
            string prompt = $"Mô tả hình ảnh minh họa cho: {the.MatTruoc}";
            string imageUrl = await CallGeminiToGetIllustration(the.MatTruoc);

            // 5. Lưu Log
            var log = new LogsGenerateAi
            {
                MaNguoiDung = maNguoiDung,
                MaThe = maThe,
                Prompt = prompt,
                UrlHinhAnh = imageUrl,
                TrangThai = "ThanhCong",
                ThoiGian = DateTime.Now
            };

            _context.LogsGenerateAis.Add(log);

            // Cập nhật ảnh vào Flashcard
            the.HinhAnhTruoc = imageUrl;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<LogsGenerateAiResponse>(log);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Lỗi hệ thống AI: " + ex.Message);
        }
    }

    public async Task<LogsGenerateAiResponse> GenerateImageForBoDeAsync(string tieuDe, Guid maNguoiDung)
    {
        const int phiAi = 5;

        // 1. Kiểm tra User & Kim cương
        var user = await _userContext.NguoiDungs.FirstOrDefaultAsync(u => u.MaNguoiDung == maNguoiDung);
        if (user == null || (user.KimCuong ?? 0) < phiAi)
            throw new Exception("Bạn không đủ kim cương (cần 5 💎)!");

        // 2. Gọi Gemini để lấy "đơn đặt hàng" cho họa sĩ
        string imageUrl = await CallGeminiToGetIllustration(tieuDe);

        // 3. Trừ kim cương và Lưu Log
        user.KimCuong -= phiAi;

        // Cần đảm bảo MaThe là int? trong Entity để gán null thành công
        var log = new LogsGenerateAi
        {
            MaNguoiDung = maNguoiDung,
            MaThe = null,
            Prompt = $"Mô tả minh họa cho bộ đề: {tieuDe}",
            UrlHinhAnh = imageUrl,
            TrangThai = "ThanhCong",
            ThoiGian = DateTime.Now
        };

        _userContext.NguoiDungs.Update(user);
        _context.LogsGenerateAis.Add(log);

        await _userContext.SaveChangesAsync();
        await _context.SaveChangesAsync();

        return _mapper.Map<LogsGenerateAiResponse>(log);
    }

    private async Task<string> CallGeminiToGetIllustration(string keyword)
    {
        string geminiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_geminiApiKey}";

        var requestBody = new
        {
            contents = new[] {
            new { parts = new[] { new { text = $"Create a detailed, high-quality visual description in English to generate a 3D digital art style image for: '{keyword}'. Return ONLY the description, no intro." } } }
        }
        };

        try
        {
            string json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(geminiUrl, content);

            if (!response.IsSuccessStatusCode) return "https://via.placeholder.com/500?text=Gemini_API_Failed";

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Fix: Use nullable dynamic and null-coalescing assignment
            dynamic? result = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            string description = result?.candidates?[0]?.content?.parts?[0]?.text ?? keyword;

            string encodedPrompt = Uri.EscapeDataString(description.Trim());
            return $"https://image.pollinations.ai/prompt/{encodedPrompt}?width=1024&height=768&nologo=true&seed={new Random().Next(1000)}";
        }
        catch
        {
            return "https://via.placeholder.com/500?text=System_Error";
        }
    }
}