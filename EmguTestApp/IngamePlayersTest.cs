using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
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


        public IngamePlayersTest(string pathPlayer1SvmModel, string pathPlayer2SvmModel, ModelTypes modelType)
        {
            switch (modelType)
            {
                case ModelTypes.KnModel:
                    p1PredictModel = new KNearest();
                    ((KNearest)p1PredictModel).DefaultK = 3;
                    p2PredictModel = new KNearest();
                    ((KNearest)p2PredictModel).DefaultK = 3;
                    break;

                case ModelTypes.SvmModel:
                    p1PredictModel = new SVM();
                    p2PredictModel = new SVM();
                    break;
            }

            //player1 model load from file
            FileStorage fs1 = new FileStorage(pathPlayer1SvmModel, FileStorage.Mode.Read);

            p1PredictModel.Read(fs1.GetRoot());
            fs1.ReleaseAndGetString();
            //

            //player2 model load from file
            FileStorage fs2 = new FileStorage(pathPlayer2SvmModel, FileStorage.Mode.Read);

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
