using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        string operador;
        double num1, num2, memoria;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void botones_Click(object sender, EventArgs e)
        {
            Button btn=(Button)sender;
            textBox1.Text += ((Button)sender).Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {

            operador = ((Button)sender).Text;
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
                num2 = double.Parse(textBox1.Text);
                double resultado = 0;
                switch (operador)
                {
                    case "+":
                        resultado = num1 + num2;
                        break;
                    case "-":
                        resultado = num1 - num2;
                        break;
                    case "*":
                        resultado = num1 * num2;
                        break;
                    case "/":
                        if (num2 != 0)
                        {
                            resultado = num1 / num2;
                        }
                        else
                        {
                            textBox1.Text = "Error";
                            return;
                        }
                        break;
                }
                textBox1.Text = resultado.ToString();
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            operador = ((Button)sender).Text;
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            operador = ((Button)sender).Text;
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            memoria = 0; 
        }

        private void button17_Click(object sender, EventArgs e)
        {
            memoria = memoria + double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            num1 = 0;
            operador = "";
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            memoria = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            String convertir = memoria.ToString();
            textBox1.Text = convertir;
                
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            double x = double.Parse(textBox1.Text);
            x = 1 / x; 
            textBox1 .Text = x.ToString();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            operador = ((Button)sender).Text;
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
