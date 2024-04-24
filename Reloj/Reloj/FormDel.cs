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
    public partial class FormDel : Form
    {
        public String pais;
        public FormDel()
        {
            InitializeComponent();
        }

       public String getPais()
        {
            return (pais);
        }
        public void setPais(String pais)
        {
            this.pais = pais;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.pais = textBox1.Text;
            Close();
        }
    }
}
