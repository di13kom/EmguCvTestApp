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

        ImageKind P1ImageKind;

        string ModelSavedFile;

        public IngamePlayersTest(ImageKind p1ImageKind, ModelTypes modelType)
        {
            P1ImageKind = p1ImageKind;

            switch (modelType)
            {
                case ModelTypes.KnModel:
                    p1PredictModel = new KNearest();
                    ((KNearest)p1PredictModel).DefaultK = 3;

                    ModelSavedFile = "KN_mlp_model.xml";
                    break;

                case ModelTypes.SvmModel:
                    p1PredictModel = new SVM();
                    ((SVM)p1PredictModel).TermCriteria = new MCvTermCriteria(1000, 0.00001);
                    ((SVM)p1PredictModel).C = 1;
                    ((SVM)p1PredictModel).SetKernel(SVM.SvmKernelType.Linear);
                    ((SVM)p1PredictModel).Type = SVM.SvmType.CSvc;

                    ModelSavedFile = "SVM_mlp_model.xml";
                    break;
            }

            //player1 model load from file
            FileStorage fs1 = new FileStorage(Path.Combine(ImageFormat.ImageParam[P1ImageKind].AimPath,ModelSavedFile), FileStorage.Mode.Read);

            p1PredictModel.Read(fs1.GetRoot());
            fs1.ReleaseAndGetString();
            //

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
    }

}
