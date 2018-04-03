using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

    /// <summary>
    /// Add the nuget package MyoSDK by Galazzoseba
    /// Need to connect to the Myo
    /// </summary>


namespace UWPMyoGalazzosesba
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        public readonly global::Myo.Myo _myo;
        public MainPage()
        {
            string msg;
            this.InitializeComponent();
            _myo = new Myo.Myo();
            try
            {
                _myo.Connect();
                _myo.OnPoseDetected += _myo_OnPoseDetected;
                _myo.OnEMGAvailable += _myo_OnEMGAvailable;
                _myo.DataAvailable += _myo_DataAvailable;
                //_myo.Unlock(Myo.Myo.UnlockType.Hold);
                tblStatus.Text = "Acquired Myo!";
            }
            catch (Exception err)
            {
                msg = err.Message;
                tblStatus.Text = "Problem connecting to Myo!";
            }

        }

        private void _myo_DataAvailable(object sender, Myo.MyoDataEventArgs e)
        {
            // get the accelerometer data and move something
            //e.Acceletometer.X;
        }

        private void _myo_OnEMGAvailable(object sender, Myo.MyoEMGEventArgs e)
        {
        }

        private async void _myo_OnPoseDetected(object sender, Myo.MyoPoseEventArgs e)
        {
            string statusString = "";
            //            throw new NotImplementedException();
            switch (e.Pose)
            {
                case Myo.MyoPoseEventArgs.PoseType.Rest:
                    statusString = " Resting ";
                    break;
                case Myo.MyoPoseEventArgs.PoseType.Fist:
                    statusString = " Fist ";
                    break;
                case Myo.MyoPoseEventArgs.PoseType.WaveIn:
                    statusString = "Wave In ";
                    break;
                case Myo.MyoPoseEventArgs.PoseType.WaveOut:
                    statusString = " Wave Out ";
                    break;
                case Myo.MyoPoseEventArgs.PoseType.DoubleTap:
                    statusString = " Double Tap ";
                    break;
                case Myo.MyoPoseEventArgs.PoseType.FingersSpread:
                    statusString = " Fingers Spread ";
                    break;
                default:
                    break;
            } // endswitch

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                tblStatus.Text = "Current Pose: " + statusString;
            });

        }

        private void btnWake_Click(object sender, RoutedEventArgs e)
        {
            _myo.Vibrate(Myo.Myo.VibrationType.Medium);
            
        }
    }
}
