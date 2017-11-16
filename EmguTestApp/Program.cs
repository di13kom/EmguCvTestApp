#define SAVEREQUIRED
//#define SHOWREPREDICTRESULT

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
using Emgu.CV.CvEnum;
using System.Drawing.Drawing2D;

namespace EmguTestApp
{
    class Program
    {
        static string FileToPlay;
        static string CurrentName = "Common";
        static readonly int FullHDWigth = 1920;
        public static readonly int FullHDHeight = 1080;
        private static string fileSaveExtension= ".bmp";

        static double TotalFrames;

        static VideoCapture capture;

        static Dictionary<ImageType, Mat> ResultDict = new Dictionary<ImageType, Mat>();

#if SHOWREPREDICTRESULT
        static Dictionary<ImageType, PredictModel> PredictList = new Dictionary<ImageType, PredictModel>();
        static Dictionary<ImageType, Tuple<string, bool>> PredictedValues = new Dictionary<ImageType, Tuple<string, bool>>();
#endif

        static void Main(string[] args)
        {

            ProcessVideo(@"d:\TestVid\KOFXIV\20171113080411.mp4"
                //, ImageType.Ingame_Player1Name
                //, ImageType.Ingame_Player2Name
                //, ImageType.OnSelect_Player1Name
                //, ImageType.OnSelect_Player2Name
                //, ImageType.RoundReadyMessage
                , ImageType.KoGoMessage
                //, ImageType.CurrentTime
                //, ImageType.TitleMenu
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
            capture = new VideoCapture(FileToPlay);
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
            try
            {
                using (Mat imgFrame = new Mat())
                {
                    VideoCapture cp = capture;

                    bool retVal = cp.Retrieve(imgFrame);
                    if (retVal == false)
                        throw new Exception("nothing Retrieve");
                    double currentFrame = cp.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames);
                    #region Prepare SubImages
                    foreach (var kvRes in ResultDict.ToList())
                    {
                        //grab Image
                        Mat newImage = new Mat(imgFrame, new System.Drawing.Rectangle(
                            ImageFormat.ImageParam[kvRes.Key].XPos,
                            ImageFormat.ImageParam[kvRes.Key].YPos,
                            ImageFormat.ImageParam[kvRes.Key].Width,
                            ImageFormat.ImageParam[kvRes.Key].Height));

                        CvInvoke.InRange(newImage,
                                            new ScalarArray(new MCvScalar(
                                                                        ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold.Blue,
                                                                        ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold.Green,
                                                                        ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold.Red)),
                                            new ScalarArray(new MCvScalar(
                                                                        ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold.Blue,
                                                                        ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold.Green,
                                                                        ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold.Red)),
                                            newImage);


                        //grab Mask if differ any of thresholds
                        if (ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold.Red != ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold.Red
                            || ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold.Green != ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold.Green
                            || ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold.Blue != ImageFormat.ImageParam[kvRes.Key].ColorLowerThreshold.Blue
                            //HigherThreshold
                            || ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold.Red != ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold.Red
                            || ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold.Green != ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold.Green
                            || ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold.Blue != ImageFormat.ImageParam[kvRes.Key].ColorHigherThreshold.Blue)
                        {
                            Mat maskU = new Mat(imgFrame, new System.Drawing.Rectangle(
                            ImageFormat.ImageParam[kvRes.Key].XPos,
                            ImageFormat.ImageParam[kvRes.Key].YPos,
                            ImageFormat.ImageParam[kvRes.Key].Width,
                            ImageFormat.ImageParam[kvRes.Key].Height));

                            CvInvoke.InRange(maskU,
                                            new ScalarArray(new MCvScalar(
                                                                        ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold.Red,
                                                                        ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold.Green,
                                                                        ImageFormat.ImageParam[kvRes.Key].MaskLowerThreshold.Blue)),
                                            new ScalarArray(new MCvScalar(
                                                                        ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold.Red,
                                                                        ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold.Green,
                                                                        ImageFormat.ImageParam[kvRes.Key].MaskHigherThreshold.Blue)),
                                            maskU);

                            CvInvoke.BitwiseOr(newImage, maskU, newImage);
                        }
                        CvInvoke.CvtColor(newImage, newImage, ColorConversion.Gray2Bgr);
                        //save it together
                        ResultDict[kvRes.Key] = newImage;
                    }
                    #endregion

                    #region Show Window
                    foreach (var kvRes in ResultDict)
                    {

                        kvRes.Value.CopyTo(new Mat(imgFrame, new Rectangle(ImageFormat.ImageParam[kvRes.Key].XPos,
                           ImageFormat.ImageParam[kvRes.Key].YPos, ImageFormat.ImageParam[kvRes.Key].Width, ImageFormat.ImageParam[kvRes.Key].Height)));

                        //kvRes.Value.Resize(backConvertValue, Emgu.CV.CvEnum.Inter.Linear).Convert<Bgr, byte>().CopyTo(imgFrame.GetSubRect(new System.Drawing.Rectangle(ImageFormat.ImageParam[kvRes.Key].XPos
                        //                                                                                        , ImageFormat.ImageParam[kvRes.Key].YPos
                        //                                                                                        , ImageFormat.ImageParam[kvRes.Key].Width
                        //                                                                                        , ImageFormat.ImageParam[kvRes.Key].Height)));
                    }
                    //CvInvoke.Imshow("BigScreen", imgFrame.Resize(0.5, Emgu.CV.CvEnum.Inter.Linear));
                    CvInvoke.Resize(imgFrame, imgFrame, new Size(imgFrame.Cols / 2, imgFrame.Rows / 2));
                    CvInvoke.Imshow("BigScreen", imgFrame);
                    //CvInvoke.Imshow("BigScreen", ResultDict.First().Value);

                    CvInvoke.WaitKey(1);
                    #endregion
                    #region Show Predicted Result
#if SHOWREPREDICTRESULT
                    Dictionary<ImageType, Task<float>> taskList = new Dictionary<ImageType, Task<float>>();


                    foreach (KeyValuePair<ImageType, UMat> kvpair in ResultDict)
                    {
                        taskList[kvpair.Key] = Task<float>.Factory.StartNew(() =>
                        {
                            return PredictList[kvpair.Key].PredictImage(kvpair.Value);
                        }
                            );
                    }
                    Task.WaitAll(taskList.Values.ToArray());

                    foreach (KeyValuePair<ImageType, UMat> kvpair in ResultDict)
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
                    #endregion
                    #region Save frames
#if SAVEREQUIRED
                    foreach (var kvpair in ResultDict)
                    {
                        string saveDirPath = Path.Combine(ImageFormat.ImageParam[kvpair.Key].AimPath, kvpair.Key.ToString());

                        if (Directory.Exists(saveDirPath) == false)
                            Directory.CreateDirectory(saveDirPath);

                        string saveFileFullName = Path.Combine(saveDirPath, DateTime.Now.ToString("yyyyMMdd_HHmmssffff_") + currentFrame + "_" + kvpair.Key.ToString() + fileSaveExtension);

                        kvpair.Value.Save(saveFileFullName);
                    }
#endif
                    #endregion SAVEREQUIRED

                    foreach (var kvRes in ResultDict)
                    {
                        kvRes.Value.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
