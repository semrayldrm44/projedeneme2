using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Psiko.Data.Repositories
{
    public class Makale
    {
        private SqlConnection _connection;

        public Makale(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Map(System.Data.IDataRecord record, Models.Makale entity)
        {
            entity.Id = (int)record["Id"];
            entity.kullaniciID =(int) record["kullaniciID"];
            entity.makaleBaslik = record["makaleBaslik"].ToString();
            entity.makaleOzet = record["makaleOzet"].ToString();

            entity.makaleIcerik = record["makaeIcerik"].ToString();
            entity.makaleResim = record["makaleResim"].ToString();
            entity.makaleTarih = (DateTime)record["makaleTarih"];
            entity.makaleOkunma = (int)record["makaleOkunma"];
            entity.makaleYorumSayisi = (int)record["makaleYorumSayisi"];
            entity.kategoriID = (int)record["kategoriID"];
        }

        public Models.Makale GetMakale(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "Select * from Makale where Id=@Id";
            cmd.Parameters.AddWithValue("@Id", id);
            Models.Makale obj = Get(cmd);

            return obj;
        }

        public List<Models.Makale> GetMakaleListesi()
        {
            SqlCommand cmd = _connection.CreateCommand(); ;
            cmd.CommandText = "Select * from Makale";
            return ToList(cmd);
        }
       
        public Models.Makale AddMakale(Models.Makale obj)
        {

            SqlCommand cmd = _connection.CreateCommand();

            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("insert into Makale ([kullaniciID],[makaleBaslik],[makaleOzet],[makaleIcerik],[makaleResim],[makaleTarih],[makaleOkunma],[makaleYorumSayisi],[kategoriID])");
            sbQuery.Append("values(@kullaniciID,@makaleBaslik,@makaleOzet,@makaleIcerik,@makaleResim,@makaleTarih,@makaleOkunma,@makaleYorumSayisi,@kategoriID);");
            sbQuery.Append("select SCOPE_IDENTITY()");

            cmd.CommandText = sbQuery.ToString();

            #region Parameters
            cmd.Parameters.AddWithValue("@kullaniciID", obj.kullaniciID);
            cmd.Parameters.AddWithValue("@makaleBaslik", obj.makaleBaslik);
            cmd.Parameters.AddWithValue("@makaleOzet", obj.makaleOzet);
            cmd.Parameters.AddWithValue("@makaleIcerik", obj.makaleIcerik);
            cmd.Parameters.AddWithValue("@makaleResim", obj.makaleResim);
            cmd.Parameters.AddWithValue("@makaleTarih", obj.makaleTarih);
            cmd.Parameters.AddWithValue("@makaleOkunma", obj.makaleOkunma);
            cmd.Parameters.AddWithValue("@makaleYorumSayisi", obj.makaleYorumSayisi);
            cmd.Parameters.AddWithValue("@kategoriID", obj.kategoriID);

            #endregion

            object returnValue = cmd.ExecuteScalar();

            if (returnValue != null)
            {
                obj.Id = int.Parse(returnValue.ToString());
            }

            _connection.Close();

            return obj;
        }

        public bool UpdateMakale(Models.Makale obj)
        {
            bool result = false;
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.AppendLine("Update Makale");
            sbQuery.AppendLine("set ");

            sbQuery.AppendLine("kullaniciID=@kullaniciID, ");
            sbQuery.AppendLine("makaleBaslik=@makaleBaslik, ");
            sbQuery.AppendLine("makaleOzet=@makaleOzet ");
            sbQuery.AppendLine("makaleIcerik=@makaleIcerik, ");
            sbQuery.AppendLine("makaleResim=@makaleResim, ");
            sbQuery.AppendLine("makaleTarih=@makaleTarih, ");
            sbQuery.AppendLine("makaleOkunma=@makaleOkunma ");
            sbQuery.AppendLine("makaleYorumSayisi=@makaleYorumSayisi, ");
            sbQuery.AppendLine("kategoriID=@kategoriID, ");
           

            sbQuery.AppendLine(" where Id=@Id");
            try
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = sbQuery.ToString();

                #region Parameters
                cmd.Parameters.AddWithValue("@kullaniciID", obj.kullaniciID);
                cmd.Parameters.AddWithValue("@makaleBaslik", obj.makaleBaslik);
                cmd.Parameters.AddWithValue("@makaleOzet", obj.makaleOzet);
                cmd.Parameters.AddWithValue("@makaleIcerik", obj.makaleIcerik);
                cmd.Parameters.AddWithValue("@makaleResim", obj.makaleResim);
                cmd.Parameters.AddWithValue("@makaleTarih", obj.makaleTarih);
                cmd.Parameters.AddWithValue("@makaleOkunma", obj.makaleOkunma);
                cmd.Parameters.AddWithValue("@makaleYorumSayisi", obj.makaleYorumSayisi);
                cmd.Parameters.AddWithValue("@kategoriID", obj.kategoriID);
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

        public bool DeleteMakale(int Id)
        {
            bool result = false;
            try
            {
                Models.Makale obj = GetMakale(Id);
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandText = "Delete from Makale where Id=@Id";
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

        protected Models.Makale Get(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();
            try
            {
                using (var reader = command.ExecuteReader())
                {
                    var item = new Models.Makale();
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
        protected List<Models.Makale> ToList(SqlCommand command)
        {
            if (_connection.State != ConnectionState.Open) _connection.Open();

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    List<Models.Makale> items = new List<Models.Makale>();
                    while (reader.Read())
                    {
                        var item = new Models.Makale();
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
