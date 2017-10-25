using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmguWpfApp
{
    public class ThresHoldStruct : INotifyPropertyChanged
    {
        bool _isEnabled;
        bool _isColorsEnabled = false;
        string _currentType;
        double _ThresHoldBlueValue = 0;
        double _ThresHoldGreenOrGrayValue = 0;
        double _ThresHoldRedValue = 0;

        double _MaxValueBlue = 0;
        double _MaxValueGreenOrGray = 0;
        double _MaxValueRed = 0;

        public ObservableCollection<string> AvailibleThresholdTypes = new ObservableCollection<string>
        {
            //"ThresHoldAdaptive",
            "ThresHoldBinaryInv",
            "ThresHoldBinary",
            "ThresHoldToZeroInv",
            "ThresHoldToZero",
            "ThresHoldTrunc"
        };

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                NotifyPropertyChanged("IsEnabled");
            }
        }

        public bool IsColorEnabled
        {
            get { return _isColorsEnabled; }
            set
            {
                _isColorsEnabled = value;
                NotifyPropertyChanged("IsColorEnabled");
            }
        }

        public string CurrentThresHoldType
        {
            get { return _currentType; }
            set
            {
                _currentType = value;
                NotifyPropertyChanged("CurrentThresHoldType");
            }
        }
        public double[] GetBGR
        {
            get { return new double[3] { _ThresHoldBlueValue, _ThresHoldGreenOrGrayValue, _ThresHoldRedValue }; }
        }

        public double ThresHoldBlue
        {
            get { return _ThresHoldBlueValue; }
            set
            {
                _ThresHoldBlueValue = value;
                NotifyPropertyChanged("ThresHoldBlue");
            }
        }

        public double ThresHoldRed
        {
            get { return _ThresHoldRedValue; }
            set
            {
                _ThresHoldRedValue = value;
                NotifyPropertyChanged("ThresHoldRed");
            }
        }

        public double ThresHoldGreenOrGray
        {
            get { return _ThresHoldGreenOrGrayValue; }
            set
            {
                _ThresHoldGreenOrGrayValue = value;
                NotifyPropertyChanged("ThresHoldGreenOrGray");
            }
        }

        public double MaxValueBlue
        {
            get { return _MaxValueBlue; }
            set
            {
                _MaxValueBlue = value;
                NotifyPropertyChanged("MaxValueBlue");
            }
        }

        public double MaxValueGreenOrGray
        {
            get { return _MaxValueGreenOrGray; }
            set
            {
                _MaxValueGreenOrGray = value;
                NotifyPropertyChanged("MaxValueGreenOrGray");
            }
        }

        public double MaxValueRed
        {
            get { return _MaxValueRed; }
            set
            {
                _MaxValueRed = value;
                NotifyPropertyChanged("MaxValueRed");
            }
        }

        public ThresHoldStruct()
        {
            CurrentThresHoldType = AvailibleThresholdTypes[1];
        }

        public IImage ProccessImage(Image<Bgr, byte> inImage)
        {
            IImage img = null;
            switch (CurrentThresHoldType)
            {
                case "ThresHoldBinary":
                    if (IsColorEnabled == true)
                    {
                        img = inImage.ThresholdBinary(new Bgr(ThresHoldBlue, ThresHoldGreenOrGray, ThresHoldRed)
                             , new Bgr(MaxValueBlue, MaxValueGreenOrGray, MaxValueRed));
                    }
                    else
                        img = inImage.Convert<Gray, byte>().ThresholdBinary(new Gray(ThresHoldGreenOrGray), new Gray(MaxValueGreenOrGray));
                    break;
                case "ThresHoldBinaryInv":
                    if (IsColorEnabled == true)
                    {
                        img = inImage.ThresholdBinaryInv(new Bgr(ThresHoldBlue, ThresHoldGreenOrGray, ThresHoldRed)
                        , new Bgr(MaxValueBlue, MaxValueGreenOrGray, MaxValueRed));
                    }
                    else
                        img = inImage.Convert<Gray, byte>().ThresholdBinaryInv(new Gray(ThresHoldGreenOrGray), new Gray(MaxValueGreenOrGray));
                    break;
                case "ThresHoldToZeroInv":
                    if (IsColorEnabled == true)
                        img = inImage.ThresholdToZeroInv(new Bgr(ThresHoldBlue, ThresHoldGreenOrGray, ThresHoldRed));
                    else
                        img = inImage.Convert<Gray, byte>().ThresholdToZeroInv(new Gray(ThresHoldGreenOrGray));
                    break;
                case "ThresHoldToZero":
                    if (IsColorEnabled == true)
                        img = inImage.ThresholdToZero(new Bgr(ThresHoldBlue, ThresHoldGreenOrGray, ThresHoldRed));
                    else
                        img = inImage.Convert<Gray, byte>().ThresholdToZero(new Gray(ThresHoldGreenOrGray));
                    break;
                case "ThresHoldTrunc":
                    if (IsColorEnabled == true)
                        img = inImage.ThresholdTrunc(new Bgr(ThresHoldBlue, ThresHoldGreenOrGray, ThresHoldRed));
                    else
                        img = inImage.Convert<Gray, byte>().ThresholdTrunc(new Gray(ThresHoldGreenOrGray));
                    break;
                //case "ThresHoldAdaptive":

                //    break;
            }
            return img;
        }
    }

    public class CannyStruct : INotifyPropertyChanged
    {
        bool _isEnabled = false;
        double _thresholdParam;
        double _thresholdLinkingParam;
        int _apertureSize = 5;
        bool _i2Gradient = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
            //if (PropertyChanged != null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs(info));
            //}
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    NotifyPropertyChanged("IsEnabled");
                }
            }
        }

        public double ThresholdParam
        {
            get { return _thresholdParam; }
            set
            {
                if (value != _thresholdParam)
                {
                    _thresholdParam = value;
                    NotifyPropertyChanged("ThresholdParam");
                }
            }
        }

        public double ThresholdLinkingParam
        {
            get { return _thresholdLinkingParam; }
            set
            {
                if (value != _thresholdLinkingParam)
                {
                    _thresholdLinkingParam = value;
                    NotifyPropertyChanged("ThresholdLinkingParam");
                }
            }
        }

        public int ApertureSize
        {
            get { return _apertureSize; }
            set
            {
                if (value != _apertureSize)
                {
                    _apertureSize = value;
                    NotifyPropertyChanged("ApertureSize");
                }
            }
        }

        public bool I2Gradient
        {
            get { return _i2Gradient; }
            set
            {
                if (value != _i2Gradient)
                {
                    _i2Gradient = value;
                    NotifyPropertyChanged("I2Gradient");
                }
            }
        }

        public Image<Gray, byte> ProccessImage(Image<Bgr, byte> inImage)
        {
            Image<Gray, byte> img = inImage.Canny(ThresholdParam, ThresholdLinkingParam, ApertureSize, I2Gradient);
            return img;
        }
    }

    public class InRangeStruct : INotifyPropertyChanged
    {
        bool _isEnabled = false;
        bool _isColorMaskSync = true;
        /// <summary>
        /// MainColor
        /// </summary>
        int _colMaxR = 255;
        int _colMaxG = 255;
        int _colMaxB = 255;

        int _colMinR = 0;
        int _colMinG = 0;
        int _colMinB = 0;

        /// <summary>
        /// MaskColor
        /// </summary>
        int _maskMaxR = 255;
        int _maskMaxG = 255;
        int _maskMaxB = 255;

        int _maskMinR = 0;
        int _maskMinG = 0;
        int _maskMinB = 0;


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    NotifyPropertyChanged("IsEnabled");
                }
            }
        }

        public bool IsColorMaskSync
        {
            get { return _isColorMaskSync; }
            set
            {
                if (value != _isColorMaskSync)
                {
                    _isColorMaskSync = value;
                    NotifyPropertyChanged("IsColorMaskSync");
                }
            }
        }

        public int ColorMaxRed
        {
            get { return _colMaxR; }
            set
            {
                if (value != _colMaxR)
                {
                    _colMaxR = value;
                    NotifyPropertyChanged("ColorMaxRed");
                }
            }
        }

        public int ColorMaxGreen
        {
            get { return _colMaxG; }
            set
            {
                if (value != _colMaxG)
                {
                    _colMaxG = value;
                    NotifyPropertyChanged("ColorMaxGreen");
                }
            }
        }

        public int ColorMaxBlue
        {
            get { return _colMaxB; }
            set
            {
                if (value != _colMaxB)
                {
                    _colMaxB = value;
                    NotifyPropertyChanged("ColorMaxBlue");
                }
            }
        }

        public int ColorMinRed
        {
            get { return _colMinR; }
            set
            {
                if (value != _colMinR)
                {
                    _colMinR = value;
                    NotifyPropertyChanged("ColorMinRed");
                }
            }
        }

        public int ColorMinGreen
        {
            get { return _colMinG; }
            set
            {
                if (value != _colMinG)
                {
                    _colMinG = value;
                    NotifyPropertyChanged("ColorMinGreen");
                }
            }
        }

        public int ColorMinBlue
        {
            get { return _colMinB; }
            set
            {
                if (value != _colMinB)
                {
                    _colMinB = value;
                    NotifyPropertyChanged("ColorMinBlue");
                }
            }
        }

        public int MaskMaxRed
        {
            get { return _maskMaxR; }
            set
            {
                if (value != _maskMaxR)
                {
                    _maskMaxR = value;
                    NotifyPropertyChanged("MaskMaxRed");
                }
            }
        }

        public int MaskMaxGreen
        {
            get { return _maskMaxG; }
            set
            {
                if (value != _maskMaxG)
                {
                    _maskMaxG = value;
                    NotifyPropertyChanged("MaskMaxGreen");
                }
            }
        }

        public int MaskMaxBlue
        {
            get { return _maskMaxB; }
            set
            {
                if (value != _maskMaxB)
                {
                    _maskMaxB = value;
                    NotifyPropertyChanged("MaskMaxBlue");
                }
            }
        }

        public int MaskMinRed
        {
            get { return _maskMinR; }
            set
            {
                if (value != _maskMinR)
                {
                    _maskMinR = value;
                    NotifyPropertyChanged("MaskMinRed");
                }
            }
        }

        public int MaskMinGreen
        {
            get { return _maskMinG; }
            set
            {
                if (value != _maskMinG)
                {
                    _maskMinG = value;
                    NotifyPropertyChanged("MaskMinGreen");
                }
            }
        }

        public int MaskMinBlue
        {
            get { return _maskMinB; }
            set
            {
                if (value != _maskMinB)
                {
                    _maskMinB = value;
                    NotifyPropertyChanged("MaskMinBlue");
                }
            }
        }

        public Image<Gray, byte> ProccessImage(Image<Bgr, byte> inImage)
        {
            Image<Gray, byte> img = inImage.InRange(new Bgr(ColorMinBlue, ColorMinGreen, ColorMinRed)
                        , new Bgr(ColorMaxBlue, ColorMaxGreen, ColorMaxRed));
            if (IsColorMaskSync == false)
            {
                using (Image<Gray, byte> mask = inImage.InRange(new Bgr(MaskMinBlue, MaskMinGreen, MaskMinRed)
                    , new Bgr(MaskMaxBlue, MaskMaxGreen, MaskMaxRed)))
                {
                    img = img.Or(mask);
                }
            }
            return img;
        }
    }
}
