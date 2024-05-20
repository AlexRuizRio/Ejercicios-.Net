using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccesoDesconectado
{
    public partial class Form1 : Form
    {
        private OleDbConnection conexion;
        private OleDbCommandBuilder comandBuilder;
        private OleDbDataAdapter adapterEmision, adapterPaises, adapterRatings;
        private DataSet dataSet = new DataSet();
        private DataTable tablaEmision, tablaPaises, tablaRatings;
        private DataRowView registro;
        private string _STRING_CONEXION = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\temp\\GestionProgramacion.mdb";




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                cargarDataSet();

                //rellamos los ComboBox
                comboBoxClasificacion.DataSource = tablaRatings;
                comboBoxClasificacion.DisplayMember = "Rating";

                comboBoxPais.DataSource = tablaPaises;
                comboBoxPais.DisplayMember = "DescPais";

                //rellenamos el listBox
                listBoxTitulos.Sorted = true;
                listBoxTitulos.DataSource = tablaEmision;
                listBoxTitulos.DisplayMember = "Titulo";

            }

        }
        /*
         * METODO PARA CONECTAR CON LA BASE DE DATOS Y CARGAR EL DATA SER,
         * FINALMENTE CERRAMOS LA CONEXION
         */
        private void cargarDataSet()
        {
            //Abrimos la conexion
            string query;
            dataSet.Clear();
            conexion = new OleDbConnection(_STRING_CONEXION);
            conexion.Open();

            //llenamos el dataSet con los distintos adapters y creamos las tablas
            query = "SELECT * FROM Emisiones";
            adapterEmision = new OleDbDataAdapter(query, conexion);
            comandBuilder = new OleDbCommandBuilder(adapterEmision);
            adapterEmision.Fill(dataSet, "Emisiones");
            tablaEmision = dataSet.Tables["Emisiones"];

            query = "SELECT * FROM Paises";
            adapterPaises = new OleDbDataAdapter(query, conexion);
            adapterPaises.Fill(dataSet, "Paises");
            tablaPaises = dataSet.Tables["Paises"];

            query = "SELECT * FROM Ratings";
            adapterRatings = new OleDbDataAdapter(query, conexion);
            adapterRatings.Fill(dataSet, "Ratings");
            tablaRatings = dataSet.Tables["Ratings"];

            //cerramos la conexion

            conexion.Close();

        }
        private void listBoxTitulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            registro = (DataRowView)listBoxTitulos.SelectedItem;
            if (registro != null)
            {
                textBoxTitulo.Text = registro["Titulo"].ToString();
                textBoxActores.Text = registro["Actores"].ToString();
                textBoxDirectores.Text = registro["Director"].ToString();
                comboBoxClasificacion.SelectedIndex = Convert.ToInt32(registro["IdRating"].ToString()) - 1;
                /*
                 * este try es porque en la base de datos hay valores en IdPais que no exiten en la tabla Paises.
                 * Si vemos un valor que no se encuentra en la tabla Paises, marcamos como "NO DEFINIDO" el comboBox
                 */
                try
                {
                    comboBoxPais.SelectedIndex = Convert.ToInt32(registro["IdPais"].ToString()) - 1;
                }
                catch
                {
                    comboBoxPais.Text = "PAIS NO DEFINIDO";
                }
            }

        }
        //METODO QUE ACTUALIZA LA BASE DE DATOS Y VUELVE A CARGAR EL DATASET ACTUALIZADO
        private void actualizarDb()
        {
            conexion.Open();
            adapterEmision.Update(dataSet, "Emisiones");
            dataSet.AcceptChanges();
            conexion.Close();
            cargarDataSet();
        }

        //METODO BOTON NUEVO

    }
}
