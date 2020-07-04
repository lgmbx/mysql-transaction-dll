using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;


namespace DLLMySqlDB
{
    /// <summary>
    /// Classe para executar uma transaçao no MySQL
    /// </summary>
    public class MySqlTransaction{

        
        private string _connectionString;
        /// <summary>
        /// GET - SET connection string
        /// </summary>
        public string ConnectionString {
            get {
                return _connectionString;
            }
            set {
                _connectionString = value;
            }
        }

        /// <summary>
        /// SET - connection string atraves do web.config
        /// </summary>
        /// <param name="name"></param>
        public void WebConfigString(string name) {
            _connectionString = ConfigurationManager.ConnectionStrings[name].ToString();
        }


        /// <summary>
        /// sql query - SELECT - INSERT - UPDATE - DELETE
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Object Query(string query) {
            using(MySqlConnection conn = new MySqlConnection(_connectionString)) {
                using(MySqlCommand cmd = new MySqlCommand(query,conn)) {
                    if (query.ToLower().Contains("select")) {
                        using(MySqlDataAdapter da = new MySqlDataAdapter()) {
                            da.SelectCommand = cmd;
                            using(DataTable dt = new DataTable()) {
                                da.Fill(dt);
                                return dt;
                            }
                        }
                    }
                    else {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
            }
        }
    

    }
}
