using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmguLearnPredict_KOFXIV;

namespace EmguLearnPredict
{
    public enum ModelType { SvmModel, KnModel };

    public class TrainModel
    {
        string ImgFileExtension = ".bmp";
        static string fileNameSaveLearnedData;
        //static string fileNameSaveLearnedDataKN = "KN_mlp_model.xml";

        string SaveDir;

        //SVM svmModel;
        IStatModel PredictModel;
        ModelType PredictModelType;

        ImageType AimImage;

        int LastMatrixIndex = 0;
        Matrix<float> TrainingData;/* = new Matrix<float>(0,0)*///fail

        Matrix<int> TrainingClasses;/* = new Matrix<int>(0,0)*/

        public TrainModel(ModelType modelType, ImageType imageKind)
        {
            //svmModel = new SVM();
            AimImage = imageKind;

            PredictModelType = modelType;
            switch (PredictModelType)
            {
                case ModelType.KnModel:
                    fileNameSaveLearnedData = "KN_mlp_model.xml";
                    break;
                case ModelType.SvmModel:
                    fileNameSaveLearnedData = "SVM_mlp_model.xml";
                    break;
            }
        }

        void CreateClassesData(string pth)
        {
            //int numClasses = Directory.EnumerateDirectories(pth).Count();
            SaveDir = pth;

            int numSamples = 0;
            Directory.EnumerateDirectories(pth).ToList().ForEach(x =>
            {
                numSamples += Directory.EnumerateFiles(x, "*.bmp").Count();
            });

            int width = Convert.ToInt32(Math.Truncate(ImageFormat.ImageParam[AimImage].Height * ImageFormat.ImageParam[AimImage].Scale));
            int height = Convert.ToInt32(Math.Truncate(ImageFormat.ImageParam[AimImage].Width * ImageFormat.ImageParam[AimImage].Scale));

            TrainingData = new Matrix<float>(numSamples, width * height);//
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

        public void LoadFromDirectory(/*string dir*/)
        {
            string dir = ImageFormat.ImageParam[AimImage].AimPath;
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

                //TrainingClasses[ind, 0] = PlayersEnum.Players[className].ClassNum;
                TrainingClasses[ind, 0] = ImageFormat.ImageParam[AimImage].Dict[className].ClassNum;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            LastMatrixIndex++;
        }

        static public void ConvertToArray(Image<Gray, byte> img, Matrix<float> mtr)
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
            catch (Exception ex)
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
                switch (PredictModelType)
                {
                    case ModelType.SvmModel:
                        PredictModel = new SVM();
                        ((SVM)PredictModel).TermCriteria = new MCvTermCriteria(1000, 0.00001);
                        ((SVM)PredictModel).C = 1;
                        ((SVM)PredictModel).SetKernel(SVM.SvmKernelType.Linear);
                        ((SVM)PredictModel).Type = SVM.SvmType.CSvc;
                        break;

                    case ModelType.KnModel:
                        PredictModel = new KNearest();
                        ((KNearest)PredictModel).DefaultK = 3;
                        //knn.
                        //PredictModel.Train(td, 300);
                        break;
                }

                PredictModel.Train(td, 300);

                string saveFile = Path.Combine(SaveDir, fileNameSaveLearnedData);

                if (File.Exists(saveFile))
                    File.Delete(saveFile);

                FileStorage fs = new FileStorage(saveFile, FileStorage.Mode.Write);
                PredictModel.Write(fs);
                fs.ReleaseAndGetString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    switch (PredictModelType)
            //    {
            //        case ModelType.SvnModel:
            //            ((SVM)PredictModel).Dispose();
            //            break;

            //        case ModelType.KnModel:
            //            ((KNearest)PredictModel).Dispose();
            //            break;

            //    }
            //    //PredictModel.Dispose();
            //    td.Dispose();
            //}
        }

        public static float LoadPredictData(string lFile, string pathToLearnData, ModelType modelType)
        {

            IStatModel predictModel;

            Image<Gray, byte> pic = new Image<Gray, byte>(lFile);

            Matrix<float> sampleMatr = new Matrix<float>(1, 15 * 125);

            Matrix<float> predictMatr = new Matrix<float>(1, 1);
            predictMatr[0, 0] = 300;




            try
            {
                //sMat.CopyTo(mtr);
                ConvertToArray(pic, sampleMatr);


                switch (modelType)
                {
                    //case ModelType.SvnModel:
                    //    predictModel = new SVM();
                    //    ((SVM)predictModel).SetKernel(SVM.SvmKernelType.Sigmoid);
                    //    fileNameSaveLearnedData = "SVM_mlp_model.xml";
                    //    break;
                    case ModelType.KnModel:
                        predictModel = new KNearest();
                        ((KNearest)predictModel).DefaultK = 3;
                        fileNameSaveLearnedData = "KN_mlp_model.xml";
                        break;
                    default:
                        predictModel = new SVM();
                        ((SVM)predictModel).TermCriteria = new MCvTermCriteria(1000, 0.00001);
                        ((SVM)predictModel).C = 1;
                        ((SVM)predictModel).SetKernel(SVM.SvmKernelType.Linear);
                        ((SVM)predictModel).Type = SVM.SvmType.CSvc;
                        fileNameSaveLearnedData = "SVM_mlp_model.xml";
                        break;
                }
                string LearnedData = Path.Combine(pathToLearnData, fileNameSaveLearnedData);
                FileStorage fs1 = new FileStorage(LearnedData, FileStorage.Mode.Read);
                predictModel.Read(fs1.GetRoot());



                fs1.ReleaseAndGetString();

                var ret = predictModel.Predict(sampleMatr, predictMatr, 300);
                //Console.WriteLine($"Player is :{PlayersEnum.Players.Values.Where(x => x.ClassNum==predict[0,0]).First().LongName}");
                //---Console.WriteLine($"Player is :{PlayersEnum.Players.Where(x => x.Value.ClassNum == predictMatr[0, 0]).FirstOrDefault().Key}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //finally
            //{
            //    switch (modelType)
            //    {
            //        case ModelType.SvnModel:
            //            ((SVM)predictModel).Dispose();
            //            break;
            //        case ModelType.KnModel:
            //            ((KNearest)predictModel).Dispose();
            //            break;
            //    }
            //}
            return predictMatr[0, 0];
        }

    }

    public class PredictModel
    {

        IStatModel p1PredictModel;

        ImageType P1ImageKind;

        string ModelSavedFile;

        ModelType myModelType;

        public PredictModel(ImageType p1ImageKind, ModelType modelType)
        {
            P1ImageKind = p1ImageKind;

            myModelType = modelType;

            switch (modelType)
            {
                case ModelType.KnModel:
                    p1PredictModel = new KNearest();
                    ((KNearest)p1PredictModel).DefaultK = 3;

                    ModelSavedFile = "KN_mlp_model.xml";
                    break;

                case ModelType.SvmModel:
                    p1PredictModel = new SVM();
                    ((SVM)p1PredictModel).TermCriteria = new MCvTermCriteria(1000, 0.00001);
                    ((SVM)p1PredictModel).C = 1;
                    ((SVM)p1PredictModel).SetKernel(SVM.SvmKernelType.Linear);
                    ((SVM)p1PredictModel).Type = SVM.SvmType.CSvc;

                    ModelSavedFile = "SVM_mlp_model.xml";
                    break;
            }

            try
            {
                //player1 model load from file
                FileStorage fs1 = new FileStorage(Path.Combine(ImageFormat.ImageParam[P1ImageKind].AimPath, ModelSavedFile), FileStorage.Mode.Read);

                p1PredictModel.Read(fs1.GetRoot());
                fs1.ReleaseAndGetString();
                fs1.Dispose();
                //
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public float PredictImage(Image<Gray, byte> p1Img)
        {
            float retValue;
            Matrix<float> p1PredictionMatrix = new Matrix<float>(1, 1);

            Matrix<float> p1TestMatrix = new Matrix<float>(1, p1Img.Height * p1Img.Width);

            try
            {
                TransformImageToArray(p1Img, p1TestMatrix);

                var x = p1PredictModel.Predict(p1TestMatrix, p1PredictionMatrix, 300);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            retValue = p1PredictionMatrix[0, 0];

            return retValue;
        }

        public void TransformImageToArray(Image<Gray, byte> img, Matrix<float> mat)
        {
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    int ind = i * img.Width + j;
                    //Console.WriteLine("index: {0}", ind);
                    mat.Data[0, ind] = img.Data[i, j, 0];
                }
            }
        }

        public void CleanModel()
        {
            try
            {
                p1PredictModel.Clear();
                switch (myModelType)
                {
                    case ModelType.SvmModel:
                        ((SVM)p1PredictModel).Dispose();
                        break;
                    case ModelType.KnModel:
                        ((KNearest)p1PredictModel).Dispose();
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
