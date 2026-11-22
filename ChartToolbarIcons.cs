using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Trade.It
{
    internal static class ChartToolbarIcons
    {
        private sealed class IconSet { public required Image Normal { get; init; } public required Image Active { get; init; } public Image? Success { get; init; } }
        private static readonly Dictionary<Button, IconSet> Icons = new();
        public static void Register(Button button, Image normal, Image active, Image? success = null) => Icons[button] = new IconSet { Normal = normal, Active = active, Success = success };
        public static void SetActive(Button button, bool active) { if (!Icons.TryGetValue(button, out var set)) return; button.Image = active ? set.Active : set.Normal; button.Invalidate(); }
        public static void SetSaveFeedback(Button button, bool success) { if (!Icons.TryGetValue(button, out var set)) return; button.Image = success && set.Success != null ? set.Success : set.Normal; button.Invalidate(); }
    }
}
