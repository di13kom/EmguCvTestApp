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
}
