using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adopcion.Models
{
    public class Pet
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int Type { get; set; }
        public int Race { get; set; }

        public int Weight { get; set; }
        public int Height { get; set; }
        public int City { get; set; }
        public int VisitCount { get; set; }
        public int Status { get; set; }

        public string Email { get; set; }

        public bool confirmedEmail { get; set; }
        public IEnumerable<SelectListItem> ListTypes { get; set; }




        public void Details(long id)
        {
            var command = new  MySqlCommand(){ CommandText = "Mascota_Seleccionar", CommandType= System.Data.CommandType.StoredProcedure};
            command.Parameters.AddWithValue("inID",id);
            var dt = Utils.BaseData.GetDataTable(command);

            if(dt.Rows.Count > 0)
            {
                this.Name = dt.Rows[0]["Mascota_Nombre"].ToString();
                this.ID = Convert.ToInt64(dt.Rows[0]["Mascota_ID"]);
                this.BirthDate = Convert.ToDateTime(dt.Rows[0]["Mascota_FechaNac"]);
                this.Type = Convert.ToInt16(dt.Rows[0]["Mascota_Tipo"]);
                this.Race = Convert.ToInt32(dt.Rows[0]["Mascota_Raza"]);
                this.Weight = Convert.ToInt16(dt.Rows[0]["Mascota_Peso"]);
                this.Height = Convert.ToInt16(dt.Rows[0]["Mascota_Altura"]);
                this.City = this.Weight = Convert.ToInt16(dt.Rows[0]["Mascota_Ciudad"]);
                this.VisitCount =  Convert.ToInt16(dt.Rows[0]["Mascota_VecesVista"]);
                this.Status = Convert.ToInt16(dt.Rows[0]["Mascota_Estado"]);
                this.Email = dt.Rows[0]["Mascota_Email"].ToString();
                this.confirmedEmail = Convert.ToBoolean(dt.Rows[0]["Mascota_EmailValido"]);

                command.CommandText = "Mascota_MarcarVista";
                Utils.BaseData.ExecuteCommand(command);
                 
            }

        }
        public void Add()
        {
            var command = new MySqlCommand() { CommandText = "Mascota_Agregar", CommandType = System.Data.CommandType.StoredProcedure };
            command.Parameters.AddWithValue("inNombre", this.Name);
            command.Parameters.AddWithValue("inID", this.ID);
            command.Parameters.AddWithValue("inFechaNac", this.BirthDate);
            command.Parameters.AddWithValue("inTipo", this.Type);
            command.Parameters.AddWithValue("inRaza", this.Race);
            command.Parameters.AddWithValue("inPeso", this.Weight);
            command.Parameters.AddWithValue("inAltura", this.Height);
            command.Parameters.AddWithValue("inCiudad", this.City);
            command.Parameters.AddWithValue("inEstado", this.Status);
            command.Parameters.AddWithValue("inEmail", this.Email);
            command.Parameters.AddWithValue("inEmailValido", this.confirmedEmail);

            using (var conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdopcionDB"].ConnectionString))
            {

                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

        }


        public List<Pet> Find(int city, int race, int type)
        {
            var pets = new List<Pet>();
            var dt = new DataTable();
            var command = new MySqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = "Mascota_Buscar" };
            command.Parameters.AddWithValue("inCiudad", city);
            command.Parameters.AddWithValue("inRaza", race);
            command.Parameters.AddWithValue("inTipo", type);
            using (var conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdopcionDB"].ConnectionString))
            {
                conn.Open();
                command.Connection = conn;
                var sqlda = new MySqlDataAdapter(command);
                sqlda.Fill(dt);
                conn.Close();
            }

            foreach (DataRow dr in dt.Rows)
            {
                var pet = new Pet();

                pet.Name = dr["Mascota_Nombre"].ToString();
                pet.ID = Convert.ToInt64(dr["Mascota_ID"]);
                pet.BirthDate = Convert.ToDateTime(dr["Mascota_FechaNac"]);
                pet.Type = Convert.ToInt16(dr["Mascota_Tipo"]);
                pet.Race = Convert.ToInt32(dr["Mascota_Raza"]);
                pet.Weight = Convert.ToInt16(dr["Mascota_Peso"]);
                pet.Height = Convert.ToInt16(dr["Mascota_Altura"]);
                pet.City = this.Weight = Convert.ToInt16(dr["Mascota_Ciudad"]);
                pet.VisitCount = Convert.ToInt16(dr["Mascota_VecesVista"]);
                pet.Status = Convert.ToInt16(dr["Mascota_Estado"]);
                pet.Email = dr["Mascota_Email"].ToString();
                pet.confirmedEmail = Convert.ToBoolean(dr["Mascota_EmailValido"]);
                pets.Add(pet);
            }

            return pets;
        }




        public void Edit()
        {
            var command = new MySqlCommand() { CommandText = "Mascota_Modificar", CommandType = System.Data.CommandType.StoredProcedure };
            command.Parameters.AddWithValue("inNombre", this.Name);
            command.Parameters.AddWithValue("inID", this.ID);
            command.Parameters.AddWithValue("inFechaNac", this.BirthDate);
            command.Parameters.AddWithValue("inTipo", this.Type);
            command.Parameters.AddWithValue("inRaza", this.Race);
            command.Parameters.AddWithValue("inPeso", this.Weight);
            command.Parameters.AddWithValue("inAltura", this.Height);
            command.Parameters.AddWithValue("inCiudad", this.City);
            command.Parameters.AddWithValue("inEstado", this.Status);
            command.Parameters.AddWithValue("inEmail", this.Email);
            command.Parameters.AddWithValue("inEmailValido", this.confirmedEmail);

            using (var conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdopcionDB"].ConnectionString))
            {

                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public void Adopt()
        {
            var command = new MySqlCommand() { CommandText = "Mascota_Adoptar", CommandType = System.Data.CommandType.StoredProcedure };
            
            command.Parameters.AddWithValue("inID", this.ID);
            using (var conn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdopcionDB"].ConnectionString))
            {

                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    command.Connection = conn;
                    command.Transaction = sqlTran;
                    command.ExecuteNonQuery();
                    sqlTran.Commit();
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}