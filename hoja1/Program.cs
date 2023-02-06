using System.Data;
using System.Diagnostics;

namespace hoja1
{
    internal class Program
    {
        // Invariantes:
        // Se verifica exp ≥ 0 y coef != 0 para todos los monomios del polinomio.
        // No existen dos monomios con el mismo exponente en el array de monomios (mon[i].exp ̸= mon[j].exp ∀i, j con 0 ≤ i<j<nMon

        const int N = 100; // tamaño de los arrays de monomios

        struct Monomio
        { // coeficiente y exponente
            public double coef;
            public int exp;
        }

        struct Polinomio
        {
            public Monomio[] mon; // array de monomios
            public int nMons; // num de monomios = primera pos libre en el array mon
        }

        static void Inserta(Monomio m, ref Polinomio p) // añade el monomio m al polinomio p
                                             // Producirá error cuando la estructura (el array de monomios)
                                             // esté llena y se requiera más espacio
        {
            int i = 0;
            bool igual = false;
            while (i < p.nMons) 
            {
                if (m.exp == p.mon[i].exp)
                {
                    p.mon[i].coef += m.coef;
                    igual = true;
                }
                i++;
            }
            if (!igual && m.coef != 0)
            {
                p.mon[p.nMons] = m;
                p.nMons++;
            }
        }

        static void LeeMonomio(out Monomio m)
        {
            Console.Write("Coeficiente: ");
            m.coef = double.Parse(Console.ReadLine()!);
            Console.Write("Exponente: ");
            m.exp = int.Parse(Console.ReadLine()!);
        }

        static void LeePolinomio(out Polinomio p)
        {
            p.mon = new Monomio[N];
            Console.Write("Número de monomios: ");
            p.nMons = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Introduce monomios");
            for (int i = 0; i < p.nMons; i++)
                LeeMonomio(out p.mon[i]);
        }

        static void EscribePolinomio(Polinomio p)
        {
            for (int i = 0; i < p.nMons; i++)
            {
                Console.WriteLine(p.mon[i].coef + "x^" + p.mon[i].exp + " + ");
            }
        }

        static int Grado(Polinomio p)
        {
            int g = 0;
            for (int i = 0; i < p.nMons; i++)
            {
                if (p.mon[i].exp > g)
                {
                    g = p.mon[i].exp;
                }
            }
            return g;
        }

        static Polinomio Suma(Polinomio p1, Polinomio p2)
        {
            for(int i = 0; i < p2.nMons; i++)
            {
                Inserta(p2.mon[i], ref p1);
            }
            return p1;
        }

        static Polinomio Resta(Polinomio p1, Polinomio p2) // Invertir coeficinte e insertar igualmente
        {
            for (int i = 0; i < p2.nMons; i++) 
            {
                p2.mon[i].coef = p2.mon[i].coef * -1;
                Inserta(p2.mon[i], ref p1);
            }
            return p1;
        }

        static double Evalua(Polinomio p, double v)
        {
            double resultado = 0;
            double x; // valor de cada monomio en cada iteracion del bucle
            for (int i = 0; i < p.nMons; i++)
            {
                x = Math.Pow(v, p.mon[i].exp);
                x *= p.mon[i].coef;
                resultado += x;
            }
            return resultado;
        }
        static void Main(string[] args)
        {
            Polinomio p = new()
            {
                mon = new Monomio[N],
                nMons = 0
            };
            
            #region
            Monomio m = new()
            {
                coef = 1,
                exp = 2
            };
            Inserta(m, ref p);
            Monomio m2 = new()
            {
                coef = 1,
                exp = 2
            };
            Inserta(m2, ref p);
            Monomio m3 = new()
            {
                coef = 3,
                exp = 4
            };
            Inserta(m3, ref p);
            Monomio m4 = new()
            {
                coef = 7,
                exp = 4
            };
            Inserta(m4, ref p);
            #endregion

            //LeeMonomio(out Monomio m5);
            //Inserta(m5, ref p);
            //Console.WriteLine("Grado del polinomio: " + Grado(p));
            
            
            Console.WriteLine("Polinomio p: ");
            EscribePolinomio(p);

            /*
            Console.WriteLine("Escribe polinomio p2: ");
            LeePolinomio(out Polinomio p2);

            Console.WriteLine("Resultado p3: ");
            //Polinomio p3 = Suma(p, p2);
            Polinomio p3 = Resta(p, p2);
            EscribePolinomio(p3);
            */ 

            Console.WriteLine("Evaluación de p sobre 5: " + Evalua(p, 5));

            while (true)
            {

            }

            // se asume que se añaden los monomios de manera ordenada en la 2a parte (no hay que ordenar nada)
        }
    }
}