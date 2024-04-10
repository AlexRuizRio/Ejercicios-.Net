using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica
{
    public partial class Form1 : Form
    {
        double num1, num2;
        double memory;
        String oper;
        public Form1()
        {
            InitializeComponent();
        }
        //Esta funcion añade el texto de la funcion al textbox1
        private void botones_num(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            textBox1.Text += ((Button)sender).Text;
        }
        private void botones_oper(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            oper = ((Button)sender).Text;
            num1 = double.Parse(textBox1.Text);
            textBox1.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            num2 = double.Parse(textBox1.Text);
            double resultado = num1;
            switch(oper)
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
                        resultado = num1 / num2;
                    else
                        textBox1.Text = "Error";
                    break;
            }
            textBox1.Text = resultado.ToString();
        }

        private void button14_delete(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void buttonCE_clean(object sender, EventArgs e)
        {
            num1 = 0;
            oper = "";
            textBox1.Text = "0";

        }

        private void button_memory_save(object sender, EventArgs e)
        {
            memory = double.Parse(textBox1.Text);
            textBox1.Text = "0";

        }

        private void button_sum(object sender, EventArgs e)
        {
            memory = double.Parse(textBox1.Text) + memory;
            textBox1.Text = "0";
        }

        private void button_reset(object sender, EventArgs e)
        {
            textBox1.Text = memory.ToString();
        }

        private void button_mem_clean(object sender, EventArgs e)
        {
            memory = 0;
        }
    }
}
