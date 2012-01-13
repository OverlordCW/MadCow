using System;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace MadCow
{
    class ShortCut
    {

        public static void Create()
        {
            var WshShell = new WshShell();

            IWshRuntimeLibrary.IWshShortcut MyShortcut;

            MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(@Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\MadCow.lnk");
            MyShortcut.TargetPath = Application.ExecutablePath;
            MyShortcut.Description = "MadCow";
            MyShortcut.Save();

        }
    }
}
