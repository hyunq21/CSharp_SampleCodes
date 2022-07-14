using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Tesseract;

namespace Tesseract_OCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bitmapImage;

        public MainWindow()
        {
            //bitmapImage = new BitmapImage(new Uri(@"../Resources/sample.png", UriKind.RelativeOrAbsolute));
            //image1.Source = bitmapImage;

            InitializeComponent();
        }

        private void click1(object sender, RoutedEventArgs e)
        {
            Bitmap bitmap = null;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(image1.Source as BitmapSource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }

            var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.TesseractAndLstm);//TesseractAndCube);
            var texts = ocr.Process(bitmap);
            MessageBox.Show(texts.GetText());
        }
    }
}
