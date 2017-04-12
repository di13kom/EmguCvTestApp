using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTestMachineLearningWithImages
{
    class SvmModelTrain
    {
        string ImgFileExtension = ".bmp";
        static string fileName = "SVM_mlp_model.xml";
        SVM svmModel;

        int LastMatrixIndex = 0;
        Matrix<float> TrainingData;/* = new Matrix<float>(0,0)*///fail

        Matrix<int> TrainingClasses;/* = new Matrix<int>(0,0)*/

        public SvmModelTrain()
        {
            //svmModel = new SVM();
        }

        void CreateClassesData(string pth)
        {
            //int numClasses = Directory.EnumerateDirectories(pth).Count();
            int numSamples = 0;
            Directory.EnumerateDirectories(pth).ToList().ForEach(x =>
            {
                numSamples += Directory.EnumerateFiles(x, "*.bmp").Count();
            });

            TrainingData = new Matrix<float>(numSamples, 15 * 125);//
            TrainingClasses = new Matrix<int>(numSamples, 1);
            //TrainingClasses = new Matrix<int>(numSamples, numClasses);

        }


        public void LoadFiles(string pth, string curClassName)
        {
            string[] filesArray = Directory.GetFiles(pth);

            foreach (string fl in filesArray)
            {
                if (fl.EndsWith(ImgFileExtension) == false)
                    continue;
                try
                {
                    //Mat mt = CvInvoke.Imread(fl, Emgu.CV.CvEnum.LoadImageType.Grayscale);

                    Image<Gray, byte> curFile = new Image<Gray, byte>(fl);
                    //Matrix<byte> mt = new Matrix<byte>(curFile.Height, curFile.Width);
                    //bool isC = curFile.Mat.IsContinuous;

                    //Mat mtt = CvInvoke.Imread(fl,Emgu.CV.CvEnum.LoadImageType.Grayscale);
                    //curFile.CopyTo();
                    FillMatrix(curFile, curClassName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void LoadFromDirectory(string dir)
        {
            CreateClassesData(dir);

            foreach (string dr in Directory.EnumerateDirectories(dir))
            {
                string curDirectory = dr;
                if (curDirectory.LastIndexOf(Path.DirectorySeparatorChar) == curDirectory.Length - 1)
                    curDirectory = curDirectory.TrimEnd('\\');
                string curClassname = curDirectory.Substring(curDirectory.LastIndexOf('\\') + 1, curDirectory.Length - 1 - curDirectory.LastIndexOf('\\'));
                Console.WriteLine(curClassname);
                LoadFiles(curDirectory, curClassname);
            }

            TrianOnDataSave();
        }

        public void FillMatrix(Image<Gray, byte> img, string className)
        {
            try
            {
                int ind = LastMatrixIndex;
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        int matInd = i * img.Width + j;
                        TrainingData[ind, matInd] = img.Data[i, j, 0];
                        //img.CopyTo(TrainingData[ind]);
                    }
                }

                TrainingClasses[ind, 0] = PlayersEnum.Players[className].ClassNum;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            LastMatrixIndex++;
        }

        static public /*byte[]*/void ConvertToArray(Image<Gray, byte> img, Matrix<float> mtr)
        {
            try
            {
                //Image<Gray, byte> img = mt.ToImage<Gray, byte>();
                //byte[] Arr = new byte[img.Width * img.Height];
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        mtr.Data[0, i * img.Width + j] = img.Data[i, j, 0];
                        //Console.WriteLine($"i - {i} j - {j} - data:{img.Data[i, j, 0]}");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //return Arr;
        }

        public void TrianOnDataSave()
        {
            TrainData td = new TrainData(TrainingData, Emgu.CV.ML.MlEnum.DataLayoutType.RowSample, TrainingClasses);
            try
            {
                svmModel = new SVM();
                svmModel.TrainAuto(td);

                if (File.Exists(fileName))
                    File.Delete(fileName);

                FileStorage fs = new FileStorage(fileName, FileStorage.Mode.Write);
                svmModel.Write(fs);
                fs.ReleaseAndGetString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                svmModel.Dispose();
                td.Dispose();
            }
        }

        public static void LoadPredictData(string lFile)
        {

            Mat sMat = CvInvoke.Imread(lFile, Emgu.CV.CvEnum.LoadImageType.Grayscale);
            Image<Gray, byte> pic = new Image<Gray, byte>(lFile);
            Mat xmat = pic.Mat;

            Matrix<float> mtr = new Matrix<float>(1, 15 * 125);
            //Matrix<byte> mtr = new Matrix<byte>(pic.Rows, pic.Cols);

            Matrix<float> predict = new Matrix<float>(1, 1);
            predict[0, 0] = 300;

            try
            {
                //sMat.CopyTo(mtr);
                ConvertToArray(pic, mtr);

                using (SVM sMod = new SVM())
                {
                    FileStorage fs1 = new FileStorage(fileName, FileStorage.Mode.Read);

                    sMod.Read(fs1.GetRoot());
                    fs1.ReleaseAndGetString();

                    var ret = sMod.Predict(mtr, predict);
                    //Console.WriteLine($"Player is :{PlayersEnum.Players.Values.Where(x => x.ClassNum==predict[0,0]).First().LongName}");
                    Console.WriteLine($"Player is :{PlayersEnum.Players.Where(x => x.Value.ClassNum==predict[0,0]).FirstOrDefault().Key}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
