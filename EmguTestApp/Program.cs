#define FIGHT
//#define CHOOSEPLAYER
//#define RESULT
//#define KOMESSAGE
//#define TITLE

//#define SAVEREQUIRED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Threading;

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

        //static public Tuple<Bgr, Bgr> p1Lives = Tuple.Create(new Bgr(0, 110, 0), new Bgr(156, 244, 145));//Green only
        static public Tuple<Bgr, Bgr> p1Lives = Tuple.Create(new Bgr(0, 60, 180), new Bgr(156, 246, 255));//Orange only
    }

    class Program
    {
        static string FileToPlay;

        //static string FileToPlay = @"d:\Q4Vid\20170404162850.mp4";


        static string svDir = @"d:\Q4Vid\Tst\";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");

        static double TotalFrames;
        //static double currentFrame;
        static DateTime LastUpdateTime;

        static Timer tmr;
        static Capture capture;

        static List<string> WindowsList;

        static List<Image<Gray, byte>> ImagesGrayTransformationList = new List<Image<Gray, byte>>();
        static List<Image<Gray, byte>> ImagesColorTransformationList = new List<Image<Gray, byte>>();

        static void Main(string[] args)
        {

            TimerCallback tk = new TimerCallback(TimerCallback);
            tmr = new Timer(tk, null, 1000, 1000);

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
            #endregion

            //Test Figth
#elif FIGHT
            #region Fight Test
            //FileToPlay = @"d:\Q4Vid\Players\Hein.mp4";
            //FileToPlay = @"d:\Q4Vid\RoundReadyMessage.mp4";
            FileToPlay = @"d:\Q4Vid\20170404111842.mp4";
            WindowsList = new List<string>()
            {
                "Player1NameColor",
                "Player2NameColor",
                "RoundReadyMessage",
                "Player1NameGray",
                "Player2NameGray",
                "Time",
                "p1Lives"
            };
            WindowsList.ForEach(x => CvInvoke.NamedWindow(x));

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
            capture = new Capture(FileToPlay);
            TotalFrames = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
            Console.WriteLine("total frames:{0}", TotalFrames);

            capture.ImageGrabbed += Capture_ImageGrabbed;
            capture.Start();
            CvInvoke.WaitKey(0);

            CvInvoke.DestroyAllWindows();
            imgFrame.Dispose();
            capture.Dispose();
        }

        private static void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            //lock (locker)
            //{
                LastUpdateTime = DateTime.Now;
            //}
            Capture cp = sender as Capture;

            cp.Retrieve(imgFrame);
#if FIGHT
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15))
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2));
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15))
                .InRange(ColorsThresHolds.p2Colors.Item1, ColorsThresHolds.p2Colors.Item2));
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(480, 505, 800, 70))
                .InRange(ColorsThresHolds.RoundReadyColors.Item1, ColorsThresHolds.RoundReadyColors.Item2));

            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new Rectangle(116, 77, 770, 15))
                .InRange(ColorsThresHolds.p1Lives.Item1, ColorsThresHolds.p1Lives.Item2));

            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.p1Gray.Item1, ColorsThresHolds.p1Gray.Item2));
            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.p2Gray.Item1, ColorsThresHolds.p2Gray.Item2));
            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(870, 50, 180, 80)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TimeGray.Item1, ColorsThresHolds.TimeGray.Item2));
#endif
#if CHOOSEPLAYER
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(25, 895, 410, 25))
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2));
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(1475, 895, 410, 25))
                .InRange(ColorsThresHolds.p2Colors.Item1, ColorsThresHolds.p2Colors.Item2));
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2));

            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TitleGray.Item1, ColorsThresHolds.TitleGray.Item2));
#endif
#if TITLE
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2));

            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TitleGray.Item1, ColorsThresHolds.TitleGray.Item2));
#endif

#if RESULT
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2));
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatchColor.Item1, ColorsThresHolds.WinLoseAfterMatchColor.Item2));//ResultP1Color
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatchColor.Item1, ColorsThresHolds.WinLoseAfterMatchColor.Item2));//ResultP2Color

            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65)).Convert<Gray, byte>()
                .InRange(ColorsThresHolds.WinLoseAfterMatchGray.Item1, ColorsThresHolds.WinLoseAfterMatchGray.Item2));//ResultP1Gray
            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65)).Convert<Gray, byte>()
                .InRange(ColorsThresHolds.WinLoseAfterMatchGray.Item1, ColorsThresHolds.WinLoseAfterMatchGray.Item2));

#endif

            var GoMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(580, 435, 750, 200))
                .InRange(ColorsThresHolds.GoMessageColors.Item1, ColorsThresHolds.GoMessageColors.Item2);
#if KOMESSAGE
            ImagesColorTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(515, 435, 885, 210))
                .InRange(ColorsThresHolds.KoMessageColors.Item1, ColorsThresHolds.KoMessageColors.Item2));

            ImagesGrayTransformationList.Add(imgFrame.GetSubRect(new System.Drawing.Rectangle(515, 435, 885, 210)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.KoMessageGray.Item1, ColorsThresHolds.KoMessageGray.Item2));
#endif


#if FIGHT
            #region Fight Test
            CvInvoke.Imshow(WindowsList[0], ImagesColorTransformationList[0]);
            CvInvoke.Imshow(WindowsList[1], ImagesColorTransformationList[1]);
            CvInvoke.Imshow(WindowsList[2], ImagesColorTransformationList[2]);
            CvInvoke.Imshow(WindowsList[6], ImagesColorTransformationList[3]);

            CvInvoke.Imshow(WindowsList[3], ImagesGrayTransformationList[0]);
            CvInvoke.Imshow(WindowsList[4], ImagesGrayTransformationList[1]);
            CvInvoke.Imshow(WindowsList[5], ImagesGrayTransformationList[2]);
            #endregion
#elif CHOOSEPLAYER
            #region Player Choose
            CvInvoke.Imshow(WindowsList[2], ImagesColorTransformationList[1]);
            CvInvoke.Imshow(WindowsList[1], ImagesColorTransformationList[0]);
            CvInvoke.Imshow(WindowsList[0], ImagesColorTransformationList[2]);
            CvInvoke.Imshow(WindowsList[3], ImagesGrayTransformationList[0]);
            #endregion
#elif TITLE
            #region Title Test
            CvInvoke.Imshow(WindowsList[0], ImagesColorTransformationList[0]);
            CvInvoke.Imshow(WindowsList[1], ImagesGrayTransformationList[0]);
            #endregion
            //Task.Delay(1000);
#elif RESULT
            #region WinLose After Match
            CvInvoke.Imshow(WindowsList[0], ImagesColorTransformationList[0]);
            CvInvoke.Imshow(WindowsList[1], ImagesColorTransformationList[1]);
            CvInvoke.Imshow(WindowsList[2], ImagesColorTransformationList[2]);
            CvInvoke.Imshow(WindowsList[3], ImagesGrayTransformationList[0]);
            CvInvoke.Imshow(WindowsList[4], ImagesGrayTransformationList[1]);
            #endregion
#elif KOMESSAGE
            CvInvoke.Imshow(WindowsList[0], ImagesColorTransformationList[0]);
            CvInvoke.Imshow(WindowsList[1], ImagesGrayTransformationList[0]);
#endif
#if SAVEREQUIRED
            string stt = System.IO.Path.Combine(svDir, DateTime.Now.ToString("yyyyMMdd_HHmmssffff"));
            p1NameColor.ToBitmap().Save(stt + "Hein1c.bmp");
            p1NameGray.ToBitmap().Save(stt + "Hein1g.bmp");
            p2NameColor.ToBitmap().Save(stt + "Hein2c.bmp");
            p2NameGray.ToBitmap().Save(stt + "Hein2g.bmp");
            //RoundReadyMessage.ToBitmap().Save(stt + ".bmp");
            //titleCap.ToBitmap().Save(stt + ".bmp");
#endif
            ImagesGrayTransformationList.Clear();
            ImagesColorTransformationList.Clear();
            //Console.WriteLine($"current frame:{currentFrame++}");
        }

        public static void TimerCallback(object obj)
        {
            DateTime dt = DateTime.Now;
            if ((dt - LastUpdateTime)>TimeSpan.FromSeconds(2))
            {
                Console.WriteLine("Stoping capture");
                capture.Stop();
                
                //capture.Dispose();
            }
        }
    }
}
