#define TITLE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;


namespace EmguTestApp
{
    static class ColorsThresHolds
    {
        static public Tuple<Bgr, Bgr> p1Colors = Tuple.Create(new Bgr(170, 160, 0), new Bgr(230, 225, 100));
        static public Tuple<Bgr, Bgr> p2Colors = Tuple.Create(new Bgr(0, 190, 95), new Bgr(95, 250, 150));

        static public Tuple<Bgr, Bgr> TitleColors = Tuple.Create(new Bgr(5, 150, 215), new Bgr(90, 200, 246));

        static public Tuple<Bgr, Bgr> RoundReadyColors = Tuple.Create(new Bgr(246, 222, 46), new Bgr(255, 255, 210));
        static public Tuple<Bgr, Bgr> GoMessageColors = Tuple.Create(new Bgr(100, 240, 240), new Bgr(180, 255, 255));

        //static public Tuple<Bgr, Bgr> WinLoseAfterMatch = Tuple.Create(new Bgr(200, 192, 188), new Bgr(255, 255, 255));
        static public Tuple<Bgr, Bgr> WinLoseAfterMatch = Tuple.Create(new Bgr(0, 0, 0), new Bgr(60, 45, 35));

    }

    class Program
    {
        static string FileToPlay;

        //static string FileToPlay = @"d:\Q4Vid\20170404162850.mp4";
        
        //static string FileFight = @"d:\Q4Vid\Mai.mp4";


        static string svDir = @"d:\Q4Vid\Tst\";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");

        static string WinCapture = "Win";
        static string WinCapture2 = "Win2";
        static string WinCaptureCol = "WinCol";
        static string WinCaptureCol2 = "Win2Col";

        static string WinTitleCapture = "Title";
        static string WinChoosePlayer = "ChoosePlayer";
        static string WinChoosePlayer2 = "ChoosePlayer2";
        static string WinRoundMessage = "RoundMessage";
        static string WinGoMessage = "GoMessage";

        static string WinLoseP1Win = "WinLoseP1";
        static string WinLoseP2Win = "WinLoseP2";

        static void Main(string[] args)
        {



            //Test Players Choose
#if CHOOSEPLAYER
            #region Player Choose
            FileToPlay = @"d:\Q4Vid\ChoosePlayers.mp4";
            
            CvInvoke.NamedWindow(WinTitleCapture);
            CvInvoke.NamedWindow(WinChoosePlayer);
            CvInvoke.NamedWindow(WinChoosePlayer2);

            CvInvoke.NamedWindow(WinGoMessage);
            #endregion
#elif TITLE

            //Test main menu
            #region Title Test
            FileToPlay = @"d:\Q4Vid\Menus.mp4";

            CvInvoke.NamedWindow(WinTitleCapture);
            CvInvoke.NamedWindow(WinGoMessage);
            #endregion

            //Test Figth
#elif FIGHT
            #region Fight Test
            FileToPlay = @"d:\Q4Vid\20170404111842.mp4";
            CvInvoke.NamedWindow(WinCapture);
            CvInvoke.NamedWindow(WinCapture2);
            CvInvoke.NamedWindow(WinRoundMessage);
            //CvInvoke.NamedWindow(WinGoMessage);
            CvInvoke.NamedWindow(WinCaptureCol);
            CvInvoke.NamedWindow(WinCaptureCol2);
            #endregion
            //WinLose
#elif RESULT
            #region WinLose After Match
            FileToPlay = @"d:\Q4Vid\PerfectGame.mp4";
            
            CvInvoke.NamedWindow(WinTitleCapture);
            CvInvoke.NamedWindow(WinLoseP1Win);
            CvInvoke.NamedWindow(WinLoseP2Win);
            #endregion
#endif
            var capture = new Capture(FileToPlay);
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
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2);
            //.InRange(new Bgr(170, 160, 0), new Bgr(230, 225, 100));
            var p2Name = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15))
                .InRange(ColorsThresHolds.p2Colors.Item1, ColorsThresHolds.p2Colors.Item2);
            //.InRange(new Bgr(0, 190, 95), new Bgr(95, 250, 150));
            var choosePlayer = imgFrame.GetSubRect(new System.Drawing.Rectangle(25, 895, 410, 25))
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2);
            //.InRange(new Bgr(180, 160, 0), new Bgr(230, 205, 100));
            var choosePlayer2 = imgFrame.GetSubRect(new System.Drawing.Rectangle(1475, 895, 410, 25))
                .InRange(ColorsThresHolds.p2Colors.Item1, ColorsThresHolds.p2Colors.Item2);
            //.InRange(new Bgr(5, 190, 100), new Bgr(80, 250, 140));

            var RoundReadyMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(480, 505, 800, 70))
                .InRange(ColorsThresHolds.RoundReadyColors.Item1, ColorsThresHolds.RoundReadyColors.Item2);
            //.InRange(new Bgr(246, 222, 46), new Bgr(255, 255, 210));
            var GoMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(580, 435, 750, 200))
                .InRange(ColorsThresHolds.GoMessageColors.Item1, ColorsThresHolds.RoundReadyColors.Item2);
            //.InRange(new Bgr(100, 240, 240), new Bgr(180, 255, 255));

            var titleCap = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2);
            //.InRange(new Bgr(5, 150, 215), new Bgr(90, 200, 246));

            var winloseP1 = imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatch.Item1, ColorsThresHolds.WinLoseAfterMatch.Item2);
            var winloseP2 = imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatch.Item1, ColorsThresHolds.WinLoseAfterMatch.Item2);

            var p1NameCol = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15));
            var p2NameCol = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15));
            var titleCol = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50));

#if FIGHT
#region Fight Test
            CvInvoke.Imshow(WinCapture, p1Name);
            CvInvoke.Imshow(WinCapture2, p2Name);
            CvInvoke.Imshow(WinRoundMessage, RoundReadyMessage);

            CvInvoke.Imshow(WinCaptureCol, p1NameCol);
            CvInvoke.Imshow(WinCaptureCol2, p2NameCol);
#endregion
#elif CHOOSEPLAYER
#region Player Choose
            CvInvoke.Imshow(WinChoosePlayer2, choosePlayer2);
            CvInvoke.Imshow(WinChoosePlayer, choosePlayer);
            CvInvoke.Imshow(WinTitleCapture, titleCap);
            CvInvoke.Imshow(WinGoMessage, titleCol);
#endregion
#elif TITLE
#region Title Test
            CvInvoke.Imshow(WinTitleCapture, titleCap);
            CvInvoke.Imshow(WinGoMessage, titleCol);
#endregion
            //Task.Delay(1000);
#elif RESULT
#region WinLose After Match
            CvInvoke.Imshow(WinTitleCapture, titleCap);
            CvInvoke.Imshow(WinLoseP1Win, winloseP1);
            CvInvoke.Imshow(WinLoseP2Win, winloseP2);
            #endregion
#endif
            //string stt = System.IO.Path.Combine(svDir, DateTime.Now.ToString("yyyyMMdd_HHmmssffff"));
            //p1Name.ToBitmap().Save(stt + "Maip1.bmp");
            //p2Name.ToBitmap().Save(stt + "Maip2.bmp");
            //RoundReadyMessage.ToBitmap().Save(stt + ".bmp");
            //titleCap.ToBitmap().Save(stt + ".bmp");
        }
    }
}
