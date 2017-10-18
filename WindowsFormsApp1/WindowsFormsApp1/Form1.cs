using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //variables del programa
        public string result;
        public string funcion;
        public string[] terminos;
        public Form1() 
        {
            InitializeComponent();
            panel1.BackColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.Format(textBox1.Text) == "Ingrese f(x) aquí")
            {
                MessageBox.Show("Ingrese una funcion! \n");
            }
            else
            {
                funcion = (textBox1.Text);
                
                maycero(funcion);
            }

               
        }

        public void maycero(string funcion)
        {
            short signo = 1;
            int primero = 0;
            double numero = 0;
            string nueva;
            string algo = "";
            double bufNum = 0;
            if (funcion[0] != '-')
            {
                nueva = funcion.Insert(0, "+");
            }
            else
            {
                nueva = funcion;
                signo *= -1;
            }
            terminos = nueva.Split('x');
            double bufPot = 0;
            double acumulador = 0;

            foreach (string termino in terminos)
            {    
                if (primero == 1)
                {
                    for (int i = 0; i < termino.Length; i++)
                    {
                        if (termino[i] == '^' && i == 0 && termino.Length == 4)
                        {
                            Console.WriteLine("acumulando" + acumulador);
                            algo = Convert.ToString(termino[i + 1]);
                            bufPot = Convert.ToDouble(algo);
                            acumulador += numero * (Math.Pow(2, bufPot));
                            algo = Convert.ToString(termino[3]);
                            numero = Convert.ToDouble(algo);
                            if(termino[2] == '-')
                            {
                                numero *= -1;
                            }
                        }
                        
                        if ( (termino[i] == '+' || termino[i] == '-') && termino.Length == 2)
                        {
                            algo = Convert.ToString(termino[1]);
                            bufNum = Convert.ToDouble(algo);
                            if(termino[i] == '-')
                            {
                                bufNum *= -1;
                            }
                            Console.WriteLine(" total hasta ahora: " + acumulador);
                            Console.WriteLine("numero independiente: " + bufNum);
                            acumulador += bufNum;
                            numero = 2 * numero;
                            Console.WriteLine("ultimo x " + numero);
                            acumulador += numero;
                        }

                    }

                    Console.WriteLine("termino");
                }
                if (termino.Substring(1, 1) != "^" && termino.Substring(1, 1) != "+" && termino.Substring(1, 1) != "-" && primero == 0)
                {

                    numero = Convert.ToDouble(termino.Substring(1, 1));
                    primero = 1;
                    
                }

            }
            Console.WriteLine("resultado" + acumulador*signo);
            //}

        }

    }
}
