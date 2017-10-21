using derivador; //incluyo el namespace derivador del archivo derive.cs
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
        double _Y = 0;
        double _X = 0;
        double Yan = 0;
        double Yant = 0;
        double Xant = 0;
        double error = 0.01;
        bool seguir = true;
        double Iant = 0;
        double Fxr = 0;
        double xr = 0;
        double resul = 0;
        bool abierto = false;
        int grado = 1;
        string Fprima = "";
        string Fsegunda = "";
        double condicion = 0;
        string buferString = "";
        public string[] FunAbierta;
        int sign = 1;


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
                if (abierto)
                {
                    MessageBox.Show("metodo abierto");
                    derive derivando = new derive();
                    maycero(funcion);
                    Console.WriteLine("Y = " + _Y + ". Y anterior = " + Yant + ", grado "+ grado);
                    Console.WriteLine(" Y * YANT = " + _Y * Yant);
                    if (_Y * Yant < 0) // I condicion
                    {
                        Fprima = derivando.derivar(funcion);
                        Fsegunda = derivando.derivar(Fprima);
                        Console.WriteLine(Fsegunda);
                        if (grado == 1)
                        {
                            Console.WriteLine("No cumple II condicion");
                            
                        }
                        if (grado == 2 )
                        {
                            if (Fsegunda[0] == '-')
                            {
                                sign = -1;
                            }
                            terminos = Fsegunda.Split('+', '-');
                            buferString = Convert.ToString(terminos[1]);
                            condicion = Convert.ToDouble(buferString);
                            condicion = condicion * sign;
                            Console.WriteLine("condicion= " + condicion);
                            if(condicion > 0)
                            {
                                Console.WriteLine("cumple II condicion");
                                //hacer la formula
                            }
                            else
                            {
                                Console.WriteLine("no cumple II condicion");    
                            }
                            
                        }
                        if (grado >= 3)
                        {
                            int j = 0;
                            if (funcion[0] == '+')
                            {
                                sign = -1;
                            }
                            //verificar condicion y hacer la formula
                            for (int i = 1; i < funcion.Length; i++)
                            {
                                if (funcion[i] != 'x' && j == 0)
                                {
                                    FunAbierta[0] += funcion[i];
                                }
                                if (funcion[i] == 'x')
                                {
                                    j = 1;
                                    if(funcion[i+1] == '-')
                                    {

                                    }
                                }
                                if (funcion[i] != 'x' && j == 1)
                                {
                                    FunAbierta[3] += funcion[i];
                                }



                                //+18x-4
                            }
                        }

                    }else
                    {
                        Console.WriteLine("no cumple I condicion");
                    }
                }
                else { 
                    maycero(funcion); //mando a buscar la diferencia entre negativo y positi
                    Console.WriteLine("Xant " + Xant + ", X, " + _X + ", Yant " + Yant + ", Y " + _Y);
                    Iant = DameiCero(ref _X, ref _Y, ref Xant, ref Yant); //primer xr
                    Console.WriteLine("IANT  " + Iant);
                    Fxr = Funcion_xr(Iant); // vuelvo a buscar en una func similar a maycero
                    Console.WriteLine("Fxr = " + Fxr);
                    seguir = true;
                    while (seguir)
                    {
                        xr = Damei(ref _X, ref _Y, ref Fxr, ref Iant);
                        Console.WriteLine("xr " + xr);
                        resul = Math.Round(((xr - Iant) / xr),4);
                        if (resul < error)
                        {
                            Console.WriteLine("La raiz aproximada es: " + xr);
                            seguir = false;
                            break;
                        }
                        else
                        {
                            Fxr = Funcion_xr(xr);
                            Iant = xr;
                        }
                    
                    }
                }
            }

               
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (abierto)
            {
                abierto = false;
            }
            else
            {
                abierto = true;
            }
        }


        public void maycero(string funcion)
        {    
            string nueva = "";
            string algo = "";
            grado = 1;
            short signo = 1;

            if (funcion[0] != '+')
            {
                if (funcion[0] == '-')
                {
                    nueva = funcion;
                    signo *= -1;
                }
                if (funcion[0] != '+' && funcion[0] != '-')
                {
                    MessageBox.Show(Convert.ToString("asdas"));
                    nueva = funcion.Insert(0, "+");
                }
            }
            else
            {
                if (funcion[0] == '+')
                {
                    nueva = funcion;
                }
            }

            terminos = nueva.Split('x');
            MessageBox.Show(Convert.ToString(nueva));
            for (int X=-10; X<11; X++)
            {
                double bufPot = 0;
                double acumulador = 0;
                int primero = 0;
                double bufNum = 0;
                double numero = 0;
                

                foreach (string termino in terminos)
                {    
                    if (primero == 1)
                    {
                        if (grado == 1)
                        {
                            if (termino[0] == '^')
                            {
                                algo = Convert.ToString(termino[1]);
                                grado = Convert.ToInt16(algo);
                            }
                        }
                        for (int i = 0; i < termino.Length; i++)
                        {
                            if (termino[i] == '^' && i == 0 && termino.Length == 4)
                            {
                                Console.WriteLine("acumulando" + acumulador);
                                algo = Convert.ToString(termino[i + 1]);
                                bufPot = Convert.ToDouble(algo);
                                acumulador += numero * (Math.Pow(X, bufPot));
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
                                numero = X * numero;
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
                Console.WriteLine("X  = |"+ X +"| Y =" + acumulador * signo);
                if ((((acumulador * signo) * 1) > 0 && Yan < 0))
                {
                    Xant = X - 1;
                    _X = X;
                    _Y = acumulador * signo;
                    Yant = Yan;
                    
                }
                Yan = acumulador * signo;
            }
           
        } // 1
        public double Funcion_xr(double iant)
        {
            double xr = 0;
            string nueva = "";
            string algo = "";
            short signo = 1;


            if (funcion[0] != '+')
            {
                if (funcion[0] == '-')
                {
                    nueva = funcion;
                    signo *= -1;
                }
                if (funcion[0] != '+' && funcion[0] != '-')
                {
                    MessageBox.Show(Convert.ToString("asdas"));
                    nueva = funcion.Insert(0, "+");
                }
            }
            else
            {
                if (funcion[0] == '+')
                {
                    nueva = funcion;
                }
            }


            terminos = nueva.Split('x');

                double bufPot = 0;
                double acumulador = 0;
                int primero = 0;
                double bufNum = 0;
                double numero = 0;

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
                                acumulador += numero * (Math.Pow(iant, bufPot));
                                algo = Convert.ToString(termino[3]);
                                numero = Convert.ToDouble(algo);
                                if (termino[2] == '-')
                                {
                                    numero *= -1;
                                }
                            }
                            if ((termino[i] == '+' || termino[i] == '-') && termino.Length == 2)
                            {
                                algo = Convert.ToString(termino[1]);
                                bufNum = Convert.ToDouble(algo);
                                if (termino[i] == '-')
                                {
                                    bufNum *= -1;
                                }
                                Console.WriteLine(" total hasta ahora: " + acumulador);
                                Console.WriteLine("numero independiente: " + bufNum);
                                acumulador += bufNum;
                                numero = iant * numero;
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
                xr = acumulador * signo;

            return Math.Round(xr, 4);
        } // 3
        public double DameiCero(ref double _X, ref double _Y, ref double Xant, ref double Yant) // 2
        {
            double divisor = 0;
            double dividendo = 0;
            double resultado = 0;
            dividendo = (_Y * (Xant - _X));
            divisor = Yant - _Y;
            resultado = (_X - (dividendo / divisor));
            return Math.Round(resultado, 4);
        }
        public double Damei(ref double _X, ref double _Y, ref double Fxr, ref double Iant)
        {
            double divisor = 0;
            double dividendo = 0;
            double resultado = 0;
            Console.WriteLine(" _X = " + _X + ", _Y = " + _Y + ", Fxr =" + Fxr + ",Iant = " + Iant);
            dividendo = (_Y * (Iant - _X));
            divisor = (Fxr - _Y);
            resultado = (_X - (dividendo / divisor));
            return Math.Round(resultado, 4);
        } //4
        public string derivar(string fun)
        {
            return fun; 
        }
    }

}
