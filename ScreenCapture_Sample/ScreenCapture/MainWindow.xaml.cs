using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ScreenCapture
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BitmapImage Capture(System.Drawing.Point mousePoint)
        {
            Bitmap bitmap = new Bitmap(50, 50, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                int currPosXOnCenter = (int)mousePoint.X - 25;
                int currPosyOnCenter = (int)mousePoint.Y - 25;
                g.CopyFromScreen(currPosXOnCenter <= 0 ? 0 : currPosXOnCenter, currPosyOnCenter <= 0 ? 0 : currPosyOnCenter, 0, 0, bitmap.Size);
                //g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

                using (MemoryStream memory = new MemoryStream())
                {
                    bitmap.Save(memory, ImageFormat.Bmp);
                    memory.Position = 0;
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();

                    return bitmapImage;
                }
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out System.Drawing.Point pPoint);

        private void Button_Click(object sender, RoutedEventArgs e) 
            //가져온 예제 이상 ㅎㅎ;; BeginInvoke 쓸거면 Task 왜만드는지
            //wpf에서 왜인지 System.Windows.Forms 안써져서 dll 호출해서 하는걸로 바꿈
        {
            Task captureTask = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    this.xImg.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        System.Drawing.Point p = new System.Drawing.Point();
                        GetCursorPos(out p);
                        this.xImg.Source = this.Capture(p);
                        //this.xImg.Source = this.Capture(Mouse.GetPosition(null));
                        //this.xImg.Source = this.Capture(PointToScreen(Mouse.GetPosition(this)));
                        //this.xImg.Source = this.Capture(System.Windows.Forms.Cursor.Position);
                    }));

                    await Task.Delay(50);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }

    
}
