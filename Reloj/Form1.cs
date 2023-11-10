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
    public partial class Reloj : Form
    {
        private Timer timer;
        private Dictionary<string, int> paises = new Dictionary<string, int>();
        private List<PaisControl> controlesPaises = new List<PaisControl>();

        public Reloj()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            textBoxHora.Text = DateTime.Now.ToString("HH:mm:ss");

            foreach (var controlPais in controlesPaises)
            {
                try
                {
                    int diferenciaHorariaPais = paises[controlPais.Nombre];
                    DateTime horaLocalPais = DateTime.Now.AddHours(diferenciaHorariaPais);
                    controlPais.CuadroTexto.Text = horaLocalPais.ToString("HH:mm:ss");
                }
                catch (Exception ex)
                {
                    controlPais.CuadroTexto.Text = $"Error: {ex.Message}";
                }
            }

            foreach (ToolStripMenuItem menuItem in paisToolStripMenuItem.DropDownItems)
            {
                if (menuItem.Tag != null && paises.ContainsKey(menuItem.Tag.ToString()))
                {
                    int diferenciaHoraria = paises[menuItem.Tag.ToString()];
                    DateTime horaLocal = DateTime.Now.AddHours(diferenciaHoraria);
                    menuItem.Text = $"{menuItem.Tag.ToString()}: {horaLocal.ToString("HH:mm:ss")}";
                }
            }
        }

        private void añadirPaísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string nombrePais = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre del país:", "Nuevo País");
            int diferenciaHoraria;

            if (string.IsNullOrEmpty(nombrePais))
                return;

            
            while (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Ingrese la diferencia horaria con respecto a su país:", "Diferencia Horaria"), out diferenciaHoraria))
            {
                MessageBox.Show("Por favor, ingrese un valor numérico para la diferencia horaria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
            ToolStripMenuItem nuevoPais = new ToolStripMenuItem(nombrePais);
            nuevoPais.Tag = nombrePais; 
            paises[nombrePais] = diferenciaHoraria; 
            paisToolStripMenuItem.DropDownItems.Add(nuevoPais);

            Label nuevaLabel = new Label();
            nuevaLabel.Text = $"Hora en {nombrePais}:";
            nuevaLabel.Location = new System.Drawing.Point(10, 80 + controlesPaises.Count * 30); 
            Controls.Add(nuevaLabel);

            TextBox nuevoTextBox = new TextBox();
            nuevoTextBox.ReadOnly = true;
            nuevoTextBox.Location = new System.Drawing.Point(150, 80 + controlesPaises.Count * 30); 
            Controls.Add(nuevoTextBox);

         
            controlesPaises.Add(new PaisControl { Nombre = nombrePais, Etiqueta = nuevaLabel, CuadroTexto = nuevoTextBox });
        }

        
        private class PaisControl
        {
            public string Nombre { get; set; }
            public Label Etiqueta { get; set; }
            public TextBox CuadroTexto { get; set; }
        }

        private void BorrarPaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            string nombrePaisABorrar = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre del país a borrar:", "Borrar País");

            if (paises.ContainsKey(nombrePaisABorrar))
            {
               
                paises.Remove(nombrePaisABorrar);

              
                var controlPais = controlesPaises.Find(cp => cp.Nombre == nombrePaisABorrar);
                if (controlPais != null)
                {
                    controlesPaises.Remove(controlPais);
                    Controls.Remove(controlPais.Etiqueta);
                    Controls.Remove(controlPais.CuadroTexto);
                }
              
                var menuItemABorrar = paisToolStripMenuItem.DropDownItems
                    .OfType<ToolStripMenuItem>()
                    .FirstOrDefault(item => item.Tag != null && item.Tag.ToString() == nombrePaisABorrar);

                if (menuItemABorrar != null)
                {
                    paisToolStripMenuItem.DropDownItems.Remove(menuItemABorrar);
                }
            }
            else
            {
                MessageBox.Show($"El país {nombrePaisABorrar} no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem borrarPaisToolStripMenuItem = new ToolStripMenuItem("Borrar País");
            borrarPaisToolStripMenuItem.Click += BorrarPaisToolStripMenuItem_Click;
            paisToolStripMenuItem.DropDownItems.Add(borrarPaisToolStripMenuItem);
        }

        private void paisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void añadirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void paisToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

      
    }
}
