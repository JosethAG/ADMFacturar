using System.Data;
using System.Data.SqlClient;

namespace ADM.Architectur
{
    public class DBData
    {
        public static string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=ADM;Integrated Security=True;";

        public static DataSet TableList(string SPName, List<DBParameter> parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SPName, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }
                }
                DataSet table = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable List(string SPName, List<DBParameter> parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SPName, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }
                }
                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);


                return table;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool Execute(string SPName, List<DBParameter> parameters = null)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SPName, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.AddWithValue(parameter.Name, parameter.Value);
                    }
                }

                int i = cmd.ExecuteNonQuery();

                return (i > 0) ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public static int ExecuteCP(string storedProcedure, List<DBParameter> parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    // Asegúrate de manejar correctamente los tipos de datos
                    SqlParameter sqlParameter;
                    if (param.Value == null)
                    {
                        sqlParameter = new SqlParameter(param.Name, DBNull.Value);
                    }
                    else
                    {
                        sqlParameter = new SqlParameter(param.Name, param.Value);
                    }
                    command.Parameters.Add(sqlParameter);
                }

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                connection.Open();
                command.ExecuteNonQuery();

                return (int)returnParameter.Value;
            }
        }

        public static int ExecuteCC(string storedProcedure, List<DBParameter> parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    // Asegúrate de manejar correctamente los tipos de datos
                    SqlParameter sqlParameter;
                    if (param.Value == null)
                    {
                        sqlParameter = new SqlParameter(param.Name, DBNull.Value);
                    }
                    else
                    {
                        sqlParameter = new SqlParameter(param.Name, param.Value);
                    }
                    command.Parameters.Add(sqlParameter);
                }

                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                connection.Open();
                command.ExecuteNonQuery();

                return (int)returnParameter.Value;
            }
        }
        public static bool ExecuteLoginValidation(string SPName, string usuario, string contrasena)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SPName, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros específicos para RealizarLogin
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Contra", contrasena);

                // Parámetro de salida
                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@Resultado";
                outputParameter.SqlDbType = SqlDbType.Bit;
                outputParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                cmd.ExecuteNonQuery();

                bool isValid = Convert.ToBoolean(outputParameter.Value);

                return isValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing stored procedure {SPName}: {ex.Message}");
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
