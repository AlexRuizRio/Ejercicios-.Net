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
        private List<Product> product;
        public Form1()
        {
            InitializeComponent();
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

        }
  
    }
}
