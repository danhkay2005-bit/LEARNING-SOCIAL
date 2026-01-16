
namespace WinForms.Services
{
    public interface IWinFormsThachDauNotifier
    {
        Task NotifyReadyToStart(int maThachDau);
        Task NotifyUpdateScore(int maThachDau, Guid userId, int score);
    }
}