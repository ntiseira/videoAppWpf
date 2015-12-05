using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Expression.Encoder;
using VideoStudioApp.Command;
using VideoStudioApp.Helper;
using VideoStudioApp.Views;
using System.Drawing;
using NReco.VideoConverter;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using Microsoft.Reporting.WinForms;
using VideoStudioApp.Model;


namespace VideoStudioApp.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {

        //public Window CurrentWindow { get; set; }
        public Window Home { get; set; }

        public MainWindowViewModel(Window home)
        {
            //CurrentWindow = window;
            Home = home;
         //   CargarReporte();
          // Prueba();
            
        }
           


        private ICommand iniciarAdminCommand;
        public ICommand IniciarAdminCommand
        {
            get
            {
                if (iniciarAdminCommand == null)
                {
                    iniciarAdminCommand = new RelayCommand(
                        param => this.IniciarAdmin()
                    );
                }
                return iniciarAdminCommand;
            }
        }


        private void IniciarAdmin()
        {
            this.Home.Close();
            Administrador admin = new Administrador( this.Home);
            admin.ShowDialog();
        }



        private ICommand iniciarAppCommand;
        public ICommand IniciarAppCommand
        {
            get
            {
                if (iniciarAppCommand == null)
                {
                    iniciarAppCommand = new RelayCommand(
                        param => this.IniciarApp()
                    );
                }
                return iniciarAppCommand;
            }
        }


        private void IniciarApp()
        {

            ConfigDispositivos admin = new ConfigDispositivos();
            admin.ShowDialog();
            this.Home.Close();
       //     this.Dispose();
        }



    }
}
