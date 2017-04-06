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
        static string FileChoosePl = @"d:\Q4Vid\ChoosePlayers.mp4";
        static string File = @"d:\Q4Vid\20170404162850.mp4";
        static string FileFight = @"d:\Q4Vid\20170404111842.mp4";//fight
        //static string FileFight = @"d:\Q4Vid\Mai.mp4";

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
            var capture = new Capture(FileChoosePl);
            CvInvoke.NamedWindow(WinTitleCapture);
            CvInvoke.NamedWindow(WinChoosePlayer);
            CvInvoke.NamedWindow(WinChoosePlayer2);
            #endregion
            //Test main menu
            #region Title Test
            //var capture = new Capture(File);
            //CvInvoke.NamedWindow(WinTitleCapture);
            #endregion
            //Test Figth
            #region Fight Test
            //var capture = new Capture(FileFight);
            //CvInvoke.NamedWindow(WinCapture);
            //CvInvoke.NamedWindow(WinCapture2);
            //CvInvoke.NamedWindow(WinRoundMessage);
            //CvInvoke.NamedWindow(WinGoMessage);

            #endregion
            var stat = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);

            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();

            CvInvoke.WaitKey(0);

            CvInvoke.DestroyAllWindows();
            imgFrame.Dispose();
            capture.Dispose();
        }

        private static void Capture_ImageGrabbed(object sender, EventArgs e)
        {

            Capture cp = sender as Capture;

            cp.Retrieve(imgFrame);
            var p1Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15))
                .InRange(new Bgr(180, 160, 0), new Bgr(230, 205, 100));
            var p2Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15))
                .InRange(new Bgr(5, 190, 100), new Bgr(80, 250, 140));
            var choosePlayer = imgFrame.GetSubRect(new System.Drawing.Rectangle(25, 895, 410, 25))
                .InRange(new Bgr(180, 160, 0), new Bgr(230, 205, 100));
            var choosePlayer2 = imgFrame.GetSubRect(new System.Drawing.Rectangle(1475, 895, 410, 25))
                .InRange(new Bgr(5, 190, 100), new Bgr(80, 250, 140));

            var RoundReadyMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(480, 505, 800, 70))
                .InRange(new Bgr(246, 222, 46), new Bgr(255, 255, 210));
            var GoMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(580, 435, 750, 200))
                .InRange(new Bgr(100, 240, 240), new Bgr(180, 255, 255));

            var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(new Bgr(5, 150, 215), new Bgr(90, 200, 246));

            //CvInvoke.Imshow(WinCapture, p1Name);
            //CvInvoke.Imshow(WinCapture2, p2Name);
            //CvInvoke.Imshow(WinRoundMessage, RoundReadyMessage);
            //CvInvoke.Imshow(WinGoMessage, GoMessage);


            CvInvoke.Imshow(WinChoosePlayer2, choosePlayer2);
            CvInvoke.Imshow(WinChoosePlayer, choosePlayer);
            CvInvoke.Imshow(WinTitleCapture, titleCap);
            //Task.Delay(1000);


            //string stt = System.IO.Path.Combine(svDir, DateTime.Now.ToString("yyyyMMdd_HHmmssffff"));
            //p1Name.ToBitmap().Save(stt + "Maip1.bmp");
            //p2Name.ToBitmap().Save(stt + "Maip2.bmp");
            //RoundReadyMessage.ToBitmap().Save(stt + ".bmp");
            //titleCap.ToBitmap().Save(stt + ".bmp");
        }
    }
}
