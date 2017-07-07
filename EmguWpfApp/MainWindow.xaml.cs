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

namespace EmguWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string xFileName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
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

                    xFileName = dlg.FileName;
                    Image<Rgb, byte> img = new Image<Rgb, byte>(xFileName);
                    ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
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
    }
}
