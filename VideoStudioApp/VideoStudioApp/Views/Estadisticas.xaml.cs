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
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Estadisticas : Window
    {
        public Window Home { get; set; }

        public Estadisticas(Window home)
        {
            InitializeComponent();
            Home = home;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             EstadisticasViewModel vm = new EstadisticasViewModel(Home, this);
            this.DataContext = vm;
        }
        
    }
}
