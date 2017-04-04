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
        static string File = @"d:\Q4Vid\20170403100710.mp4";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);

        static string WinCapture = "Win";
        static string WinCapture2 = "Win2";
        static string WinTitleCapture = "Title";

        static void Main(string[] args)
        {
            CvInvoke.NamedWindow(WinCapture);
            CvInvoke.NamedWindow(WinCapture2);
            CvInvoke.NamedWindow(WinTitleCapture);

            var capture = new Capture(File);
            var stat = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);

            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
            //while (fCount < stat)
            //{
            //    var fr = capture.QueryFrame();

            //    //capture.Start();

            //    CvInvoke.Imshow(WinCapture, fr);
            ////    fCount++;
            ////}

            CvInvoke.WaitKey(0);

            //CvInvoke.DestroyWindow(WinCapture);
            //CvInvoke.DestroyWindow(WinCapture2);
            CvInvoke.DestroyAllWindows();
        }

        private static void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            Capture cp = sender as Capture;
            //imgFrame.ROI = new System.Drawing.Rectangle(35, 110, 500, 500);

            cp.Retrieve(imgFrame);
            var p1Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15)).Convert<Gray,byte>();
            var p2Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15)).Convert<Gray, byte>();
            var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(27, 28, 600, 50)).Convert<Gray, byte>();
            //
            CvInvoke.Threshold(p1Name, p1Name, 110, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            CvInvoke.Threshold(p2Name, p2Name, 110, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            CvInvoke.Threshold(titleCap, titleCap, 160, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            //
            CvInvoke.Imshow(WinCapture, p1Name);
            CvInvoke.Imshow(WinCapture2, p2Name);
            CvInvoke.Imshow(WinTitleCapture, titleCap);
            //Console.WriteLine(cp.Ptr);
            //CvInvoke.DestroyAllWindows();
        }
    }
}
