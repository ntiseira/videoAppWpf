using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
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


        public void AgregarColonia(string colonia, string municipio)
        {
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            OrderedDictionary listParam = new OrderedDictionary();
            listParam.Add("nombreColonia", colonia);
            listParam.Add("nombreMunicipio", municipio);

            var data = mysqlAccess.ExecuteProcedure("AgregarColonia", listParam);
        }


        public void AgregarLugar(string lugar)
        {
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            OrderedDictionary listParam = new OrderedDictionary();
            listParam.Add("nombreLugar", lugar);

            var data = mysqlAccess.ExecuteProcedure("AgregarLugar", listParam);
        }



        public List<ComboBoxD> GetMunicipios()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            var data = mysqlAccess.ExecuteProcedure("ObtenerMunicipios", null);
            return ParseDataTableComboBox(data);
        }

        public List<ComboBoxD> GetColonias()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
           var data = mysqlAccess.ExecuteProcedure("ObtenerColonias", null);
           return ParseDataTableComboBox(data);
        }

        public List<ComboBoxD> GetBrigadas()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            var data = mysqlAccess.ExecuteProcedure("ObtenerBrigadas", null);
            return ParseDataTableComboBox(data);
        }

        public List<ComboBoxD> GetLugares()
        {
            List<ComboBoxD> list = new List<ComboBoxD>();
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            var data = mysqlAccess.ExecuteProcedure("ObtenerLugares", null);

            return ParseDataTableComboBox(data);
            
        }


        private List<ComboBoxD> ParseDataTableComboBox(System.Data.DataTable dataTable)
        {
           List<ComboBoxD> list = new List<ComboBoxD>();

            for(int i = 0; i< dataTable.Rows.Count ; i ++)
            {
                DataRow row = dataTable.Rows[i];
                var values = row.ItemArray;
                ComboBoxD cbo = new ComboBoxD();

                if (values.Count() == 1)
                {
                    cbo.ID = i.ToString() ;
                    cbo.Descripcion = values[0].ToString();
                }
                
                else
                {
                    cbo.ID = values[0].ToString();
                    cbo.Descripcion = values[1].ToString();    
                }
                
                list.Add(cbo);
            }
            return list;
        }


    }
}
