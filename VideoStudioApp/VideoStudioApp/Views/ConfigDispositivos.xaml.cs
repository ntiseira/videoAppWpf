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
    /// Lógica de interacción para ConfigDispositivos.xaml
    /// </summary>
    public partial class ConfigDispositivos : Window
    {
        


        public ConfigDispositivos()
        {
            InitializeComponent();        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigDispositivosViewModel vm = new ConfigDispositivosViewModel(this);
            this.DataContext = vm;
        }


    }
}
