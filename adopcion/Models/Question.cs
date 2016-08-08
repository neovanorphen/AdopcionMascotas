using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Adopcion.Models
{
    public class Question
    {
        public long pet_ID { get; set; }

        public string Interested_Name { get; set; }

        public string Interested_Phone { get; set; }

        public string Interested_Email { get; set; }

        public string Interested_Question { get; set; }


        public void AddQuestion()
        {
            
            var command = new MySqlCommand() { CommandText = "Mascota_AgregarConsulta", CommandType = System.Data.CommandType.StoredProcedure };
            command.Parameters.AddWithValue("inMascota_ID", this.pet_ID);
            command.Parameters.AddWithValue("inInteresado_Nombre", this.Interested_Name);
            command.Parameters.AddWithValue("inInteresado_Telefono", this.Interested_Phone);
            command.Parameters.AddWithValue("inInteresado_Email", this.Interested_Email);
            command.Parameters.AddWithValue("inInteresado_Pregunta", this.Interested_Question);


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

        public List<Question> GetQuestions(long id)
        {
            var questions = new List<Question>();
            var dt = new DataTable();
            var command = new MySqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = "Mascota_Consultas" };
            command.Parameters.AddWithValue("inID", id);
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
                var question = new Question();
                question.Interested_Name = dr["Interesado_Nombre"].ToString();
                question.Interested_Email = dr["Interesado_Email"].ToString();
                question.Interested_Phone = dr["Interesado_Telefono"].ToString();
                question.Interested_Question = dr["Interesado_Pregunta"].ToString();

                questions.Add(question);
            }

            return questions;
        }


    }
}