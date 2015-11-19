using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStudioApp.Model
{
    public class Grabacion : ModelBase
    {

        private string edad;
        public string Edad
        {
            get
            {
                return edad;
            }
            set
            {
                if (edad != value)
                {
                    edad = value;
                    OnPropertyChanged("Edad");
                }
            }
        }



        private string brigada;
        public string Brigada
        {
            get
            {
                return brigada;
            }
            set
            {
                if (brigada != value)
                {
                    brigada = value;
                    OnPropertyChanged("Brigada");
                }
            }
        }


        private string lugar;
        public string Lugar
        {
            get
            {
                return lugar;
            }
            set
            {
                if (lugar != value)
                {
                    lugar = value;
                    OnPropertyChanged("Lugar");
                }
            }
        }

        

        private string municipio;
        public string Municipio
        {
            get
            {
                return municipio;
            }
            set
            {
                if (municipio != value)
                {
                    municipio = value;
                    OnPropertyChanged("Municipio");
                }
            }
        }




        private string colonia;
        public string Colonia
        {
            get
            {
                return colonia;
            }
            set
            {
                if (colonia != value)
                {
                    colonia = value;
                    OnPropertyChanged("Colonia");
                }
            }
        }

        private string otrosColonia;
        public string OtrosColonia
        {
            get
            {
                return otrosColonia;
            }
            set
            {
                if (otrosColonia != value)
                {
                    otrosColonia = value;
                    OnPropertyChanged("OtrosColonia");
                }
            }
        }


    }
}
