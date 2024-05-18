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
        String busqueda;

        public Form1()
        {
            InitializeComponent();
            lstDepid.Hide();
            lstid.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            ctn = new OleDbConnection();

            ctn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; " + "Data Source=C:\\temp\\emple.mdb";

            // Apertura de la conexión

            ctn.Open();
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
            lstBus.Items.Add("Apellidos");
            lstBus.Items.Add("Oficio");
            lstBus.Items.Add("Salario");
            lstBus.Items.Add("Fecha Alta");
            lstBus.Items.Add("Comisión");
        }
        // FUNCION QUE SI SELECCIONAN EN LOS LISTBOX SE CARGAR LOS DATOS DEL DEPARTAMENTO O LOCALIZACION
        private void LstLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar();
            LimpiarTextBox();
            lstDepid.SelectedIndex = lstLoc.SelectedIndex;
            lstDepart.SelectedIndex = lstLoc.SelectedIndex;
            CargarEmpleados();
        }
        private void LstDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar();
            LimpiarTextBox();
            lstDepid.SelectedIndex = lstDepart.SelectedIndex;
            lstLoc.SelectedIndex = lstDepart.SelectedIndex;
            CargarEmpleados();
        }
        // CARGA A LOS EMPLEADOS SEGUN EL SELECTINDEX DE LOS LISTBOS DE DEPART O LOC
        private void CargarEmpleados()
        {
            Limpiar();
            LimpiarTextBox();
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
        // MARCA TODOS LOS LISTBOX DEL EMPLEADO QUE SEÑALES EN CUALQUIERA DE (APELLIDO, OFICIO, FECHA, COMISION, SALARIO)
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

            TextEmpleado();
        }
        //MUESTRA AL EMPLEADO EN LOS TEXTBOX SEGUN EL SELECTITEM
        private void TextEmpleado()
        {
            if (lstApe.SelectedIndex == null)
            {

            }
            else
            {
                textBoxApe.Text = lstApe.SelectedItem.ToString();
                textBoxOfi.Text = lstOfi.SelectedItem.ToString();
                textBoxSal.Text = lstSal.SelectedItem.ToString();
                textBoxFech.Text = lstFech.SelectedItem.ToString().Substring(0, 10);
                textBoxComi.Text = lstComi.SelectedItem.ToString();
            }
        }
        //BOTONES
        private void guardar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lstid.GetItemText(lstid.SelectedItem));
            IDbCommand cmd = new OleDbCommand();
            cmd.Connection = ctn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE EMPLE SET APELLIDO='" + textBoxApe.Text + "', OFICIO='" + textBoxOfi.Text
                             + "', SALARIO='" + Convert.ToDecimal(textBoxSal.Text) + "', COMISION='" + Convert.ToDecimal(textBoxComi.Text)
                             + "' WHERE EMP_NO = " + id;

            MessageBox.Show("Empleado actualizado: " + cmd.ExecuteNonQuery());
            CargarEmpleados();
        }

        private void borrar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lstid.GetItemText(lstid.SelectedItem));
            IDbCommand cmd = new OleDbCommand();
            cmd.Connection = ctn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM EMPLE WHERE EMP_NO = " + id;

            MessageBox.Show("Empleado borrado: " + cmd.ExecuteNonQuery());
            CargarEmpleados();
        }
        private void nuevo_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = ctn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select max(EMP_NO) from EMPLE";
            int numDep = Convert.ToInt32(lstDepid.GetItemText(lstDepid.SelectedItem));
            int clave = Convert.ToInt32(cmd.ExecuteScalar());
            clave++;
            cmd.CommandText = "INSERT INTO EMPLE (EMP_NO, APELLIDO, OFICIO, SALARIO, FECHA_ALT, COMISION, DEPT_NO) " +
                         "VALUES (@id, @apellido, @oficio, @salario, @fechaAlt, @comision, @deptNo)";

            // Añadir parámetros a la consulta
            cmd.Parameters.AddWithValue("@id", clave);
            cmd.Parameters.AddWithValue("@apellido", textBoxApe.Text);
            cmd.Parameters.AddWithValue("@oficio", textBoxOfi.Text);
            cmd.Parameters.AddWithValue("@salario", Convert.ToDecimal(textBoxSal.Text));
            cmd.Parameters.AddWithValue("@fechaAlt", DateTime.Now.Date);
            cmd.Parameters.AddWithValue("@comision", Convert.ToDecimal(textBoxComi.Text));
            cmd.Parameters.AddWithValue("@deptNo", numDep);
            MessageBox.Show("Empleado nuevo añadido: " + cmd.ExecuteNonQuery());

            CargarEmpleados();
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {

            var var1 =textBus.Text;
            if (busqueda.Equals("FECHA_ALT"))
            {
                var1 += "%";

            }

            IDbCommand cmd;
            cmd = new OleDbCommand();
            cmd.Connection = ctn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from EMPLE where " + busqueda + " like " + "'" + var1 + "'";
            cmd.ExecuteNonQuery();

            IDataReader lector;
            lector = cmd.ExecuteReader();

            Limpiar();
            while (lector.Read())
            {
                lstid.Items.Add(lector.GetValue(0));
                lstApe.Items.Add(lector.GetString(1));
                lstOfi.Items.Add(lector.GetString(2));
                lstSal.Items.Add(lector.GetValue(5));
                lstFech.Items.Add(lector.GetValue(4).ToString().Substring(0, 10));
                lstComi.Items.Add(lector.GetValue(6));
            }

        }

        private void limpiar_Click(object sender, EventArgs e)
        {
            textBoxApe.Clear();
            textBoxOfi.Clear();
            textBoxSal.Clear();
            textBoxFech.Clear();
            textBoxComi.Clear();

        }
        //BOTONES DE SUBIR Y BAJAR EN LA LISTA DE EMPLEADOS

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (lstApe.SelectedIndex > 0)
            {
                lstApe.SelectedIndex = lstApe.SelectedIndex - 1;
            }
            else if (lstApe.SelectedIndex == 0)
            {
                lstApe.SelectedIndex = lstApe.Items.Count - 1;
            }
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (lstApe.SelectedIndex < lstApe.Items.Count - 1)
            {
                lstApe.SelectedIndex = lstApe.SelectedIndex + 1;
            }
            else if (lstApe.SelectedIndex == lstApe.Items.Count - 1)
            {
                lstApe.SelectedIndex = 0;
            }
        }
        //SELECTOR DEL CAMPO DE LA BUSQUEDA
        private void LstBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sel = lstBus.SelectedItem.ToString();

            switch (sel)
            {
                case "Apellidos":
                    busqueda = "APELLIDO";
                    break;

                case "Oficio":
                    busqueda = "OFICIO";
                    break;

                case "Salario":
                    busqueda = "SALARIO";
                    break;

                case "Fecha Alta":
                    busqueda = "FECHA_ALT";
                    break;

                case "Comisión":
                    busqueda = "COMISION";
                    break;
            }
        }
    
        // LIMPIAJE EL 1 DE LOS LISTBOX Y EL SEGUNDO DE LOS TEXTBOX
        private void Limpiar()
        {
            lstApe.Items.Clear();
            lstOfi.Items.Clear();
            lstSal.Items.Clear();
            lstFech.Items.Clear();
            lstComi.Items.Clear();
            lstid.Items.Clear();
        }
        private void LimpiarTextBox()
        {
            textBoxApe.Clear();
            textBoxOfi.Clear();
            textBoxSal.Clear();
            textBoxFech.Clear();
            textBoxComi.Clear();
        }
        // BOTONES DE ARRIBA A LA IZQUIERDA:MINIMIZAR, MAXIMIZAR Y CERRAR

        private void PbSalir_Click(object sender, EventArgs e)
        {
            ctn.Close();
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }




        /*
private void PbMinimizar_Click(object sender, EventArgs e)
{
   this.WindowState = FormWindowState.Minimized;
}

private void PbMaximizar_Click(object sender, EventArgs e)
{
   if (this.WindowState == FormWindowState.Maximized)
   {
       this.WindowState = FormWindowState.Normal;
   }
   else
   {
       this.WindowState = FormWindowState.Maximized;
   }
}
*/
    }
}
