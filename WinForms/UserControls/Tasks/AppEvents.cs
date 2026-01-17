using System;
using System.Collections.Generic;
using System.Text;

namespace WinForms.UserControls.Tasks
{
    public static class AppEvents
    {
        public static event Action? UserStatsChanged;
        public static event Action<int>? ItemBought;

        public static void OnUserStatsChanged()
        {
            UserStatsChanged?.Invoke();
        }

        public static void OnItemBought(int itemId)
        {
            ItemBought?.Invoke(itemId);
        }

        // ===== Alias (tiện lợi) =====
        public static void RaiseUserStatsChanged()
        {
            OnUserStatsChanged();
        }

        public static void RaiseItemBought(int itemId)
        {
            OnItemBought(itemId);
        }
    }
}
