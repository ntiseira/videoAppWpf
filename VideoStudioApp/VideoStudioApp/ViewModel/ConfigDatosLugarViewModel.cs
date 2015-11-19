﻿using Microsoft.Expression.Encoder.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Business;
using VideoStudioApp.Command;
using VideoStudioApp.Model;
using VideoStudioApp.Views;

namespace VideoStudioApp.ViewModel
{
   public class ConfigDatosLugarViewModel:ViewModelBase
    {

        public Window Home { get; set; }
       public Window CurrentWindow { get; set; }
       public Grabacion GrabacionVideoCurrent { get; set; }

       public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

       public List<ComboBoxD> ListaBrigadas { get; set; }

       public List<ComboBoxD> ListaMunicipios { get; set; }
       public List<ComboBoxD> ListaColonias { get; set; }
       
       public ConfigDatosLugarViewModel(Window home, Window current, EncoderDevice selectedAudio, EncoderDevice selectedVideo)
        {
            Home = home;
            CurrentWindow = current;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            GrabacionVideoCurrent = new Grabacion();
            CargarCombos();
        }

       public void CargarCombos()
       {
           AppManager appManager = new AppManager();
           ListaBrigadas = appManager.GetBrigadas();
           ListaColonias = appManager.GetColonias();
           ListaMunicipios = appManager.GetMunicipios();
       }


       private ICommand iniciarConfigDatosPersonaCommand;
        public ICommand IniciarConfigDatosPersonaCommand
        {
            get
            {
                if (iniciarConfigDatosPersonaCommand == null)
                {
                    iniciarConfigDatosPersonaCommand = new RelayCommand(
                        param => this.IniciarConfigDatosPersona()
                    );
                }
                return iniciarConfigDatosPersonaCommand;
            }
        }

        private void IniciarConfigDatosPersona()
        {
            this.CurrentWindow.Close();
            ConfigDatosPersona viewGra = new ConfigDatosPersona(this.Home, SelectedAudio, SelectedVideo);
            viewGra.ShowDialog();
        }



    }
}
