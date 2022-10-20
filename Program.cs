using System.Diagnostics;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FakeBSODoutro;
static class Program
{
    const UInt32 WM_KEYDOWN = 0x0100;
    const int VK_F11 = 0x7A;

    [DllImport("user32.dll")]
    static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

    [STAThread]
    static void Main()
    {
        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;

            var temporaryFilePath = String.Format("{0}{1}{2}", Path.GetTempPath(), Guid.NewGuid().ToString("N"), ".mp3");
            
            using (var memoryStream = new MemoryStream(Properties.Resources.outro))
            using (var tempFileStream = new FileStream(temporaryFilePath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.Position = 0;
                
                memoryStream.WriteTo(tempFileStream);
            }
            
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = temporaryFilePath;
            wplayer.settings.setMode("loop", false);
            wplayer.controls.currentPosition = 4.5;
            wplayer.controls.play();

            while (true)
            {
                Thread.Sleep(20);
                if (Convert.ToInt32(wplayer.controls.currentPosition) == 16)
                {
                    if (File.Exists(temporaryFilePath))
                        File.Delete(temporaryFilePath);
                    while (true)
                    {
                        Thread.Sleep(50);
                        wplayer.controls.currentPosition = 16.6;
                    }
                }
            }
        }).Start();

        Thread.Sleep(160);

        Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
        foreach (Process proc in processes)
            PostMessage(proc.MainWindowHandle, WM_KEYDOWN, VK_F11, 0);

        Countdown();
        
        ShowFakeBsod();
        
        Console.Read();
    }

    static void Countdown()
    {
        int counter = 10;
        while (!counter.Equals(0))
        {
            Console.WriteLine($"Poop in {counter} seconds");
            counter--;
            Thread.Sleep(1000);
        }
        Console.WriteLine("Pooping now!");
        Thread.Sleep(900);
    }

    static async Task ShowFakeBsod()
    {
        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            FakeBSODform.BsodForm bsodForm = new();
            Console.WriteLine("Press Enter to close this window");
        }).Start();
    }
}