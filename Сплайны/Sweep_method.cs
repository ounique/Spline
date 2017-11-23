using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сплайны
{
    class Sweep_method
    {
        static int n;
        static double h;

        static double[] a;
        static double[] b;
        static double[] c;
        static double[] d;

        static double[] u;
        static double[] v;

        static double[] y;

        public Sweep_method()
        { }
        static double func(double x)
        {
            return x*Math.Exp(-x);
        }
        static double df(double x)
        {
            return Math.Exp(-x) - x*Math.Exp(-x);
        }
        private static void Fill(double[] f, double fa, double fb)
        {
            for (int i = 1; i < n-1; i++)
            {
                a[i] = 4;
                b[i] = 1;
                c[i] = 1;
                d[i] = 3/h * (f[i+1]-f[i-1]);
            }

            a[0] = 1;
            b[0] = 0;
            c[0] = 0;
            d[0] = fa;

            a[n - 1] = 1;
            b[n - 1] = 0;
            c[n - 1] = 0;
            d[n - 1] = fb;
        }
        public static double[] Sweep(double[] fx, double dfa, double dfb, int nn, double hh)
        {
            n = nn;
            h = hh;

            a = new double[n]; b = new double[n]; u = new double[n];
            c = new double[n]; d = new double[n]; v = new double[n];
            y = new double[n];

            Fill(fx, dfa, dfb);
            
            u[0] = d[0]/a[0];  // U
            v[0] = -b[0]/a[0]; // V

            for (int i = 1; i < n; i++)
            {
                u[i] = (d[i] - u[i - 1] * c[i]) / (c[i] * v[i - 1] + a[i]);
                v[i] = -b[i] / (c[i] * v[i - 1] + a[i]);
            }

            y[n - 1] = u[n-1];
            v[n - 1] = 0;

            for (int i = n - 2; i >= 0; i--)
            {
                y[i] = v[i] * y[i + 1] + u[i];
            }
            return y;
        }
    }
}
