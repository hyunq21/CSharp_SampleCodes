using OpenCvSharp;
using System;
using System.Windows;
using System.Windows.Threading;

namespace OpenCVTest
{
    public partial class MainWindow
    {
        VideoCapture cam;
        Mat frame;
        DispatcherTimer timer;
        bool is_initCam, is_initTimer;


        public MainWindow()
        {
            InitializeComponent();

            Mat image = Cv2.ImRead($@"C:\Users\hyunq21\Pictures\Screenshots\스크린샷(1).png");//, ImreadModes.Grayscale);
            //Cv2.ImShow("image", image);//이미지 출력 함수를 활용해 이미지를 새로운 윈도우 창에 표시할 수 있습니다.
            //Cv2.WaitKey(0);//시간 대기 함수의 값을 0으로 두어, 키 입력이 있을 때까지 유지합니다.
            //Cv2.DestroyAllWindows();//모든 윈도우창 제거 함수로 키 입력 발생시, 윈도우 창을 종료합니다.
            Cam_7.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(image);


            // 카메라, 타이머(0.01ms 간격) 초기화
            is_initCam = init_camera();
            is_initTimer = init_Timer(1);//0.01);

            // 초기화 완료면 타이머 실행
            if (is_initTimer && is_initCam) timer.Start();
        }



        private void windows_loaded(object sender, RoutedEventArgs e)
        {
            // 카메라, 타이머(0.01ms 간격) 초기화
            is_initCam = init_camera();
            is_initTimer = init_Timer(0.01);

            // 초기화 완료면 타이머 실행
            if (is_initTimer && is_initCam) timer.Start();
        }

        private bool init_Timer(double interval_ms)
        {
            try
            {
                timer = new DispatcherTimer();

                timer.Interval = TimeSpan.FromMilliseconds(interval_ms);
                timer.Tick += new EventHandler(timer_tick);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool init_camera()
        {
            try
            {
                // 0번 카메라로 VideoCapture 생성 (카메라가 없으면 안됨)
                cam = new VideoCapture(0);
                cam.FrameHeight = (int)Cam_1.Height;
                cam.FrameWidth = (int)Cam_1.Width;

                // 카메라 영상을 담을 Mat 변수 생성
                frame = new Mat();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            // 0번 장비로 생성된 VideoCapture 객체에서 frame을 읽어옴
            cam.Read(frame);


            Mat blur = new Mat();
            Cv2.GaussianBlur(frame, blur, new OpenCvSharp.Size(3, 3), 1, 0, BorderTypes.Default);

            Mat sobel = new Mat();
            Cv2.Sobel(blur, sobel, MatType.CV_32F, 1, 0, ksize: 3, scale: 1, delta: 0, BorderTypes.Default);
            sobel.ConvertTo(sobel, MatType.CV_8UC1);

            Mat scharr = new Mat();
            Cv2.Scharr(blur, scharr, MatType.CV_32F, 1, 0, scale: 1, delta: 0, BorderTypes.Default);
            scharr.ConvertTo(scharr, MatType.CV_8UC1);

            Mat laplacian = new Mat();
            Cv2.Laplacian(blur, laplacian, MatType.CV_32F, ksize: 3, scale: 1, delta: 0, BorderTypes.Default);
            laplacian.ConvertTo(laplacian, MatType.CV_8UC1);

            Mat canny = new Mat();
            Cv2.Canny(frame, canny, 100, 200, 3, true);

            // 읽어온 Mat 데이터를 Bitmap 데이터로 변경 후 컨트롤에 그려줌
            Cam_1.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(frame);
            Cam_2.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(blur);
            Cam_3.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(sobel);
            Cam_4.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(scharr);
            Cam_5.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(laplacian);
            Cam_6.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(canny);



            //Cam_1.Source = OpenCvSharp.WpfExtensions.WriteableBitmapConverter.ToWriteableBitmap(frame);
        }




    }
}
