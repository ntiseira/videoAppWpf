using Microsoft.Expression.Encoder.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para ConfigDatosPersona.xaml
    /// </summary>
    public partial class ConfigDatosPersona : Window
    {
        public Window GrabacionWindow { get; set; }
    
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }
        public Grabacion SelectedGrabacion { get; set; }

        public ConfigDatosPersona(Window grabacionW, EncoderDevice selectedVideo, EncoderDevice selectedAudio, Grabacion grabacion)
        {
            InitializeComponent();
            GrabacionWindow = grabacionW;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            SelectedGrabacion = grabacion;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigDatosPersonaViewModel vm = new ConfigDatosPersonaViewModel(GrabacionWindow, this, SelectedVideo, SelectedAudio, SelectedGrabacion);
            this.DataContext = vm;
        }

        public static bool onlyNumeric(string text)
                {
                Regex regex = new Regex("[^0-9.-]+"); //regex that allows numeric input only
                return !regex.IsMatch(text); // 
                }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !onlyNumeric(e.Text);
        }
    }
}
