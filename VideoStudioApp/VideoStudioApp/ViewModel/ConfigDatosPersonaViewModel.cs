﻿using Microsoft.Expression.Encoder.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VideoStudioApp.Command;
using VideoStudioApp.Model;
using VideoStudioApp.Views;

namespace VideoStudioApp.ViewModel
{
    public class ConfigDatosPersonaViewModel:ViewModelBase
    {

        public Window GrabacionW { get; set; }
       public Window CurrentWindow { get; set; }

      
        public EncoderDevice SelectedVideo { get; set; }
        public EncoderDevice SelectedAudio { get; set; }
        public Grabacion SelectedGrabacion { get; set; }


        public ConfigDatosPersonaViewModel(Window grabacionW,  Window current, EncoderDevice selectedAudio,
            EncoderDevice selectedVideo, Grabacion grabacion)
        {
            GrabacionW = grabacionW;
            SelectedGrabacion = grabacion;
            CurrentWindow = current;
            SelectedAudio = selectedAudio;
            SelectedVideo = selectedVideo;
            CargarDatosLugar();
            SelectedGrabacion.Edad = "";
            SelectedGrabacion.Nombre = "";
        }

        private void CargarDatosLugar()
        {
            TextBrigada = SelectedGrabacion.Brigada;
            TextLugar = SelectedGrabacion.Lugar;
            TextMunicipio = SelectedGrabacion.Municipio;
            TextColonia = SelectedGrabacion.Colonia;        
        }


        private string textBrigada;
        public string TextBrigada
        {
            get
            {
                return textBrigada;
            }
            set
            {
                textBrigada = value;
                OnPropertyChanged("TextBrigada");
            }
        }


        private string textLugar;
        public string TextLugar
        {
            get
            {
                return textLugar;
            }
            set
            {
                textLugar = value;
                OnPropertyChanged("TextLugar");
            }
        }


        private string textMunicipio;
        public string TextMunicipio
        {
            get
            {
                return textMunicipio;
            }
            set
            {
                textMunicipio = value;
                OnPropertyChanged("TextMunicipio");
            }
        }



        private string textColonia;
        public string TextColonia
        {
            get
            {
                return textColonia;
            }
            set
            {
                textColonia = value;
                OnPropertyChanged("TextColonia");
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
             this.CurrentWindow.Close();
             MainWindow main = new MainWindow();
             main.ShowDialog();
         }


        

        private ICommand iniciarGrabacionCommand;
        public ICommand IniciarGrabacionCommand
        {
            get
            {
                if (iniciarGrabacionCommand == null)
                {
                    iniciarGrabacionCommand = new RelayCommand(
                        param => this.IniciarGrabacion()
                    );
                }
                return iniciarGrabacionCommand;
            }
        }

        private void IniciarGrabacion()
        {
            if (SelectedGrabacion.Edad == null || SelectedGrabacion.Nombre == null)
            {
                System.Windows.MessageBox.Show("Debe ingresar nombre y la edad");       
            }
            else
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Convert.ToInt32(SelectedGrabacion.Edad);
                today.AddYears(-age);
                SelectedGrabacion.Edad = today.ToString();


                this.CurrentWindow.Close();


                if (GrabacionW == null)
                {
                    GrabacionVideo viewGra = new GrabacionVideo(SelectedAudio, SelectedVideo, SelectedGrabacion);
                    viewGra.ShowDialog();
                }
                else
                {

                    var vm = (GrabacionVideoViewModel)GrabacionW.DataContext;
                    vm.SelectedGrabacion = SelectedGrabacion;
                    vm.InitializeVm();
                    GrabacionW.DataContext = vm;
                    GrabacionW.Show();
                    
                }

            
             
            
            }

            
        }



    }
}
