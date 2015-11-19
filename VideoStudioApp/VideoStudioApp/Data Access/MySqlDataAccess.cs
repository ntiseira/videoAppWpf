using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStudioApp.Data_Access
{
   public class MySqlDataAccess
   {

       private static MySqlDataAccess instancia;
       private string ConnectionString { get; set; }

       public static MySqlDataAccess GetInstance()
       {
           if (instancia == null)
           {
               instancia = new MySqlDataAccess();
           }
           return instancia;
       }


       public MySqlDataAccess()
       {
           ConnectionString = ConfigurationManager.AppSettings["DataBaseDetails"];
       
       }

       public DataTable ExecuteProcedure(string procedureName, OrderedDictionary listParam)
       {
           DataTable dt = new DataTable();
           using (MySqlConnection con = new MySqlConnection(GetInstance().ConnectionString))
           {
               using (MySqlCommand cmd = new MySqlCommand(procedureName, con))
               {
                   cmd.CommandType = CommandType.StoredProcedure;

                   if (listParam != null && listParam.Count > 0)
                   {
                       foreach (DictionaryEntry parameter in listParam)
                       {
                           cmd.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);                        
                       }
                   }
                  
                  
                   using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                   {
                     
                       sda.Fill(dt);                       
                   }
               }             
           }

           return dt;
       }









    }
}
