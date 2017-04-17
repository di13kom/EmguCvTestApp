#define FIGHT
//#define CHOOSEPLAYER
//#define RESULT
//#define KOMESSAGE
//#define TITLE

//#define SAVEREQUIRED
#define SHOWREPREDICTRESULT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Threading;
using System.IO;
using EmguTestMachineLearningWithImages;

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

        static string CurrentName = "Common";
        static string playersDir = @"d:\Q4Vid\Players\";
        static string pl1Subdir = @"Images\Player1\";
        static string pl2Subdir = @"Images\Player2\";
        static string fileVideoExtension = ".mp4";
        static string fileImageExtension = ".bmp";

        static string svDir = @"d:\Q4Vid\Tst\";
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");

        static double TotalFrames;
        //static double currentFrame;
        static DateTime LastUpdateTime;

        static Timer tmr;
        static Capture capture;

        static Dictionary<string, Image<Gray, byte>> ResultDict = null;

#if SHOWREPREDICTRESULT
        static IngamePlayersTest predictClass;
#endif

        static void Main(string[] args)
        {
#if SHOWREPREDICTRESULT
            predictClass = new IngamePlayersTest(@"d:\Q4Vid\Players\Images\Player1\KN_mlp_model.xml", @"d:\Q4Vid\Players\Images\Player2\KN_mlp_model.xml", ModelTypes.KnModel);
            //predictClass = new IngamePlayersTest(@"d:\Q4Vid\Players\Images\Player1\SVM_mlp_model.xml", @"d:\Q4Vid\Players\Images\Player2\SVM_mlp_model.xml", ModelTypes.SvmModel);
#endif

            TimerCallback tk = new TimerCallback(TimerCallback);
            tmr = new Timer(tk, null, 1000, 1000);

            //Test Players Choose
#if CHOOSEPLAYER
            #region Player Choose
            //FileToPlay = @"d:\Q4Vid\RamonChoose.mp4";
            FileToPlay = @"d:\Q4Vid\ChoosePlayers.mp4";
            playersDir = @"d:\Q4Vid\ChoosePlayers\";

            ResultDict = new Dictionary<string, Image<Gray, byte>>
            {
                { "Title",null },
                {"Player1NameColor",null },
                {"Player2NameColor",null },
                {"TitleGray",null },
            };

            #endregion
#elif TITLE

            //Test main menu
            #region Title Test
            FileToPlay = @"d:\Q4Vid\Menus.mp4";

            ResultDict = new Dictionary<string, Image<Gray, byte>>
            {
                { "TitleColor",null },
                { "TitleGray",null },
            };
            #endregion

            //Test Figth
#elif FIGHT
            #region Fight Test
            //FileToPlay = @"d:\Q4Vid\Players\LongVideoWithImages\10sec\Alice.mp4";
            //FileToPlay = @"d:\Q4Vid\RoundReadyMessage.mp4";
            //FileToPlay = @"d:\Q4Vid\20170404111842.mp4";
            FileToPlay = @"d:\Q4Vid\20170331153923.mp4";
            //FileToPlay = playersDir + CurrentName + fileVideoExtension;

            ResultDict = new Dictionary<string, Image<Gray, byte>>
            {
                { "Player1NameColor",null },
                {"Player2NameColor",null },
                {"RoundReadyMessage",null },
                {"Player1NameGray",null },
                {"Player2NameGray",null },
                {"Time",null },
                {"p1Lives",null },
            };

            #endregion
            //WinLose
#elif RESULT
            #region WinLose After Match
            FileToPlay = @"d:\Q4Vid\PerfectGame.mp4";

            ResultDict = new Dictionary<string, Image<Gray, byte>>
            {
                {"Title",null },
                {"Result1Color",null },
                {"Result2Color",null },
                {"Result1Gray",null },
                {"Result2Gray",null },
            };

            #endregion
#elif KOMESSAGE
            FileToPlay = @"d:\Q4Vid\Players\10sec\Mai.mp4";

            ResultDict = new Dictionary<string, Image<Gray, byte>>
            {
                { "Ko Message Color",null },
                { "Ko Message Gray",null },
            };
#endif

            foreach (string key in ResultDict.Keys)
            {
                CvInvoke.NamedWindow(key);
            }
            //
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
            ResultDict["Player1NameColor"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15))
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2);
            ResultDict["Player2NameColor"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15))
                .InRange(ColorsThresHolds.p2Colors.Item1, ColorsThresHolds.p2Colors.Item2);
            ResultDict["RoundReadyMessage"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(480, 505, 800, 70))
                .InRange(ColorsThresHolds.RoundReadyColors.Item1, ColorsThresHolds.RoundReadyColors.Item2);
            ResultDict["p1Lives"] = imgFrame.GetSubRect(new Rectangle(116, 77, 770, 15))
                .InRange(ColorsThresHolds.p1Lives.Item1, ColorsThresHolds.p1Lives.Item2);
            ResultDict["Player1NameGray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(35, 110, 125, 15)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.p1Gray.Item1, ColorsThresHolds.p1Gray.Item2);
            ResultDict["Player2NameGray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(1760, 110, 125, 15)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.p2Gray.Item1, ColorsThresHolds.p2Gray.Item2);
            ResultDict["Time"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(870, 50, 180, 80)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TimeGray.Item1, ColorsThresHolds.TimeGray.Item2);

#endif
#if CHOOSEPLAYER
            
            ResultDict["Title"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2).Resize(0.5, Emgu.CV.CvEnum.Inter.Linear);
            ResultDict["TitleGray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TitleGray.Item1, ColorsThresHolds.TitleGray.Item2).Resize(0.5, Emgu.CV.CvEnum.Inter.Linear);
            ResultDict["Player1NameColor"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(25, 895, 410, 25))
                .InRange(ColorsThresHolds.p1Colors.Item1, ColorsThresHolds.p1Colors.Item2).Resize(0.5, Emgu.CV.CvEnum.Inter.Linear);
            ResultDict["Player2NameColor"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(1475, 895, 410, 25))
                .InRange(ColorsThresHolds.p2Colors.Item1, ColorsThresHolds.p2Colors.Item2).Resize(0.5, Emgu.CV.CvEnum.Inter.Linear);

#endif
#if TITLE
            ResultDict["TitleColor"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2);

            ResultDict["TitleGray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.TitleGray.Item1, ColorsThresHolds.TitleGray.Item2);
#endif

#if RESULT
            ResultDict["Title"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(100, 30, 550, 50))
                .InRange(ColorsThresHolds.TitleColors.Item1, ColorsThresHolds.TitleColors.Item2);
            ResultDict["Result1Color"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatchColor.Item1, ColorsThresHolds.WinLoseAfterMatchColor.Item2);//ResultP1Color
            ResultDict["Result2Color"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65))
                .InRange(ColorsThresHolds.WinLoseAfterMatchColor.Item1, ColorsThresHolds.WinLoseAfterMatchColor.Item2);//ResultP2Color
            ResultDict["Result1Gray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(15, 760, 415, 65)).Convert<Gray, byte>()
                .InRange(ColorsThresHolds.WinLoseAfterMatchGray.Item1, ColorsThresHolds.WinLoseAfterMatchGray.Item2);
            ResultDict["Result2Gray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(1445, 760, 415, 65)).Convert<Gray, byte>()
                .InRange(ColorsThresHolds.WinLoseAfterMatchGray.Item1, ColorsThresHolds.WinLoseAfterMatchGray.Item2);
#endif

            var GoMessage = imgFrame.GetSubRect(new System.Drawing.Rectangle(580, 435, 750, 200))
                .InRange(ColorsThresHolds.GoMessageColors.Item1, ColorsThresHolds.GoMessageColors.Item2);
#if KOMESSAGE
            ResultDict["Ko Message Color"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(515, 435, 885, 210))
                .InRange(ColorsThresHolds.KoMessageColors.Item1, ColorsThresHolds.KoMessageColors.Item2);
            ResultDict["Ko Message Gray"] = imgFrame.GetSubRect(new System.Drawing.Rectangle(515, 435, 885, 210)).Convert<Gray, byte>()
                .ThresholdBinary(ColorsThresHolds.KoMessageGray.Item1, ColorsThresHolds.KoMessageGray.Item2);
#endif

            //Windows Show

            foreach (KeyValuePair<string, Image<Gray, byte>> kvRes in ResultDict)
            {
                CvInvoke.Imshow(kvRes.Key, kvRes.Value);
            }

#if (FIGHT||CHOOSEPLAYER)
#if SAVEREQUIRED

            string p1DirFullPath = playersDir + pl1Subdir + CurrentName;
            string p2DirFullPath = playersDir + pl2Subdir + CurrentName;

            if (Directory.Exists(p1DirFullPath) == false)
                Directory.CreateDirectory(p1DirFullPath);
            if (Directory.Exists(p2DirFullPath) == false)
                Directory.CreateDirectory(p2DirFullPath);

            string FileFullNameP1 = p1DirFullPath + "\\" + CurrentName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmssffff") + fileImageExtension;
            string FileFullNameP2 = p2DirFullPath + "\\" + CurrentName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmssffff") + fileImageExtension;

            ResultDict["Player1NameColor"].ToBitmap().Save(FileFullNameP1);
            ResultDict["Player2NameColor"].ToBitmap().Save(FileFullNameP2);

#endif
#if SHOWREPREDICTRESULT
            float[] predVal = predictClass.PredictImage(ResultDict["Player1NameColor"], ResultDict["Player2NameColor"]);
            Console.WriteLine("player1: {0}, player2: {1}"
                , PlayersEnum.Players.Where(x => x.Value.ClassNum == predVal[0]).FirstOrDefault().Key
                , PlayersEnum.Players.Where(x => x.Value.ClassNum == predVal[1]).FirstOrDefault().Key);
#endif
#endif
            //ImagesGrayTransformationList.Clear();
            //ImagesColorTransformationList.Clear();
            //Console.WriteLine($"current frame:{currentFrame++}");
        }

        public static void TimerCallback(object obj)
        {
            DateTime dt = DateTime.Now;
            if ((dt - LastUpdateTime) > TimeSpan.FromSeconds(2))
            {
                //Console.WriteLine("Stoping capture");
                capture.Stop();

                Environment.Exit(1);
                //capture.Dispose();
            }
        }

        public static bool CompareImagesByPixel(Image<Gray, byte> img1, Image<Gray, byte> img2)
        {
            bool retValue = false;
            if (img1.Width == img2.Width || img1.Height == img2.Height)
            {
                for (int i = 0; i < img1.Height; i++)
                {
                    for (int j = 0; j < img1.Width; j++)
                    {
                        if (img1.Data[i, j, 0] != img2.Data[i, j, 0])
                            return retValue;
                    }
                }
                retValue = true;
            }
            return retValue;
        }
    }
}
