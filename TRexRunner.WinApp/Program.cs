using System;

namespace TRexRunner.WinApp
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TRexRunnerGame())
                game.Run();
        }
    }
}
