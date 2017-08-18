using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace CameraWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Capture cp = null;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            cp = new Capture(1);
            cp.SetCaptureProperty(CapProp.FrameWidth, 1920);
            cp.SetCaptureProperty(CapProp.FrameHeight, 1080);
            //CvInvoke.NamedWindow("win");
            cp.ImageGrabbed += Cp_ImageGrabbed;
            cp.Start();

        }

        private void Cp_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Capture cp = sender as Capture;
                Image<Bgr, byte> imgFrame = new Image<Bgr, byte>(1920, 1080);

                cp.Retrieve(imgFrame);

                //var x  = imgFrame.GetSubRect(new System.Drawing.Rectangle(260, 720, 500, 600)).SmoothGaussian(43);
                //x.CopyTo(imgFrame);

                Dispatcher.Invoke(() =>
                {
                    ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(imgFrame);
                });

                //Imshow Faster. almost no delay.
                //CvInvoke.Imshow("win", imgFrame);
                imgFrame.Dispose();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static class EmguWpfBitmap
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// Convert an IImage to a WPF BitmapSource. The result can be used in the Set Property of Image.Source
        /// </summary>
        /// <param name="image">The Emgu CV Image</param>
        /// <returns>The equivalent BitmapSource</returns>
        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }
    }
}
