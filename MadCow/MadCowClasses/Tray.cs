
using System.Windows.Forms;

namespace MadCow
{
    internal static class Tray
    {
        /// <summary>
        /// Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.
        /// </summary>
        /// <param name="tipText">The text to display on the balloon tip.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="tipText"/> is null or an empty string.</exception>
        internal static void ShowBalloonTip(string tipText)
        {
            ShowBalloonTip(tipText, ToolTipIcon.Info);
        }

        /// <summary>
        /// Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.
        /// </summary>
        /// <param name="tipText">The text to display on the balloon tip.</param>
        /// <param name="tipIcon">One of the <see cref="T:System.Windows.Forms.ToolTipIcon"/> values.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="tipText"/> is null or an empty string.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        /// <paramref name="tipIcon"/> is not a member of <see cref="T:System.Windows.Forms.ToolTipIcon"/>.
        /// </exception>
        internal static void ShowBalloonTip(string tipText, ToolTipIcon tipIcon)
        {
            ShowBalloonTip(1000, "MadCow", tipText, tipIcon);
        }

        /// <summary>
        /// Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.
        /// </summary>
        /// <param name="timeout">The time period, in milliseconds, the balloon tip should display.</param>
        /// <param name="tipTitle">The title to display on the balloon tip.</param>
        /// <param name="tipText">The text to display on the balloon tip.</param>
        /// <param name="tipIcon">One of the <see cref="T:System.Windows.Forms.ToolTipIcon"/> values.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="timeout"/> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="tipText"/> is null or an empty string.</exception>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
        /// <paramref name="tipIcon"/> is not a member of <see cref="T:System.Windows.Forms.ToolTipIcon"/>.
        /// </exception>
        internal static void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            if (Configuration.MadCow.TrayNotificationsEnabled)
            {
                Form1.GlobalAccess.MadCowTrayIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
            }
        }
    }
}
