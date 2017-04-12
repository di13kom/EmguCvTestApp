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
            int numClasses = Directory.EnumerateDirectories(pth).Count();
            int numSamples = 0;
            Directory.EnumerateDirectories(pth).ToList().ForEach(x =>
            {
                numSamples += Directory.EnumerateFiles(x, "*.bmp").Count();
            });

            TrainingData = new Matrix<float>(numSamples, 15 * 128);//
            TrainingClasses = new Matrix<int>(numSamples, numClasses);

        }


        public void LoadFiles(string pth)
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
                LoadFiles(curDirectory);
            }
        }

        public void FillMatrix(Image<Gray, byte> img, string className)
        {
            int ind = LastMatrixIndex;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    TrainingData[ind, i+j] = img.Data[i, j, 0];
                    //img.CopyTo(TrainingData[ind]);
                }
            }

            TrainingClasses[ind, 0] = className;

            LastMatrixIndex++;
        }

        public byte[] ConvertToArray(Image<Gray, byte> img)
        {

            //Image<Gray, byte> img = mt.ToImage<Gray, byte>();
            byte[] Arr = new byte[img.Width * img.Height];
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Arr[i + j] = img.Data[i, j, 0];
                }
            }

            return Arr;
        }
    }
}
