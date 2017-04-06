#define FIGHT
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
        static public Tuple<Gray, Gray> p1Gray = Tuple.Create(new Gray(110), new Gray(255));

        static public Tuple<Bgr, Bgr> p2Colors = Tuple.Create(new Bgr(0, 190, 95), new Bgr(95, 250, 150));
        static public Tuple<Gray, Gray> p2Gray = Tuple.Create(new Gray(110), new Gray(255));

        static public Tuple<Bgr, Bgr> TitleColors = Tuple.Create(new Bgr(5, 150, 215), new Bgr(90, 200, 246));
        static public Tuple<Gray, Gray> TitleGray = Tuple.Create(new Gray(157), new Gray(255));

        static public Tuple<Bgr, Bgr> RoundReadyColors = Tuple.Create(new Bgr(246, 222, 46), new Bgr(255, 255, 210));
        static public Tuple<Bgr, Bgr> GoMessageColors = Tuple.Create(new Bgr(100, 240, 240), new Bgr(180, 255, 255));

        static public Tuple<Bgr, Bgr> KoMessageColors = Tuple.Create(new Bgr(140, 230, 240), new Bgr(/*205*/190, 255, 255));
        static public Tuple<Gray, Gray> KoMessageGray = Tuple.Create(new Gray(240), new Gray(255));


        //static public Tuple<Bgr, Bgr> WinLoseAfterMatch = Tuple.Create(new Bgr(200, 192, 188), new Bgr(255, 255, 255));
        static public Tuple<Bgr, Bgr> WinLoseAfterMatchColor = Tuple.Create(new Bgr(0, 0, 0), new Bgr(60, 45, 35));
        static public Tuple<Gray, Gray> WinLoseAfterMatchGray = Tuple.Create(new Gray(160), new Gray(255));

        static public Tuple<Bgr, Bgr> TimeColor = Tuple.Create(new Bgr(180, 160, 149), new Bgr(230, 230, 190));
        static public Tuple<Gray, Gray> TimeGray = Tuple.Create(new Gray(190), new Gray(255));
    }

    class Program
    {
        static string FileToPlay;

        //static string FileToPlay = @"d:\Q4Vid\20170404162850.mp4";


        static string svDir = @"d:\Q4Vid\Tst\";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");

        static double TotalFrames;

        static List<string> WindowsList;// = new List<string>();

        //static string WinCapture = "Win";
        //static string WinCapture2 = "Win2";
        //static string WinCaptureCol = "WinCol";
        //static string WinCaptureCol2 = "Win2Col";

        //static string WinTitleCapture = "Title";
        //static string WinChoosePlayer = "ChoosePlayer";
        //static string WinChoosePlayer2 = "ChoosePlayer2";
        //static string WinRoundMessage = "RoundMessage";
        //static string WinGoMessage = "GoMessage";

        //static string WinResultP1Color = "ResultP1Color";
        //static string WinResultP2Color = "ResultP2Color";
        //static string WinResultP1ResultGray = "ResultP1Gray";
        //static string WinResultP2Gray = "ResultP2Gray";

        //static string WinTime = "Time";

        static void Main(string[] args)
        {



            //Test Players Choose
#if CHOOSEPLAYER
            #region Player Choose
            FileToPlay = @"d:\Q4Vid\ChoosePlayers.mp4";

            WindowsList = new List<string>()
            {
                "Title",
                "Player1Name",
                "Player2Name",
                "TitleGray"
            };
            WindowsList.ForEach(x => CvInvoke.NamedWindow(x));

            //CvInvoke.NamedWindow(WinTitleCapture);
            //CvInvoke.NamedWindow(WinChoosePlayer);
            //CvInvoke.NamedWindow(WinChoosePlayer2);
            //CvInvoke.NamedWindow(WinGoMessage);

            #endregion
#elif TITLE

            //Test main menu
            #region Title Test
            FileToPlay = @"d:\Q4Vid\Menus.mp4";

            WindowsList = new List<string>() 
            { 
                "TitleColor",
                "TitleGray",
            };
            WindowsList.ForEach(x => CvInvoke.NamedWindow(x));
            //CvInvoke.NamedWindow(WinTitleCapture);
            //CvInvoke.NamedWindow(WinGoMessage);
            #endregion

            //Test Figth
#elif FIGHT
            #region Fight Test
            //FileToPlay = @"d:\Q4Vid\Players\Antonov.mp4";
            FileToPlay = @"d:\Q4Vid\20170404111842.mp4";
            WindowsList = new List<string>()
            {
                "Player1NameColor",
                "Player2NameColor",
                "RoundReadyMessage",
                "Player1NameGray",
                "Player2NameGray",
                "Time"
            };
            WindowsList.ForEach(x => CvInvoke.NamedWindow(x));

            //CvInvoke.NamedWindow(WinCapture);
            //CvInvoke.NamedWindow(WinCapture2);
            //CvInvoke.NamedWindow(WinRoundMessage);
            
            //CvInvoke.NamedWindow(WinCaptureCol);
            //CvInvoke.NamedWindow(WinCaptureCol2);
            //CvInvoke.NamedWindow(WinTime);
            #endregion
            //WinLose
#elif RESULT
            #region WinLose After Match
            FileToPlay = @"d:\Q4Vid\PerfectGame.mp4";

            WindowsList = new List<string>()
            { 
                "Title",
                "Result1Color",
                "Result2Color",
                "Result1Gray",
                "Result2Gray",
            };
            WindowsList.ForEach(x => CvInvoke.NamedWindow(x));

            //CvInvoke.NamedWindow(WinTitleCapture);
            //CvInvoke.NamedWindow(WinResultP1Color);
            //CvInvoke.NamedWindow(WinResultP2Color);
            //CvInvoke.NamedWindow(WinResultP1ResultGray);
            //CvInvoke.NamedWindow(WinResultP2Gray);
            #endregion
#elif KOMESSAGE
            FileToPlay = @"d:\Q4Vid\Players\Mai.mp4";
            WindowsList = new List<string>() 
            { 
                "Ko Message Color",
                "Ko Message Gray",
            };
            CvInvoke.NamedWindow(WindowsList[0]);
            CvInvoke.NamedWindow(WindowsList[1]);
#endif
            var capture = new Capture(FileToPlay);
            TotalFrames = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);

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
            var p1NameColor = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15))
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2);
            //.InRange(new Bgr(170, 160, 0), new Bgr(230, 225, 100));
            var p2NameColor = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15))
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
                .InRange(ColorsThresHolds.GoMessageColors.Item1, ColorsThresHolds.GoMessageColors.Item2);
            //.InRange(new Bgr(100, 240, 240), new Bgr(180, 255, 255));

            var titleColor = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2);
            //.InRange(new Bgr(5, 150, 215), new Bgr(90, 200, 246));

            var ResultP1Color = imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatchColor.Item1, ColorsThresHolds.WinLoseAfterMatchColor.Item2);
            var ResultP1Gray = imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65)).Convert<Gray, byte>()
                .InRange(ColorsThresHolds.WinLoseAfterMatchGray.Item1, ColorsThresHolds.WinLoseAfterMatchGray.Item2);

            var ResultP2Color = imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatchColor.Item1, ColorsThresHolds.WinLoseAfterMatchColor.Item2);
            var ResultP2Gray = imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65)).Convert<Gray, byte>()
                .InRange(ColorsThresHolds.WinLoseAfterMatchGray.Item1, ColorsThresHolds.WinLoseAfterMatchGray.Item2);

            var KoMessageGray = imgFrame.GetSubRect(new System.Drawing.Rectangle(515, 435, 885, 210)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.KoMessageGray.Item1, ColorsThresHolds.KoMessageGray.Item2);

            var KoMessageColor = imgFrame.GetSubRect(new System.Drawing.Rectangle(515, 435, 885, 210))
                .InRange(ColorsThresHolds.KoMessageColors.Item1, ColorsThresHolds.KoMessageColors.Item2);
            var timeMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(870, 50, 180, 80)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TimeGray.Item1, ColorsThresHolds.TimeGray.Item2);
            //.InRange(ColorsThresHolds.Time.Item1, ColorsThresHolds.Time.Item2);


            var p1NameGray = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.p1Gray.Item1, ColorsThresHolds.p1Gray.Item2);
            var p2NameGray = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.p2Gray.Item1, ColorsThresHolds.p2Gray.Item2);
            var titleGray = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TitleGray.Item1, ColorsThresHolds.TitleGray.Item2);

#if FIGHT
            #region Fight Test
            CvInvoke.Imshow(WindowsList[0], p1NameColor);
            CvInvoke.Imshow(WindowsList[1], p2NameColor);
            CvInvoke.Imshow(WindowsList[2], RoundReadyMessage);

            CvInvoke.Imshow(WindowsList[3], p1NameGray);
            CvInvoke.Imshow(WindowsList[4], p2NameGray);
            CvInvoke.Imshow(WindowsList[5], timeMessage);
            #endregion
#elif CHOOSEPLAYER
            #region Player Choose
            CvInvoke.Imshow(WindowsList[2], choosePlayer2);
            CvInvoke.Imshow(WindowsList[1], choosePlayer);
            CvInvoke.Imshow(WindowsList[0], titleColor);
            CvInvoke.Imshow(WindowsList[3], titleGray);
            #endregion
#elif TITLE
            #region Title Test
            CvInvoke.Imshow(WindowsList[0], titleColor);
            CvInvoke.Imshow(WindowsList[1], titleGray);
            #endregion
            //Task.Delay(1000);
#elif RESULT
            #region WinLose After Match
            CvInvoke.Imshow(WindowsList[0], titleColor);
            CvInvoke.Imshow(WindowsList[1], ResultP1Color);
            CvInvoke.Imshow(WindowsList[2], ResultP2Color);
            CvInvoke.Imshow(WindowsList[3], ResultP1Gray);
            CvInvoke.Imshow(WindowsList[4], ResultP2Gray);
            #endregion
#elif KOMESSAGE
            CvInvoke.Imshow(WindowsList[0], KoMessageColor);
            CvInvoke.Imshow(WindowsList[1], KoMessageGray);
#endif
            //string stt = System.IO.Path.Combine(svDir, DateTime.Now.ToString("yyyyMMdd_HHmmssffff"));
            //p1Name.ToBitmap().Save(stt + "Maip1.bmp");
            //p2Name.ToBitmap().Save(stt + "Maip2.bmp");
            //RoundReadyMessage.ToBitmap().Save(stt + ".bmp");
            //titleCap.ToBitmap().Save(stt + ".bmp");
        }
    }
}
