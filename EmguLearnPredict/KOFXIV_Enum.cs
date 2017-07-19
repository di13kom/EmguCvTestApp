using Emgu.CV.Structure;
using EmguLearnPredict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTestMachineLearningWithImages
{
    #region Test_PredictionDefinitionClass
    //public class Players1Name : PredictionDefinitionClass
    //{
    //    static imType imageType = new imType { ImageTypeName = "Ingame_Player1Name" };
    //    #region _predictionClassNames
    //    private static Dictionary<string, StructureDef> _predictionClassNames = new Dictionary<string, StructureDef>()
    //    {
    //        {"_BlackFull",  new StructureDef() {LongName="_BlackFull",             ClassNum = -1 } },
    //        {"_WhiteFull",  new StructureDef() {LongName="_WhiteFull",             ClassNum = -2 } },
    //        {"_Random",     new StructureDef() {LongName="_Random",                ClassNum = -3 } },
    //        {"_Dirty",      new StructureDef() {LongName="_Dirty",                 ClassNum = -4 } },

    //        {"Alice",       new StructureDef() {LongName="Alice",                  ClassNum = 01 } },
    //        {"Andy",        new StructureDef() {LongName="Andy Bogard",            ClassNum = 02 } },
    //        {"Angel",       new StructureDef() {LongName="Angel",                  ClassNum = 03 } },
    //        {"Antonov",     new StructureDef() {LongName="Antonov",                ClassNum = 04 } },
    //        {"Athena",      new StructureDef() {LongName="Athena Asamiya",         ClassNum = 05 } },
    //        {"Bandeiras",   new StructureDef() {LongName="Bandeiras Hattori",      ClassNum = 06 } },
    //        {"Benimaru",    new StructureDef() {LongName="Benimaru Nikado",        ClassNum = 07 } },
    //        {"Billy",       new StructureDef() {LongName="Billy Kane",             ClassNum = 08 } },
    //        {"Chang",       new StructureDef() {LongName="Chang Koehan",           ClassNum = 09 } },
    //        {"Chin",        new StructureDef() {LongName="Chin Gentsai",           ClassNum = 10 } },
    //        {"Choi",        new StructureDef() {LongName="Choi Bounge",            ClassNum = 11 } },
    //        {"Clark",       new StructureDef() {LongName="Clark",                  ClassNum = 12 } },
    //        {"Diamon",      new StructureDef() {LongName="Goro Diamon",            ClassNum = 13 } },
    //        {"Dinosaur",    new StructureDef() {LongName="King of Dinosaurs",      ClassNum = 14 } },
    //        {"Gang_IL",     new StructureDef() {LongName="Gang_IL",                ClassNum = 15 } },
    //        {"Geese",       new StructureDef() {LongName="Geese Howard",           ClassNum = 16 } },
    //        {"Hein",        new StructureDef() {LongName="Hein",                   ClassNum = 17 } },
    //        {"Iori",        new StructureDef() {LongName="Iori Yagami",            ClassNum = 18 } },
    //        {"Joe",         new StructureDef() {LongName="Joe Higashi",            ClassNum = 19 } },
    //        {"K'",          new StructureDef() {LongName="K'",                     ClassNum = 20 } },
    //        {"Kensou",      new StructureDef() {LongName="Sie Kensou",             ClassNum = 21 } },
    //        {"Kim",         new StructureDef() {LongName="Kim",                    ClassNum = 22 } },
    //        {"King",        new StructureDef() {LongName="King",                   ClassNum = 23 } },
    //        {"Kukri",       new StructureDef() {LongName="Kukri",                  ClassNum = 24 } },
    //        {"Kula",        new StructureDef() {LongName="Kula Diamond",           ClassNum = 25 } },
    //        {"Kyo",         new StructureDef() {LongName="Kyo Kusanagi",           ClassNum = 26 } },
    //        {"Leona",       new StructureDef() {LongName="Leona",                  ClassNum = 27 } },
    //        {"Luong",       new StructureDef() {LongName="Luong",                  ClassNum = 28 } },
    //        {"Love",        new StructureDef() {LongName="Love Heart",             ClassNum = 29 } },
    //        {"Mai",         new StructureDef() {LongName="Mai Shiranui",           ClassNum = 30 } },
    //        {"Mature",      new StructureDef() {LongName="Mature",                 ClassNum = 31 } },
    //        {"Maxima",      new StructureDef() {LongName="Maxima",                 ClassNum = 32 } },
    //        {"Meitenkun",   new StructureDef() {LongName="Meitenkun",              ClassNum = 33 } },
    //        {"Mian",        new StructureDef() {LongName="Mian",                   ClassNum = 34 } },
    //        {"Muimui",      new StructureDef() {LongName="Muimui",                 ClassNum = 35 } },
    //        {"Nakoruru",    new StructureDef() {LongName="Nakoruru",               ClassNum = 36 } },
    //        {"Nelson",      new StructureDef() {LongName="Nelson",                 ClassNum = 37 } },
    //        {"Ralf",        new StructureDef() {LongName="Ralf",                   ClassNum = 38 } },
    //        {"Ramon",       new StructureDef() {LongName="Ramon",                  ClassNum = 39 } },
    //        {"Robert",      new StructureDef() {LongName="Robert Garcia",          ClassNum = 40 } },
    //        {"Ryo",         new StructureDef() {LongName="Ryo Sakazaki",           ClassNum = 41 } },
    //        {"Shunei",      new StructureDef() {LongName="Shunei",                 ClassNum = 42 } },
    //        {"Sylvie",      new StructureDef() {LongName="Sylvie Paula Paula",     ClassNum = 43 } },
    //        {"Terry",       new StructureDef() {LongName="Terry Bogard",           ClassNum = 44 } },
    //        {"Tung",        new StructureDef() {LongName="Tung Fu Rue",            ClassNum = 45 } },
    //        {"Verse",       new StructureDef() {LongName="Verse",                  ClassNum = 46 } },
    //        {"Vice",        new StructureDef() {LongName="Vice",                   ClassNum = 47 } },
    //        {"Xanadu",      new StructureDef() {LongName="Xanadu",                 ClassNum = 48 } },
    //        {"Yuri",        new StructureDef() {LongName="Yuri Sakazaki",          ClassNum = 49 } },
    //        {"Zarina",      new StructureDef() {LongName="Zarina",                 ClassNum = 50 } },
    //    };
    //    #endregion
    //    #region _imageParam
    //    private Dictionary<imType, ImageParamsDef> _imageParam = new Dictionary<imType, ImageParamsDef>
    //    {
    //        {
    //            imageType,               new ImageParamsDef { AimPath = @"c:\EmguLearnState\Players\Images\Player1\",
    //            ColorLowerThreshold =   new Bgr(170, 160, 0),     ColorHigherThreshold =    new Bgr(230, 225, 100),
    //            MaskLowerThreshold =    new Bgr(170, 160, 0),     MaskHigherThreshold =     new Bgr(230, 225, 100),
    //            XPos = 35,       YPos = 110,      Width = 125,     Height = 15,      Scale = 1.0f,      Dict=_predictionClassNames }
    //        },
    //    };
    //    #endregion

    //    public override Dictionary<imType, ImageParamsDef> ImageParam()
    //    {
    //        return _imageParam;
    //    }
    //    public override imType ImageTypeAccs()
    //    {
    //        return imageType;
    //    }
    //}
    #endregion

    public class Players1NameX : PredictionDefinitionClassXXX
    {
        string _ImageTypeName;
        #region _predictionClassNames
        private Dictionary<string, StructureDef> _predictionClassNames = new Dictionary<string, StructureDef>()
        {
            {"_BlackFull",  new StructureDef() {LongName="_BlackFull",             ClassNum = -1 } },
            {"_WhiteFull",  new StructureDef() {LongName="_WhiteFull",             ClassNum = -2 } },
            {"_Random",     new StructureDef() {LongName="_Random",                ClassNum = -3 } },
            {"_Dirty",      new StructureDef() {LongName="_Dirty",                 ClassNum = -4 } },

            {"Alice",       new StructureDef() {LongName="Alice",                  ClassNum = 01 } },
            {"Andy",        new StructureDef() {LongName="Andy Bogard",            ClassNum = 02 } },
            {"Angel",       new StructureDef() {LongName="Angel",                  ClassNum = 03 } },
            {"Antonov",     new StructureDef() {LongName="Antonov",                ClassNum = 04 } },
            {"Athena",      new StructureDef() {LongName="Athena Asamiya",         ClassNum = 05 } },
            {"Bandeiras",   new StructureDef() {LongName="Bandeiras Hattori",      ClassNum = 06 } },
            {"Benimaru",    new StructureDef() {LongName="Benimaru Nikado",        ClassNum = 07 } },
            {"Billy",       new StructureDef() {LongName="Billy Kane",             ClassNum = 08 } },
            {"Chang",       new StructureDef() {LongName="Chang Koehan",           ClassNum = 09 } },
            {"Chin",        new StructureDef() {LongName="Chin Gentsai",           ClassNum = 10 } },
            {"Choi",        new StructureDef() {LongName="Choi Bounge",            ClassNum = 11 } },
            {"Clark",       new StructureDef() {LongName="Clark",                  ClassNum = 12 } },
            {"Diamon",      new StructureDef() {LongName="Goro Diamon",            ClassNum = 13 } },
            {"Dinosaur",    new StructureDef() {LongName="King of Dinosaurs",      ClassNum = 14 } },
            {"Gang_IL",     new StructureDef() {LongName="Gang_IL",                ClassNum = 15 } },
            {"Geese",       new StructureDef() {LongName="Geese Howard",           ClassNum = 16 } },
            {"Hein",        new StructureDef() {LongName="Hein",                   ClassNum = 17 } },
            {"Iori",        new StructureDef() {LongName="Iori Yagami",            ClassNum = 18 } },
            {"Joe",         new StructureDef() {LongName="Joe Higashi",            ClassNum = 19 } },
            {"K'",          new StructureDef() {LongName="K'",                     ClassNum = 20 } },
            {"Kensou",      new StructureDef() {LongName="Sie Kensou",             ClassNum = 21 } },
            {"Kim",         new StructureDef() {LongName="Kim",                    ClassNum = 22 } },
            {"King",        new StructureDef() {LongName="King",                   ClassNum = 23 } },
            {"Kukri",       new StructureDef() {LongName="Kukri",                  ClassNum = 24 } },
            {"Kula",        new StructureDef() {LongName="Kula Diamond",           ClassNum = 25 } },
            {"Kyo",         new StructureDef() {LongName="Kyo Kusanagi",           ClassNum = 26 } },
            {"Leona",       new StructureDef() {LongName="Leona",                  ClassNum = 27 } },
            {"Luong",       new StructureDef() {LongName="Luong",                  ClassNum = 28 } },
            {"Love",        new StructureDef() {LongName="Love Heart",             ClassNum = 29 } },
            {"Mai",         new StructureDef() {LongName="Mai Shiranui",           ClassNum = 30 } },
            {"Mature",      new StructureDef() {LongName="Mature",                 ClassNum = 31 } },
            {"Maxima",      new StructureDef() {LongName="Maxima",                 ClassNum = 32 } },
            {"Meitenkun",   new StructureDef() {LongName="Meitenkun",              ClassNum = 33 } },
            {"Mian",        new StructureDef() {LongName="Mian",                   ClassNum = 34 } },
            {"Muimui",      new StructureDef() {LongName="Muimui",                 ClassNum = 35 } },
            {"Nakoruru",    new StructureDef() {LongName="Nakoruru",               ClassNum = 36 } },
            {"Nelson",      new StructureDef() {LongName="Nelson",                 ClassNum = 37 } },
            {"Ralf",        new StructureDef() {LongName="Ralf",                   ClassNum = 38 } },
            {"Ramon",       new StructureDef() {LongName="Ramon",                  ClassNum = 39 } },
            {"Robert",      new StructureDef() {LongName="Robert Garcia",          ClassNum = 40 } },
            {"Ryo",         new StructureDef() {LongName="Ryo Sakazaki",           ClassNum = 41 } },
            {"Shunei",      new StructureDef() {LongName="Shunei",                 ClassNum = 42 } },
            {"Sylvie",      new StructureDef() {LongName="Sylvie Paula Paula",     ClassNum = 43 } },
            {"Terry",       new StructureDef() {LongName="Terry Bogard",           ClassNum = 44 } },
            {"Tung",        new StructureDef() {LongName="Tung Fu Rue",            ClassNum = 45 } },
            {"Verse",       new StructureDef() {LongName="Verse",                  ClassNum = 46 } },
            {"Vice",        new StructureDef() {LongName="Vice",                   ClassNum = 47 } },
            {"Xanadu",      new StructureDef() {LongName="Xanadu",                 ClassNum = 48 } },
            {"Yuri",        new StructureDef() {LongName="Yuri Sakazaki",          ClassNum = 49 } },
            {"Zarina",      new StructureDef() {LongName="Zarina",                 ClassNum = 50 } },
        };
        #endregion
        #region _imageParam
        private ImageParamsDefXXX _imageParam =     
             new ImageParamsDefXXX { AimPath = @"c:\EmguLearnState\Players\Images\Player1\",
                ColorLowerThreshold =   new Bgr(170, 160, 0),     ColorHigherThreshold =    new Bgr(230, 225, 100),
                MaskLowerThreshold =    new Bgr(170, 160, 0),     MaskHigherThreshold =     new Bgr(230, 225, 100),
                XPos = 35,       YPos = 110,      Width = 125,     Height = 15,      Scale = 1.0f  }
            
        ;
        #endregion
        public Players1NameX(string name="Player1NameX") : base(name)
        {
        }

        public override ImageParamsDefXXX ImageParam
        {
            get { return _imageParam; }
        }

        public override string ImageTypeName
        {
            get { return _ImageTypeName; }

            set { _ImageTypeName = value; }
        }
    }
}
