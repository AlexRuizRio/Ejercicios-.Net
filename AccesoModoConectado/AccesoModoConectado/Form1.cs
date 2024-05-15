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

namespace AccesoModoConectado
{
    public partial class Form1 : Form
    {
        private OleDbConnection ctn;
        private List<Categories> listcategories;
        public Form1()
        {
            InitializeComponent();

            // Suscribir el método listBox_SelectedIndexChanged al evento SelectedIndexChanged de ambos ListBox
            listProductID.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listProductName.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listUnitPrice.SelectedIndexChanged += listBox_SelectedIndexChanged;
            listUnitInStock.SelectedIndexChanged += listBox_SelectedIndexChanged;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctn = new OleDbConnection();
            int x = 12;
            int y = 100;

            ctn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " + "Data Source=C:\\temp\\nwind.mdb";

            // Apertura de la conexión

            ctn.Open();
            obtenerCategorias(ctn);
            foreach (Categories categoria in listcategories)
            {
                RadioButton radiob = new RadioButton();
                radiob.Text = categoria.CategoryName;
                radiob.Name = "radioButton" + categoria.CategoryID.ToString();
                radiob.AutoSize = true;
                radiob.Location = new Point(x, y);
                radiob.Size = new Size(80, 17);
                radiob.CheckedChanged += new EventHandler(metodoRadiob);
                Controls.Add(radiob);
                y += 25;

            }
        }
        private void obtenerCategorias (OleDbConnection ctn)
        {
            listcategories = new List<Categories>();
            try
            {
                OleDbCommand cmd = ctn.CreateCommand();
                cmd.CommandText = "SELECT CategoryID, CategoryName FROM Categories";
                OleDbDataReader lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    Categories categoria = new Categories();
                    categoria.CategoryID = Convert.ToInt32(lector["CategoryID"]);
                    categoria.CategoryName = lector["CategoryName"].ToString();
                    listcategories.Add(categoria);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las categorías: " + ex.Message);
            }
        }
        private void metodoRadiob (object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            listProductID.Items.Clear();
            listProductName.Items.Clear();
            listUnitInStock.Items.Clear();
            listUnitPrice.Items.Clear();
            int categoryId = Convert.ToInt32(radioButton.Name.Substring("radioButton".Length));
            OleDbCommand cmd = ctn.CreateCommand();
            cmd.CommandText = "SELECT ProductID, ProductName, UnitPrice, UnitsInStock FROM Products WHERE CategoryID = @CategoryID";
            cmd.Parameters.AddWithValue("@CategoryID", categoryId);

            //Comprobacion de si esta abietr la conexion y la abre en el caso de que no la este
            if (ctn.State != ConnectionState.Open)
                ctn.Open();

 
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listProductID.Items.Add(reader["ProductID"].ToString());
                listProductName.Items.Add(reader["ProductName"].ToString());
                listUnitInStock.Items.Add(reader["UnitPrice"].ToString());
                listUnitPrice.Items.Add(reader["UnitsInStock"].ToString());
            }

            // Cierra el lector
            reader.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ctn.Close();
        }

        private void actualizar_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = ctn;
            cmd.CommandText = "Update Products set " + "ProductName='" + textProductName.Text + "', UnitPrice=" +
            textUnitPrice.Text + ", UnitsInStock=" + textUnitInStock.Text + " where ProductID=" + textProduct.Text;
            int registro = cmd.ExecuteNonQuery();
            MessageBox.Show("Se ha actualizado " + registro + " registro");
   
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtén el índice seleccionado del ListBox que generó el evento
            int selectedIndex = ((ListBox)sender).SelectedIndex;

            // Verifica si el sender es listProductID y asegúrate de que haya un ítem seleccionado
            if (sender == listProductID && selectedIndex != -1)
            {
                // Establece el índice seleccionado en los otros ListBox
                listProductName.SelectedIndex = selectedIndex;
                listUnitPrice.SelectedIndex = selectedIndex;
                listUnitInStock.SelectedIndex = selectedIndex;
            }
            // Verifica si el sender es listProductName y asegúrate de que haya un ítem seleccionado
            else if (sender == listProductName && selectedIndex != -1)
            {
                // Establece el índice seleccionado en los otros ListBox
                listProductID.SelectedIndex = selectedIndex;
                listUnitPrice.SelectedIndex = selectedIndex;
                listUnitInStock.SelectedIndex = selectedIndex;
            }
            // Verifica si el sender es listUnitPrice y asegúrate de que haya un ítem seleccionado
            else if (sender == listUnitPrice && selectedIndex != -1)
            {
                // Establece el índice seleccionado en los otros ListBox
                listProductID.SelectedIndex = selectedIndex;
                listProductName.SelectedIndex = selectedIndex;
                listUnitInStock.SelectedIndex = selectedIndex;
            }
            // Verifica si el sender es listUnitInStock y asegúrate de que haya un ítem seleccionado
            else if (sender == listUnitInStock && selectedIndex != -1)
            {
                // Establece el índice seleccionado en los otros ListBox
                listProductID.SelectedIndex = selectedIndex;
                listProductName.SelectedIndex = selectedIndex;
                listUnitPrice.SelectedIndex = selectedIndex;
            }
        }
    }
}
