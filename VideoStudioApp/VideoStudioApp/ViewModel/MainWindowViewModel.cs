﻿using System;
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
          // Prueba();
            
        }


      

        public void Prueba()
        {
            try
            {
                var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
              
                ffMpeg.Invoke("-i c:\\NICOLAS_2015_27_11.avi -i C:\\marcaAgua.jpg -filter_complex \"overlay=350:300\" -codec:a copy pruebaadddaaa.avi");

                


                //         ffMpeg.Invoke("-i " + WebcamCtrl.VideoDirectory + "\\" + WebcamCtrl.NombreVideo + ".mp4 " + "-i " + pathMarcaAgua + " -filter_complex \"overlay=10:30\" -codec:a copy videova.mp4");




                // NReco.VideoConverter.FFMpegConverter wrap = new FFMpegConverter();
                //wrap.Invoke("-i c:\\test.avi -i C:\\prueba.bmp -filter_complex \"overlay=10:10\" c:\\test2.avi");

                //var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                //Convierte de formato
                //  ffMpeg.ConvertMedia("c:\\test.avi", "video.mp4", Format.mp4);

              //  ffMpeg.Invoke("-i c:\\video.mp4 -i C:\\tigre.jpg -filter_complex \"[0:v][1:v]overlay=main_w-overlay_w-10:10\" -codec:a copy videova.mp4");

                // ffMpeg.Invoke("-i c:\\video.mp4 -i C:\\prueba.bmp -filter_complex \"[0:v][1:v]overlay=main_w-overlay_w-10:10\" -codec:a copy videoaa.mp4");
          
            }
            catch (Exception ex)
            {
                throw ex;
            }

        

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
