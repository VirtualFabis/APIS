using apis.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace apis.Controllers.Querys
{
    public class EveryQuerys
    {
        public object[] Sql_Login(Users request)
        {
            try
            {
                string query = @"SELECT * FROM usuarios WHERE wiw = @wiw and password = @password";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@wiw", request.wiw);
                cmd.Parameters.AddWithValue("@password", request.password);
                DataTable dt = new DataAccess().GetTable(cmd);

                if (dt.Rows.Count > 0)
                {
                    object[] dr = dt.Rows[0].ItemArray;
                    Users response = new Users()
                    {
                        id = Convert.ToInt16(dr[0]),
                        wiw = dr[1].ToString(),
                        name = dr[2].ToString(),
                        email = dr[3].ToString(),
                        password = dr[4].ToString(),
                    };

                    return new object[] { true, response };
                }
                else
                {
                    return new object[] { false, "User or password Incorrect" };
                }
            }
            catch (Exception ex)
            {
                return new object[] { false, $"Query Exception: {ex.Message}" };
            }
        }
        public object[] Sql_Register(Users request)
        {
            try
            {
                string query = @"INSERT INTO usuarios (id, wiw, name, email, password)
                    VALUES (NULL, @wiw, @name, @email, @password); SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@wiw", request.wiw);
                cmd.Parameters.AddWithValue("@name", request.name);
                cmd.Parameters.AddWithValue("@email", request.email);
                cmd.Parameters.AddWithValue("@password", request.password);
                DataTable dt = new DataAccess().GetTable(cmd);

                if (dt.Rows.Count > 0)
                {
                    object[] dr = dt.Rows[0].ItemArray;
                    Users response = new Users()
                    {
                        id = Convert.ToInt16(dr[0]),
                    };

                    return new object[] { true, response };
                }
                else
                {
                    return new object[] { false, "Request no inserted" };
                }
            }
            catch (Exception ex)
            {
                return new object[] { false, $"Query Exception: {ex.Message}" };
            }
        }
        public object[] Sql_InsertTask(Task request)
        {
            try
            {
                string query = @"INSERT INTO task (id, title, description, activate)
                    VALUES (@id, @title, @description, @activate); SELECT LAST_INSERT_ID()";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@id", request.id);
                cmd.Parameters.AddWithValue("@title", request.task);
                cmd.Parameters.AddWithValue("@description", request.desc);
                cmd.Parameters.AddWithValue("@activate", true);
                DataTable dt = new DataAccess().GetTable(cmd);

                if (dt.Rows.Count > 0)
                {
                    object[] dr = dt.Rows[0].ItemArray;
                    Task response = new Task()
                    {
                        id = Convert.ToInt16(dr[0]),
                    };

                    return new object[] { true, response };
                }
                else
                {
                    return new object[] { false, "Request no inserted" };
                }
            }
            catch (Exception ex)
            {
                return new object[] { false, $"Query Exception: {ex.Message}" };
            }
        }
        public object[] SQL_GetTask(Task request)
        {
            try
            {
                List<Task> list = new List<Task>();

                string query = @"SELECT id, idTask, title, description, activate FROM task WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@id", request.id);

                DataTable dt = new DataAccess().GetTable(cmd);

                foreach (DataRow row in dt.Rows)
                {
                    Task s = new Task()
                    {
                        id = Convert.ToInt16(row[0]),
                        idTask = Convert.ToInt16(row[1]),
                        task = row.ItemArray[2].ToString(),
                        desc = row.ItemArray[3].ToString(),
                        activate = Convert.ToBoolean(row[4]),
                    };

                    list.Add(s);
                }

                return new object[] { true, list };
            }
            catch (Exception ex)
            {
                return new object[] { false, "Query Exception: " + ex.Message };
            }
        }
        public object[] Sql_UpdateTask(Task request)
        {
            try
            {
                string query = @"UPDATE task SET activate = @activate WHERE idTask = @idTask;SELECT LAST_INSERT_ID();";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@idTask", request.idTask);
                cmd.Parameters.AddWithValue("@activate", request.activate);
                DataTable dt = new DataAccess().GetTable(cmd);

                if (dt.Rows.Count > 0)
                {
                    object[] dr = dt.Rows[0].ItemArray;
                    Task response = new Task()
                    {
                        id = Convert.ToInt16(dr[0]),
                    };

                    return new object[] { true, response };
                }
                else
                {
                    return new object[] { false, "Request no Updated" };
                }
            }
            catch (Exception ex)
            {
                return new object[] { false, $"Query Exception: {ex.Message}" };
            }
        }
        public object[] Sql_DeleteTask(Task request)
        {
            try
            {
                string query = @"DELETE  FROM task WHERE idTask = @idTask;";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("@idTask", request.idTask);
                DataTable dt = new DataAccess().GetTable(cmd);

                if (dt.Rows.Count > 0)
                {
                    object[] dr = dt.Rows[0].ItemArray;
                    Task response = new Task()
                    {
                        id = Convert.ToInt16(dr[0]),
                    };

                    return new object[] { true, response };
                }
                else
                {
                    return new object[] { false, "Request no Updated" };
                }
            }
            catch (Exception ex)
            {
                return new object[] { false, $"Query Exception: {ex.Message}" };
            }
        }
    }
}
    
    public class DataAccess
    {
        private string SqlServer()
        {
            return "datasource=127.0.0.1;port=3306;username=root;password=;database=fabis;";
        }

        public dynamic GetTable(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(SqlServer()))
            {
                cmd.Connection = con;
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        var tempdrcols = dr[col] == DBNull.Value ? " " : dr[col];
                        row.Add(col.ColumnName, tempdrcols);
                    }
                    rows.Add(row);
                }

                return dt;
            }
        }
    }
