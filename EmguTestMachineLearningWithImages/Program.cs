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
            //SvmModelTrain.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain mod = new SvmModelTrain();
            mod.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player1");


            //check
            //SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Kyo\Kyo_20170411_1226211031.bmp");
        }
    }
}
