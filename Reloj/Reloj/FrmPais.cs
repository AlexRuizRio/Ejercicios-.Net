using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reloj
{
    public partial class FrmPais : Form
    {
        private String nombre;
        private int dif;
        public FrmPais()
        {
            InitializeComponent();
        }

        public string getNombre()
        {  
            return (nombre);
        }

        public int getDif()
        {
            return (dif);
        }
        public int Dif { get => dif; set => dif = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            nombre = textNombre.Text;
            try
            {
                dif = Convert.ToInt16(textDif.Text);
                Close();
            }
            catch
            {
                MessageBox.Show("El campo diferencia tiene que ser un valor numérico");
            }
            
        }
    }
}
