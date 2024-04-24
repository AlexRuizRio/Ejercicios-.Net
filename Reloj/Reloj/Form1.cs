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
    public partial class Form1 : Form
    {
        private Boolean pais = false;
        private int dif;
        public Form1()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime hora = DateTime.Now;
            textHora.Text = DateTime.Now.ToString("HH:mm:ss");
            if (pais)
                textBox2.Text = hora.AddHours(dif).ToString("HH:mm:ss");

        }
        private void añadirPaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPais fadd = new FrmPais(); // Instancio el formulario
            fadd.ShowDialog(); // La app se para hasta que este formulario se cierre
            ToolStripMenuItem elemento = new ToolStripMenuItem();
            elemento.Name = fadd.getNombre();
            elemento.Tag = fadd.getDif();
            elemento.Text = fadd.getNombre();
            elemento.Click += new System.EventHandler(controladorPais_Click);
            toolStripMenuItem1.DropDownItems.Add(elemento);

        }

        private void borrarPaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDel fdel = new FormDel();
            fdel.ShowDialog();
            if (toolStripMenuItem1.DropDownItems.ContainsKey(fdel.getPais()))
            {
                toolStripMenuItem1.DropDownItems.RemoveByKey(fdel.getPais());
            }
            else
            {
                MessageBox.Show("No existe el pais", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void controladorPais_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem elemento = (ToolStripMenuItem)sender;
            labelnombre.Text = elemento.Text;
            dif = Convert.ToInt32(elemento.Tag);
            pais = true;
        }
    }
}

