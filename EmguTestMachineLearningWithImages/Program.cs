using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTestMachineLearningWithImages
{
    class Program
    {

        static void Main(string[] args)
        {
            
            //SvmModelTrain mod2 = new SvmModelTrain();
            //mod2.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player2");


            //SvmModelTrain mod = new SvmModelTrain();
            //mod.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player1");



            //check
            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Kyo\Kyo_20170411_1226211031.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Andy\Andy_20170411_1215266888.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Ralf\Ralf_20170411_1229404501.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Zarina\Zarina_20170411_1233591736.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\K'\K'_20170411_1225241691.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Diamon\Diamon_20170411_1220515177.bmp", @"d:\Q4Vid\Players\Images\Player1");
        }
    }
}
