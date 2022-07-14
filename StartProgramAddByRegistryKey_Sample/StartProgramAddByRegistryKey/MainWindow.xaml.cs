using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StartProgramAddByRegistryKey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string strAppName = "TestWindowsStart";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void click1(object sender, RoutedEventArgs e)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                try
                {
                    //레지스트리 등록...
                    if (rk.GetValue(strAppName) == null)
                    {
                        rk.SetValue(strAppName, AppDomain.CurrentDomain.BaseDirectory);// ExecutablePath.ToString());
                        //System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    }

                    //레지스트리 닫기...
                    rk.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류: " + ex.Message.ToString());
                }

                MessageBox.Show(strAppName + " 프로그램을 레지스트리에 등록 하였습니다." + System.Environment.NewLine);
            }

        }

        private void click2(object sender, RoutedEventArgs e)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {

                try
                {
                    //레지스트리 삭제
                    if (rk.GetValue(strAppName) != null)
                    {
                        rk.DeleteValue(strAppName, false);
                    }


                    //레지스트리 닫기...
                    rk.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("오류: " + ex.Message.ToString());
                }

                MessageBox.Show(strAppName + " 프로그램을 레지스트리에 삭제 하였습니다." + System.Environment.NewLine);
            }

        }
    }
    }
}
