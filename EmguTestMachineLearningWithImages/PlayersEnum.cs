using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguTestMachineLearningWithImages
{
    public static class PlayersEnum
    {
        public class PlayerDef
        {
            //public string Name { get; set; }
            public string LongName { get; set; }
            public int ClassNum { get; set; }
        }

        public static Dictionary<string, PlayerDef> Players = new Dictionary<string, PlayerDef>()
        {
            {"_BlackFull",  new PlayerDef() {LongName="_BlackFull",             ClassNum = -1 } },
            {"_WhiteFull",  new PlayerDef() {LongName="_WhiteFull",             ClassNum = -2 } },
            {"_Random",     new PlayerDef() {LongName="_Random",                ClassNum = -3 } },

            {"Alice",       new PlayerDef() {LongName="Alice",                  ClassNum = 1 } },
            {"Andy",        new PlayerDef() {LongName="Andy Bogard",            ClassNum = 2 } },
            {"Angel",       new PlayerDef() {LongName="Angel",                  ClassNum = 3 } },
            {"Antonov",     new PlayerDef() {LongName="Antonov",                ClassNum = 4 } },
            {"Athena",      new PlayerDef() {LongName="Athena Asamiya",         ClassNum = 5 } },
            {"Bandeiras",   new PlayerDef() {LongName="Bandeiras Hattori",      ClassNum = 6 } },
            {"Benimaru",    new PlayerDef() {LongName="Benimaru Nikado",        ClassNum = 7 } },
            {"Billy",       new PlayerDef() {LongName="Billy Kane",             ClassNum = 8 } },
            {"Chang",       new PlayerDef() {LongName="Chang Koehan",           ClassNum = 9 } },
            {"Chin",        new PlayerDef() {LongName="Chin Gentsai",           ClassNum = 10 } },
            {"Choi",        new PlayerDef() {LongName="Choi Bounge",            ClassNum = 11 } },
            {"Clark",       new PlayerDef() {LongName="Clark",                  ClassNum = 12 } },
            {"Diamon",      new PlayerDef() {LongName="Goro Diamon",            ClassNum = 13 } },
            {"Dinosaur",    new PlayerDef() {LongName="King of Dinosaurs",      ClassNum = 14 } },
            {"Gang_IL",     new PlayerDef() {LongName="Gang_IL",                ClassNum = 15 } },
            {"Geese",       new PlayerDef() {LongName="Geese Howard",           ClassNum = 16 } },
            {"Hein",        new PlayerDef() {LongName="Hein",                   ClassNum = 17 } },
            {"Iori",        new PlayerDef() {LongName="Iori Yagami",            ClassNum = 18 } },
            {"Joe",         new PlayerDef() {LongName="Joe Higashi",            ClassNum = 19 } },
            {"K'",          new PlayerDef() {LongName="K'",                     ClassNum = 20 } },
            {"Kensou",      new PlayerDef() {LongName="Sie Kensou",             ClassNum = 21 } },
            {"Kim",         new PlayerDef() {LongName="Kim",                    ClassNum = 22 } },
            {"King",        new PlayerDef() {LongName="King",                   ClassNum = 23 } },
            {"Kukri",       new PlayerDef() {LongName="Kukri",                  ClassNum = 24 } },
            {"Kula",        new PlayerDef() {LongName="Kula Diamond",           ClassNum = 25 } },
            {"Kyo",         new PlayerDef() {LongName="Kyo Kusanagi",           ClassNum = 26 } },
            {"Leona",       new PlayerDef() {LongName="Leona",                  ClassNum = 27 } },
            {"Luong",       new PlayerDef() {LongName="Luong",                  ClassNum = 28 } },
            {"Love",        new PlayerDef() {LongName="Love Heart",             ClassNum = 29 } },
            {"Mai",         new PlayerDef() {LongName="Mai Shiranui",           ClassNum = 30 } },
            {"Mature",      new PlayerDef() {LongName="Mature",                 ClassNum = 31 } },
            {"Maxima",      new PlayerDef() {LongName="Maxima",                 ClassNum = 32 } },
            {"Meitenkun",   new PlayerDef() {LongName="Meitenkun",              ClassNum = 33 } },
            {"Mian",        new PlayerDef() {LongName="Mian",                   ClassNum = 34 } },
            {"Muimui",      new PlayerDef() {LongName="Muimui",                 ClassNum = 35 } },
            {"Nakoruru",    new PlayerDef() {LongName="Nakoruru",               ClassNum = 36 } },
            {"Nelson",      new PlayerDef() {LongName="Nelson",                 ClassNum = 37 } },
            {"Ralf",        new PlayerDef() {LongName="Ralf",                   ClassNum = 38 } },
            {"Ramon",       new PlayerDef() {LongName="Ramon",                  ClassNum = 39 } },
            {"Robert",      new PlayerDef() {LongName="Robert Garcia",          ClassNum = 40 } },
            {"Ryo",         new PlayerDef() {LongName="Ryo Sakazaki",           ClassNum = 41 } },
            {"Shunei",      new PlayerDef() {LongName="Shunei",                 ClassNum = 42 } },
            {"Sylvie",      new PlayerDef() {LongName="Sylvie Paula Paula",     ClassNum = 43 } },
            {"Terry",       new PlayerDef() {LongName="Terry Bogard",           ClassNum = 44 } },
            {"Tung",        new PlayerDef() {LongName="Tung Fu Rue",            ClassNum = 45 } },
            {"Verse",       new PlayerDef() {LongName="Verse",                  ClassNum = 46 } },
            {"Vice",        new PlayerDef() {LongName="Vice",                   ClassNum = 47 } },
            {"Xanadu",      new PlayerDef() {LongName="Xanadu",                 ClassNum = 48 } },
            {"Yuri",        new PlayerDef() {LongName="Yuri Sakazaki",          ClassNum = 49 } },
            {"Zarina",      new PlayerDef() {LongName="Zarina",                 ClassNum = 50 } },
        };
    }

    public enum ImageKind { Ingame_Player1Name,
                            Ingame_Player2Name,
                            OnSelect_Player1Name,
                            OnSelect_Player2Name,
                            TitleMenu, KoGoMessage,
                            Result_Player1,
                            Result_Player2,
                            PlayerWins,
                            RoundReadyMessage }
    public static class ImageFormat
    {

        //public class ImageCommonParams
        //{
        //    public int Width { get; set; }
        //    public int Height { get; set; }
        //    public float Scale { get; set; }
        //}

        public class ImageParamsDef
        {
            public string AimPath { get; set; }
            public Bgr ColorLowerThreshold { get; set; }
            public Bgr ColorHigherThreshold { get; set; }
            public int XPos { get; set; }
            public int YPos { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public float Scale { get; set; }
        }

        //public static Dictionary<ImageKind, ImageCommonParams> ImageSizeDic = new Dictionary<ImageKind, ImageCommonParams>()
        //{
        //    { ImageKind.Ingame_PlayerName,          new ImageCommonParams { Width=125,     Height=15,      Scale=1} },
        //    { ImageKind.ChoosePlayer_PlayerName,    new ImageCommonParams { Width=410,     Height=25,      Scale=0.5f} },
        //    { ImageKind.TitleMenu,                  new ImageCommonParams { Width=550,     Height=50,      Scale=0.5f} }
        //};

        public static Dictionary<ImageKind, ImageParamsDef> ImageParam = new Dictionary<ImageKind, ImageParamsDef>
        {
            {
                ImageKind.Ingame_Player1Name,               new ImageParamsDef { AimPath = @"d:\Q4Vid\Players\Images\Player1\",
                ColorLowerThreshold = new Bgr(170, 160, 0),     ColorHigherThreshold =  new Bgr(230, 225, 100),
                XPos = 35,       YPos = 110,      Width = 125,     Height = 15,      Scale = 1.0f}
            },

            {
                ImageKind.Ingame_Player2Name,               new ImageParamsDef { AimPath = @"d:\Q4Vid\Players\Images\Player2\",
                ColorLowerThreshold = new Bgr(0, 190, 95),     ColorHigherThreshold =  new Bgr(95, 250, 150),
                XPos = 1760,       YPos = 110,      Width = 125,     Height = 15,      Scale = 1.0f}
            },

            {
                ImageKind.OnSelect_Player1Name,             new ImageParamsDef { AimPath = @"D:\Q4Vid\ChoosePlayers\Images\Player1",
                ColorLowerThreshold = new Bgr(170, 160, 0),     ColorHigherThreshold =  new Bgr(230, 225, 100),
                XPos = 25,      YPos =  895,        Width = 410,    Height = 25,    Scale = 0.5f}
            },

            {
                ImageKind.OnSelect_Player2Name,             new ImageParamsDef { AimPath = @"D:\Q4Vid\ChoosePlayers\Images\Player2",
                ColorLowerThreshold = new Bgr(0, 190, 95),     ColorHigherThreshold =  new Bgr(95, 250, 150),
                XPos = 1475,      YPos =  895,        Width = 410,    Height = 25,    Scale = 0.5f}
            },

            {
                ImageKind.TitleMenu,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\Titles",
                ColorLowerThreshold = new Bgr(5, 150, 215),     ColorHigherThreshold = new Bgr(90, 200, 246),
                XPos = 100,      YPos =  30,        Width = 550,    Height = 50,    Scale = 0.5f}
            },

            {
                ImageKind.KoGoMessage,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\KoGoMessage",
                ColorLowerThreshold = new Bgr(140, 230, 240),   ColorHigherThreshold = new Bgr(/*205*/190, 255, 255),
                XPos = 515,      YPos =  435,        Width = 885,    Height = 210,    Scale = 0.5f}
            },
            //
            {
                ImageKind.Result_Player1,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\Result\Player1",
                ColorLowerThreshold = new Bgr(0, 0, 0),         ColorHigherThreshold = new Bgr(60, 45, 35),
                XPos = 15,      YPos =  760,        Width = 415,    Height = 65,    Scale = 0.5f}
            },

            {
                ImageKind.Result_Player2,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\Result\Player2",
                ColorLowerThreshold = new Bgr(0, 0, 0),         ColorHigherThreshold = new Bgr(60, 45, 35),
                XPos = 1445,      YPos =  760,        Width = 415,    Height = 65,    Scale = 0.5f}
            },

            {
                ImageKind.PlayerWins,                           new ImageParamsDef { AimPath = @"D:\Q4Vid\Result\PlayerWins",
                ColorLowerThreshold = new Bgr(40, 220, 255),    ColorHigherThreshold =  new Bgr(200, 255, 255),
                XPos = 240,      YPos =  505,        Width = 1450,    Height = 70,    Scale = 0.5f}
            },

            {
                ImageKind.RoundReadyMessage,                    new ImageParamsDef { AimPath = @"D:\Q4Vid\Result\RoundReady",
                ColorLowerThreshold = new Bgr(246, 222, 46),     ColorHigherThreshold =  new Bgr(255, 255, 210),
                XPos = 350,      YPos =  505,        Width = 1215,    Height = 70,    Scale = 0.5f}
            },
        };
    }
}
