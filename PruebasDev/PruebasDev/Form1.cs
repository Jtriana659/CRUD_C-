namespace PruebasDev
{
    public partial class Form1 : Form
    {
        private DataAccess dataAccess = new DataAccess();
        public Form1()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void CargarDatos()
        {
            List<Persona> personas = dataAccess.ObtenerPersonas();
            dataGridView1.DataSource = personas;
        }

        

        private void LimpiarCampos()
        {
            txtname.Text = "";
            txtedad.Text = "";
        }

        private void btnAgregar_Click_Click(object sender, EventArgs e)
        {
            Persona nuevaPersona = new Persona
            {
                Nombre = txtname.Text,
                Edad = Convert.ToInt32(txtedad.Text)
            };

            dataAccess.AgregarPersona(nuevaPersona);
            CargarDatos();
            LimpiarCampos();
        }
    }
}