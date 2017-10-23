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
        public double _Y = 0;
        public double _X = 0;
        public double Yan = 0;
        public double Yant = 0;
        public double Xant = 0;
        public double error = 0.01;
        public bool seguir = true;
        public double Iant = 0;
        public double Fxr = 0;
        public double xr = 0;
        public double resul = 0;
        public bool abierto = false;
        public int grado = 1;
        public string Fprima = "";
        public string Fsegunda = "";
        public double condicion = 0;
        public double condicion2 = 0;
        public string buferString = "";
        public string FunAbierta = "";
        public int sign = 1;
        public int sign2 = 1;
        public double termino1 = 0;
        public double termino2 = 0;
        //_______________________
        public double FprimaX = 0;
        public double FprimaDivY = 0;
        public double nuevoX = 0;
        public string FuncBackup = "";



        public Form1() 
        {
            InitializeComponent();
            panel1.BackColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            if (string.IsNullOrEmpty(textBox1.Text) || string.Format(textBox1.Text) == "Ingrese f(x) aquí")
            {
                MessageBox.Show("Ingrese una funcion! \n");
            }
            else
            {
                funcion = (textBox1.Text);
                FuncBackup = funcion;
                if (abierto)
                {
                    MessageBox.Show("metodo abierto");
                    derive derivando = new derive();//creo objeto que tiene funciones para derivar
                    maycero(funcion);
                    Console.WriteLine("_X =" + _X + ", Xant= " + Xant +  ", Y = " + _Y + ". Y anterior = " + Yant + ", grado "+ grado);
                    Console.WriteLine(" Y * YANT = " + _Y * Yant);
                    if (_Y * Yant < 0) // I condicion
                    {
                        Fprima = derivando.derivar(funcion); //primer derivada
                        Fsegunda = derivando.derivar(Fprima); //segunda derivada
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
                                //Desarrollar formula_____________
                                
                                FprimaX = fprimera(ref Fprima,ref FunAbierta,ref termino1,ref j,ref termino2,ref _X);
                                Console.WriteLine("F'(xi) = " + FprimaX);
                                FprimaDivY = _Y / FprimaX;
                                Console.WriteLine("F(xi)/F'(xi) = " + FprimaDivY);
                                Xant = _X - (FprimaDivY);
                                Xant = Math.Round(Xant, 4);
                                while (seguir)
                                {
                                    Fxr = Funcion_xr(Xant); //saco F(X)
                                    Console.WriteLine("Xant" + Fxr);
                                    FprimaX = fprimera(ref Fprima, ref FunAbierta, ref termino1, ref j, ref termino2, ref Xant); //saco F'(x)
                                    Console.WriteLine("FprimaX" + FprimaX);
                                    FprimaDivY = Fxr / FprimaX; //div entre f(x) y f'(x)
                                    nuevoX = Xant - FprimaDivY;
                                    if(((nuevoX - Xant) / nuevoX) < error)
                                    {
                                        Console.WriteLine("la raiz aprox es: " + Math.Round(nuevoX,4));
                                        break;
                                    }
                                    else
                                    {
                                        Xant = nuevoX;
                                    }
                                }
                                //_______________________________
                            }
                            else
                            {
                                Console.WriteLine("no cumple II condicion");    
                            }  
                        }
                        if (grado >= 3)
                        {
                            if (Fsegunda[0] == '-')
                            {
                                sign = -1;
                            }
                            for (int i = 1; i < Fsegunda.Length; i++)
                            {
                                if (Fsegunda[i] != 'x' && j == 0)
                                {
                                    FunAbierta += Fsegunda[i];
                                   
                                }
                                if (Fsegunda[i] == 'x')
                                {
                                    j = 1;
                                    if (Fsegunda[i + 1] == '-')
                                    {
                                        sign2 = -1;
                                    }
                                    termino1 = Convert.ToDouble(FunAbierta);
                                    termino1 = termino1 * sign;
                                    FunAbierta = "";
                                }
                                if (Fsegunda[i] != 'x' && j == 1 && Fsegunda[i] != '+' && Fsegunda[i] != '-')
                                {
                                    FunAbierta += Fsegunda[i];
                                }
                            }
                            termino2 = Convert.ToDouble(FunAbierta);
                            termino2 = termino2 * sign2;
                            condicion = Xant * termino1 + termino2; //x= X e Xant
                            condicion2 = _X * termino1 + termino2;
                            if (condicion > 0 || condicion2 > 0)
                            {

                                Console.WriteLine("cumple II condicion");
                                //desarrollar pg;

                                Fxr = Funcion_xr(_X);
                                funcion = Fprima; //cambio la funcion derivada primera a funcion.
                                FprimaX = Funcion_xr(_X);
                                FprimaDivY = Fxr / FprimaX;
                                Xant = _X - FprimaDivY;

                                while (seguir)
                                {
                                    funcion = FuncBackup; //vuelvo a la func original
                                    Fxr = Funcion_xr(Xant);
                                    funcion = Fprima; //cambio a la 1er derivada de la F
                                    FprimaX = Funcion_xr(Xant);
                                    FprimaDivY = Fxr / FprimaX;
                                    nuevoX = Xant - FprimaDivY;
                                    if (((nuevoX - Xant) / nuevoX) < error)
                                    {
                                        Console.WriteLine("la razi aprox es: " + nuevoX);
                                        break;
                                    }
                                    else
                                    {
                                        Xant = nuevoX;
                                    }
                                }
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
                restablecer();
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
                                acumulador += bufNum;
                                numero = X * numero;
                                acumulador += numero;
                            }
                        }
                    }
                    if (termino.Substring(1, 1) != "^" && termino.Substring(1, 1) != "+" && termino.Substring(1, 1) != "-" && primero == 0)
                    {
                        numero = Convert.ToDouble(termino.Substring(1, 1));
                        primero = 1;
                    }
                }
                Console.WriteLine("X  = |"+ X +"| Y =" + acumulador * signo);
                if ((((acumulador * signo) * 1) > 0 && Yan < 0)) //salidas
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
                                acumulador += bufNum;
                                numero = iant * numero;
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
        //__________abierto_________________
        public double fprimera(ref string Fprima, ref string FunAbierta, ref double termino1, ref int j, ref double termino2, ref double _X)
        {
            for (int i = 1; i < Fprima.Length; i++)
            {
                if (Fprima[i] != 'x' && j == 0)
                {
                    FunAbierta += Fprima[i];

                }
                if (Fprima[i] == 'x')
                {
                    j = 1;
                    if (Fprima[i + 1] == '-')
                    {
                        sign2 = -1;
                    }
                    termino1 = Convert.ToDouble(FunAbierta);
                    termino1 = termino1 * sign;
                    FunAbierta = "";
                }
                if (Fprima[i] != 'x' && j == 1 && Fprima[i] != '+' && Fprima[i] != '-')
                {
                    FunAbierta += Fprima[i];
                }
            }
            termino2 = Convert.ToDouble(FunAbierta);
            termino2 = termino2 * sign2;
            return  (termino1 * _X + termino2);
        }
        void restablecer()
        {
            result = "";
            funcion = "";
            _Y = 0;
            _X = 0;
            Yan = 0;
            Yant = 0;
            Xant = 0;
            error = 0.01;
            seguir = true;
            Iant = 0;
            Fxr = 0;
            xr = 0;
            resul = 0;
            abierto = false;
            grado = 1;
            Fprima = "";
            Fsegunda = "";
            condicion = 0;
            condicion2 = 0;
            buferString = "";
            FunAbierta = "";
            sign = 1;
            sign2 = 1;
            termino1 = 0;
            termino2 = 0;
            //_______________________
            FprimaX = 0;
            FprimaDivY = 0;
            nuevoX = 0;
            FuncBackup = "";
        }
    }

}
