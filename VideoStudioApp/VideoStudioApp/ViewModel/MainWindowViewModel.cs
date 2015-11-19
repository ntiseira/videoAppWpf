using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Views;

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
            this.Home.Hide();
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
            this.Home.Hide();
            ConfigDispositivos admin = new ConfigDispositivos(this.Home);
            admin.ShowDialog();
        }



    }
}
