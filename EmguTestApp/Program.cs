//#define FIGHT
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
        //static public Tuple<Bgr, Bgr> p1Colors = Tuple.Create(new Bgr(170, 160, 0), new Bgr(230, 225, 100));
        static public Tuple<Gray, Gray> p1Gray = Tuple.Create(new Gray(110), new Gray(255));

        //static public Tuple<Bgr, Bgr> p2Colors = Tuple.Create(new Bgr(0, 190, 95), new Bgr(95, 250, 150));
        static public Tuple<Gray, Gray> p2Gray = Tuple.Create(new Gray(110), new Gray(255));

        //static public Tuple<Bgr, Bgr> TitleColors = Tuple.Create(new Bgr(5, 150, 215), new Bgr(90, 200, 246));
        static public Tuple<Gray, Gray> TitleGray = Tuple.Create(new Gray(157), new Gray(255));

        //static public Tuple<Bgr, Bgr> RoundReadyColors = Tuple.Create(new Bgr(246, 222, 46), new Bgr(255, 255, 210));
        static public Tuple<Bgr, Bgr> GoMessageColors = Tuple.Create(new Bgr(100, 240, 240), new Bgr(180, 255, 255));

        //static public Tuple<Bgr, Bgr> KoMessageColors = Tuple.Create(new Bgr(140, 230, 240), new Bgr(/*205*/190, 255, 255));
        static public Tuple<Gray, Gray> KoMessageGray = Tuple.Create(new Gray(240), new Gray(255));


        //static public Tuple<Bgr, Bgr> WinLoseAfterMatch = Tuple.Create(new Bgr(200, 192, 188), new Bgr(255, 255, 255));
        //static public Tuple<Bgr, Bgr> WinLoseAfterMatchColor = Tuple.Create(new Bgr(0, 0, 0), new Bgr(60, 45, 35));
        static public Tuple<Gray, Gray> WinLoseAfterMatchGray = Tuple.Create(new Gray(160), new Gray(255));

        static public Tuple<Bgr, Bgr> TimeColor = Tuple.Create(new Bgr(180, 160, 149), new Bgr(230, 230, 190));
        static public Tuple<Gray, Gray> TimeGray = Tuple.Create(new Gray(190), new Gray(255));

        static public Tuple<Bgr, Bgr> p1Lives = Tuple.Create(new Bgr(0, 110, 0), new Bgr(156, 244, 145));//Green only
        //static public Tuple<Bgr, Bgr> p1Lives = Tuple.Create(new Bgr(0, 60, 180), new Bgr(156, 246, 255));//Orange only
    }

    class Program
    {
        static string FileToPlay;

        //static string FileToPlay = @"d:\Q4Vid\20170404162850.mp4";

        static string CurrentName = "Common";
        
        static string fileVideoExtension = ".mp4";
        static string fileImageExtension = ".bmp";

        
        static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);
        //static Image<Bgr, byte> BackGr = new Image<Bgr, byte>(@"C:\Users\User\Desktop\rgb\black2.bmp");


        static double TotalFrames;
        //static double currentFrame;
        static DateTime LastUpdateTime;

        static Timer tmr;
        static Capture capture;

        static Dictionary<ImageKind, Image<Gray, byte>> ResultDict = new Dictionary<ImageKind, Image<Gray, byte>>();

#if SHOWREPREDICTRESULT
        static List<PredictModel> PredictList = new List<PredictModel>();
        static List<Tuple<string, string, bool>> PredictedValues = new List<Tuple<string, string, bool>>();
#endif

        static void Main(string[] args)
        {

            ProcessVideo(@"d:\Q4Vid\20170501104655.mp4"
                , ImageKind.Ingame_Player1Name
                , ImageKind.Ingame_Player2Name
                //, ImageKind.OnSelect_Player1Name
                //, ImageKind.OnSelect_Player2Name
                //, ImageKind.RoundReadyMessage
                , ImageKind.PlayerWins
                , ImageKind.KoGoMessage
                //, ImageKind.TitleMenu
                //, ImageKind.Result_Player1
                //, ImageKind.Result_Player2
                , ImageKind.CurrentTime
                );
        }

        static void ProcessVideo(string videoName, params ImageKind[] ImageProps)
        {
#if SHOWREPREDICTRESULT
            foreach (var ing in ImageProps)
            {
                PredictList.Add(new PredictModel(ing, ModelType.SvmModel));
                PredictedValues.Add(new Tuple<string, string, bool>(string.Empty, string.Empty, true));
            }
#endif

            TimerCallback tk = new TimerCallback(TimerCallback);
            tmr = new Timer(tk, null, 1000, 1000);

            FileToPlay = videoName;

            CurrentName = Path.GetFileNameWithoutExtension(videoName);

            //Test main menu
#if TITLE
            #region Title Test
            //FileToPlay = @"d:\Q4Vid\Menus.mp4";
            //FileToPlay = @"d:\Q4Vid\ResultWinLose.mp4";
            FileToPlay = @"d:\Q4Vid\ResultWithChoi.mp4";
            #endregion

#endif
            //Test Players Choose
#if CHOOSEPLAYER
            #region Player Choose
            //FileToPlay = @"d:\Q4Vid\RamonChoose.mp4";
            //FileToPlay = @"d:\Q4Vid\ChoosePlayers.mp4";
            //FileToPlay = @"d:\Q4Vid\ChoosePlayersLong.mp4";
            FileToPlay = @"d:\Q4Vid\ChoosePlayerRandom.mp4";
            #endregion
#endif
            //Test Figth
#if FIGHT
            #region Fight Test
            //FileToPlay = @"d:\Q4Vid\Players\LongVideoWithImages\10sec\Alice.mp4";
            //FileToPlay = @"d:\Q4Vid\Round1Message.mp4";
            //FileToPlay = @"d:\Q4Vid\20170404111842.mp4";
            //FileToPlay = @"d:\Q4Vid\ReadyMessage.mp4";
            FileToPlay = @"d:\Q4Vid\RoundFinalMessage.mp4";
            //FileToPlay = @"d:\Q4Vid\Round2_0.mp4";
            //FileToPlay = @"d:\Q4Vid\Player1Wins_0.mp4";
            //FileToPlay = playersDir + CurrentName + fileVideoExtension;
            #endregion
#endif
            //WinLose
#if RESULT
            #region WinLose After Match
            //FileToPlay = @"d:\Q4Vid\PerfectGame.mp4";
            //FileToPlay = @"d:\Q4Vid\ResultLoseWin.mp4";
            FileToPlay = @"d:\Q4Vid\ResultWinLose.mp4";
            #endregion
#endif
#if KOMESSAGE
            FileToPlay = @"d:\Q4Vid\Players\LongVideoWithImages\10sec\Mai.mp4";
#endif
            foreach (ImageKind img in ImageProps)
            {
                ResultDict.Add(img, null);
            }


            foreach (ImageKind key in ResultDict.Keys)
            {
                CvInvoke.NamedWindow(key.ToString());
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
            #region Definitions
#if FIGHT
#endif
#if CHOOSEPLAYER
#endif
#if TITLE
#endif
#if RESULT
#endif
#if KOMESSAGE
#endif
            #endregion

            foreach (KeyValuePair<ImageKind, Image<Gray, byte>> kvRes in ResultDict.ToList())
            {
                //grab Image
                Image<Gray, byte> image = imgFrame.GetSubRect(new System.Drawing.Rectangle(
                    ImageFormat.ImageParam[kvRes.Key].XPos,
                    ImageFormat.ImageParam[kvRes.Key].YPos,
                    ImageFormat.ImageParam[kvRes.Key].Width,
                    ImageFormat.ImageParam[kvRes.Key].Height))
                .InRange(
                    ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold,
                    ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold)
                .Resize(
                    ImageFormat.ImageParam[kvRes.Key].Scale, Emgu.CV.CvEnum.Inter.Linear);

                //grab Mask
                Image<Gray, byte> mask = imgFrame.GetSubRect(new System.Drawing.Rectangle(
                    ImageFormat.ImageParam[kvRes.Key].XPos,
                    ImageFormat.ImageParam[kvRes.Key].YPos,
                    ImageFormat.ImageParam[kvRes.Key].Width,
                    ImageFormat.ImageParam[kvRes.Key].Height))
                .InRange(
                    ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold,
                    ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold)
                .Resize(
                    ImageFormat.ImageParam[kvRes.Key].Scale, Emgu.CV.CvEnum.Inter.Linear);

                //save it together
                ResultDict[kvRes.Key] = image.Or(mask);
            }
            //Windows Show

            foreach (KeyValuePair<ImageKind, Image<Gray, byte>> kvRes in ResultDict)
            {
                CvInvoke.Imshow(kvRes.Key.ToString(), kvRes.Value);
            }

#if SHOWREPREDICTRESULT

            //Console.WriteLine();
            int ind = 0;

            Task<float>[] taskList = new Task<float>[ResultDict.Count];


            foreach (KeyValuePair<ImageKind, Image<Gray, byte>> kvpair in ResultDict)
            {

                int i = ind;
                taskList[i] = Task<float>.Factory.StartNew(() =>
                {
                    return PredictList[i].PredictImage(kvpair.Value);
                }
                    );
                ind++;
            }
            Task.WaitAll(taskList);

            ind = 0;
            foreach (KeyValuePair<ImageKind, Image<Gray, byte>> kvpair in ResultDict)
            {
                float fl = taskList[ind].Result;

                string val = ImageFormat.ImageParam[kvpair.Key].Dict.Where(x => x.Value.ClassNum == fl).FirstOrDefault().Key;
                if (PredictedValues[ind].Item2 != val)
                    PredictedValues[ind] = new Tuple<string, string, bool>(kvpair.Key.ToString(), val, true);
                else
                    PredictedValues[ind] = new Tuple<string, string, bool>(kvpair.Key.ToString(), val, false);
                //Console.WriteLine($"{kvpair.Key.ToString()} :{val}");
                ind++;
            }
            if (PredictedValues.Any(x => x.Item3 == true))
            {
                Console.Clear();
                for (int l = 0; l < PredictedValues.Count; l++)
                {
                    Console.WriteLine($"{PredictedValues[l].Item1} : {PredictedValues[l].Item2}");
                }
            }
#endif

#if SAVEREQUIRED
            foreach (KeyValuePair<ImageKind, Image<Gray, byte>> kvpair in ResultDict)
            {
                string saveDirPath = Path.Combine(ImageFormat.ImageParam[kvpair.Key].AimPath,kvpair.Key.ToString());

                if (Directory.Exists(saveDirPath) == false)
                    Directory.CreateDirectory(saveDirPath);

                string saveFileFullName = Path.Combine(saveDirPath, CurrentName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmssffff") + fileImageExtension);

                kvpair.Value.ToBitmap().Save(saveFileFullName);
            }
#endif

            foreach (KeyValuePair<ImageKind, Image<Gray, byte>> kvRes in ResultDict)
            {
                kvRes.Value.Dispose();
            }
            //imgFrame.Dispose();
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
