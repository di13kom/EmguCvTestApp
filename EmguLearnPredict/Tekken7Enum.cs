using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguLearnPredict_Tekken7
{
    public static class Tekken7Enum
    {
        public class StructureDef
        {
            //public string Name { get; set; }
            public string LongName { get; set; }
            public int ClassNum { get; set; }
        }

        #region ResumeGame
        public static Dictionary<string, StructureDef> ResumeGame = new Dictionary<string, StructureDef>
        {
            {"_BlackFull",                          new StructureDef() {LongName="_BlackFull",                                      ClassNum = -1 } },
            {"_WhiteFull",                          new StructureDef() {LongName="_WhiteFull",                                      ClassNum = -2 } },

            {"ResumeGame",                          new StructureDef() {LongName="ResumeGame",                                      ClassNum = 01 } },
        };
        #endregion

        #region PlayerPause
        public static Dictionary<string, StructureDef> PlayerPause = new Dictionary<string, StructureDef>
        {
            {"_BlackFull",                          new StructureDef() {LongName="_BlackFull",                                      ClassNum = -1 } },
            {"_WhiteFull",                          new StructureDef() {LongName="_WhiteFull",                                      ClassNum = -2 } },

            {"Player1Pause",                        new StructureDef() {LongName="Player1Pause",                                    ClassNum = 01 } },
            {"Player2Pause",                        new StructureDef() {LongName="Player2Pause",                                    ClassNum = 02 } },
        };
        #endregion
    }
    public enum ImageType
    {
        PlayerPause,
        ResumeGame,
        p1Wins
    }
    public static class ImageFormat
    {

        public class ImageParamsDef
        {
            public string AimPath { get; set; }
            public IReadOnlyDictionary<string, Tekken7Enum.StructureDef> Dict { get; set; }
            public Bgr ColorLowerThreshold { get; set; }
            public Bgr ColorHigherThreshold { get; set; }
            public Bgr MaskLowerThreshold { get; set; }
            public Bgr MaskHigherThreshold { get; set; }
            public int XPos { get; set; }
            public int YPos { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public float Scale { get; set; }
        }


        public static Dictionary<ImageType, ImageParamsDef> ImageParam = new Dictionary<ImageType, ImageParamsDef>
        {
            {
                ImageType.ResumeGame,               new ImageParamsDef { AimPath = @"c:\EmguLearnState\Tekken7\ResumeGame",
                ColorLowerThreshold =   new Bgr(100, 65, 0),        ColorHigherThreshold =    new Bgr(225, 170, 60),
                MaskLowerThreshold =    new Bgr(100, 65, 0),        MaskHigherThreshold =     new Bgr(225, 170, 60),
                XPos = 104,       YPos = 954,      Width = 135,     Height = 18,      Scale = 1.0f,      Dict=Tekken7Enum.ResumeGame }
            },

            {
                ImageType.PlayerPause,              new ImageParamsDef { AimPath = @"c:\EmguLearnState\Tekken7\PlayerPause",
                ColorLowerThreshold =   new Bgr(100, 65, 0),        ColorHigherThreshold =    new Bgr(225, 170, 60),
                MaskLowerThreshold =    new Bgr(100, 65, 0),        MaskHigherThreshold =     new Bgr(225, 170, 60),
                XPos = 890,       YPos = 322,      Width = 144,     Height = 18,      Scale = 1.0f,      Dict=Tekken7Enum.PlayerPause }
            },

            {
                ImageType.p1Wins,              new ImageParamsDef { AimPath = @"c:\EmguLearnState\Tekken7\P1Wins",
                ColorLowerThreshold =   new Bgr(55, 90, 105),        ColorHigherThreshold =    new Bgr(205, 240, 240),
                MaskLowerThreshold =    new Bgr(0, 0, 0),           MaskHigherThreshold =     new Bgr(30, 30, 5),
                //XPos = 164,       YPos = 860,      Width = 255,     Height = 33,      Scale = 1.0f,      Dict=Tekken7Enum.PlayerPause }
                XPos = 72,       YPos = 860,      Width = 255,     Height = 75,      Scale = 1.0f,      Dict=Tekken7Enum.PlayerPause }
            },
        };
    }
}
