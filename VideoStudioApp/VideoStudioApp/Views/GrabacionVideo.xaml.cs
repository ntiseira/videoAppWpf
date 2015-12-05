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
using VideoStudioApp.Model;
using VideoStudioApp.ViewModel;

namespace VideoStudioApp.Views
{
    /// <summary>
    /// Lógica de interacción para GrabacionVideo.xaml
    /// </summary>
    public partial class GrabacionVideo : Window
    {
        public Window Config { get; set; }
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public Grabacion SelectedGrabacion { get; set; }
        public GrabacionVideoViewModel ViewModelGrabacion { get; set; }
        
        public GrabacionVideo(EncoderDevice selectedVideo, EncoderDevice selectedAudio, Grabacion grabacionSelected)
        {
            InitializeComponent();
        
            SelectedGrabacion = grabacionSelected;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            WebcamCtrl.NombreVideo = SelectedGrabacion.Nombre + "_" + DateTime.Now.ToString("yyyy_dd_MM");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModelGrabacion = new GrabacionVideoViewModel(this, SelectedVideo, SelectedAudio, this.WebcamCtrl, SelectedGrabacion);
            this.DataContext = ViewModelGrabacion;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (ViewModelGrabacion != null)
            {
                ViewModelGrabacion.InitializeVm();
            }



        }
    }
}
