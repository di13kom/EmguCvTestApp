using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguLearnPredict
{
    public class StructureDef
    {
        //public string Name { get; set; }
        public string LongName { get; set; }
        public int ClassNum { get; set; }
    }

    #region PredictionDefinitionClass
    //public class ImageParamsDef
    //{
    //    public string AimPath { get; set; }
    //    public IReadOnlyDictionary<string, StructureDef> Dict { get; set; }
    //    public Bgr ColorLowerThreshold { get; set; }
    //    public Bgr ColorHigherThreshold { get; set; }
    //    public Bgr MaskLowerThreshold { get; set; }
    //    public Bgr MaskHigherThreshold { get; set; }
    //    public int XPos { get; set; }
    //    public int YPos { get; set; }
    //    public int Width { get; set; }
    //    public int Height { get; set; }
    //    public float Scale { get; set; }
    //}
    //public class imType
    //{
    //    public string ImageTypeName = string.Empty;
    //}

    //abstract public class PredictionDefinitionClass
    //{
    //    //public Dictionary<string, StructureDef> _predictionName;
    //    //protected Dictionary<ImageTypeName, ImageParamsDef> _imageParam;

    //    abstract public Dictionary<imType, ImageParamsDef> ImageParam();
    //    abstract public imType ImageTypeAccs();

    //}
    #endregion

    #region PredictionDefinitionClassXXX
    public class ImageParamsDefXXX
    {
        public string AimPath { get; set; }
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

    abstract public class PredictionDefinitionClassXXX
    {
        public PredictionDefinitionClassXXX(string name)
        {
            ImageTypeName = name;
        }

        public abstract string ImageTypeName
        {
            get;
            set;
        }

        public abstract ImageParamsDefXXX ImageParam
        {
            get;
        }
    }
    #endregion
}
