using System.Diagnostics;
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
            
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

            wplayer.URL = @"Assets/outro.mp3";
            wplayer.controls.currentPosition = 4.5;
            wplayer.controls.play();
            
            while (true)
            {
                Thread.Sleep(40);
                if (Convert.ToInt32(wplayer.controls.currentPosition) == 16)
                {
                    wplayer.controls.currentPosition = 16.45;
                }
            }
        }).Start();

        Thread.Sleep(150);

        Process[] processes = Process.GetProcessesByName("FakeBSODoutro");
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