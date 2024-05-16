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

            lstDepart.SelectedIndexChanged += listBox_SelectedIndexChanged;
            lstLoc.SelectedIndexChanged += listBox_SelectedIndexChanged;
            lstDepid.SelectedIndexChanged += listBox_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ctn = new OleDbConnection();

            ctn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " + "Data Source=C:\\temp\\emple.mdb";

            // Apertura de la conexión

            ctn.Open();
            MessageBox.Show(ctn.State.ToString());

            try
            {
                OleDbCommand cmd = ctn.CreateCommand();
                cmd.CommandText = "SELECT * FROM depart";
                OleDbDataReader lector = cmd.ExecuteReader();

                while (lector.Read())
                {
                    lstDepart.Items.Add(lector.GetString(1));
                    lstLoc.Items.Add(lector.GetString(2));
                    lstDepid.Items.Add(lector.GetValue(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener las categorías: " + ex.Message);
            }
        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Selecion_listBoxGeneral(sender, e);
            Limpiar();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = ctn;
            cmd.CommandText = "SELECT * FROM Emple " +
                   "INNER JOIN DEPART ON Emple.DEPT_NO = DEPART.DEPT_NO " +
                   "WHERE DEPART.DNOMBRE = '" + lstDepart.SelectedItem.ToString() + "'";

            OleDbDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                lstApe.Items.Add(lector["APELLIDO"]);
                lstOfi.Items.Add(lector["OFICIO"]);
                lstSal.Items.Add(lector["SALARIO"]);
                lstFech.Items.Add(lector["FECHA_ALT"]);
                lstComi.Items.Add(lector["COMISION"]);
                lstid.Items.Add(lector["EMP_NO"]);
            }

        }
        private void LstApellido_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lst = (ListBox)sender;
            int selected = lst.SelectedIndex;
            lstApe.SelectedIndex = selected;
            lstOfi.SelectedIndex = selected;
            lstSal.SelectedIndex = selected;
            lstComi.SelectedIndex = selected;
            lstFech.SelectedIndex = selected;
            lstid.SelectedIndex = selected;

        }
        private void Limpiar()
        {
            lstApe.Items.Clear();
            lstOfi.Items.Clear();
            lstSal.Items.Clear();
            lstFech.Items.Clear();
            lstComi.Items.Clear();
            lstid.Items.Clear();
        }
        private void Selecion_listBoxGeneral (object sender, EventArgs e)
        {
            ListBox lst = (ListBox)sender;
            int seleccion = lst.SelectedIndex;

            lstDepart.SelectedIndex = seleccion;
            lstLoc.SelectedIndex = seleccion;
            lstDepid.SelectedIndex = seleccion;
        }

    }
}
