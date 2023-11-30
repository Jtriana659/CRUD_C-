namespace PruebasDev
{
    public partial class Form1 : Form
    {
        private DataAccess dataAccess = new DataAccess();

        public Form1()
        {
            InitializeComponent();
            CargarDatos();  // Al cargar el formulario, se cargan los datos en el DataGridView.
        }

        // Evento Load del formulario (no se utiliza actualmente, puedes eliminarlo si no es necesario).
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Método para cargar los datos en el DataGridView.
        private void CargarDatos()
        {
            List<Persona> personas = dataAccess.ObtenerPersonas();  // Obtiene la lista de personas desde la base de datos.
            dataGridView1.DataSource = personas;  // Asigna la lista de personas como fuente de datos para el DataGridView.
        }

        // Método para limpiar los campos de los TextBox.
        private void LimpiarCampos()
        {
            txtname.Text = "";
            txtedad.Text = "";
        }

        // Evento Click del botón "Agregar".
        private void btnAgregar_Click_Click(object sender, EventArgs e)
        {
            // Crea una nueva persona con los datos ingresados en los TextBox.
            Persona nuevaPersona = new Persona
            {
                Nombre = txtname.Text,
                Edad = Convert.ToInt32(txtedad.Text)
            };

            dataAccess.AgregarPersona(nuevaPersona);  // Agrega la nueva persona a la base de datos.
            CargarDatos();  // Recarga los datos en el DataGridView.
            LimpiarCampos();  // Limpia los campos de los TextBox.
        }

        // Evento Click del botón "Eliminar".
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtiene el ID de la persona seleccionada y la elimina de la base de datos.
                int id = ((Persona)dataGridView1.SelectedRows[0].DataBoundItem).Id;
                dataAccess.EliminarPersona(id);
                CargarDatos();  // Recarga los datos en el DataGridView.
                LimpiarCampos();  // Limpia los campos de los TextBox.
            }
        }

        // Evento Click del botón "Actualizar".
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)  // Verifica si hay una fila seleccionada.
            {
                // Obtiene la persona seleccionada y actualiza sus datos con los TextBox.
                Persona personaSeleccionada = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;
                personaSeleccionada.Nombre = txtname.Text;
                personaSeleccionada.Edad = Convert.ToInt32(txtedad.Text);

                dataAccess.ActualizarPersona(personaSeleccionada);  // Actualiza los datos en la base de datos.
                CargarDatos();  // Recarga los datos en el DataGridView.
                LimpiarCampos();  // Limpia los campos de los TextBox.
            }
        }

    }
}