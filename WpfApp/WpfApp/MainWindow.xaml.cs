using System;
using System.Diagnostics;
using System.IO;
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
        public MainWindow()
        {
            InitializeComponent();
            StartPythonServer();
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
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };

            try
            {
                Process.Start(startInfo);
                // 等待服务器启动
                System.Threading.Thread.Sleep(2000);
                // 导航到服务器地址
                chromeBrowser.Address = "http://127.0.0.1:9924/";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动Python服务器失败: {ex.Message}");
            }
        }
    }
}