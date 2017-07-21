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
        private string CannyFileName;

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
                    IImage img;
                    //MessageBox.Show($"File chosen:{dlg.FileName}");
                    if (mItem.Name == "FileOpenHeader_InRangeTab")
                    {
                        xFileName = dlg.FileName;
                        img = new Image<Rgb, byte>(xFileName);
                        ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                    }
                    else if (mItem.Name == "FileOpenHeader_ThresHoldTab")
                    {
                        xFileNameThres = dlg.FileName;
                        if (ThresHold_Color_CheckBox.IsChecked == true)
                            img = new Image<Rgb, byte>(xFileNameThres);
                        else
                            img = new Image<Gray, byte>(xFileNameThres);

                        ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
                    }
                    else if (mItem.Name == "FileOpenHeader_CannyTab")
                    {
                        CannyFileName = dlg.FileName;
                        img = new Image<Rgb, byte>(CannyFileName);
                        CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
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
                IImage img;
                if (ThresHold_Color_CheckBox.IsChecked == true)
                    img = new Image<Bgr, byte>(xFileNameThres);
                else
                    img = new Image<Gray, byte>(xFileNameThres);
                ThresHoldStruct curStruct = (ThresHoldStruct)ThresHold_Combobox.SelectedItem;
                //var y = ThresHold_Combobox.SelectedValue;
                curStruct.ThresHoldBlue = ThresHoldColor_blue.Value;
                curStruct.ThresHoldGreenOrGray = ThresHoldColor_green.Value;
                curStruct.ThresHoldRed = ThresHoldColor_red.Value;

                curStruct.MaxValueBlue = ThresHoldMaxValue_blue.Value;
                curStruct.MaxValueGreenOrGray = ThresHoldMaxValue_green.Value;
                curStruct.MaxValueRed = ThresHoldMaxValue_red.Value;

                switch (curStruct.Name)
                {
                    case "ThresHoldBinary":
                        if (ThresHold_Color_CheckBox.IsChecked == true)
                        {
                            ((Image<Bgr, byte>)img)._ThresholdBinary(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreenOrGray, curStruct.ThresHoldRed)
                                , new Bgr(curStruct.MaxValueBlue, curStruct.MaxValueGreenOrGray, curStruct.MaxValueRed));
                        }
                        else
                            ((Image<Gray, byte>)img)._ThresholdBinary(new Gray(curStruct.ThresHoldGreenOrGray), new Gray(curStruct.MaxValueGreenOrGray));
                        break;
                    case "ThresHoldBinaryInv":
                        if (ThresHold_Color_CheckBox.IsChecked == true)
                        {
                            ((Image<Bgr, byte>)img)._ThresholdBinaryInv(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreenOrGray, curStruct.ThresHoldRed)
                            , new Bgr(curStruct.MaxValueBlue, curStruct.MaxValueGreenOrGray, curStruct.MaxValueRed));
                        }
                        else
                            ((Image<Gray, byte>)img)._ThresholdBinaryInv(new Gray(curStruct.ThresHoldGreenOrGray), new Gray(curStruct.MaxValueGreenOrGray));
                        break;
                    case "ThresHoldToZeroInv":
                        if (ThresHold_Color_CheckBox.IsChecked == true)
                            ((Image<Bgr, byte>)img)._ThresholdToZeroInv(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreenOrGray, curStruct.ThresHoldRed));
                        else
                            ((Image<Gray, byte>)img)._ThresholdToZeroInv(new Gray(curStruct.ThresHoldGreenOrGray));
                        break;
                    case "ThresHoldToZero":
                        if (ThresHold_Color_CheckBox.IsChecked == true)
                            ((Image<Bgr, byte>)img)._ThresholdToZero(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreenOrGray, curStruct.ThresHoldRed));
                        else
                            ((Image<Gray, byte>)img)._ThresholdToZero(new Gray(curStruct.ThresHoldGreenOrGray));
                        break;
                    case "ThresHoldTrunc":
                        if (ThresHold_Color_CheckBox.IsChecked == true)
                            ((Image<Bgr, byte>)img)._ThresholdTrunc(new Bgr(curStruct.ThresHoldBlue, curStruct.ThresHoldGreenOrGray, curStruct.ThresHoldRed));
                        else
                            ((Image<Gray, byte>)img)._ThresholdTrunc(new Gray(curStruct.ThresHoldGreenOrGray));
                        break;
                    case "ThresHoldAdaptive":

                        break;

                }
                ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
                img.Dispose();
            }
        }

        private void Canny_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Image<Gray, byte> img = new Image<Bgr, byte>(CannyFileName).Canny(CannyTab_ThresholdParam.Value, CannyTab_ThresholdLinkingParam.Value, 3, true);
                CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                img.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }

    public class ThresHoldStruct
    {
        string _name;
        double _ThresHoldBlueValue;
        double _ThresHoldGreenOrGrayValue;
        double _ThresHoldRedValue;

        double _MaxValueBlue;
        double _MaxValueGreenOrGray;
        double _MaxValueRed;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double[] GetBGR
        {
            get { return new double[3] { _ThresHoldBlueValue, _ThresHoldGreenOrGrayValue, _ThresHoldRedValue }; }
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

        public double ThresHoldGreenOrGray
        {
            get { return _ThresHoldGreenOrGrayValue; }
            set { _ThresHoldGreenOrGrayValue = value; }
        }

        public double MaxValueBlue
        {
            get { return _MaxValueBlue; }
            set { _MaxValueBlue = value; }
        }

        public double MaxValueGreenOrGray
        {
            get { return _MaxValueGreenOrGray; }
            set { _MaxValueGreenOrGray = value; }
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
            _ThresHoldGreenOrGrayValue = 0;
            _ThresHoldRedValue = 0;
            _MaxValueBlue = 0;
            _MaxValueGreenOrGray = 0;
            _MaxValueRed = 0;
        }
    }
}
