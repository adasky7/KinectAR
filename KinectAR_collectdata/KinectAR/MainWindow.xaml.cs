using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
using Microsoft.Kinect;
using LightBuzz.Vitruvius;
using System.IO;
using System.Windows.Threading;

namespace KinectAR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Initialise components
        /// </summary>
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        PlayersController _playersController;

        ///Joint types
        JointType JTHead = JointType.Head;
        JointType JTNeck = JointType.Neck;
        JointType JTAnkleLeft = JointType.AnkleLeft;
        JointType JTAnkleRight = JointType.AnkleRight;
        JointType JTElbowLeft = JointType.ElbowLeft;
        JointType JTElbowRight = JointType.ElbowRight;
        JointType JTFootLeft = JointType.FootLeft;
        JointType JTFootRight = JointType.FootRight;
        JointType JTHandLeft = JointType.HandLeft;
        JointType JTHandRight = JointType.HandRight;
        JointType JTHandTipLeft = JointType.HandTipLeft;
        JointType JTHandTipRight = JointType.HandTipRight;
        JointType JTHipLeft = JointType.HipLeft;
        JointType JTHipRight = JointType.HipRight;
        JointType JTKneeLeft = JointType.KneeLeft;
        JointType JTKneeRight = JointType.KneeRight;
        JointType JTShoulderLeft = JointType.ShoulderLeft;
        JointType JTShoulderRight = JointType.ShoulderRight;
        JointType JTSpineBase = JointType.SpineBase;
        JointType JTSpineMid = JointType.SpineMid;
        JointType JTSpineShoulder = JointType.SpineShoulder;
        JointType JTThumbLeft = JointType.ThumbLeft;
        JointType JTThumbRight = JointType.ThumbRight;
        JointType JTWristLeft = JointType.WristLeft;
        JointType JTWristRight = JointType.WristRight;

        ///Joint positions
        Joint Head, Neck, SpineShoulder, SpineMid, SpineBase;
        Joint ShoulderLeft, ElbowLeft, WristLeft, HandLeft, HandTipLeft, ThumbLeft;
        Joint ShoulderRight, ElbowRight, WristRight, HandRight, HandTipRight, ThumbRight;
        Joint HipLeft, KneeLeft, AnkleLeft, FootLeft;
        Joint HipRight, KneeRight, AnkleRight, FootRight;    
     

        /// <summary>
        /// Connection status
        /// </summary>
        public bool isKinectConnectedToPC = false;
        public bool isHeadTracked = false;
        public bool isHandsTracked = false;
        public bool isToeTracked = false;
        public bool RecBtnClick = false;

        //variables required to deal with frame calculations
        string filePath = "SkeletonData.txt";
        public StreamWriter File;

        private readonly DispatcherTimer _timer;    //timer for fps
        private int _totalFrameCount;   //number of valid frames processed
        private int _fps;   //number of frames per second being processed
        private int _totalSeconds;  //total time in seconds we've been processing frames

        
        
        public MainWindow()
        {
            InitializeComponent();
            File = new StreamWriter(filePath);

            //create timer to allow calculate fps
            _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            _timer.Tick += TimerOnTick;


            _sensor = KinectSensor.GetDefault();
            

            if (_sensor != null)
            {
                _sensor.Open();
                _sensor.IsAvailableChanged += this.Sensor_changed;
                ///change kinect indicator
                if (_sensor.IsOpen)
                {
                    //isKinectConnectedToPC = true;
                    changeKinectIndicatorcolor(Indicator_kinect, true);
                }
                
                
                //Open kinect reader
                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;

                //Player control
                _playersController = new PlayersController();
                _playersController.BodyEntered += UserReporter_BodyEntered;
                _playersController.BodyLeft += UserReporter_BodyLeft;
                _playersController.Start();
            }
            
        }
        

        private void Sensor_changed (object sender, IsAvailableChangedEventArgs e)
        {
            isKinectConnectedToPC = _sensor.IsAvailable ? true : false;
            changeKinectIndicatorcolor(Indicator_kinect, true);
        }

        /// <summary>
        /// kinect frame reader
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            //color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (viewer.Visualization == Visualization.Color)
                    {
                        viewer.Image = frame.ToBitmap();
                    }
                }
            }

            //Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    var bodies = frame.Bodies();
                    _playersController.Update(bodies);
                    Body body = bodies.Closest();

                    if (body != null)
                    {
                        //define the joints after skeleton has been tracked
                        Head    = body.Joints[JTHead];
                        Neck    = body.Joints[JTNeck];
                        SpineShoulder   = body.Joints[JTSpineShoulder];
                        SpineMid        = body.Joints[JTSpineMid];
                        SpineBase       = body.Joints[JTSpineBase];
                        ShoulderLeft    = body.Joints[JTShoulderLeft];
                        ElbowLeft       = body.Joints[JTElbowLeft];
                        WristLeft       = body.Joints[JTWristLeft];
                        HandLeft        = body.Joints[JTHandLeft];
                        ShoulderRight   = body.Joints[JTShoulderRight];
                        ElbowRight      = body.Joints[JTElbowRight];
                        WristRight      = body.Joints[JTWristRight];
                        HandRight       = body.Joints[JTHandRight];
                        AnkleLeft       = body.Joints[JTAnkleLeft];
                        AnkleRight      = body.Joints[JTAnkleRight];
                        FootLeft        = body.Joints[JTFootLeft];
                        FootRight       = body.Joints[JTFootRight];
                        HandTipLeft     = body.Joints[JTHandTipLeft];
                        HandTipRight    = body.Joints[JTHandTipRight];
                        HipLeft         = body.Joints[JTHipLeft];
                        HipRight        = body.Joints[JTHipRight];
                        KneeLeft        = body.Joints[JTKneeLeft];
                        KneeRight       = body.Joints[JTKneeRight];
                        ThumbLeft       = body.Joints[JTThumbLeft];
                        ThumbRight      = body.Joints[JTThumbRight];


                        if (body.IsTracked)
                        {
                            viewer.DrawBody(body);      //Draw body skeleton view

                            //apply angles to skeleton body view
                            AngleLeftElbow.Update(ShoulderLeft, ElbowLeft, WristLeft, 100);
                            AngleLeftShoulder.Update(SpineShoulder, ShoulderLeft, ElbowLeft, 100);
                            AngleRightElbow.Update(WristRight, ElbowRight, ShoulderRight, 100);
                            AngleRightShoulder.Update(ElbowRight, ShoulderRight, SpineShoulder, 100);

                            //if rec button is clicked
                            if (RecBtnClick == true)
                            {
                                TotalFrameCount++;      //keep record of frame count

                                /*
                                 Write joint data to file
                                1. Head, 2. Neck, 3. Spine_Shoulder,4.  Spine_Mid, 5. Spine_Base,
                                6. Shoulder_left, 7. Elbow_left, 8. Wrist_Left, 9. Hand_Left, 10. Hand_Tip_Left, 11. Thumb_Left,
                                12. Shoulder_right, 13. Elbow_Right, 14. Wrist_Right, 15. Hand_Right, 16. Hand_Tip_Left, 17. Thumb Right,
                                18. Hip_Left, 19. Knee_Left, 20. Ankle_Left, 21. Foot_Left,
                                22. Hip_Right, 23. Knee_Right, 24. Ankle_Right, 25. Foot_Right                                  
                                */
                                File.Write(TotalFrameCount + ",");
                                GetPositionCood(Head, File);
                                GetPositionCood(Neck, File);
                                GetPositionCood(SpineShoulder, File);
                                GetPositionCood(SpineMid, File);
                                GetPositionCood(SpineBase, File);
                                GetPositionCood(ShoulderLeft, File);
                                GetPositionCood(ElbowLeft, File);
                                GetPositionCood(WristLeft, File);
                                GetPositionCood(HandLeft, File);
                                GetPositionCood(HandTipLeft, File);
                                GetPositionCood(ThumbLeft, File);
                                GetPositionCood(ShoulderRight, File);
                                GetPositionCood(ElbowRight, File);
                                GetPositionCood(WristRight, File);
                                GetPositionCood(HandRight, File);
                                GetPositionCood(HandTipRight, File);
                                GetPositionCood(ThumbRight, File);
                                GetPositionCood(HipLeft, File);
                                GetPositionCood(KneeLeft, File);
                                GetPositionCood(AnkleLeft, File);
                                GetPositionCood(FootLeft, File);
                                GetPositionCood(HipRight, File);
                                GetPositionCood(KneeRight, File);
                                GetPositionCood(AnkleRight, File);
                                GetPositionCood(FootRight, File);
                                File.Write("\r\n");
                            }
                           
                        }
                        
                    }
                }
            }
            //Depth
            using (var depthframe = reference.DepthFrameReference.AcquireFrame())
            {
                if (depthframe != null)
                {
                    depthviewer.Image = depthframe.ToBitmap();
                }
            }
        }

        //New user entered scene
        void UserReporter_BodyEntered(object sender, PlayersControllerEventArgs e)
        {
            clearScene();
        }

        //Body left scene
        void UserReporter_BodyLeft(object sender, PlayersControllerEventArgs e)
        {
            clearScene();
        }

        //clear screen of skeleton data
        void clearScene()
        {
            viewer.Clear();
            depthviewer.Clear();
            AngleLeftElbow.Clear();
            AngleLeftShoulder.Clear();
            AngleRightElbow.Clear();
            AngleRightShoulder.Clear();

        }


        public int TotalFrameCount
        {
            get { return _totalFrameCount; }
            set { _totalFrameCount = value; OnPropertyChanged(); }
        }

        public int Fps
        {
            get { return _fps; }
            set { _fps = value; OnPropertyChanged(); }
        }

        private void TimerOnTick(object sender, object o)
        {
            _totalSeconds++;    //increment the total seconds counter
            Fps = _totalFrameCount / _totalSeconds; //Calculate the current fps value
        }

        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Start data recording to files when record button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_data_rec(object sender, RoutedEventArgs e)
        {
            //_timer.Start();
            //File = new StreamWriter(filePath);
            RecBtnClick = true;
                       
        }

        /// <summary>
        /// Stop record and close files when button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_record(object sender, RoutedEventArgs e)
        {
            //_timer.Stop();
            File.Write("END");  //Write end to the file after recording is finished
            File.Close();
            RecBtnClick = false;

        }

        //Indicator light change
        private void changeKinectIndicatorcolor(Ellipse indicator, bool status)
        {
            if (status)
            {
                indicator = Indicator_kinect;
               
                if (isKinectConnectedToPC)
                {
                    //Fill with green color
                    indicator.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#19EC47"));
                }
                else
                {
                    //fill with red color
                    indicator.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D41818"));
                }
               
            }
            else
            {
                //fill with red color
                indicator.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D41818"));
            }
        }

        //return position coordinates for joint axis
        private void GetPositionCood(Joint j, StreamWriter DataFile)
        {
            float jx = j.Position.X;
            float jy = j.Position.Y;
            float jz = j.Position.Z;

            DataFile.Write(jx + "," + jy + "," + jz + ",");

        }
    }
}
