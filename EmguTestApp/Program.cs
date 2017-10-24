﻿//#define SAVEREQUIRED
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
using EmguLearnPredict;
//using EmguLearnPredict_Tekken7;
using EmguLearnPredict_KOFXIV;


namespace EmguTestApp
{
    class Program
    {
        static string FileToPlay;
        static string CurrentName = "Common";
        static string fileImageExtension = ".bmp";
        public static readonly int FullHDWigth = 1920;
        public static readonly int FullHDHeight = 1080;
        //static Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);

        static double TotalFrames;
        static double currentFrame = 0;
        static Capture capture;

        static Dictionary<ImageType, Image<Gray, byte>> ResultDict = new Dictionary<ImageType, Image<Gray, byte>>();

#if SHOWREPREDICTRESULT
        static Dictionary<ImageType, PredictModel> PredictList = new Dictionary<ImageType, PredictModel>();
        static Dictionary<ImageType, Tuple<string, bool>> PredictedValues = new Dictionary<ImageType, Tuple<string, bool>>();
#endif

        static void Main(string[] args)
        {

            ProcessVideo(@"d:\TestVid\KOFXIV\NotEndedRound\20170923181419.mp4"
                , ImageType.Ingame_Player1Name
                , ImageType.Ingame_Player2Name
                //, ImageType.OnSelect_Player1Name
                //, ImageType.OnSelect_Player2Name
                //, ImageType.RoundReadyMessage
                //, ImageType.KoGoMessage
                //, ImageType.CurrentTime
                , ImageType.TitleMenu
                //, ImageType.Result_Player1
                //, ImageType.Result_Player2
                //Tekken
                //, ImageType.PlayerPause
                //, ImageType.ResumeGame
                //, ImageType.GameMessages
                );
        }

        static void ProcessVideo(string videoName, params ImageType[] ImageProps)
        {
#if SHOWREPREDICTRESULT
            foreach (var ing in ImageProps)
            {
                PredictList.Add(ing, new PredictModel(ing, ModelType.SvmModel));
                PredictedValues.Add(ing, new Tuple<string, bool>(string.Empty, true));
            }
#endif
            FileToPlay = videoName;

            CurrentName = Path.GetFileNameWithoutExtension(videoName);

            foreach (ImageType img in ImageProps)
            {
                ResultDict.Add(img, null);
            }

            CvInvoke.NamedWindow("BigScreen");
            capture = new Capture(FileToPlay);
            TotalFrames = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
            Console.WriteLine("total frames:{0}", TotalFrames);

            //capture.ImageGrabbed += Capture_ImageGrabbed;
            //capture.Start();
            //CvInvoke.WaitKey(0);
            while (capture.Grab() == true)
            {
                Capture_ImageGrabbed();
            }

            CvInvoke.DestroyAllWindows();
            capture.Dispose();
        }

        //private static void Capture_ImageGrabbed(object sender, EventArgs e)
        private static void Capture_ImageGrabbed()
        {
            Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(FullHDWigth, FullHDHeight);
            try
            {
                Capture cp = capture;

                bool retVal = cp.Retrieve(imgFrame);
                if (retVal == false)
                    throw new Exception("nothing Retrieve");

                foreach (KeyValuePair<ImageType, Image<Gray, byte>> kvRes in ResultDict.ToList())
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
                #region Show Window
                foreach (KeyValuePair<ImageType, Image<Gray, byte>> kvRes in ResultDict)
                {
                    //CvInvoke.Imshow(kvRes.Key.ToString(), kvRes.Value);
                    double backConvertValue = 1 / ImageFormat.ImageParam[kvRes.Key].Scale;
                    kvRes.Value.Resize(backConvertValue, Emgu.CV.CvEnum.Inter.Linear).Convert<Bgr, byte>().CopyTo(imgFrame.GetSubRect(new System.Drawing.Rectangle(ImageFormat.ImageParam[kvRes.Key].XPos
                                                                                                            , ImageFormat.ImageParam[kvRes.Key].YPos
                                                                                                            , ImageFormat.ImageParam[kvRes.Key].Width
                                                                                                            , ImageFormat.ImageParam[kvRes.Key].Height)));
                }
                CvInvoke.Imshow("BigScreen", imgFrame.Resize(0.5, Emgu.CV.CvEnum.Inter.Linear));
                CvInvoke.WaitKey(1);
                #endregion

#if SHOWREPREDICTRESULT
                Dictionary<ImageType, Task<float>> taskList = new Dictionary<ImageType, Task<float>>();


                foreach (KeyValuePair<ImageType, Image<Gray, byte>> kvpair in ResultDict)
                {
                    taskList[kvpair.Key] = Task<float>.Factory.StartNew(() =>
                    {
                        return PredictList[kvpair.Key].PredictImage(kvpair.Value);
                    }
                        );
                }
                Task.WaitAll(taskList.Values.ToArray());

                foreach (KeyValuePair<ImageType, Image<Gray, byte>> kvpair in ResultDict)
                {
                    float fl = taskList[kvpair.Key].Result;

                    string val = ImageFormat.ImageParam[kvpair.Key].Dict.Where(x => x.Value.ClassNum == fl).FirstOrDefault().Key;

                    if (PredictedValues[kvpair.Key].Item1 != val)
                        PredictedValues[kvpair.Key] = new Tuple<string, bool>(val, true);
                    else
                        PredictedValues[kvpair.Key] = new Tuple<string, bool>(val, false);
                }
                if (PredictedValues.Any(x => x.Value.Item2 == true))
                {
                    foreach (KeyValuePair<ImageType, Tuple<string, bool>> kvpair in PredictedValues)
                    {
                        Console.WriteLine($"{currentFrame} - {kvpair.Key.ToString()} : {PredictedValues[kvpair.Key].Item1}");
                    }
                }
#endif

#if SAVEREQUIRED
            foreach (KeyValuePair<ImageType, Image<Gray, byte>> kvpair in ResultDict)
            {
                string saveDirPath = Path.Combine(ImageFormat.ImageParam[kvpair.Key].AimPath, kvpair.Key.ToString());

                if (Directory.Exists(saveDirPath) == false)
                    Directory.CreateDirectory(saveDirPath);

                string saveFileFullName = Path.Combine(saveDirPath, CurrentName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmssffff_") + currentFrame + fileImageExtension);

                kvpair.Value.ToBitmap().Save(saveFileFullName);
            }
#endif

                foreach (KeyValuePair<ImageType, Image<Gray, byte>> kvRes in ResultDict)
                {
                    kvRes.Value.Dispose();
                }
                //imgFrame.Dispose();
                currentFrame++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
