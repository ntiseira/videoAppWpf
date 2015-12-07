using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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


        public void AgregarColonia(string colonia, string municipio)
        {
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            OrderedDictionary listParam = new OrderedDictionary();
            listParam.Add("nombreColonia", colonia);
            listParam.Add("nombreMunicipio", municipio);

            var data = mysqlAccess.ExecuteProcedure("AgregarColonia", listParam);
        }


            public void AgregarGrabacion(string lugar, string brigada, string colonia, string municipio, string nombre, DateTime edad)
        {
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            OrderedDictionary listParam = new OrderedDictionary();
            listParam.Add("pLugar", lugar);
            listParam.Add("pBrigada", brigada);
            listParam.Add("pColonia", colonia);
            listParam.Add("pMunicipio", municipio);
            listParam.Add("pNombre", nombre);
            listParam.Add("pEdad", edad);                

            var data = mysqlAccess.ExecuteProcedure("AgregarGrabacion", listParam);
        }

        


        public void AgregarLugar(string lugar)
        {
            MySqlDataAccess mysqlAccess = new MySqlDataAccess();
            OrderedDictionary listParam = new OrderedDictionary();
            listParam.Add("nombreLugar", lugar);

            var data = mysqlAccess.ExecuteProcedure("AgregarLugar", listParam);
        }

        public List<Reporte> GetReporte( string edad, string brigada, string lugar, string municipio, string colonia,
            DateTime fechaInicial, DateTime fechaHasta)
        {
            try
            {
                List<Reporte> list = new List<Reporte>();
                MySqlDataAccess mysqlAccess = new MySqlDataAccess();
                OrderedDictionary listParam = new OrderedDictionary();
                

                listParam.Add("pEdad", int.Parse(edad));
                listParam.Add("pBrigada", brigada);
                listParam.Add("pLugar", lugar);
                listParam.Add("pMunicipio", municipio);
                listParam.Add("pColonia", colonia);             

                listParam.Add("pFechaInicial", fechaInicial.Date);


                listParam.Add("pFechaFinal", fechaHasta.Date);
                var data = mysqlAccess.ExecuteProcedure("ObtenerReporte", listParam);


                foreach (DataRow row in data.Rows)
                {
                    Reporte itemReporte = new Reporte();
                    itemReporte.Brigada = row["brigada"].ToString();
                    itemReporte.Colonia = row["colonia"].ToString();

                    DateTime edadD = Convert.ToDateTime(row["edad"].ToString());


                    itemReporte.Edad = (DateTime.Now.Year - edadD.Year).ToString();
                    itemReporte.Lugar = row["lugar"].ToString();
                    itemReporte.Municipio = row["municipio"].ToString();
                    itemReporte.Nombre = row["nombre"].ToString();
                    itemReporte.FechaInicial = Convert.ToDateTime(row["creado"].ToString());

                    list.Add(itemReporte);
                }

                return list;
            }

            catch (Exception ex)
            {
                throw ex;
            }
         //   return ParseDataTableComboBox(data);
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
