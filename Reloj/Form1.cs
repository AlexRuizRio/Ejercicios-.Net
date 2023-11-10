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
        private List<Label> labelsPaises = new List<Label>();
        private List<TextBox> textBoxesHorasPaises = new List<TextBox>();
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

            
            foreach (ToolStripMenuItem menuItem in paisToolStripMenuItem.DropDownItems)
            {
                if (menuItem.Tag != null && paises.ContainsKey(menuItem.Tag.ToString()))
                {
                    int diferenciaHoraria = paises[menuItem.Tag.ToString()];
                    DateTime horaLocal = DateTime.Now.AddHours(diferenciaHoraria);
                    menuItem.Text = $"{menuItem.Tag.ToString()}: {horaLocal.ToString("HH:mm:ss")}";
                }
            }

            
            for (int i = 0; i < labelsPaises.Count; i++)
            {
                int diferenciaHorariaPais = paises[labelsPaises[i].Text];
                DateTime horaLocalPais = DateTime.Now.AddHours(diferenciaHorariaPais);
                textBoxesHorasPaises[i].Text = horaLocalPais.ToString("HH:mm:ss");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void añadirPaísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pedir al usuario que ingrese el nombre del país y la diferencia horaria
            string nombrePais = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre del país:", "Nuevo País");
            int diferenciaHoraria;

            if (string.IsNullOrEmpty(nombrePais))
                return;

            // Validar la entrada de la diferencia horaria
            while (!int.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Ingrese la diferencia horaria con respecto a su país:", "Diferencia Horaria"), out diferenciaHoraria))
            {
                MessageBox.Show("Por favor, ingrese un valor numérico para la diferencia horaria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Agregar el país al menú y al diccionario
            ToolStripMenuItem nuevoPais = new ToolStripMenuItem(nombrePais);
            nuevoPais.Tag = nombrePais; // Almacenar el nombre del país como Tag
            paises[nombrePais] = diferenciaHoraria; // Almacenar la diferencia horaria en el diccionario
            paisToolStripMenuItem.DropDownItems.Add(nuevoPais);

            // Generar dinámicamente una nueva Label y TextBox
            Label nuevaLabel = new Label();
            nuevaLabel.Text = $"Hora en {nombrePais}:";
            nuevaLabel.Location = new System.Drawing.Point(10, 80 + labelsPaises.Count * 30); // Ajusta la posición según sea necesario
            Controls.Add(nuevaLabel);
            labelsPaises.Add(nuevaLabel);

            TextBox nuevoTextBox = new TextBox();
            nuevoTextBox.ReadOnly = true;
            nuevoTextBox.Location = new System.Drawing.Point(150, 80 + textBoxesHorasPaises.Count * 30); // Ajusta la posición según sea necesario
            Controls.Add(nuevoTextBox);
            textBoxesHorasPaises.Add(nuevoTextBox);
        }
    }
}
