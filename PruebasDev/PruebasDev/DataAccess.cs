using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PruebasDev
{
    internal class DataAccess
    {
        private string connectionString = "Data Source=miBaseDeDatos.db;Version=3;";

        public DataAccess()
        {
            // En el constructor, asegúrate de que la tabla "Personas" exista en la base de datos
            CrearTablaPersonas();
        }

        private void CrearTablaPersonas()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "CREATE TABLE IF NOT EXISTS Personas (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nombre TEXT, Edad INTEGER)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Persona> ObtenerPersonas()
        {
            List<Persona> personas = new List<Persona>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Personas";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            personas.Add(new Persona
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Edad = Convert.ToInt32(reader["Edad"])
                            });
                        }
                    }
                }
            }

            return personas;
        }
        public void AgregarPersona (Persona persona)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Personas (Nombre, Edad) VALUES (@Nombre, @Edad)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection)) 
                {
                    cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", persona.Edad);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void ActualizarPersona(Persona persona)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Personas SET Nombre=@Nombre, Edad=@Edad WHERE Id=@Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", persona.Edad);
                    cmd.Parameters.AddWithValue("@Id", persona.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void EliminarPersona(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Personas WHERE Id=@Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
