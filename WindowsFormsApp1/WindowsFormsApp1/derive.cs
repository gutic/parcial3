using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace derivador
{
    public class derive
    {

        public string funcion;
        public int cuanto;
        public int contador = 0;
        public int TotalMono = 0;
        public char[] grado = { 'x', '^' };
        public string submono;
        public string[] parte;
        public int TotalParte;
        public int MaxGrado;
        public int multi;

        public string formato(string funcion)
        {
            string salir = "";
            int j = 0;
            for (int i = 0; i < funcion.Length - 1; i++)
            {
                if ((funcion[i] == '-' || funcion[i] == '+') && i > 0)
                {
                    if (funcion[i - 1] != '^')
                    {
                        salir += funcion.Substring(j, i - j) + " ";
                        j = i;
                    }
                }
            }
            salir += funcion.Substring(j, funcion.Length - j);
            return salir;
        }   //funciona

        public string derivar(string funcion)
        {
            string salida = "";
            string[] terminos;
            string dx;
            bool EsPositivo = true;
            double potencia = 0, coeficiente = 0;
            funcion = formato(funcion);
            terminos = funcion.Split();
            foreach (string termino in terminos)
            {
                separar(termino, ref coeficiente, ref potencia, ref EsPositivo);
                dx = derivarTermino(coeficiente, potencia, EsPositivo);
                salida += dx;
            }
            if (!(salida.Trim().Length > 0))
            {
                return "0";
            }
            if (salida[1] == '+')
            {
                return salida.Substring(3);
            }
            if (salida[1] == '-')
            {
                return Convert.ToString(salida[1]) + salida.Substring(3);
            }
            if (salida[0] == ' ')
            {
                return salida.Substring(1);
            }
            return salida;
        }
        private void separar(string termino, ref double coeficiente, ref double potencia, ref bool EsPositivo)
        {
            int fincoeficiente = 0;
            int inicio;
            EsPositivo = true;
            if (termino[0] == '-')
            {
                EsPositivo = false;
                inicio = 1;
            }
            else if (termino[0] == '+')
            {
                EsPositivo = true;
                inicio = 1;
            }
            else
            {
                inicio = 0;
            }
            if (termino == Convert.ToString('x'))
            {
                coeficiente = 1; //cuento numero de coeficientes
            }
            else
            {
                for (int i = 0; i < termino.Length - 1; i++)
                {
                    if (!(char.IsNumber(termino[i])))
                    {
                        fincoeficiente = i - 1; //veo donde termina el coeficiente
                        break;
                    }
                }
                string c;
                if (fincoeficiente <= 0)
                {
                    c = termino.Substring(inicio, 1);

                }
                else if (inicio == 0)
                {
                    c = termino.Substring(inicio, fincoeficiente + 1);

                }
                else
                {
                    c = termino.Substring(inicio, fincoeficiente);

                }
                if (c == Convert.ToString('x') || c.Length == 0)
                {
                    coeficiente = 1;
                }
                else
                {
                    if (Convert.ToString(c) != string.Empty)
                    {
                        //MessageBox.Show(c);
                        double valor;
                        double.TryParse(c, out valor);
                        //MessageBox.Show(Convert.ToString(valor));
                        coeficiente = valor;
                    }
                }
            }
            //saco la potencia
            if (termino.IndexOf("^") >= 0 && termino.IndexOf("(") < 0)
            {
                potencia = Convert.ToInt32(termino.Substring(termino.IndexOf("^") + 1));
            }
            else if (termino.IndexOf("x") >= 0 && termino.IndexOf("^") < 0)
            {
                potencia = 1;
            }
            //fraccion no funciona, retorno//
            else if (termino.IndexOf(")") >= 0)
            {
                string fraccion;
                inicio = termino.IndexOf("(") + 1;
                fraccion = termino.Substring(inicio, (termino.Length - inicio) - 1);
                potencia = fraccionADecimal(fraccion);
            }
            else
            {
                potencia = 0;
            }
        }
        private Single fraccionADecimal(string fraccion)
        {
            int primer, segundo;
            primer = Convert.ToInt32(fraccion.Substring(0, fraccion.IndexOf("/")));
            segundo = Convert.ToInt32(fraccion.Substring(fraccion.IndexOf("/") + 1));
            return primer / segundo;
        }
        private string derivarTermino(double coeficiente, double potencia, bool EsPositivo)
        {
            string salida = "";
            double termino;
            if (!(EsPositivo))
            {
                if (potencia < 0)
                {
                    salida += "+";
                }
                else
                {
                    salida += "-";
                }
            }
            else if (potencia >= 0)
            {
                salida += "+";
            }
            else
            {
                salida += "-";
            }
            termino = Math.Abs(coeficiente * potencia);
            switch (Convert.ToInt32(potencia))
            {
                case 1:
                    return salida + termino;

                case 2:
                    return salida + termino + "x";
                case 0:
                    return "";
                default:
                    if (termino != 1)
                    {
                        return salida + termino + "x^" + (potencia - 1);
                    }
                    else
                    {
                        return salida + "x^" + (potencia - 1);
                    }
            }
        }


    }
}
