using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MadCow
{
    class MadCowRunProcedure
    {
        public static void RunMadCow(int runType) //0 - No D3 Client //1 - Client Installed
        {
            if (runType == 1)
            {
                PreRequeriments.CheckPrerequeriments();
                Diablo3.FindDiablo3();
                Diablo3.VerifyVersion();
                DownloadRevision.DownloadLatest();
                Uncompress.UncompressFiles();
                Compile.ExecuteCommandSync(Compile.msbuildPath + " " + Compile.compileArgs);
                Compile.ModifyMooegeINI();
                Compile.WriteVbsPath();
                MPQprocedure.ValidateMD5();
                MPQprocedure.MpqTransfer();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n _________________________________________"
                                 + "\n| Process has been completed successfully |"
                                 + "\n| Check your desktop for Mooege shortcut! |"
                                 + "\n'-----------------------------------------'\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                PreRequeriments.CheckPrerequeriments();
                DownloadRevision.DownloadLatest();
                Uncompress.UncompressFiles();
                Compile.ExecuteCommandSync(Compile.msbuildPath + " " + Compile.compileArgs);
                Compile.ModifyMooegeINI();
                Compile.WriteVbsPath();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n _________________________________________"
                                 + "\n| Warning: Since you dont have Diablo 3  |"
                                 + "\n| client installed, MadCow is unable to  |"
                                 + "\n| copy MPQ files automatically for you.  |"
                                 + "\n|                                        |"
                                 + "\n| Please move MPQ files manually into    |"
                                 + "\n| ..//MadCow/MPQ/ Folder in order for    |"
                                 + "\n| Mooege to work.                        |"
                                 + "\n'-----------------------------------------'\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n _________________________________________"
                                 + "\n| Process has been completed successfully |"
                                 + "\n| Check your desktop for Mooege shortcut! |"
                                 + "\n'-----------------------------------------'\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
