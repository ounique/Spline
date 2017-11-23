using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сплайны
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double[] s(double[] fx, double[] x, double[] m, double[] X, double h)
        {
            double[] temp = new double[X.Length];

            int n = fx.Length;
            int k = (X.Length - 1)/(n-1);

            double t;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    t = (X[i * k + j] - x[i]) / h;
                    temp[i * k + j] = fx[i] * f1(t) + fx[i + 1] * f2(t) + m[i] * h * f3(t) + m[i + 1] * h * f4(t);
                }
            }
            temp[(n - 1) * k] = fx[n - 1] * f1(0) + m[n - 1] * h * f3(0);
            return temp;
        }
        double f1(double t)
        {
            return (t - 1) * (t - 1) * (2 * t + 1);
        }
        double f2(double t)
        {
            return t * t * (3 - 2 * t);
        }
        double f3(double t)
        {
            return t * (1 - t) * (1 - t);
        }
        double f4(double t)
        {
            return -t * t * (1 - t);
        }
        
        double f(double x)
        {
            return x*Math.Exp(-x);
        }
        double[] FillMass(int n, double a, double b)
        {
            double[] temp = new double[n];
            double h = (b - a) / (n - 1);
            for (int i = 0; i < n; i++)
            {
                temp[i] = a + i * h;
            }
            return temp;
        }
        double[] FillMass(double[] X)
        {
            double[] temp = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                temp[i] = f(X[i]);
            }
            return temp;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            /*  Узловые точки для построения сплайна  */
            int n = 6;
            double a = 0;
            double b = 2;
            double h = (b - a) / (n - 1);
            /******************************************/
            
            double[] x = FillMass(n, a, b);
            double[] fx = FillMass(x);

            /* Вычисление первых производных */
            double dfa = (fx[1] - fx[0]) / h;
            double dfb = (fx[fx.Length - 1] - fx[fx.Length - 2]) / h;
            /********************************/

            double[] m = Sweep_method.Sweep(fx, dfa, dfb, n, h);

            int N = 501;        // Количество точек сплайна
            int k = (N - 1) / (n - 1); // Количество точек между x[i] и x[i+1]
            double[] X = FillMass(N, a, b);
            double[] S = s(fx, x, m, X, h);

            for (int i = 0; i < N; i++)
            {
                chart1.Series[0].Points.AddXY(X[i], S[i]);
            }

            double temp;
            for (int i = 0; i < 201; i++)
            {
                temp = a + i*0.01;
                chart1.Series[1].Points.AddXY(temp, f(temp));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
