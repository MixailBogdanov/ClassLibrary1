using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ChatHost
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_Min = 2;
        const int SW_Max = 3;
        const int SW_Norm = 4;
        static void Main(string[] args)
        {         
        
            var handle = GetConsoleWindow();

            // System.Threading.Thread.Sleep(3000);
            ShowWindow(handle, SW_Min);
            //  System.Threading.Thread.Sleep(3000);
            // ShowWindow(handle, SW_Norm);
            // System.Threading.Thread.Sleep(3000);
            // ShowWindow(handle, SW_Min);
            //System.Threading.Thread.Sleep(3000);
            // ShowWindow(handle, SW_Max);
            // System.Threading.Thread.Sleep(3000);
            //ShowWindow(handle, SW_HIDE);
            //System.Threading.Thread.Sleep(3000);
            // ShowWindow(handle, SW_SHOW);
            using (var host = new ServiceHost(typeof(ClassLibrary1.ServiceChat)))
            {
                host.Open();
                Console.WriteLine("Start");
                Console.ReadLine();
            }
        }
    }
}
