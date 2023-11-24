using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Peliculas
{
    public partial class Form1 : Form
    {
        private List<Pelicula> peliculas;
        private ListBox listBoxTitulo;
        private ListBox listBoxActores;
        private ListBox listBoxDirector;

        public Form1()
        {
            InitializeComponent();
            LoadPeliculas();

            // Inicializa los ListBox
            listBoxTitulo = new ListBox
            {
                Location = new System.Drawing.Point(200, 250), // Ajusta la posición vertical
                Size = new System.Drawing.Size(150, 200),
                BorderStyle = BorderStyle.FixedSingle
            };
            listBoxActores = new ListBox
            {
                Location = new System.Drawing.Point(400, 250), // Ajusta la posición vertical
                Size = new System.Drawing.Size(150, 200),
                BorderStyle = BorderStyle.FixedSingle
            };
            listBoxDirector = new ListBox
            {
                Location = new System.Drawing.Point(600, 250), // Ajusta la posición vertical
                Size = new System.Drawing.Size(150, 200),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Añade los ListBox a la colección Controls del formulario
            this.Controls.Add(listBoxTitulo);
            this.Controls.Add(listBoxActores);
            this.Controls.Add(listBoxDirector);

         
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Obtén clasificaciones únicas
            var clasificaciones = peliculas.Select(p => p.Clasificacion).Distinct().ToList();

            foreach (var clasificacion in clasificaciones)
            {
                RadioButton radioButton = new RadioButton
                {
                    Text = clasificacion,
                    AutoSize = true,
                    Location = new System.Drawing.Point(10, 10 + clasificaciones.IndexOf(clasificacion) * 25)
                };

                // Asocia el evento click con el controlador de eventos
                radioButton.Click += RadioButton_Click;

                // Añade el RadioButton a la colección Controls del formulario
                this.Controls.Add(radioButton);
            }
        }
        private void LoadPeliculas()
        {
            peliculas = new List<Pelicula>();

            try
            {
                // Lee las líneas del archivo y crea objetos Pelicula
                string[] lines = File.ReadAllLines("peliculas.txt");
                foreach (string line in lines)
                {
                    string[] data = line.Split('#');
                    Pelicula pelicula = new Pelicula
                    {
                        Titulo = data[0].Trim(),
                        Actores = data[1].Trim(),
                        Director = data[2].Trim(),
                        Clasificacion = data[3].Trim()
                    };
                    peliculas.Add(pelicula);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las películas: {ex.Message}");
            }
        }

       

        private void RadioButton_Click(object sender, EventArgs e)
        {
            // Borra los ListBox existentes
            listBoxTitulo.Items.Clear();
            listBoxActores.Items.Clear();
            listBoxDirector.Items.Clear();

            // Obtén la clasificación seleccionada
            string clasificacionSeleccionada = (sender as RadioButton).Text;

            // Filtra las películas por la clasificación seleccionada
            var peliculasFiltradas = peliculas.Where(p => p.Clasificacion == clasificacionSeleccionada).ToList();

            // Muestra los datos en los ListBox
            foreach (var pelicula in peliculasFiltradas)
            {
                listBoxTitulo.Items.Add(pelicula.Titulo);
                listBoxActores.Items.Add(pelicula.Actores);
                listBoxDirector.Items.Add(pelicula.Director);
            }
        }
    }

    public class Pelicula
    {
        public string Titulo { get; set; }
        public string Actores { get; set; }
        public string Director { get; set; }
        public string Clasificacion { get; set; }
    }
}
