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
using System.IO;
using System.ComponentModel;

namespace EmguWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string CannyFileName;
        ThresHoldStruct ThresHold = new ThresHoldStruct();
        CannyStruct Canny = new CannyStruct();
        InRangeStruct inRange = new InRangeStruct();

        private string CanvasFileName;
        Capture cp;
        //private bool isMouseButtonPressed;
        private Point StartPoint;
        private Point EndPoint;
        Rectangle rct = null;

        Image<Bgr, byte> ImgRegion;
        Image<Bgr, byte> PreviousImage;

        bool IsVideoPlaying = false;
        Image<Gray, byte> cannyImg;
        //public bool IsCannyEnabled { get; set; }
        //public int CannyParam1 { get; set; }
        //public int CannyParam2 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ThresHold_Combobox.ItemsSource = ThresHold.AvailibleThresholdValues;
            CannyCheckBox_CanvasTab.DataContext = this;
            //CannyTab_ThresholdParam.DataContext = this;
            //CannyTab_ThresholdLinkingParam.DataContext = this;
            InRangeTab.DataContext = inRange;
            ThresHoldTab.DataContext = ThresHold;
            CannyTab.DataContext = Canny;


            InRangeCheckBox_CanvasTab.DataContext = inRange;
            ThresHoldCheckBox_CanvasTab.DataContext = ThresHold;
            CannyCheckBox_CanvasTab.DataContext = Canny;
            //CannyTab_ListBox_Aperture.ItemsSource = ApertureValues;
        }

        //private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        //{
        //    MenuItem mItem = (MenuItem)sender;
        //    try
        //    {
        //        OpenFileDialog dlg = new OpenFileDialog();
        //        dlg.DefaultExt = "*.png";
        //        dlg.Filter = "BMP Files(*.bmp) | *.bmp|JPEG Files(*.jpeg) | *.jpeg| PNG Files(*.png) | *.png| JPG Files(*.jpg) | *.jpg| GIF Files(*.gif) | *.gif";
        //        dlg.Multiselect = false;
        //        bool? res = dlg.ShowDialog();

        //        if (res == true)
        //        {
        //            IImage img;
        //            //MessageBox.Show($"File chosen:{dlg.FileName}");
        //            if (mItem.Name == "FileOpenHeader_InRangeTab")
        //            {
        //                xFileName = dlg.FileName;
        //                img = new Image<Rgb, byte>(xFileName);
        //                ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
        //            }
        //            else if (mItem.Name == "FileOpenHeader_ThresHoldTab")
        //            {
        //                xFileNameThres = dlg.FileName;
        //                if (ThresHold_Color_CheckBox.IsChecked == true)
        //                    img = new Image<Rgb, byte>(xFileNameThres);
        //                else
        //                    img = new Image<Gray, byte>(xFileNameThres);

        //                ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
        //            }
        //            else if (mItem.Name == "FileOpenHeader_CannyTab")
        //            {
        //                CannyFileName = dlg.FileName;
        //                img = new Image<Rgb, byte>(CannyFileName);
        //                CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
        //            }
        //            else if (mItem.Name == "FileOpenHeader_CanvasTab")
        //            {
        //                CanvasFileName = dlg.FileName;
        //                img = new Image<Rgb, byte>(CanvasFileName);
        //                ImageViewer_CanvasTab.Source = EmguWpfBitmap.ToBitmapSource(img);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (inRange.IsEnabled)
                {
                    Image<Gray, byte> img = ImgRegion.InRange(new Bgr(inRange.ColorMinBlue, inRange.ColorMinGreen, inRange.ColorMinRed)
                        , new Bgr(inRange.ColorMaxBlue, inRange.ColorMaxGreen, inRange.ColorMaxRed));
                    if (inRange.IsColorMaskSync == false)
                    {
                        Image<Gray, byte> mask = ImgRegion.InRange(new Bgr(inRange.MaskMinBlue, inRange.MaskMinGreen, inRange.MaskMinRed)
                            , new Bgr(inRange.MaskMaxBlue, inRange.MaskMaxGreen, inRange.MaskMaxRed));
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
            try
            {
                if (ThresHold.IsEnabled == true)
                {
                    IImage img = null;
                    switch (ThresHold.CurrentThresHoldValue)
                    {
                        case "ThresHoldBinary":
                            if (ThresHold.IsColorEnabled == true)
                            {
                                img = ImgRegion.ThresholdBinary(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)
                                     , new Bgr(ThresHold.MaxValueBlue, ThresHold.MaxValueGreenOrGray, ThresHold.MaxValueRed));
                            }
                            else
                                img = ImgRegion.Convert<Gray, byte>().ThresholdBinary(new Gray(ThresHold.ThresHoldGreenOrGray), new Gray(ThresHold.MaxValueGreenOrGray));
                            break;
                        case "ThresHoldBinaryInv":
                            if (ThresHold.IsColorEnabled == true)
                            {
                                img = ImgRegion.ThresholdBinaryInv(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)
                                , new Bgr(ThresHold.MaxValueBlue, ThresHold.MaxValueGreenOrGray, ThresHold.MaxValueRed));
                            }
                            else
                                img = ImgRegion.Convert<Gray, byte>().ThresholdBinaryInv(new Gray(ThresHold.ThresHoldGreenOrGray), new Gray(ThresHold.MaxValueGreenOrGray));
                            break;
                        case "ThresHoldToZeroInv":
                            if (ThresHold.IsColorEnabled == true)
                                img = ImgRegion.ThresholdToZeroInv(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed));
                            else
                                img = ImgRegion.Convert<Gray, byte>().ThresholdToZeroInv(new Gray(ThresHold.ThresHoldGreenOrGray));
                            break;
                        case "ThresHoldToZero":
                            if (ThresHold.IsColorEnabled == true)
                                img = ImgRegion.ThresholdToZero(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed));
                            else
                                img = ImgRegion.Convert<Gray, byte>().ThresholdToZero(new Gray(ThresHold.ThresHoldGreenOrGray));
                            break;
                        case "ThresHoldTrunc":
                            if (ThresHold.IsColorEnabled == true)
                                img = ImgRegion.ThresholdTrunc(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed));
                            else
                                img = ImgRegion.Convert<Gray, byte>().ThresholdTrunc(new Gray(ThresHold.ThresHoldGreenOrGray));
                            break;
                        case "ThresHoldAdaptive":

                            break;

                    }
                    ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
                    img.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Canny_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Canny.IsEnabled && ImgRegion != null)
                Canny_ValueChanged();
        }

        private void Canny_ValueChanged()
        {
            try
            {
                //Image<Gray, byte> img = new Image<Bgr, byte>(CannyFileName).Canny(CannyTab_ThresholdParam.Value, CannyTab_ThresholdLinkingParam.Value, 3, true);
                Image<Gray, byte> img = ImgRegion.Canny(Canny.ThresholdParam, Canny.ThresholdLinkingParam, Canny.ApertureSize, Canny.I2Gradient);
                CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                img.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CanvasElement_CanvasTab_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && IsVideoPlaying == false)
            {
                EndPoint = e.GetPosition(CanvasElement_CanvasTab);
                if (rct != null)
                    CanvasElement_CanvasTab.Children.Remove(rct);
                rct = new Rectangle();
                rct.Stroke = Brushes.Red;
                rct.StrokeThickness = 3;

                rct.StrokeStartLineCap = PenLineCap.Square;

                if (EndPoint.X > StartPoint.X)
                {
                    Canvas.SetLeft(rct, StartPoint.X);
                    rct.Width = EndPoint.X - StartPoint.X;
                }
                else
                {
                    Canvas.SetLeft(rct, EndPoint.X);
                    rct.Width = StartPoint.X - EndPoint.X;
                }
                if (EndPoint.Y > StartPoint.Y)
                {
                    rct.Height = EndPoint.Y - StartPoint.Y;
                    Canvas.SetTop(rct, StartPoint.Y);
                }
                else
                {
                    rct.Height = StartPoint.Y - EndPoint.Y;
                    Canvas.SetTop(rct, EndPoint.Y);
                }
                CanvasElement_CanvasTab.Children.Add(rct);
            }
            else
            {
                if (CanvasElement_CanvasTab.Children.Contains(rct) && IsVideoPlaying == false)
                {
                    //Transform transform = this.CanvasElement_CanvasTab.LayoutTransform;
                    // Get the size of canvas

                    //Size size = new Size(EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y);
                    //// Measure and arrange the surface
                    //// VERY IMPORTANT
                    ////CanvasElement_CanvasTab.Measure(size);
                    ////CanvasElement_CanvasTab.Arrange(new Rect(StartPoint, EndPoint));

                    //newImg = new Image();
                    //newImg.Source = ImageViewer_CanvasTab.Source;
                    //RectangleGeometry rctG = new RectangleGeometry(new Rect(StartPoint.X, StartPoint.Y, EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y));
                    //newImg.Clip = rctG;

                    //// Create a render bitmap and push the surface to it
                    //RenderTargetBitmap renderBitmap =
                    //  new RenderTargetBitmap(
                    //    (int)size.Width,
                    //    (int)size.Height,
                    //    96d,
                    //    96d,
                    //    PixelFormats.Pbgra32);
                    //renderBitmap.Render(newImg);

                    //// Create a file stream for saving image
                    //Uri path = new Uri(@"d:\1.bmp");

                    //using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
                    //{
                    //    // Use png encoder for our data
                    //    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    //    // push the rendered bitmap to it
                    //    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    //    // save the data to the stream
                    //    encoder.Save(outStream);
                    //}

                    //// Restore previously saved layout
                    ////CanvasElement_CanvasTab.LayoutTransform = transform;



                    //Canvas.SetTop(newImg, 200);
                    //this.CanvasElement_CanvasTab.Children.Add(newImg);

                    //BitmapImage bi = new BitmapImage(path);
                    //ImageViewer.Source = bi;
                    //xFileName = path.OriginalString;
                    double startX = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                    double startY = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                    double wdth = StartPoint.X > EndPoint.X ? StartPoint.X - EndPoint.X : EndPoint.X - StartPoint.X;
                    double hgth = StartPoint.Y > EndPoint.Y ? StartPoint.Y - EndPoint.Y : EndPoint.Y - StartPoint.Y;
                    ImgRegion = PreviousImage.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth));

                    ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(ImgRegion);
                    //xFileName = "1";
                    CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(ImgRegion);
                    ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(ImgRegion);

                    this.CanvasElement_CanvasTab.Children.Remove(rct);
                    //CvInvoke.NamedWindow("wnd");
                    //CvInvoke.Imshow("wnd", ImgRegion);
                }
            }
        }

        private void CanvasElement_CanvasTab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //isMouseButtonPressed = false;
            //EndPoint = e.GetPosition(CanvasElement_CanvasTab);
            CanvasElement_CanvasTab.Children.Remove(rct);
        }

        private void CanvasElement_CanvasTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //isMouseButtonPressed = true;
            StartPoint = e.GetPosition(CanvasElement_CanvasTab);
            //Console.WriteLine($"Hello position:{x}");
        }

        private void FileOpenHeader_CanvasTab_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "*.mp4";
            dlg.Filter = "MPEG-4(*.mp4) | *.mp4";
            dlg.Multiselect = false;
            bool? res = dlg.ShowDialog();

            if (res == true)
            {
                CanvasFileName = dlg.FileName;
                PlayButton_CanvasTab.IsEnabled = true;
            }
        }

        private void PlayButton_Click_CanvasTab(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CanvasFileName != null && File.Exists(CanvasFileName))
                {
                    cp = new Capture(CanvasFileName);
                    cp.ImageGrabbed += Cp_ImageGrabbed;
                    cp.Start();
                    PlayButton_CanvasTab.IsEnabled = false;
                    StopButton_CanvasTab.IsEnabled = true;
                    IsVideoPlaying = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PlayButton_Click_CanvasTab: {ex.Message}");
            }
        }

        private void Cp_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Capture xCp = (Capture)sender;
                Image<Bgr, byte> imgX = new Image<Bgr, byte>(1920, 1080);
                xCp.Retrieve(imgX);
                //CannyTab_ImageViewer.Dispatcher.Invoke(() => {
                //imgX = imgX.Resize(0.5, Emgu.CV.CvEnum.Inter.Linear);
                if (Canny.IsEnabled == true)
                {
                    double startX = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                    double startY = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                    double wdth = StartPoint.X > EndPoint.X ? StartPoint.X - EndPoint.X : EndPoint.X - StartPoint.X;
                    double hgth = StartPoint.Y > EndPoint.Y ? StartPoint.Y - EndPoint.Y : EndPoint.Y - StartPoint.Y;
                    imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth))
                        .Canny(Canny.ThresholdParam, Canny.ThresholdLinkingParam, Canny.ApertureSize, Canny.I2Gradient)
                        .Convert<Bgr, byte>().CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));

                }
                else if (inRange.IsEnabled)
                {
                    double startX = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                    double startY = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                    double wdth = StartPoint.X > EndPoint.X ? StartPoint.X - EndPoint.X : EndPoint.X - StartPoint.X;
                    double hgth = StartPoint.Y > EndPoint.Y ? StartPoint.Y - EndPoint.Y : EndPoint.Y - StartPoint.Y;

                    //
                    Image<Gray, byte> inrageImg = imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth))
                        .InRange(new Bgr(inRange.ColorMinBlue, inRange.ColorMinGreen, inRange.ColorMinRed)
                        , new Bgr(inRange.ColorMaxBlue, inRange.ColorMaxGreen, inRange.ColorMaxRed));
                    if (inRange.IsColorMaskSync == false)
                    {
                        Image<Gray, byte> mask = imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth))
                            .InRange(new Bgr(inRange.MaskMinBlue, inRange.MaskMinGreen, inRange.MaskMinRed)
                            , new Bgr(inRange.MaskMaxBlue, inRange.MaskMaxGreen, inRange.MaskMaxRed));
                        inrageImg = inrageImg.Or(mask);
                        mask.Dispose();
                    }
                    //
                    inrageImg.Convert<Bgr, byte>().CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));
                }
                else if (ThresHold.IsEnabled)
                {
                    double startX = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                    double startY = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                    double wdth = StartPoint.X > EndPoint.X ? StartPoint.X - EndPoint.X : EndPoint.X - StartPoint.X;
                    double hgth = StartPoint.Y > EndPoint.Y ? StartPoint.Y - EndPoint.Y : EndPoint.Y - StartPoint.Y;

                    //
                    IImage img;
                    img = imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth));
                    //
                    switch (ThresHold.CurrentThresHoldValue)
                    {
                        case "ThresHoldBinary":
                            if (ThresHold.IsColorEnabled == true)
                            {
                                img = ((Image<Bgr,byte>)img).ThresholdBinary(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)
                                     , new Bgr(ThresHold.MaxValueBlue, ThresHold.MaxValueGreenOrGray, ThresHold.MaxValueRed)).Convert<Bgr, byte>();
                            }
                            else
                                img = ((Image<Bgr, byte>)img).Convert<Gray, byte>().ThresholdBinary(new Gray(ThresHold.ThresHoldGreenOrGray), new Gray(ThresHold.MaxValueGreenOrGray)).Convert<Bgr, byte>();
                            break;
                        case "ThresHoldBinaryInv":
                            if (ThresHold.IsColorEnabled == true)
                            {
                                img = ((Image<Bgr, byte>)img).ThresholdBinaryInv(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)
                                , new Bgr(ThresHold.MaxValueBlue, ThresHold.MaxValueGreenOrGray, ThresHold.MaxValueRed)).Convert<Bgr, byte>();
                            }
                            else
                                img = ((Image<Bgr, byte>)img).Convert<Gray, byte>().ThresholdBinaryInv(new Gray(ThresHold.ThresHoldGreenOrGray), new Gray(ThresHold.MaxValueGreenOrGray)).Convert<Bgr, byte>();
                            break;
                        case "ThresHoldToZeroInv":
                            if (ThresHold.IsColorEnabled == true)
                                img = ((Image<Bgr, byte>)img).ThresholdToZeroInv(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)).Convert<Bgr, byte>();
                            else
                                img = ((Image<Bgr, byte>)img).Convert<Gray, byte>().ThresholdToZeroInv(new Gray(ThresHold.ThresHoldGreenOrGray)).Convert<Bgr, byte>();
                            break;
                        case "ThresHoldToZero":
                            if (ThresHold.IsColorEnabled == true)
                                img = ((Image<Bgr, byte>)img).ThresholdToZero(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)).Convert<Bgr, byte>();
                            else
                                img = ((Image<Bgr, byte>)img).Convert<Gray, byte>().ThresholdToZero(new Gray(ThresHold.ThresHoldGreenOrGray)).Convert<Bgr, byte>();
                            break;
                        case "ThresHoldTrunc":
                            if (ThresHold.IsColorEnabled == true)
                                img = ((Image<Bgr, byte>)img).ThresholdTrunc(new Bgr(ThresHold.ThresHoldBlue, ThresHold.ThresHoldGreenOrGray, ThresHold.ThresHoldRed)).Convert<Bgr, byte>();
                            else
                                img = ((Image<Bgr, byte>)img).Convert<Gray, byte>().ThresholdTrunc(new Gray(ThresHold.ThresHoldGreenOrGray)).Convert<Bgr, byte>();
                            break;
                        case "ThresHoldAdaptive":

                            break;
                    }
                    ((Image<Bgr,byte>)img).CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));
                    
                }
                Dispatcher.Invoke(() =>
                {
                    Tbox_CanvasTab.Text = xCp.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames).ToString();
                    ImageViewer_CanvasTab.Source = EmguWpfBitmap.ToBitmapSource(imgX);
                });

                if (PreviousImage != null)
                    PreviousImage.Dispose();
                PreviousImage = imgX;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in cp_ImageGrabbed: {ex.Message}");
            }
        }

        private void PauseButton_Click_CanvasTab(object sender, RoutedEventArgs e)
        {
            cp.Pause();
        }

        private void StopButton_Click_CanvasTab(object sender, RoutedEventArgs e)
        {
            if (cp != null)
            {
                cp.Stop();
                PlayButton_CanvasTab.IsEnabled = true;
                IsVideoPlaying = false;
            }
        }


        private void CheckBox_CanvasTab_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkB = (CheckBox)sender;
            switch (chkB.Name)
            {
                case "CannyCheckBox_CanvasTab":
                    inRange.IsEnabled = false;
                    ThresHold.IsEnabled = false;
                    Canny.IsEnabled = true;
                    break;
                case "InRangeCheckBox_CanvasTab":
                    Canny.IsEnabled = false;
                    ThresHold.IsEnabled = false;
                    inRange.IsEnabled = true;
                    break;
                case "ThresHoldCheckBox_CanvasTab":
                    Canny.IsEnabled = false;
                    inRange.IsEnabled = false;
                    ThresHold.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        //private void CannyTab_ListBox_Aperture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (Canny.IsEnabled && ImgRegion != null)
        //        Canny_ValueChanged();
        //}

        private void CannyTab_Checkbox_I2Gradien_Checked(object sender, RoutedEventArgs e)
        {
            if (Canny.IsEnabled && ImgRegion != null)
                Canny_ValueChanged();
        }

        private void CannyTab_Aperture_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Canny.IsEnabled && ImgRegion != null)
                Canny_ValueChanged();
        }
    }
}
