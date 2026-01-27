using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process? pythonServerProcess;

        // 导入Windows API函数
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        // 控制台控制事件类型
        const uint CTRL_C_EVENT = 0;
        const uint CTRL_BREAK_EVENT = 1;

        public MainWindow()
        {
            InitializeComponent();
            StartPythonServer();
            this.Closing += MainWindow_Closing;
        }

        private void StartPythonServer()
        {
            string pythonServerPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "python-server");
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k cd /d \"{pythonServerPath}\" && python manage.py runserver 0.0.0.0:9924",
                WorkingDirectory = pythonServerPath,
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardInput = true,
                WindowStyle = ProcessWindowStyle.Minimized
            };

            try
            {
                pythonServerProcess = Process.Start(startInfo);
                if (pythonServerProcess != null)
                {
                    // 等待服务器启动
                    System.Threading.Thread.Sleep(2000);
                    // 导航到服务器地址
                    chromeBrowser.Address = "http://127.0.0.1:9924/";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动Python服务器失败: {ex.Message}");
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (pythonServerProcess != null && !pythonServerProcess.HasExited)
            {
                try
                {
                    // 附加到目标进程的控制台
                    if (AttachConsole((uint)pythonServerProcess.Id))
                    {
                        // 生成Ctrl+Break事件
                        GenerateConsoleCtrlEvent(CTRL_BREAK_EVENT, 0);
                        // 释放控制台
                        FreeConsole();
                        // 等待进程退出
                        pythonServerProcess.WaitForExit(2000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发送Ctrl+Break信号失败: {ex.Message}");
                }
            }
        }
    }
}