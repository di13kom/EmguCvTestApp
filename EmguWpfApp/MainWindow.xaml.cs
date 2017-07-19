using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmguWpfApp;
using System.Collections.ObjectModel;

namespace EmguWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string xFileName;
        private string xFileNameThres;
        public ObservableCollection<ThresHoldStruct> ThresHoldCollection = new ObservableCollection<ThresHoldStruct>
        {
            new ThresHoldStruct("ThresHoldAdaptive"),
            new ThresHoldStruct("ThresHoldBinaryInv"),
            new ThresHoldStruct("ThresHoldBinary"),
            new ThresHoldStruct("ThresHoldToZeroInv"),
            new ThresHoldStruct("ThresHoldToZero"),
            new ThresHoldStruct("ThresHoldTrunc"),
        };

        public MainWindow()
        {
            InitializeComponent();
            ThresHold_Combobox.ItemsSource = ThresHoldCollection;
            ThresHold_Combobox.DisplayMemberPath = "Name";
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mItem = (MenuItem)sender;
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "*.png";
                dlg.Filter = "BMP Files(*.bmp) | *.bmp|JPEG Files(*.jpeg) | *.jpeg| PNG Files(*.png) | *.png| JPG Files(*.jpg) | *.jpg| GIF Files(*.gif) | *.gif";
                dlg.Multiselect = false;
                bool? res = dlg.ShowDialog();

                if (res == true)
                {
                    //MessageBox.Show($"File chosen:{dlg.FileName}");
                    if (mItem.Name == "FileOpenHeader_InRangeTab")
                    {
                        xFileName = dlg.FileName;
                        Image<Rgb, byte> img = new Image<Rgb, byte>(xFileName);
                        ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                    }
                    else if (mItem.Name == "FileOpenHeader_ThresHoldTab")
                    {
                        xFileNameThres = dlg.FileName;
                        Image<Rgb, byte> img = new Image<Rgb, byte>(xFileNameThres);
                        ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (string.IsNullOrEmpty(xFileName) != true)
                {
                    int colMaxR = Convert.ToInt32(ColorMax_red.Value);
                    int colMaxG = Convert.ToInt32(ColorMax_green.Value);
                    int colMaxB = Convert.ToInt32(ColorMax_blue.Value);

                    int colMinR = Convert.ToInt32(ColorMin_red.Value);
                    int colMinG = Convert.ToInt32(ColorMin_green.Value);
                    int colMinB = Convert.ToInt32(ColorMin_blue.Value);

                    Image<Gray, byte> img = new Image<Bgr, byte>(xFileName).InRange(new Bgr(colMinB, colMinG, colMinR), new Bgr(colMaxB, colMaxG, colMaxR));
                    if (ColorMask_Sync.IsChecked == false)
                    {
                        int maskMaxR = Convert.ToInt32(MaskMax_red.Value);
                        int maskMaxG = Convert.ToInt32(MaskMax_green.Value);
                        int maskMaxB = Convert.ToInt32(MaskMax_blue.Value);

                        int maskMinR = Convert.ToInt32(MaskMin_red.Value);
                        int maskMinG = Convert.ToInt32(MaskMin_green.Value);
                        int maskMinB = Convert.ToInt32(MaskMin_blue.Value);

                        Image<Gray, byte> mask = new Image<Bgr, byte>(xFileName).InRange(new Bgr(maskMinB, maskMinG, maskMinR), new Bgr(maskMaxB, maskMaxG, maskMaxR));
                        img = img.Or(mask);
                        mask.Dispose();
                    }
                    ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                    img.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ThresHold_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Slider curSlider = (Slider)sender;
            if (ThresHold_Combobox.SelectedValue != null && string.IsNullOrEmpty(xFileNameThres) == false)
            {
                Image<Bgr, byte> img = new Image<Bgr, byte>(xFileNameThres);
                ThresHoldStruct curStruct = (ThresHoldStruct)ThresHold_Combobox.SelectedItem;
                //var y = ThresHold_Combobox.SelectedValue;
                curStruct.ThresHoldBlue = ThresHoldColor_blue.Value;
                curStruct.ThresHoldGreen = ThresHoldColor_green.Value;
                curStruct.ThresHoldRed = ThresHoldColor_red.Value;

                curStruct.MaxValueBlue = ThresHoldMaxValue_blue.Value;
                curStruct.MaxValueGreen = ThresHoldMaxValue_green.Value;
                curStruct.MaxValueRed = ThresHoldMaxValue_red.Value;

                switch (curStruct.Name)
                {
                    case "ThresHoldBinary":
                        img._ThresholdBinary(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreen, curStruct.ThresHoldRed)
                            , new Bgr(curStruct.MaxValueBlue, curStruct.MaxValueGreen, curStruct.MaxValueRed));
                        break;
                    case "ThresHoldBinaryInv":
                        img._ThresholdBinaryInv(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreen, curStruct.ThresHoldRed)
                            , new Bgr(curStruct.MaxValueBlue, curStruct.MaxValueGreen, curStruct.MaxValueRed));
                        break;
                    case "ThresHoldToZeroInv":
                        img._ThresholdToZeroInv(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreen, curStruct.ThresHoldRed));
                        break;
                    case "ThresHoldToZero":
                        img._ThresholdToZero(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreen, curStruct.ThresHoldRed));
                        break;
                    case "ThresHoldTrunc":
                        img._ThresholdTrunc(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreen, curStruct.ThresHoldRed));
                        break;
                    case "ThresHoldAdaptive":

                        break;

                }
                ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
                img.Dispose();
            }
        }
    }

    public class ThresHoldStruct
    {
        string _name;
        double _ThresHoldBlueValue;
        double _ThresHoldGreenValue;
        double _ThresHoldRedValue;

        double _MaxValueBlue;
        double _MaxValueGreen;
        double _MaxValueRed;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double[] GetBGR
        {
            get { return new double[3] { _ThresHoldBlueValue, _ThresHoldGreenValue, _ThresHoldRedValue }; }
        }
        public double ThresHoldBlue
        {
            get { return _ThresHoldBlueValue; }
            set { _ThresHoldBlueValue = value; }
        }

        public double ThresHoldRed
        {
            get { return _ThresHoldRedValue; }
            set { _ThresHoldRedValue = value; }
        }

        public double ThresHoldGreen
        {
            get { return _ThresHoldGreenValue; }
            set { _ThresHoldGreenValue = value; }
        }

        public double MaxValueBlue
        {
            get { return _MaxValueBlue; }
            set { _MaxValueBlue = value; }
        }

        public double MaxValueGreen
        {
            get { return _MaxValueGreen; }
            set { _MaxValueGreen = value; }
        }

        public double MaxValueRed
        {
            get { return _MaxValueRed; }
            set { _MaxValueRed = value; }
        }

        public ThresHoldStruct(string name)
        {
            Name = name;
            _ThresHoldBlueValue = 0;
            _ThresHoldGreenValue = 0;
            _ThresHoldRedValue = 0;
            _MaxValueBlue = 0;
            _MaxValueGreen = 0;
            _MaxValueRed = 0;
        }
    }
}
