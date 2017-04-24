using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psiko.Data.Repositories
{
    public class Kategori
    {
        private SqlConnection _connection;

        public Kategori(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Map(System.Data.IDataRecord record, Models.Kategori entity)
        {
            entity.Id = (int)record["Id"];
            entity.anakategoriID = (int)record["anakategoriID"];
            entity.kategoriAd = record["kategoriAd"].ToString();
            entity.kategoriSira =(int) record["kategoriSira"];
            entity.kategoriAdet = (int)record["kategoriAdet"];
            entity.kategoriResim = record["kategoriResim"].ToString();
           
        }

        public Models.Kategori GetKategori(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "Select * from Kategori where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);
            Models.Kategori obj = Get(cmd);

            return obj;
        }

        public List<Models.Kategori> GetKategoriListesi()
        {
            SqlCommand cmd = _connection.CreateCommand(); ;
            cmd.CommandText = "Select * from Kategori";
            return ToList(cmd);
        }
       

        public Models.Kategori AddKategori(Models.Kategori obj)
        {

            SqlCommand cmd = _connection.CreateCommand();

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("insert into Kategori ([anakategoriID],[kategoriAd],[kategoriSira],[kategoriAdet],[kategoriResim])");
            sbQuery.Append("values(@anakategoriID,@kategoriAd,@kategoriSira,@kategoriAdet,@kategoriResim);");
            sbQuery.Append("select SCOPE_IDENTITY()");

            cmd.CommandText = sbQuery.ToString();

            #region Parameters
            cmd.Parameters.AddWithValue("@anakategoriID", obj.anakategoriID);
            cmd.Parameters.AddWithValue("@kategoriAd", obj.kategoriAd);
            cmd.Parameters.AddWithValue("@kategoriSira", obj.kategoriSira);
            cmd.Parameters.AddWithValue("@kategoriAdet", obj.kategoriAdet);
            cmd.Parameters.AddWithValue("@kategoriResim", obj.kategoriResim);

            #endregion

            object returnValue = cmd.ExecuteScalar();

            if (returnValue != null)
            {
                obj.Id = int.Parse(returnValue.ToString());
            }

            _connection.Close();

            return obj;
        }

        public bool UpdateKategori(Models.Kategori obj)
        {
            bool result = false;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("Update Kategori");
            sbQuery.AppendLine("set ");

            sbQuery.AppendLine("anakategoriID=@anakategoriID, ");
            sbQuery.AppendLine("kategoriAd=@kategoriAd, ");
            sbQuery.AppendLine("kategoriSira=@kategoriSira ");
            sbQuery.AppendLine("kategoriAdet=@kategoriAdet ");
            sbQuery.AppendLine("kategoriResim=@kategoriResim ");

            sbQuery.AppendLine(" where Id=@Id");
            try
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = sbQuery.ToString();

                #region Parameters
                cmd.Parameters.AddWithValue("@anakategoriID", obj.anakategoriID);
                cmd.Parameters.AddWithValue("@kategoriAd", obj.kategoriAd);
                cmd.Parameters.AddWithValue("@kategoriSira", obj.kategoriSira);
                cmd.Parameters.AddWithValue("@kategoriAdet", obj.kategoriAdet);
                cmd.Parameters.AddWithValue("@kategoriResim", obj.kategoriResim);
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

        public bool DeleteKategori(int Id)
        {
            bool result = false;
            try
            {
                Models.Kategori obj = GetKategori(Id);
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "Delete from Kategori where Id=@Id";
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

        protected Models.Kategori Get(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    var item = new Models.Kategori();
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
        protected List<Models.Kategori> ToList(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    List<Models.Kategori> items = new List<Models.Kategori>();
                    while (reader.Read())
                    {
                        var item = new Models.Kategori();
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
