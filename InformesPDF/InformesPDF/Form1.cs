using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InformesPDF
{
    public partial class Form1 : Form
    {
        private OleDbConnection conexion;
        private OleDbCommandBuilder comandBuilder;
        private OleDbDataAdapter adapterEmision, adapterPaises, adapterRatings;
        private DataSet dataSet = new DataSet();
        private DataTable tablaEmision, tablaPaises, tablaRatings;
        private DataRowView registro;
        private string _STRING_CONEXION = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\temp\\GestionProgramacion.mdb";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarDataSet();
            crearRadioButtons();
            dataGridView.DataSource = dataSet.Tables["Emisiones"];
            dataGridView.Sort(dataGridView.Columns["Titulo"], ListSortDirection.Ascending);
        }
        private void crearRadioButtons()
        {
            int offset = 0;
            int inicio = 200;
            DataTable tablaRatings = dataSet.Tables["Ratings"];
            foreach (DataRow registro in tablaRatings.Rows)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = registro["Rating"].ToString();
                radioButton.Name = registro["IdRating"].ToString();
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
                radioButton.Location = new System.Drawing.Point(900, inicio + offset);
                radioButton.AutoSize = true;
                offset += 20;
                this.Controls.Add(radioButton);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton seleccion = (RadioButton)sender;
            dataSet.Tables["Emisiones"].DefaultView.RowFilter = "IdRating='" + seleccion.Name + "'";
            dataGridView.DataSource = dataSet.Tables["Emisiones"];
            dataGridView.Sort(dataGridView.Columns["Titulo"], ListSortDirection.Ascending);
        }
        private void cargarDataSet()
        {
            //Abrimos la conexion
            string query;
            dataSet.Clear();
            conexion = new OleDbConnection(_STRING_CONEXION);
            conexion.Open();

            //llenamos el dataSet con los distintos adapters y creamos las tablas
            query = "SELECT * FROM Emisiones";
            adapterEmision = new OleDbDataAdapter(query, conexion);
            comandBuilder = new OleDbCommandBuilder(adapterEmision);
            adapterEmision.Fill(dataSet, "Emisiones");
            tablaEmision = dataSet.Tables["Emisiones"];

            query = "SELECT * FROM Paises";
            adapterPaises = new OleDbDataAdapter(query, conexion);
            adapterPaises.Fill(dataSet, "Paises");
            tablaPaises = dataSet.Tables["Paises"];

            query = "SELECT * FROM Ratings";
            adapterRatings = new OleDbDataAdapter(query, conexion);
            adapterRatings.Fill(dataSet, "Ratings");
            tablaRatings = dataSet.Tables["Ratings"];

            //cerramos la conexion

            conexion.Close();

        }
        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (dataGridView.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Output.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.Value != null)
                                    {
                                        pdfTable.AddCell(cell.Value.ToString());
                                    }
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Data Exported Successfully !!!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Info");
            }
        }
    }
}
