using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using VideoStudioApp.Business;
using VideoStudioApp.Command;
using VideoStudioApp.Helper;
using VideoStudioApp.Model;
using VideoStudioApp.Views;

namespace VideoStudioApp.ViewModel
{
   public class AdministradorViewModel : ViewModelBase
   {

       public Window Home { get; set; }
       public Window CurrentWindow { get; set; }
       public TextBlock plain { get; set; }

       public AdministradorViewModel(Window home, Window window, ref TextBlock txtPlain)
        {
            plain = txtPlain;
            CurrentWindow = window;
            Home = home;
         
        }


       private string _PasswordInVM;
       public string PasswordInVM
       {
           get
           {
               return _PasswordInVM;
           }
           set
           {
               _PasswordInVM = value;
               OnPropertyChanged("PasswordInVM");
           }
       }




       private ICommand loginCommand;
       public ICommand LoginCommand
       {
           get
           {
               if (loginCommand == null)
               {
                   loginCommand = new RelayCommand(
                     param => IniciarEstadisticas()
                   );
               }
               return loginCommand;
           }
       }


        private void IniciarEstadisticas()
        {

            AppManager manager = new AppManager();
            var text = plain.Text;
            
            if (manager.ValidateUser("ntiseira", text))
            {
                this.CurrentWindow.Close();
                Estadisticas viewEsta = new Estadisticas(this.Home);
                viewEsta.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("El usuario y/o contraseña son incorrectos");           
            }
                        
        }


        private ICommand regresarCommand;
        public ICommand RegresarCommand
        {
            get
            {
                if (regresarCommand == null)
                {
                    regresarCommand = new RelayCommand(
                        param => this.Regresar()
                    );
                }
                return regresarCommand;
            }
        }


        private void Regresar()
        {
            this.CurrentWindow.Close();
            this.Home.Show();
        }




    }
}
