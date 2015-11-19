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
using VideoStudioApp.Helper;
using VideoStudioApp.ViewModel;

namespace VideoStudioApp.Views
{
    /// <summary>
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Window
    {

        public Window MenuPrincipal { get; set; }


        public Administrador(Window home)
        {
            InitializeComponent();
            MenuPrincipal = home;

        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            AdministradorViewModel vm = new AdministradorViewModel(MenuPrincipal, this,ref this.plain);
            this.DataContext = vm;
        }
    }
}
