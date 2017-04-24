using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psiko.Data.Repositories
{
    public class Iletisim
    {
        private SqlConnection _connection;

        public Iletisim (string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Map(System.Data.IDataRecord record, Models.Iletisim entity)
        {
            entity.Id = (int)record["Id"];
            entity.iletisimIcerik = record["iletisimIcerik"].ToString();
            entity.iletisimTarih =(DateTime) record["iletisimTarih"];
            entity.kullaniciID =(int) record["kullaniciID"];
        }

        public Models.Iletisim GetIletisim(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "Select * from Iletisim where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);
            Models.Iletisim obj = Get(cmd);

            return obj;
        }

        public List<Models.Iletisim> GetIletisimListesi()
        {
            SqlCommand cmd = _connection.CreateCommand(); ;
            cmd.CommandText = "Select * from Iletisim";
            return ToList(cmd);
        }
      

        public Models.Iletisim AddIletisim(Models.Iletisim obj)
        {

            SqlCommand cmd = _connection.CreateCommand();

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("insert into Iletisim ([iletisimIcerik],[iletisimTarih],[kullaniciID])");
            sbQuery.Append("values(@iletisimIcerik,@iletisimTarih,@kullaniciID);");
            sbQuery.Append("select SCOPE_IDENTITY()");

            cmd.CommandText = sbQuery.ToString();

            #region Parameters
            cmd.Parameters.AddWithValue("@kullaniciAdSoyad", obj.iletisimIcerik);
            cmd.Parameters.AddWithValue("@kullaniciSifre", obj.iletisimTarih);
            cmd.Parameters.AddWithValue("@tur", obj.kullaniciID);

            #endregion

            object returnValue = cmd.ExecuteScalar();

            if (returnValue != null)
            {
                obj.Id = int.Parse(returnValue.ToString());
            }

            _connection.Close();

            return obj;
        }

        public bool UpdateIletisim(Models.Iletisim obj)
        {
            bool result = false;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("Update Iletisim");
            sbQuery.AppendLine("set ");

            sbQuery.AppendLine("iletisimIcerik=@iletisimIcerik, ");
            sbQuery.AppendLine("iletisimTarih=@iletisimTarih, ");
            sbQuery.AppendLine("kullaniciID=@kullaniciID ");

            sbQuery.AppendLine(" where Id=@Id");
            try
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = sbQuery.ToString();

                #region Parameters
                cmd.Parameters.AddWithValue("@iletisimIcerik", obj.iletisimIcerik);
                cmd.Parameters.AddWithValue("@iletisimTarih", obj.iletisimTarih);
                cmd.Parameters.AddWithValue("@kullaniciID", obj.kullaniciID);
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                #endregion

                var returnValue = cmd.ExecuteNonQuery();

                if (int.Parse(returnValue.ToString()) > 0)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            _connection.Close();

            return result;
        }

        public bool DeleteIletisim(int Id)
        {
            bool result = false;
            try
            {
                Models.Iletisim obj = GetIletisim(Id);
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "Delete from Iletisim where Id=@Id";
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

        protected Models.Iletisim Get(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    var item = new Models.Iletisim();
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
        protected List<Models.Iletisim> ToList(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    List<Models.Iletisim> items = new List<Models.Iletisim>();
                    while (reader.Read())
                    {
                        var item = new Models.Iletisim();
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
