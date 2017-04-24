using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psiko.Data.Repositories
{
    public class Kullanici
    {
        private SqlConnection _connection;

        public Kullanici(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Map(System.Data.IDataRecord record, Models.Kullanici entity)
        {
            entity.Id = (int)record["Id"];
            entity.AdSoyadi = record["kullaniciAdSoyad"].ToString();
            entity.Sifre = record["kullaniciSifre"].ToString();
            entity.Tur = record["tur"].ToString();
        }

        public Models.Kullanici GetKullanici(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "Select * from Kullanici where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);
            Models.Kullanici obj = Get(cmd);

            return obj;
        }

        public List<Models.Kullanici> GetKullaniciListesi()
        {
            SqlCommand cmd = _connection.CreateCommand(); ;
            cmd.CommandText = "Select * from Kullanici";
            return ToList(cmd);
        }
        public List<Models.Kullanici> GetKullaniciListesiByTur(string tur)
        {
            SqlCommand cmd = _connection.CreateCommand(); ;
            cmd.CommandText = "Select * from Kullanici where [tur]=@tur";
            cmd.Parameters.AddWithValue("@tur", tur);
            return ToList(cmd);
        }

        public Models.Kullanici AddKullanici(Models.Kullanici obj)
        {
           
            SqlCommand cmd = _connection.CreateCommand();

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("insert into Kullanici ([kullaniciAdSoyad],[kullaniciSifre],[tur])");
            sbQuery.Append("values(@kullaniciAdSoyad,@kullaniciSifre,@tur);");
            sbQuery.Append("select SCOPE_IDENTITY()");

            cmd.CommandText = sbQuery.ToString();

            #region Parameters
            cmd.Parameters.AddWithValue("@kullaniciAdSoyad", obj.AdSoyadi);
            cmd.Parameters.AddWithValue("@kullaniciSifre", obj.Sifre);
            cmd.Parameters.AddWithValue("@tur", obj.Tur);

            #endregion

            object returnValue = cmd.ExecuteScalar();

            if (returnValue != null)
            {
                obj.Id = int.Parse(returnValue.ToString());
            }

            _connection.Close();

            return obj;
        }

        public bool UpdateKullanici(Models.Kullanici obj)
        {
            bool result = false;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("Update Kullanici");
            sbQuery.AppendLine("set ");

            sbQuery.AppendLine("kullaniciAdSoyad=@kullaniciAdSoyad, ");
            sbQuery.AppendLine("kullaniciSifre=@kullaniciSifre, ");
            sbQuery.AppendLine("tur=@tur ");

            sbQuery.AppendLine(" where Id=@Id");
            try
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = sbQuery.ToString();

                #region Parameters
                cmd.Parameters.AddWithValue("@kullaniciAdSoyad", obj.AdSoyadi);
                cmd.Parameters.AddWithValue("@kullaniciSifre", obj.Sifre);
                cmd.Parameters.AddWithValue("@tur", obj.Tur);
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

        public bool DeleteKullanici(int Id)
        {
            bool result = false;
            try
            {
                Models.Kullanici obj = GetKullanici(Id);
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "Delete from Kullanici where Id=@Id";
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
        
        protected Models.Kullanici Get(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    var item = new Models.Kullanici();
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
        protected List<Models.Kullanici> ToList(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    List<Models.Kullanici> items = new List<Models.Kullanici>();
                    while (reader.Read())
                    {
                        var item = new Models.Kullanici();
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
