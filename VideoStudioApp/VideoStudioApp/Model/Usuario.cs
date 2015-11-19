using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStudioApp.Model
{
    public class Usuario : ModelBase
    {


        private string nombreUsuario;
       public string NombreUsuario
        {
            get
            {
                return nombreUsuario;
            }
            set
            {
                if (nombreUsuario != value)
                {
                    nombreUsuario = value;
                    OnPropertyChanged("NombreUsuario");
                }
            }
        }


       private string password;
       public string Password
       {
           get
           {
               return password;
           }
           set
           {
               if (password != value)
               {
                   password = value;
                   OnPropertyChanged("Password");
               }
           }
       }




    }
}
