using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using EmguTestMachineLearningWithImages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTestApp
{
    enum ModelTypes { SvmModel, KnModel };

    class IngamePlayersTest
    {

        IStatModel p1PredictModel;
        IStatModel p2PredictModel;

        ImageKind P1ImageKind;
        ImageKind P2ImageKind;

        string ModelSavedFile;

        public IngamePlayersTest(ImageKind p1ImageKind, ImageKind p2ImageKind, ModelTypes modelType)
        {
            P1ImageKind = p1ImageKind;
            P2ImageKind = p2ImageKind;


            switch (modelType)
            {
                case ModelTypes.KnModel:
                    p1PredictModel = new KNearest();
                    ((KNearest)p1PredictModel).DefaultK = 3;
                    p2PredictModel = new KNearest();
                    ((KNearest)p2PredictModel).DefaultK = 3;

                    ModelSavedFile = "KN_mlp_model.xml";
                    break;

                case ModelTypes.SvmModel:
                    p1PredictModel = new SVM();
                    ((SVM)p1PredictModel).TermCriteria = new MCvTermCriteria(1000, 0.00001);
                    ((SVM)p1PredictModel).C = 1;
                    ((SVM)p1PredictModel).SetKernel(SVM.SvmKernelType.Linear);
                    ((SVM)p1PredictModel).Type = SVM.SvmType.CSvc;
                    p2PredictModel = new SVM();
                    ((SVM)p2PredictModel).TermCriteria = new MCvTermCriteria(1000, 0.00001);
                    ((SVM)p2PredictModel).C = 1;
                    ((SVM)p2PredictModel).SetKernel(SVM.SvmKernelType.Linear);
                    ((SVM)p2PredictModel).Type = SVM.SvmType.CSvc;

                    ModelSavedFile = "SVM_mlp_model.xml";
                    break;
            }

            //player1 model load from file
            FileStorage fs1 = new FileStorage(Path.Combine(ImageFormat.ImageParam[P1ImageKind].AimPath,ModelSavedFile), FileStorage.Mode.Read);

            p1PredictModel.Read(fs1.GetRoot());
            fs1.ReleaseAndGetString();
            //

            //player2 model load from file
            FileStorage fs2 = new FileStorage(Path.Combine(ImageFormat.ImageParam[P2ImageKind].AimPath, ModelSavedFile), FileStorage.Mode.Read);

            p2PredictModel.Read(fs2.GetRoot());
            fs2.ReleaseAndGetString();
            //

        }

        public float[] PredictImage(Image<Gray, byte> p1Img, Image<Gray, byte> p2Img)
        {
            float[] retValue = new float[2];
            Matrix<float> p1PredictionMatrix = new Matrix<float>(1, 1);
            Matrix<float> p2PredictionMatrix = new Matrix<float>(1, 1);

            Matrix<float> p1TestMatrix = new Matrix<float>(1, p1Img.Height * p1Img.Width);
            Matrix<float> p2TestMatrix = new Matrix<float>(1, p2Img.Height * p2Img.Width);

            //Matrix<float> p1TestMatrix = new Matrix<float>(TransformImageToArray(p1Img));
            //Matrix<float> p2TestMatrix = new Matrix<float>(TransformImageToArray(p2Img));

            try
            {
                TransformImageToArray(p1Img, p1TestMatrix);
                TransformImageToArray(p2Img, p2TestMatrix);

                var x = p1PredictModel.Predict(p1TestMatrix, p1PredictionMatrix, 300);
                var y = p2PredictModel.Predict(p2TestMatrix, p2PredictionMatrix, 300);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            retValue[0] = p1PredictionMatrix[0, 0];
            retValue[1] = p2PredictionMatrix[0, 0];

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
    }

}
