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

        // Método privado para crear la tabla "Personas" si no existe
        private void CrearTablaPersonas()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL para crear la tabla "Personas" si no existe
                string query = "CREATE TABLE IF NOT EXISTS Personas (Id INTEGER PRIMARY KEY AUTOINCREMENT, Nombre TEXT, Edad INTEGER)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener todas las personas de la tabla "Personas"
        public List<Persona> ObtenerPersonas()
        {
            List<Persona> personas = new List<Persona>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL para seleccionar todas las personas de la tabla
                string query = "SELECT * FROM Personas";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        // Itera a través de los resultados y agrega personas a la lista
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

        // Método para agregar una nueva persona a la tabla "Personas"
        public void AgregarPersona(Persona persona)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL para insertar una nueva persona
                string query = "INSERT INTO Personas (Nombre, Edad) VALUES (@Nombre, @Edad)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    // Parámetros para la consulta SQL
                    cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", persona.Edad);

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para actualizar los datos de una persona en la tabla "Personas"
        public void ActualizarPersona(Persona persona)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL para actualizar una persona por su ID
                string query = "UPDATE Personas SET Nombre=@Nombre, Edad=@Edad WHERE Id=@Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    // Parámetros para la consulta SQL
                    cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", persona.Edad);
                    cmd.Parameters.AddWithValue("@Id", persona.Id);

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar una persona de la tabla "Personas" por su ID
        public void EliminarPersona(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Consulta SQL para eliminar una persona por su ID
                string query = "DELETE FROM Personas WHERE Id=@Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    // Parámetro para la consulta SQL
                    cmd.Parameters.AddWithValue("@Id", id);

                    // Ejecuta la consulta
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}