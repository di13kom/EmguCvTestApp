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

        #region Players
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
        #endregion

        #region Titles
        public static Dictionary<string, PlayerDef> Titles = new Dictionary<string, PlayerDef>
        {
            {"_BlackFull",                          new PlayerDef() {LongName="_BlackFull",                                     ClassNum = -1 } },

            {"MainMenu",                            new PlayerDef() {LongName="MainMenu",                                       ClassNum = 1 } },
            {"Gallery",                             new PlayerDef() {LongName="Gallery",                                        ClassNum = 2 } },
            {"Mission",                             new PlayerDef() {LongName="Mission",                                        ClassNum = 3 } },
            {"Options",                             new PlayerDef() {LongName="Options",                                        ClassNum = 4 } },
            {"PlayerData",                          new PlayerDef() {LongName="PlayerData",                                     ClassNum = 5 } },
            {"Result_VersusSingle",                 new PlayerDef() {LongName="Result_VersusSingle",                            ClassNum = 6 } },
            {"Tutorial",                            new PlayerDef() {LongName="Tutorial",                                       ClassNum = 7 } },
            {"Versus",                              new PlayerDef() {LongName="Versus",                                         ClassNum = 8 } },
            {"ControllerSelect_VersusSingle",       new PlayerDef() {LongName="ControllerSelect_VersusSingle",                  ClassNum = 9 } },
            {"ControllerSelect_VersusTeam",         new PlayerDef() {LongName="ControllerSelect_VersusTeam",                    ClassNum = 10 } },
            {"MemberSelect_Story",                  new PlayerDef() {LongName="MemberSelect_Story",                             ClassNum = 11 } },
            {"MemberSelect_Training",               new PlayerDef() {LongName="MemberSelect_Training",                          ClassNum = 12 } },
            {"MemberSelect_VersusSingle",           new PlayerDef() {LongName="MemberSelect_VersusSingle",                      ClassNum = 13 } },
            {"MemberSelect_VersusTeam",             new PlayerDef() {LongName="MemberSelect_VersusTeam",                        ClassNum = 14 } },
            {"StageSelect_VersusSingle",            new PlayerDef() {LongName="StageSelect_VersusSingle",                       ClassNum = 15 } },
        };
        #endregion

        #region ResultMatch
        public static Dictionary<string, PlayerDef> ResultMatch = new Dictionary<string, PlayerDef>
        {
            {"_BlackFull",                          new PlayerDef() {LongName="_BlackFull",                                     ClassNum = -1 } },
            {"_WhiteFull",                          new PlayerDef() {LongName="_WhiteFull",                                     ClassNum = -2 } },

            {"Player1Win",                          new PlayerDef() {LongName="Player1Win",                                     ClassNum = 1 } },
            {"Player1Lose",                         new PlayerDef() {LongName="Player1Lose",                                    ClassNum = 2 } },
            {"Player2Win",                          new PlayerDef() {LongName="Player2Win",                                     ClassNum = 3 } },
            {"Player2Lose",                         new PlayerDef() {LongName="Player2Lose",                                    ClassNum = 4 } },
        };
        #endregion

        #region CurrentTime
        public static Dictionary<string, PlayerDef> CurrentTime = new Dictionary<string, PlayerDef>
        {
              {"_BlackFull",                        new PlayerDef() {LongName="_BlackFull",                                     ClassNum = -1 } },
              {"_WhiteFull",                        new PlayerDef() {LongName="_WhiteFull",                                     ClassNum = -2 } },

              {"00",                                new PlayerDef() {LongName="00",                                             ClassNum = 00 } },
              {"01",                                new PlayerDef() {LongName="01",                                             ClassNum = 01 } },
              {"02",                                new PlayerDef() {LongName="02",                                             ClassNum = 02 } },
              {"03",                                new PlayerDef() {LongName="03",                                             ClassNum = 03 } },
              {"04",                                new PlayerDef() {LongName="04",                                             ClassNum = 04 } },
              {"05",                                new PlayerDef() {LongName="05",                                             ClassNum = 05 } },
              {"06",                                new PlayerDef() {LongName="06",                                             ClassNum = 06 } },
              {"07",                                new PlayerDef() {LongName="07",                                             ClassNum = 07 } },
              {"08",                                new PlayerDef() {LongName="08",                                             ClassNum = 08 } },
              {"09",                                new PlayerDef() {LongName="09",                                             ClassNum = 09 } },

              {"10",                                new PlayerDef() {LongName="10",                                             ClassNum = 10 } },
              {"11",                                new PlayerDef() {LongName="11",                                             ClassNum = 11 } },
              {"12",                                new PlayerDef() {LongName="12",                                             ClassNum = 12 } },
              {"13",                                new PlayerDef() {LongName="13",                                             ClassNum = 13 } },
              {"14",                                new PlayerDef() {LongName="14",                                             ClassNum = 14 } },
              {"15",                                new PlayerDef() {LongName="15",                                             ClassNum = 15 } },
              {"16",                                new PlayerDef() {LongName="16",                                             ClassNum = 16 } },
              {"17",                                new PlayerDef() {LongName="17",                                             ClassNum = 17 } },
              {"18",                                new PlayerDef() {LongName="18",                                             ClassNum = 18 } },
              {"19",                                new PlayerDef() {LongName="19",                                             ClassNum = 19 } },

              {"20",                                new PlayerDef() {LongName="20",                                             ClassNum = 20 } },
              {"21",                                new PlayerDef() {LongName="21",                                             ClassNum = 21 } },
              {"22",                                new PlayerDef() {LongName="22",                                             ClassNum = 22 } },
              {"23",                                new PlayerDef() {LongName="23",                                             ClassNum = 23 } },
              {"24",                                new PlayerDef() {LongName="24",                                             ClassNum = 24 } },
              {"25",                                new PlayerDef() {LongName="25",                                             ClassNum = 25 } },
              {"26",                                new PlayerDef() {LongName="26",                                             ClassNum = 26 } },
              {"27",                                new PlayerDef() {LongName="27",                                             ClassNum = 27 } },
              {"28",                                new PlayerDef() {LongName="28",                                             ClassNum = 28 } },
              {"29",                                new PlayerDef() {LongName="29",                                             ClassNum = 29 } },

              {"30",                                new PlayerDef() {LongName="30",                                             ClassNum = 30 } },
              {"31",                                new PlayerDef() {LongName="31",                                             ClassNum = 31 } },
              {"32",                                new PlayerDef() {LongName="32",                                             ClassNum = 32 } },
              {"33",                                new PlayerDef() {LongName="33",                                             ClassNum = 33 } },
              {"34",                                new PlayerDef() {LongName="34",                                             ClassNum = 34 } },
              {"35",                                new PlayerDef() {LongName="35",                                             ClassNum = 35 } },
              {"36",                                new PlayerDef() {LongName="36",                                             ClassNum = 36 } },
              {"37",                                new PlayerDef() {LongName="37",                                             ClassNum = 37 } },
              {"38",                                new PlayerDef() {LongName="38",                                             ClassNum = 38 } },
              {"39",                                new PlayerDef() {LongName="39",                                             ClassNum = 39 } },

              {"40",                                new PlayerDef() {LongName="40",                                             ClassNum = 40 } },
              {"41",                                new PlayerDef() {LongName="41",                                             ClassNum = 41 } },
              {"42",                                new PlayerDef() {LongName="42",                                             ClassNum = 42 } },
              {"43",                                new PlayerDef() {LongName="43",                                             ClassNum = 43 } },
              {"44",                                new PlayerDef() {LongName="44",                                             ClassNum = 44 } },
              {"45",                                new PlayerDef() {LongName="45",                                             ClassNum = 45 } },
              {"46",                                new PlayerDef() {LongName="46",                                             ClassNum = 46 } },
              {"47",                                new PlayerDef() {LongName="47",                                             ClassNum = 47 } },
              {"48",                                new PlayerDef() {LongName="48",                                             ClassNum = 48 } },
              {"49",                                new PlayerDef() {LongName="49",                                             ClassNum = 49 } },

              {"50",                                new PlayerDef() {LongName="50",                                             ClassNum = 50 } },
              {"51",                                new PlayerDef() {LongName="51",                                             ClassNum = 51 } },
              {"52",                                new PlayerDef() {LongName="52",                                             ClassNum = 52 } },
              {"53",                                new PlayerDef() {LongName="53",                                             ClassNum = 53 } },
              {"54",                                new PlayerDef() {LongName="54",                                             ClassNum = 54 } },
              {"55",                                new PlayerDef() {LongName="55",                                             ClassNum = 55 } },
              {"56",                                new PlayerDef() {LongName="56",                                             ClassNum = 56 } },
              {"57",                                new PlayerDef() {LongName="57",                                             ClassNum = 57 } },
              {"58",                                new PlayerDef() {LongName="58",                                             ClassNum = 58 } },
              {"59",                                new PlayerDef() {LongName="59",                                             ClassNum = 59 } },

              {"60",                                new PlayerDef() {LongName="60",                                             ClassNum = 60 } },
              {"61",                                new PlayerDef() {LongName="61",                                             ClassNum = 61 } },
              {"62",                                new PlayerDef() {LongName="62",                                             ClassNum = 62 } },
              {"63",                                new PlayerDef() {LongName="63",                                             ClassNum = 63 } },
              {"64",                                new PlayerDef() {LongName="64",                                             ClassNum = 64 } },
              {"65",                                new PlayerDef() {LongName="65",                                             ClassNum = 65 } },
              {"66",                                new PlayerDef() {LongName="66",                                             ClassNum = 66 } },
              {"67",                                new PlayerDef() {LongName="67",                                             ClassNum = 67 } },
              {"68",                                new PlayerDef() {LongName="68",                                             ClassNum = 68 } },
              {"69",                                new PlayerDef() {LongName="69",                                             ClassNum = 69 } },

              {"70",                                new PlayerDef() {LongName="70",                                             ClassNum = 70 } },
              {"71",                                new PlayerDef() {LongName="71",                                             ClassNum = 71 } },
              {"72",                                new PlayerDef() {LongName="72",                                             ClassNum = 72 } },
              {"73",                                new PlayerDef() {LongName="73",                                             ClassNum = 73 } },
              {"74",                                new PlayerDef() {LongName="74",                                             ClassNum = 74 } },
              {"75",                                new PlayerDef() {LongName="75",                                             ClassNum = 75 } },
              {"76",                                new PlayerDef() {LongName="76",                                             ClassNum = 76 } },
              {"77",                                new PlayerDef() {LongName="77",                                             ClassNum = 77 } },
              {"78",                                new PlayerDef() {LongName="78",                                             ClassNum = 78 } },
              {"79",                                new PlayerDef() {LongName="79",                                             ClassNum = 79 } },

              {"80",                                new PlayerDef() {LongName="80",                                             ClassNum = 80 } },
              {"81",                                new PlayerDef() {LongName="81",                                             ClassNum = 81 } },
              {"82",                                new PlayerDef() {LongName="82",                                             ClassNum = 82 } },
              {"83",                                new PlayerDef() {LongName="83",                                             ClassNum = 83 } },
              {"84",                                new PlayerDef() {LongName="84",                                             ClassNum = 84 } },
              {"85",                                new PlayerDef() {LongName="85",                                             ClassNum = 85 } },
              {"86",                                new PlayerDef() {LongName="86",                                             ClassNum = 86 } },
              {"87",                                new PlayerDef() {LongName="87",                                             ClassNum = 87 } },
              {"88",                                new PlayerDef() {LongName="88",                                             ClassNum = 88 } },
              {"89",                                new PlayerDef() {LongName="89",                                             ClassNum = 89 } },

              {"90",                                new PlayerDef() {LongName="90",                                             ClassNum = 90 } },
              {"91",                                new PlayerDef() {LongName="91",                                             ClassNum = 91 } },
              {"92",                                new PlayerDef() {LongName="92",                                             ClassNum = 92 } },
              {"93",                                new PlayerDef() {LongName="93",                                             ClassNum = 93 } },
              {"94",                                new PlayerDef() {LongName="94",                                             ClassNum = 94 } },
              {"95",                                new PlayerDef() {LongName="95",                                             ClassNum = 95 } },
              {"96",                                new PlayerDef() {LongName="96",                                             ClassNum = 96 } },
              {"97",                                new PlayerDef() {LongName="97",                                             ClassNum = 97 } },
              {"98",                                new PlayerDef() {LongName="98",                                             ClassNum = 98 } },
              {"99",                                new PlayerDef() {LongName="99",                                             ClassNum = 99 } },
        };
        #endregion

        #region PlayerRoundWins
        public static Dictionary<string, PlayerDef> PlayerRoundWins = new Dictionary<string, PlayerDef>
        {
            {"_BlackFull",                          new PlayerDef() {LongName="_BlackFull",                                     ClassNum = -1 } },
            {"_WhiteFull",                          new PlayerDef() {LongName="_WhiteFull",                                     ClassNum = -2 } },
            {"_Dirty",                              new PlayerDef() {LongName="_Dirty",                                         ClassNum = -3 } },

            {"Player1Win",                          new PlayerDef() {LongName="Player1Win",                                     ClassNum = 1 } },
            {"Player2Win",                          new PlayerDef() {LongName="Player2Win",                                     ClassNum = 2 } },
            {"PerfectGame",                         new PlayerDef() {LongName="PerfectGame",                                    ClassNum = 3 } },
        };
        #endregion
    }

    public enum ImageKind
    {
        Ingame_Player1Name,
        Ingame_Player2Name,
        OnSelect_Player1Name,
        OnSelect_Player2Name,
        TitleMenu, KoGoMessage,
        Result_Player1,
        Result_Player2,
        PlayerWins,
        RoundReadyMessage,
        CurrentTime
    }
    public static class ImageFormat
    {

        public class ImageParamsDef
        {
            public string AimPath { get; set; }
            public IReadOnlyDictionary<string, PlayersEnum.PlayerDef> Dict { get; set; }
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
                ColorLowerThreshold =   new Bgr(170, 160, 0),     ColorHigherThreshold =    new Bgr(230, 225, 100),
                MaskLowerThreshold =    new Bgr(170, 160, 0),     MaskHigherThreshold =     new Bgr(230, 225, 100),
                XPos = 35,       YPos = 110,      Width = 125,     Height = 15,      Scale = 1.0f,      Dict=PlayersEnum.Players }
            },

            {
                ImageKind.Ingame_Player2Name,               new ImageParamsDef { AimPath = @"d:\Q4Vid\Players\Images\Player2\",
                ColorLowerThreshold =   new Bgr(0, 190, 95),     ColorHigherThreshold =     new Bgr(95, 250, 150),
                MaskLowerThreshold =    new Bgr(0, 190, 95),     MaskHigherThreshold =      new Bgr(95, 250, 150),
                XPos = 1760,       YPos = 110,      Width = 125,     Height = 15,      Scale = 1.0f,      Dict=PlayersEnum.Players }
            },

            {
                ImageKind.OnSelect_Player1Name,             new ImageParamsDef { AimPath = @"D:\Q4Vid\ChoosePlayers\Images\Player1",
                ColorLowerThreshold =   new Bgr(170, 160, 0),   ColorHigherThreshold =      new Bgr(230, 225, 100),
                MaskLowerThreshold =    new Bgr(170, 160, 0),   MaskHigherThreshold =       new Bgr(230, 225, 100),
                XPos = 25,      YPos =  895,        Width = 410,    Height = 25,    Scale = 0.5f,      Dict=PlayersEnum.Players }
            },

            {
                ImageKind.OnSelect_Player2Name,             new ImageParamsDef { AimPath = @"D:\Q4Vid\ChoosePlayers\Images\Player2",
                ColorLowerThreshold =   new Bgr(0, 190, 95),     ColorHigherThreshold =     new Bgr(95, 250, 150),
                MaskLowerThreshold =    new Bgr(0, 190, 95),     MaskHigherThreshold =      new Bgr(95, 250, 150),
                XPos = 1475,      YPos =  895,        Width = 410,    Height = 25,    Scale = 0.5f,      Dict=PlayersEnum.Players }
            },

            {
                ImageKind.TitleMenu,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\Titles",
                ColorLowerThreshold =   new Bgr(5, 150, 215),   ColorHigherThreshold =      new Bgr(90, 200, 246),
                MaskLowerThreshold =    new Bgr(5, 150, 215),   MaskHigherThreshold =       new Bgr(90, 200, 246),
                XPos = 100,      YPos =  30,        Width = 550,    Height = 50,    Scale = 0.5f,       Dict=PlayersEnum.Titles}
            },

            {
                ImageKind.KoGoMessage,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\KoGoMessage",
                //ColorLowerThreshold =   new Bgr(140, 240, 240), ColorHigherThreshold =      new Bgr(/*205*//*190*/255, 255, 255),
                ColorLowerThreshold =    new Bgr(0, 0, 40),      ColorHigherThreshold =       new Bgr(30, 140, 255),
                    //MaskLowerThreshold =    new Bgr(0, 0, 40),      MaskHigherThreshold =       new Bgr(10, 10, 120),
                MaskLowerThreshold =    new Bgr(0, 0, 40),      MaskHigherThreshold =       new Bgr(30, 140, 255),
                XPos = 515,      YPos =  435,        Width = 885,    Height = 210,    Scale = 0.5f}
            },
            //
            {
                ImageKind.Result_Player1,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\Result\Player1",
                ColorLowerThreshold =   new Bgr(0, 0, 0),       ColorHigherThreshold =      new Bgr(60, 45, 35),
                MaskLowerThreshold =    new Bgr(0, 0, 0),       MaskHigherThreshold =       new Bgr(60, 45, 35),
                XPos = 15,      YPos =  760,        Width = 415,    Height = 65,    Scale = 0.5f,       Dict=PlayersEnum.ResultMatch}
            },

            {
                ImageKind.Result_Player2,                        new ImageParamsDef { AimPath = @"D:\Q4Vid\Result\Player2",
                ColorLowerThreshold =   new Bgr(0, 0, 0),       ColorHigherThreshold =      new Bgr(60, 45, 35),
                MaskLowerThreshold =    new Bgr(0, 0, 0),       MaskHigherThreshold =       new Bgr(60, 45, 35),
                XPos = 1445,      YPos =  760,        Width = 415,    Height = 65,    Scale = 0.5f,     Dict=PlayersEnum.ResultMatch}
            },

            {
                ImageKind.PlayerWins,                           new ImageParamsDef { AimPath = @"D:\Q4Vid\PlayerWins",
                ColorLowerThreshold =   new Bgr(40, 220, 255),  ColorHigherThreshold =      new Bgr(170, 255, 255),
                //MaskLowerThreshold =   new Bgr(40, 220, 255),  MaskHigherThreshold =      new Bgr(170, 255, 255),
                //ColorLowerThreshold =    new Bgr(0, 10, 60),     ColorHigherThreshold =       new Bgr(20, 120, 164),
                MaskLowerThreshold =    new Bgr(0, 0, 40),     MaskHigherThreshold =       new Bgr(20, 113, 255),
                XPos = 240,      YPos =  505,        Width = 1450,    Height = 70,    Scale = 0.25f,     Dict=PlayersEnum.PlayerRoundWins}
            },

            {
                ImageKind.RoundReadyMessage,                    new ImageParamsDef { AimPath = @"D:\Q4Vid\RoundReady",
                ColorLowerThreshold =   new Bgr(246, 222, 46),  ColorHigherThreshold =      new Bgr(255, 255, 210),
                //MaskLowerThreshold =   new Bgr(246, 222, 46),  MaskHigherThreshold =      new Bgr(255, 255, 210),
                //ColorLowerThreshold =    new Bgr(90, 0, 0),    ColorHigherThreshold =       new Bgr(200, 100, 30),
                MaskLowerThreshold =    new Bgr(90, 0, 0),    MaskHigherThreshold =       new Bgr(200, 100, 30),
                XPos = 350,      YPos =  505,        Width = 1215,    Height = 70,    Scale = 0.5f}
            },

            {
                ImageKind.CurrentTime,                          new ImageParamsDef { AimPath = @"D:\Q4Vid\CurrentTime",
                ColorLowerThreshold =   new Bgr(180, 160, 140),  ColorHigherThreshold =      new Bgr(230, 230, 210),
                MaskLowerThreshold =    new Bgr(210, 210, 210),  MaskHigherThreshold =       new Bgr(255, 255, 255),
                //ColorLowerThreshold =    new Bgr(0, 50, 100),  ColorHigherThreshold =       new Bgr(80, 160, 250),
                //MaskLowerThreshold =   new Bgr(0, 0, 0),  MaskHigherThreshold =      new Bgr(60, 50, 80),
                XPos = 875,      YPos =  55,        Width = 170,    Height = 75,    Scale = 0.5f,       Dict=PlayersEnum.CurrentTime}
            },
    };
    }
}
