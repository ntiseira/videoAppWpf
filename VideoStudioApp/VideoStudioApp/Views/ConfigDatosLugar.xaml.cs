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
    /// Lógica de interacción para ConfigDatosLugar.xaml
    /// </summary>
    public partial class ConfigDatosLugar : Window
    {
        public Window Home { get; set; }
        public Window Config { get; set; }
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

        public ConfigDatosLugar(Window config, EncoderDevice selectedVideo, EncoderDevice selectedAudio)
        {
            InitializeComponent();            
            Config = config;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigDatosLugarViewModel vm = new ConfigDatosLugarViewModel(this,Config, SelectedVideo, SelectedAudio);
            this.DataContext = vm;
        }
    }
}
