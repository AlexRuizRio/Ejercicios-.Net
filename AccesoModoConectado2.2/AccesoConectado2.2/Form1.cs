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

namespace AccesoConectado2._2
{
    public partial class Form1 : Form
    {
        private OleDbConnection ctn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctn = new OleDbConnection();

            ctn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " + "Data Source=C:\\temp\\emple.mdb";

            // Apertura de la conexión

            ctn.Open();
            MessageBox.Show(ctn.State.ToString());
        }
    }
}
