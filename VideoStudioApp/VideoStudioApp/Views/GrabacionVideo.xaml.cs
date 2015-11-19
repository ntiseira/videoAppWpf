using Microsoft.Expression.Encoder.Devices;
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
using System.Windows.Shapes;
using VideoStudioApp.ViewModel;

namespace VideoStudioApp.Views
{
    /// <summary>
    /// Lógica de interacción para GrabacionVideo.xaml
    /// </summary>
    public partial class GrabacionVideo : Window
    {
        public Window Home { get; set; }
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public GrabacionVideo(Window home, EncoderDevice selectedVideo, EncoderDevice selectedAudio)
        {
            InitializeComponent();
            Home = home;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GrabacionVideoViewModel vm = new GrabacionVideoViewModel(Home, SelectedVideo, SelectedAudio, this.WebcamCtrl);
            this.DataContext = vm;
        }
    }
}
