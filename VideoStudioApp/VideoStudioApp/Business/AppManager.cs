using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStudioApp.Data_Access;
using VideoStudioApp.Model;

namespace VideoStudioApp.Business
{
    public class AppManager
    {


        public List<Reporte> GetReportes()
        {
            List<Reporte> reportes = new List<Reporte>();
            return reportes;
        }

        public bool ValidateUser(string userName, string pass)
        {
            bool respu = false;
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            OrderedDictionary listParam = new OrderedDictionary();
            listParam.Add("userName", userName);
            listParam.Add("pass", pass);
              var data =  mysqlAccess.ExecuteProcedure("ValidarUsuario", listParam);
              if (data.Rows.Count > 0)
              {
                  respu = true;
              }
              return respu;
        }


        public List<ComboBoxD> GetColonias()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
           var data = mysqlAccess.ExecuteProcedure("ObtenerColonias", null);
           return list;
        }

        public List<ComboBoxD> GetBrigadas()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            var data = mysqlAccess.ExecuteProcedure("ObtenerBrigadas", null);
            return list;
        }

        public List<ComboBoxD> GetLugares()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            var data = mysqlAccess.ExecuteProcedure("ObtenerLugares", null);
            return list;
        }


    }
}
