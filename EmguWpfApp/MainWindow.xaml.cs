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
using System.Xml.Linq;

namespace EmguWpfApp
{
    public enum Status
    {
        Unknown,
        VideoChosen,
        Play,
        ImageChosen,
        Pause,
        Stop
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ThresHoldStruct ThresHold = new ThresHoldStruct();
        CannyStruct Canny = new CannyStruct();
        InRangeStruct inRange = new InRangeStruct();

        private string CanvasVideoFileName = null;
        private string CanvasImageFileName = null;
        Capture cp;

        private Point StartPoint;
        private Point EndPoint;
        Rectangle rct = null;

        Image<Bgr, byte> ImgRegion;
        Image<Bgr, byte> CurrentFrame;

        double FrameNumOnPause;
        Status CanvasStatus = Status.Unknown;
        double CaptureWidth;
        double CaptureHeight;

        public MainWindow()
        {
            InitializeComponent();
            ThresHold_Combobox.ItemsSource = ThresHold.AvailibleThresholdTypes;
            
            InRangeTab.DataContext = inRange;
            ThresHoldTab.DataContext = ThresHold;
            CannyTab.DataContext = Canny;


            InRangeCheckBox_CanvasTab.DataContext = inRange;
            ThresHoldCheckBox_CanvasTab.DataContext = ThresHold;
            CannyCheckBox_CanvasTab.DataContext = Canny;
        }


        private void InRange_ValueChanged()
        {
            try
            {
                if (inRange.IsEnabled)
                {
                    using (IImage img = inRange.ProccessImage(ImgRegion))
                    {
                        ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Canny_ValueChanged()
        {
            try
            {

                using (IImage img = Canny.ProccessImage(ImgRegion))
                {
                    CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(img);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ThresHold_ValueChanged()
        {
            try
            {
                if (ThresHold.IsEnabled == true)
                {
                    using (IImage img = ThresHold.ProccessImage(ImgRegion))
                    {
                        ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReleaseMouseWithRectangel()
        {
            if (CanvasElement_CanvasTab.Children.Contains(rct) && CanvasStatus > Status.Play)
            {
                double startX = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                double startY = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                double wdth = StartPoint.X > EndPoint.X ? StartPoint.X - EndPoint.X : EndPoint.X - StartPoint.X;
                double hgth = StartPoint.Y > EndPoint.Y ? StartPoint.Y - EndPoint.Y : EndPoint.Y - StartPoint.Y;
                ImgRegion = CurrentFrame.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth));

                ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(ImgRegion);
                //xFileName = "1";
                CannyTab_ImageViewer.Source = EmguWpfBitmap.ToBitmapSource(ImgRegion);
                ImageViewer_ThresHoldTab.Source = EmguWpfBitmap.ToBitmapSource(ImgRegion);

                this.CanvasElement_CanvasTab.Children.Remove(rct);
                //CvInvoke.NamedWindow("wnd");
                //CvInvoke.Imshow("wnd", ImgRegion);
            }
        }

        private void Cp_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Capture xCp = (Capture)sender;
                Image<Bgr, byte> imgX = new Image<Bgr, byte>((int)CaptureWidth, (int)CaptureHeight);
                xCp.Retrieve(imgX);
                //CannyTab_ImageViewer.Dispatcher.Invoke(() => {
                //imgX = imgX.Resize(0.5, Emgu.CV.CvEnum.Inter.Linear);
                double startX = 0;
                double startY = 0;
                double wdth = 0;
                double hgth = 0;
                if (StartPoint != null && EndPoint != null & rct != null)
                {

                    startX = StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X;
                    startY = StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y;
                    wdth = StartPoint.X > EndPoint.X ? StartPoint.X - EndPoint.X : EndPoint.X - StartPoint.X;
                    hgth = StartPoint.Y > EndPoint.Y ? StartPoint.Y - EndPoint.Y : EndPoint.Y - StartPoint.Y;

                    if (Canny.IsEnabled == true)
                    {
                        Canny.ProccessImage(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)))
                            .Convert<Bgr, byte>().CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));

                    }
                    else if (inRange.IsEnabled)
                    {
                        inRange.ProccessImage(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)))
                            .Convert<Bgr, byte>().CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));
                    }
                    else if (ThresHold.IsEnabled)
                    {
                        if (ThresHold.IsColorEnabled == true)
                            ((Image<Bgr, byte>)ThresHold.ProccessImage(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth))))
                                .CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));
                        else
                            ((Image<Gray, byte>)ThresHold.ProccessImage(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth))))
                                .Convert<Bgr, byte>().CopyTo(imgX.GetSubRect(new System.Drawing.Rectangle((int)startX, (int)startY, (int)wdth, (int)hgth)));
                    }
                }
                Dispatcher.Invoke(() =>
                {
                    Tbox_CanvasTab.Text = xCp.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames).ToString();
                    ImageViewer_CanvasTab.Source = EmguWpfBitmap.ToBitmapSource(imgX);
                });

                if (CurrentFrame != null)
                    CurrentFrame.Dispose();
                CurrentFrame = imgX;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in cp_ImageGrabbed: {ex.Message}");
            }
        }

//UI handlers
    //Canvas Tab
        private void CanvasElement_CanvasTab_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanvasStatus > Status.Play)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
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
                    ReleaseMouseWithRectangel();
                }
            }
        }

        private void CanvasElement_CanvasTab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //isMouseButtonPressed = false;
            //EndPoint = e.GetPosition(CanvasElement_CanvasTab);
            ReleaseMouseWithRectangel();
            //if (rct != null)
            //    CanvasElement_CanvasTab.Children.Remove(rct);
        }

        private void CanvasElement_CanvasTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //isMouseButtonPressed = true;
            if (CanvasStatus > Status.Play)
                StartPoint = e.GetPosition(CanvasElement_CanvasTab);
            //Console.WriteLine($"Hello position:{x}");
        }

        private void FileOpenHeader_CanvasTab_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "*.mp4";
            dlg.Filter = "MPEG-4(*.mp4) | *.mp4";
            dlg.Multiselect = false;
            CanvasStatus = Status.VideoChosen;
            bool? res = dlg.ShowDialog();

            if (res == true)
            {
                CanvasVideoFileName = dlg.FileName;
                CanvasImageFileName = null;
                PlayButton_CanvasTab.IsEnabled = true;
            }
        }

        private void PlayButton_Click_CanvasTab(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CanvasStatus >= Status.VideoChosen)
                {
                    if (CanvasStatus == Status.Pause)
                        cp.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, FrameNumOnPause);
                    else if (CanvasStatus == Status.Stop)
                    {
                        FrameNumOnPause = 0;
                        cp.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, 0);
                    }
                    else if (CanvasStatus == Status.VideoChosen)
                    {
                        cp = new Capture(CanvasVideoFileName);
                        CaptureWidth = cp.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth);
                        CaptureHeight = cp.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight);
                        cp.ImageGrabbed += Cp_ImageGrabbed;
                    }
                    cp.Start();
                    PlayButton_CanvasTab.IsEnabled = false;
                    StopButton_CanvasTab.IsEnabled = true;
                    CanvasStatus = Status.Play;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PlayButton_Click_CanvasTab: {ex.Message}");
            }
        }

        private void PauseButton_Click_CanvasTab(object sender, RoutedEventArgs e)
        {
            if (CanvasStatus == Status.Play)
            {
                cp.Pause();
                PlayButton_CanvasTab.IsEnabled = true;
                CanvasStatus = Status.Pause;
                FrameNumOnPause = cp.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames);
            }
        }

        private void StopButton_Click_CanvasTab(object sender, RoutedEventArgs e)
        {
            if (CanvasStatus == Status.Play)
            {
                cp.Stop();
                PlayButton_CanvasTab.IsEnabled = true;
                CanvasStatus = Status.Stop;
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
        //Menu Items
        private void SaveFileMenuItem_CanvasTab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Canny.IsEnabled || ThresHold.IsEnabled || inRange.IsEnabled) && CanvasStatus > Status.VideoChosen && StartPoint != null && EndPoint != null)
                {
                    XElement rootDoc = new XElement(XName.Get("ImageProperties"));

                    XElement StartPosX = new XElement(XName.Get("StartPositionX"));
                    StartPosX.Value = (EndPoint.X > StartPoint.X ? StartPoint.X : EndPoint.X).ToString();
                    rootDoc.Add(StartPosX);

                    XElement StartPosY = new XElement(XName.Get("StartPositionY"));
                    StartPosY.Value = (EndPoint.Y > StartPoint.Y ? StartPoint.Y : EndPoint.Y).ToString();
                    rootDoc.Add(StartPosY);

                    XElement Width = new XElement(XName.Get("ImageWidth"));
                    Width.Value = (EndPoint.X > StartPoint.X ? EndPoint.X - StartPoint.X : StartPoint.X - EndPoint.X).ToString();
                    rootDoc.Add(Width);

                    XElement Height = new XElement(XName.Get("ImageHeight"));
                    Height.Value = (EndPoint.Y > StartPoint.Y ? EndPoint.Y - StartPoint.Y : StartPoint.Y - EndPoint.Y).ToString();
                    rootDoc.Add(Height);

                    XElement ProcessType = new XElement(XName.Get("ProcessingFilter"));
                    if (Canny.IsEnabled == true)
                    {
                        XAttribute xatr = new XAttribute(XName.Get("Type"), "Canny");
                        ProcessType.Add(xatr);

                        XElement CannyThresHoldParam = new XElement(XName.Get("ThresHoldParam"));
                        CannyThresHoldParam.Value = Canny.ThresholdParam.ToString();
                        ProcessType.Add(CannyThresHoldParam);

                        XElement CannyThresHoldLinkingParam = new XElement(XName.Get("ThresHoldLinkingParam"));
                        CannyThresHoldLinkingParam.Value = Canny.ThresholdLinkingParam.ToString();
                        ProcessType.Add(CannyThresHoldLinkingParam);

                        XElement CannyApertureSize = new XElement(XName.Get("ApertureSize"));
                        CannyApertureSize.Value = Canny.ApertureSize.ToString();
                        ProcessType.Add(CannyApertureSize);

                        XElement CannyI2Gradient = new XElement(XName.Get("I2Gradient"));
                        CannyI2Gradient.Value = Canny.I2Gradient.ToString();
                        ProcessType.Add(CannyI2Gradient);
                    }
                    else if (ThresHold.IsEnabled == true)
                    {
                        XAttribute xatr = new XAttribute(XName.Get("Type"), "ThresHold");
                        ProcessType.Add(xatr);

                        XElement ThresholdTypeOperation = new XElement(XName.Get("ThresholdTypeOperation"));
                        ThresholdTypeOperation.Value = ThresHold.CurrentThresHoldType.ToString();
                        ProcessType.Add(ThresholdTypeOperation);

                        XElement IsColorThresholding = new XElement(XName.Get("IsColorThresHolding"));
                        IsColorThresholding.Value = ThresHold.IsColorEnabled.ToString();
                        ProcessType.Add(IsColorThresholding);

                        if (ThresHold.IsColorEnabled == true)
                        {
                            //Colors
                            XElement BlueThresHoldValue = new XElement(XName.Get("BlueThresHoldValue"));
                            BlueThresHoldValue.Value = ThresHold.ThresHoldBlue.ToString();
                            ProcessType.Add(BlueThresHoldValue);

                            XElement GreenThresHoldValue = new XElement(XName.Get("GreenThresHoldValue"));
                            GreenThresHoldValue.Value = ThresHold.ThresHoldGreenOrGray.ToString();
                            ProcessType.Add(GreenThresHoldValue);

                            XElement RedThresHoldValue = new XElement(XName.Get("RedThresHoldValue"));
                            RedThresHoldValue.Value = ThresHold.ThresHoldRed.ToString();
                            ProcessType.Add(RedThresHoldValue);
                            //

                            //MaxValues 
                            XElement BlueMaxValue = new XElement(XName.Get("BlueMaxValue"));
                            BlueMaxValue.Value = ThresHold.MaxValueBlue.ToString();
                            ProcessType.Add(BlueMaxValue);

                            XElement GreenMaxValue = new XElement(XName.Get("GreenMaxValue"));
                            GreenMaxValue.Value = ThresHold.MaxValueGreenOrGray.ToString();
                            ProcessType.Add(GreenMaxValue);

                            XElement RedMaxValue = new XElement(XName.Get("RedMaxValue"));
                            RedMaxValue.Value = ThresHold.MaxValueRed.ToString();
                            ProcessType.Add(RedMaxValue);
                        }
                        else
                        {
                            XElement GrayThresHoldValue = new XElement(XName.Get("GrayThresHoldValue"));
                            GrayThresHoldValue.Value = ThresHold.ThresHoldGreenOrGray.ToString();
                            ProcessType.Add(GrayThresHoldValue);

                            XElement GrayMaxValue = new XElement(XName.Get("GrayMaxValue"));
                            GrayMaxValue.Value = ThresHold.MaxValueGreenOrGray.ToString();
                            ProcessType.Add(GrayMaxValue);
                        }

                    }
                    else if (inRange.IsEnabled == true)
                    {
                        //ProcessType.Value = "InRange";
                        XAttribute xatr = new XAttribute(XName.Get("Type"), "InRange");
                        ProcessType.Add(xatr);

                        //Colors Min
                        XElement ColorMinB = new XElement(XName.Get("ColorMinimumBlue"));
                        ColorMinB.Value = inRange.ColorMinBlue.ToString();
                        ProcessType.Add(ColorMinB);

                        XElement ColorMinG = new XElement(XName.Get("ColorMinimumGreen"));
                        ColorMinG.Value = inRange.ColorMinGreen.ToString();
                        ProcessType.Add(ColorMinG);

                        XElement ColorMinR = new XElement(XName.Get("ColorMinimumRed"));
                        ColorMinR.Value = inRange.ColorMinRed.ToString();
                        ProcessType.Add(ColorMinR);

                        //Colors Max
                        XElement ColorMaxB = new XElement(XName.Get("ColorMaximumBlue"));
                        ColorMaxB.Value = inRange.ColorMaxBlue.ToString();
                        ProcessType.Add(ColorMaxB);

                        XElement ColorMaxG = new XElement(XName.Get("ColorMaximumGreen"));
                        ColorMaxG.Value = inRange.ColorMaxGreen.ToString();
                        ProcessType.Add(ColorMaxG);

                        XElement ColorMaxR = new XElement(XName.Get("ColorMaximumRed"));
                        ColorMaxR.Value = inRange.ColorMaxRed.ToString();
                        ProcessType.Add(ColorMaxR);

                        //MaskSyncColor
                        XElement isSync = new XElement(XName.Get("ColorSyncMask"));
                        isSync.Value = inRange.IsColorMaskSync.ToString();
                        ProcessType.Add(isSync);

                        //Mask Min
                        XElement MaskMinB = new XElement(XName.Get("MaskMinimumBlue"));
                        MaskMinB.Value = inRange.MaskMinBlue.ToString();
                        ProcessType.Add(MaskMinB);

                        XElement MaskMinG = new XElement(XName.Get("MaskMinimumGreen"));
                        MaskMinG.Value = inRange.MaskMinGreen.ToString();
                        ProcessType.Add(MaskMinG);

                        XElement MaskMinR = new XElement(XName.Get("MaskMinimumRed"));
                        MaskMinR.Value = inRange.MaskMinRed.ToString();
                        ProcessType.Add(MaskMinR);

                        //Colors Max
                        XElement MaskMaxB = new XElement(XName.Get("MaskMaximumBlue"));
                        MaskMaxB.Value = inRange.MaskMaxBlue.ToString();
                        ProcessType.Add(MaskMaxB);

                        XElement MaskMaxG = new XElement(XName.Get("MaskMaximumGreen"));
                        MaskMaxG.Value = inRange.MaskMaxGreen.ToString();
                        ProcessType.Add(MaskMaxG);

                        XElement MaskMaxR = new XElement(XName.Get("MaskMaximumRed"));
                        MaskMaxR.Value = inRange.MaskMaxRed.ToString();
                        ProcessType.Add(MaskMaxR);
                    }

                    rootDoc.Add(ProcessType);

                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.DefaultExt = "*.xml";
                    dlg.Filter = "XML(*.xml) | *.xml";
                    bool? res = dlg.ShowDialog();

                    if (res == true)
                    {
                        rootDoc.Save(dlg.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error on Save xml: {ex.Message}");
            }
        }

        private void LoadFileMenuItem_CanvasTab_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "*.xml";
                dlg.Filter = "XML(*.xml) | *.xml";
                dlg.Multiselect = false;
                bool? res = dlg.ShowDialog();

                if (res == true)
                {
                    XElement xDoc = XElement.Load(dlg.FileName);
                    double startX = double.Parse(xDoc.Element("StartPositionX").Value);
                    double startY = double.Parse(xDoc.Element("StartPositionY").Value);
                    rct = new Rectangle();

                    StartPoint = new Point(startX, startY);
                    double width = double.Parse(xDoc.Element("ImageWidth").Value);
                    double height = double.Parse(xDoc.Element("ImageHeight").Value);
                    EndPoint = new Point(startX + width, startY + height);

                    XElement xElem = xDoc.Element("ProcessingFilter");
                    XAttribute xAttr = xElem.Attribute("Type");
                    if (xAttr != null && xAttr.Value == "InRange"/* && InRangeTab.IsEnabled == true*/)
                    {
                        inRange.IsEnabled = true;
                        //Color Max
                        //inRange.PropertyChanged+= dontwork

                        inRange.ColorMaxBlue = int.Parse(xElem.Element("ColorMaximumBlue").Value);
                        inRange.ColorMaxGreen = int.Parse(xElem.Element("ColorMaximumGreen").Value);
                        inRange.ColorMaxRed = int.Parse(xElem.Element("ColorMaximumRed").Value);
                        //Color Min
                        inRange.ColorMinBlue = int.Parse(xElem.Element("ColorMinimumBlue").Value);
                        inRange.ColorMinGreen = int.Parse(xElem.Element("ColorMinimumGreen").Value);
                        inRange.ColorMinRed = int.Parse(xElem.Element("ColorMinimumRed").Value);

                        inRange.IsColorMaskSync = bool.Parse(xElem.Element("ColorSyncMask").Value);
                        //Mask Max
                        inRange.MaskMaxBlue = int.Parse(xElem.Element("MaskMaximumBlue").Value);
                        inRange.MaskMaxGreen = int.Parse(xElem.Element("MaskMaximumGreen").Value);
                        inRange.MaskMaxRed = int.Parse(xElem.Element("MaskMaximumRed").Value);
                        //Mask Min
                        inRange.MaskMinBlue = int.Parse(xElem.Element("MaskMinimumBlue").Value);
                        inRange.MaskMinGreen = int.Parse(xElem.Element("MaskMinimumGreen").Value);
                        inRange.MaskMinRed = int.Parse(xElem.Element("MaskMinimumRed").Value);
                    }
                    else if (xAttr != null && xAttr.Value == "Canny")
                    {
                        Canny.IsEnabled = true;

                        Canny.ThresholdParam = int.Parse(xElem.Element("ThresHoldParam").Value);
                        Canny.ThresholdLinkingParam = int.Parse(xElem.Element("ThresHoldLinkingParam").Value);
                        Canny.I2Gradient = bool.Parse(xElem.Element("I2Gradient").Value);
                        Canny.ApertureSize = int.Parse(xElem.Element("ApertureSize").Value);
                    }
                    else if (xAttr != null && xAttr.Value == "ThresHold")
                    {
                        ThresHold.IsEnabled = true;

                        //Type
                        ThresHold.CurrentThresHoldType = xElem.Element("ThresholdTypeOperation").Value;
                        //IsColor
                        ThresHold.IsColorEnabled = bool.Parse(xElem.Element("IsColorThresHolding").Value);

                        if (ThresHold.IsColorEnabled == true)
                        {
                            //ThresHold
                            ThresHold.ThresHoldBlue = int.Parse(xElem.Element("BlueThresHoldValue").Value);
                            ThresHold.ThresHoldGreenOrGray = int.Parse(xElem.Element("GreenThresHoldValue").Value);
                            ThresHold.ThresHoldRed = int.Parse(xElem.Element("RedThresHoldValue").Value);
                            //MaxValue
                            ThresHold.MaxValueBlue = int.Parse(xElem.Element("BlueMaxValue").Value);
                            ThresHold.MaxValueGreenOrGray = int.Parse(xElem.Element("GreenMaxValue").Value);
                            ThresHold.MaxValueRed = int.Parse(xElem.Element("RedMaxValue").Value);

                        }
                        else
                        {
                            ThresHold.ThresHoldGreenOrGray = int.Parse(xElem.Element("GrayThresHoldValue").Value);
                            ThresHold.MaxValueGreenOrGray = int.Parse(xElem.Element("GrayMaxValue").Value);
                        }
                    }
                    //Put to filter tabs
                    CanvasElement_CanvasTab.Children.Add(rct);
                    ReleaseMouseWithRectangel();

                    //Show image on tab's ImageSource if exist
                    if (xAttr != null && xAttr.Value == "InRange" && CurrentFrame != null)
                        InRange_ValueChanged();
                    else if (xAttr != null && xAttr.Value == "Canny" && CurrentFrame != null)
                        Canny_ValueChanged();
                    else if (xAttr != null && xAttr.Value == "ThresHold" && CurrentFrame != null)
                        ThresHold_ValueChanged();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FileOpenImageHeader_CanvasTab_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "*.png";
            dlg.Filter = "BMP Files(*.bmp) | *.bmp|JPEG Files(*.jpeg) | *.jpeg| PNG Files(*.png) | *.png| JPG Files(*.jpg) | *.jpg| GIF Files(*.gif) | *.gif";
            dlg.Multiselect = false;

            bool? res = dlg.ShowDialog();

            if (res == true)
            {
                CanvasImageFileName = dlg.FileName;
                CanvasVideoFileName = null;
                CurrentFrame = new Image<Bgr, byte>(CanvasImageFileName);
                ImageViewer_CanvasTab.Source = EmguWpfBitmap.ToBitmapSource(CurrentFrame);
                CanvasStatus = Status.ImageChosen;
            }
        }

    //InRange Tab 
        private void InRange_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (inRange.IsEnabled && ImgRegion != null)
                InRange_ValueChanged();
        }

        private void ColorMask_Sync_Checked(object sender, RoutedEventArgs e)
        {
            if (inRange.IsEnabled && ImgRegion != null)
                InRange_ValueChanged();
        }
    //Canny Tab
        private void Canny_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Canny.IsEnabled && ImgRegion != null)
                Canny_ValueChanged();
        }

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
    //ThresHold Tab
        private void ThresHold_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ThresHold.IsEnabled && ImgRegion != null)
                ThresHold_ValueChanged();
        }

        private void ThresHold_Color_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ThresHold.IsEnabled && ImgRegion != null)
                ThresHold_ValueChanged();
        }

        private void ThresHold_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThresHold.IsEnabled && ImgRegion != null)
                ThresHold_ValueChanged();
        }

        private void ColorType_InRangeTab_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton chkB = (RadioButton)sender;
            switch (chkB.Content)
            {
                case "RGB":
                    inRange.ColorType = ColorType_enum.BGR;
                    break;
                case "HSV":
                    inRange.ColorType = ColorType_enum.HSV;
                    break;
                //case "HSI_CheckBox_InRangeTab":
                //    inRange.ColorType = (int)ColorType_enum.HSI;
                //    break;
                //case "HSL_CheckBox_InRangeTab":
                //    inRange.ColorType = (int)ColorType_enum.HLS;
                //    break;
            }
        }
    }
}
