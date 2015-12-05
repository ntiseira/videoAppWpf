using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStudioApp.Model
{
    public class ConfigWaterMark   : ModelBase
    {

        private string videoUrl;
        public string VideoUrl
        {
            get
            {
                return videoUrl;
            }
            set
            {
                if (videoUrl != value)
                {
                    videoUrl = value;
                    OnPropertyChanged("VideoUrl");
                }
            }
        }


        private string nombreVideo;
        public string NombreVideo
        {
            get
            {
                return nombreVideo;
            }
            set
            {
                if (nombreVideo != value)
                {
                    nombreVideo = value;
                    OnPropertyChanged("NombreVideo");
                }
            }
        }




    }
}
