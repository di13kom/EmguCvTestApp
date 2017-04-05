using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguTestApp
{
    class Program
    {
        static string FileChoosePl = @"d:\Q4Vid\20170405113751.mp4";
        static string File = @"d:\Q4Vid\20170404162850.mp4";
        static string svDir = @"d:\Q4Vid\Tst\";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");

        static string WinCapture = "Win";
        static string WinCapture2 = "Win2";
        static string WinTitleCapture = "Title";
        static string WinChoosePlayer = "ChoosePlayer";

        static void Main(string[] args)
        {
            //CvInvoke.NamedWindow(WinCapture);
            CvInvoke.NamedWindow(WinCapture2);
            CvInvoke.NamedWindow(WinTitleCapture);
            CvInvoke.NamedWindow(WinChoosePlayer);

            var capture = new Capture(FileChoosePl);
            var stat = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);

            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();

            CvInvoke.WaitKey(0);

            //CvInvoke.DestroyWindow(WinCapture);
            //CvInvoke.DestroyWindow(WinCapture2);
            CvInvoke.DestroyAllWindows();
            imgFrame.Dispose();
        }

        private static void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Capture cp = sender as Capture;
            //imgFrame.ROI = new System.Drawing.Rectangle(35, 110, 500, 500);

            cp.Retrieve(imgFrame);
            var p1Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15)).Convert<Gray, byte>();
            var p2Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15)).Convert<Gray, byte>();
            var choosePlayer = imgFrame.GetSubRect(new System.Drawing.Rectangle(20, 895, 400, 25))/*.Convert<Gray, byte>()*/;

            var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))/*.Convert<Gray, byte>()*/;
            //var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50));
            //
            CvInvoke.Threshold(p1Name, p1Name, 110, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            CvInvoke.Threshold(p2Name, p2Name, 110, 255, Emgu.CV.CvEnum.ThresholdType.Binary);

            //CvInvoke.Threshold(titleCap, titleCap, 157, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            var titleCapgr = titleCap.InRange(new Bgr(5, 150, 210), new Bgr(90, 200, 246));

            //CvInvoke.Threshold(choosePlayer, choosePlayer, 200, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            var choosePlayerGr = choosePlayer.InRange(new Bgr(180, 180, 0), new Bgr(230, 205, 100));
            

            //choosePlayer = choosePlayer.ThresholdBinary(new Gray(240), new Gray(255));
            //CvInvoke.Threshold(titleCap, titleCap, 180, 255, Emgu.CV.CvEnum.ThresholdType.ToZero);

            //titleCap = titleCap.ThresholdBinary(new Bgr(11, 154, 244), new Bgr(28, 148, 223));
            //titleCap = titleCap.ThresholdToZero(new Bgr(28, 148, 223));
            //BackGr.ROI = new System.Drawing.Rectangle(0, 0, 550, 50);
            //titleCap = titleCap.And(BackGr);


            //
            //CvInvoke.Imshow(WinCapture, p1Name);
            //CvInvoke.Imshow(WinCapture2, p2Name);
            CvInvoke.Imshow(WinTitleCapture, titleCapgr);
            CvInvoke.Imshow(WinChoosePlayer, choosePlayerGr);
            CvInvoke.Imshow(WinCapture2, choosePlayer);
            //Task.Delay(1000);
            //string stt = System.IO.Path.Combine(svDir, DateTime.Now.ToString("yyyyMMdd_HHmmssffff"));
            //titleCap.ToBitmap().Save(stt + ".bmp");
        }
    }
}
