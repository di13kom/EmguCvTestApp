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
        static string FileChoosePl = @"d:\Q4Vid\20170405122929.mp4";
        static string File = @"d:\Q4Vid\20170404162850.mp4";
        static string FileFight = @"d:\Q4Vid\20170330185346.mp4";
        
        static string svDir = @"d:\Q4Vid\Tst\";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");

        static string WinCapture = "Win";
        static string WinCapture2 = "Win2";
        static string WinTitleCapture = "Title";
        static string WinChoosePlayer = "ChoosePlayer";
        static string WinChoosePlayer2 = "ChoosePlayer2";
        static string WinRoundMessage = "RoundMessage";
        static string WinGoMessage = "GoMessage";

        static void Main(string[] args)
        {
            


            #region Player Choose
            //Test Players Choose
            //var capture = new Capture(FileChoosePl);
            //CvInvoke.NamedWindow(WinTitleCapture);
            //CvInvoke.NamedWindow(WinChoosePlayer);
            //CvInvoke.NamedWindow(WinChoosePlayer2);
            #endregion
            //Test main menu
            #region Title Test
            //var capture = new Capture(File);
            //CvInvoke.NamedWindow(WinTitleCapture);
            #endregion
            //Test Figth
            #region Fight Test
            var capture = new Capture(FileFight);
            CvInvoke.NamedWindow(WinCapture);
            CvInvoke.NamedWindow(WinCapture2);
            CvInvoke.NamedWindow(WinRoundMessage);
            //CvInvoke.NamedWindow(WinGoMessage);

            #endregion
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
            var p1Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15))/*.Convert<Gray, byte>()*/;
            var p2Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15))/*.Convert<Gray, byte>()*/;
            var choosePlayer = imgFrame.GetSubRect(new System.Drawing.Rectangle(25, 895, 410, 25))/*.Convert<Gray, byte>()*/;
            var choosePlayer2 = imgFrame.GetSubRect(new System.Drawing.Rectangle(1475, 895, 410, 25));

            var RoundReadyMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(480, 505, 800, 70));
            var GoMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(580, 435, 750, 200));

            var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))/*.Convert<Gray, byte>()*/;
            //var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50));
            //
            //CvInvoke.Threshold(p1Name, p1Name, 110, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            //CvInvoke.Threshold(p2Name, p2Name, 110, 255, Emgu.CV.CvEnum.ThresholdType.Binary);

            //CvInvoke.Threshold(titleCap, titleCap, 157, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            var titleCapgr = titleCap.InRange(new Bgr(5, 150, 215), new Bgr(90, 200, 246));

            //CvInvoke.Threshold(choosePlayer, choosePlayer, 200, 255, Emgu.CV.CvEnum.ThresholdType.Binary);
            var choosePlayerGr = choosePlayer.InRange(new Bgr(180, 180, 0), new Bgr(230, 205, 100));
            var choosePlayer2Gr = choosePlayer2.InRange(new Bgr(10, 190, 100), new Bgr(80, 250, 130));

            var p1NameGr = p1Name.InRange(new Bgr(180, 170, 0), new Bgr(230, 205, 100));
            var p2NameGr = p2Name.InRange(new Bgr(5, 190, 100), new Bgr(80, 250, 140));

            var RoundReadyMessageGr = RoundReadyMessage.InRange(new Bgr(246,222,46), new Bgr(255,255,210));
            var GoMessageGr = GoMessage.InRange(new Bgr(100,240,240), new Bgr(180,255,255));
            //choosePlayer = choosePlayer.ThresholdBinary(new Gray(240), new Gray(255));
            //CvInvoke.Threshold(titleCap, titleCap, 180, 255, Emgu.CV.CvEnum.ThresholdType.ToZero);

            //titleCap = titleCap.ThresholdBinary(new Bgr(11, 154, 244), new Bgr(28, 148, 223));
            //titleCap = titleCap.ThresholdToZero(new Bgr(28, 148, 223));
            //BackGr.ROI = new System.Drawing.Rectangle(0, 0, 550, 50);
            //titleCap = titleCap.And(BackGr);


            //
            CvInvoke.Imshow(WinCapture, p1NameGr);
            CvInvoke.Imshow(WinCapture2, p2NameGr);
            CvInvoke.Imshow(WinRoundMessage, RoundReadyMessageGr);
            //CvInvoke.Imshow(WinGoMessage, GoMessageGr);


            //CvInvoke.Imshow(WinChoosePlayer2, choosePlayer2Gr);
            //CvInvoke.Imshow(WinChoosePlayer, choosePlayerGr);
            //CvInvoke.Imshow(WinTitleCapture, titleCapgr);
            //Task.Delay(1000);
            //string stt = System.IO.Path.Combine(svDir, DateTime.Now.ToString("yyyyMMdd_HHmmssffff"));
            //RoundReadyMessageGr.ToBitmap().Save(stt + ".bmp");
            //titleCap.ToBitmap().Save(stt + ".bmp");
        }
    }
}
