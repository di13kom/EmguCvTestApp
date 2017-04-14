#define LearningRequired

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

#if LearningRequired
            //SvmModelTrain mod2 = new SvmModelTrain();
            //mod2.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player2");


            //SvmModelTrain mod = new SvmModelTrain();
            //mod.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player1");

            //Kn
            SvmModelTrain mod2 = new SvmModelTrain();
            mod2.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player2");


            SvmModelTrain mod = new SvmModelTrain();
            mod.LoadFromDirectory(@"d:\Q4Vid\Players\Images\Player1");
#else
            //orig
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\Alice\Alice_20170411_1215141381.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\Alice\Alice_20170411_1215144231.bmp", @"d:\Q4Vid\Players\Images\Player2");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\Alice\Alice_20170411_1215146011.bmp", @"d:\Q4Vid\Players\Images\Player1");

            //
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\_BlackFull\_Black_00.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\_WhiteFull\_White_12.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\_BlackFull\_Black_05.bmp", @"d:\Q4Vid\Players\Images\Player2");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\_WhiteFull\_White_02.bmp", @"d:\Q4Vid\Players\Images\Player2");
            //


            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\Antonov\Antonov_20170411_1218516021.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\Antonov\Antonov_20170411_1218516321.bmp", @"d:\Q4Vid\Players\Images\Player2");

            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\Angel\Angel_20170411_1218394513.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\Angel\Angel_20170411_1218395203.bmp", @"d:\Q4Vid\Players\Images\Player2");
                
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\Athena\Athena_20170411_1219045404.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\Athena\Athena_20170411_1219042184.bmp", @"d:\Q4Vid\Players\Images\Player2");

            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\Bandeiras\Bandeiras_20170411_1219236977.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\Bandeiras\Bandeiras_20170411_1219237347.bmp", @"d:\Q4Vid\Players\Images\Player2");

            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player1\K'\K'_20170411_1225241051.bmp", @"d:\Q4Vid\Players\Images\Player1");
            SvmModelTrain.LoadPredictDataKN(@"d:\Q4Vid\Players\Images\Player2\K'\K'_20170411_1225241051.bmp", @"d:\Q4Vid\Players\Images\Player2");
            //



            //check
            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Kyo\Kyo_20170411_1226211031.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Andy\Andy_20170411_1215266888.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Ralf\Ralf_20170411_1229404501.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Zarina\Zarina_20170411_1233591736.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\K'\K'_20170411_1225241691.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Diamon\Diamon_20170411_1220515177.bmp", @"d:\Q4Vid\Players\Images\Player1");



            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Alice\Alice_20170413_1648581405.bmp", @"d:\Q4Vid\Players\Images\Player1");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player1\Alice\Alice_20170413_1648584025.bmp", @"d:\Q4Vid\Players\Images\Player1");


            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player2\Alice\Alice_20170413_1648581405.bmp", @"d:\Q4Vid\Players\Images\Player2");

            SvmModelTrain.LoadPredictData(@"d:\Q4Vid\Players\Images\Player2\Alice\Alice_20170413_1648584894.bmp", @"d:\Q4Vid\Players\Images\Player2");
#endif
        }
    }
}
