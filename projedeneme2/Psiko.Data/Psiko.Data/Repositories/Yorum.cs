using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psiko.Data.Repositories
{
    public class Yorum
    {
        private SqlConnection _connection;

        public Yorum(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Map(System.Data.IDataRecord record, Models.Yorum entity)
        {
            entity.Id = (int)record["Id"];
            entity.yorumIcerik = record["yorumIcerik"].ToString();
            entity.yorumTarih = (DateTime)record["yorumTarih"];
            entity.yorumOnay = (bool)record["yorumOnay"];
            entity.yorumResim = record["yorumResim"].ToString();
            entity.makaleID = (int)record["makaleID"];
            entity.kullaniciID = (int)record["kullaniciID"];

        }

        public Models.Yorum GetYorum(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "Select * from Yorum where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);
            Models.Yorum obj = Get(cmd);

            return obj;
        }

        public List<Models.Yorum> GetYorumListesi()
        {
            SqlCommand cmd = _connection.CreateCommand(); ;
            cmd.CommandText = "Select * from Yorum";
            return ToList(cmd);
        }
        
        public Models.Yorum AddYorum(Models.Yorum obj)
        {

            SqlCommand cmd = _connection.CreateCommand();

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("insert into Yorum ([yorumIcerik],[yorumTarih],[yorumOnay],[yorumResim],[makaleID],[kullaniciID])");
            sbQuery.Append("values(@yorumIcerik,@yorumTarih,@yorumOnay,@yorumResim,@makaleID,@kullaniciID);");
            sbQuery.Append("select SCOPE_IDENTITY()");

            cmd.CommandText = sbQuery.ToString();

            #region Parameters
            cmd.Parameters.AddWithValue("@yorumIcerik", obj.yorumIcerik);
            cmd.Parameters.AddWithValue("@yorumTarih", obj.yorumTarih);
            cmd.Parameters.AddWithValue("@yorumOnay", obj.yorumOnay);
            cmd.Parameters.AddWithValue("@yorumResim", obj.yorumResim);
            cmd.Parameters.AddWithValue("@makaleID", obj.makaleID);
            cmd.Parameters.AddWithValue("@kullaniciID", obj.kullaniciID);

            #endregion

            object returnValue = cmd.ExecuteScalar();

            if (returnValue != null)
            {
                obj.Id = int.Parse(returnValue.ToString());
            }

            _connection.Close();

            return obj;
        }

       
        public bool DeleteYorum(int Id)
        {
            bool result = false;
            try
            {
                Models.Yorum obj = GetYorum(Id);
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "Delete from Yorum where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                var returnValue = cmd.ExecuteNonQuery();

                _connection.Close();

                if (int.Parse(returnValue.ToString()) > 0)
                {

                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        protected Models.Yorum Get(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    var item = new Models.Yorum();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        Map(reader, item);
                    }

                    _connection.Close();

                    return item;
                }
            }
            catch
            {
                return null;
            }



        }
        protected List<Models.Yorum> ToList(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    List<Models.Yorum> items = new List<Models.Yorum>();
                    while (reader.Read())
                    {
                        var item = new Models.Yorum();
                        Map(reader, item);
                        items.Add(item);
                    }

                    _connection.Close();


                    return items;
                }
            }
            catch
            {
                return null;
            }


        }

    }
}
