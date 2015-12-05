using Microsoft.Expression.Encoder.Devices;
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

       public Window CurrentWindow { get; set; }

       public Window ConfigWindow { get; set; }


       public Grabacion GrabacionVideoCurrent { get; set; }

       public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }

       public List<ComboBoxD> ListaBrigadas { get; set; }

       public List<ComboBoxD> ListaMunicipios { get; set; }
       public List<ComboBoxD> ListaColonias { get; set; }

       public ConfigDatosLugarViewModel( Window current, Window config, EncoderDevice selectedAudio, EncoderDevice selectedVideo)
       {         
            CurrentWindow = current;
            ConfigWindow = config;
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


        private ICommand regresarMenuCommand;
        public ICommand RegresarMenuCommand
        {
            get
            {
                if (regresarMenuCommand == null)
                {
                    regresarMenuCommand = new RelayCommand(
                        param => this.RegresarMenu()
                    );
                }
                return regresarMenuCommand;
            }
        }

        private void RegresarMenu()
        {
            MainWindow principal = new MainWindow();
            principal.ShowDialog();
          //  this.Dispose();
            this.CurrentWindow.Close();
        }


        private ICommand regresarPreviewCommand;
        public ICommand RegresarPreviewCommand
        {
            get
            {
                if (regresarPreviewCommand == null)
                {
                    regresarPreviewCommand = new RelayCommand(
                        param => this.RegresarPreview()
                    );
                }
                return regresarPreviewCommand;
            }
        }

        private void RegresarPreview()
        {   
            
            ConfigDispositivos view = new ConfigDispositivos();
            view.ShowDialog();
           // this.Dispose();
            this.CurrentWindow.Close();
          
        }


        private void IniciarConfigDatosPersona()
        {

            AppManager appManager = new AppManager();
      

            if (GrabacionVideoCurrent.Colonia == "Colonia-Otros")
            {
                if (GrabacionVideoCurrent.OtrosColonia == null || GrabacionVideoCurrent.Municipio == null)
                {
                    System.Windows.MessageBox.Show("Debe ingresar una colonia ");

                }
                else
                {
                    if (GrabacionVideoCurrent.Municipio != null && GrabacionVideoCurrent.Brigada != null &&
                        GrabacionVideoCurrent.Colonia != null && GrabacionVideoCurrent.Lugar != null)
                    {
                        appManager.AgregarColonia(GrabacionVideoCurrent.OtrosColonia, GrabacionVideoCurrent.Municipio);
                        appManager.AgregarLugar(GrabacionVideoCurrent.Lugar);

                        ConfigDatosPersona viewGra = new ConfigDatosPersona(null,SelectedAudio, SelectedVideo,
                            GrabacionVideoCurrent);
                        viewGra.ShowDialog();

                        this.CurrentWindow.Close();
                       // this.Dispose();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Por favor complete todos los datos");
                    }
                }

            }
            else
            {
                  if (GrabacionVideoCurrent.Municipio != null && GrabacionVideoCurrent.Brigada != null &&
                        GrabacionVideoCurrent.Colonia != null && GrabacionVideoCurrent.Lugar != null)
                    {
                appManager.AgregarLugar(GrabacionVideoCurrent.Lugar);

                
                ConfigDatosPersona viewGra = new ConfigDatosPersona(null, SelectedAudio, SelectedVideo, GrabacionVideoCurrent);
                viewGra.ShowDialog();

                this.CurrentWindow.Close();
               // this.Dispose();
                    }
                  else
                  {
                      System.Windows.MessageBox.Show("Por favor complete todos los datos");
                  }
            }


          
        }



    }
}
