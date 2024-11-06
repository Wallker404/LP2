using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP2
{
    public enum Sign
    {
        Min,Max
    }
    public static class SimplexMethod
    {
        public static double[] GetAnswer(double[,] a, Sign sign, int[] high, int[] left)
        {
            while (!checkB(a) || !CheckF(a))
            {
                if (!checkB(a))
                {
                    a = FixForB(a, high, left);
                }
                if (!CheckF(a))
                {
                    a = FixForF(a, high, left);
                }
            }
            double[] answer = new double[a.GetLength(0)];
            for (int n = 1; n <= left.Length; n++)
            {
                bool t = false;
                for (int i = 0; i < left.Length; i++)
                {
                    
                    if (n == left[i])
                    {
                        answer[n-1] = a[i + 1, a.GetLength(1) - 1];
                        Console.WriteLine("X" + n + " = "+ a[i+1, a.GetLength(1)-1]);
                        t = true;
                        break;
                    }
                }
                if (!t)
                {
                    answer[n - 1] = 0;
                    Console.WriteLine("X" + n + " = 0");
                }
            }
            if(sign == Sign.Min)
            {
                answer[answer.Length - 1] = -a[0, a.GetLength(1) - 1];
                Console.WriteLine("F = " + -a[0,a.GetLength(1)-1]);
            }
            else
            {
                answer[answer.Length - 1] = a[0, a.GetLength(1) - 1];
                Console.WriteLine("F = " + a[0, a.GetLength(1)-1]);
            }
            return answer;
        }
        public static double[,] FixForB(double[,] a, int[] high, int[] left)
        {
            int t = 0;
            double tempt = 0;
            for (int i = 1; i < a.GetLength(0); i++)
            {
                if (Math.Abs(tempt) <= Math.Abs(a[i, a.GetLength(1) - 1]))
                {
                    t = i;
                    tempt = a[i, a.GetLength(1) - 1];
                }
            }
            int l = 0;
            double tempk = 0;
            double[,] nw = new double[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(1) - 1; i++)
            {
                if (Math.Abs(tempk) <= Math.Abs(a[t, i]))
                {
                    l = i;
                    tempk = a[t, i];
                }
            }
            int k = 0;
            double templ = 0;
            for (int i = 1; i < a.GetLength(0); i++)
            {
                if (Math.Abs(templ) <= Math.Abs(a[i, l])&& a[i,l]<0)
                {
                    k = i;
                    templ = a[i, l];
                }
            }
            int tem = left[k-1];
            left[k-1] = high[l];
            high[l] = tem;
            nw[k, l] = 1 / a[k, l];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                if (i != l) nw[k, i] = a[k, i] * nw[k, l];
            }
            for (int i = 0; i < a.GetLength(1); i++)
            {
                if (i != k) nw[i, l] = a[i, l] * -nw[k, l];
            }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (i != k && j != l)
                    {
                        nw[i, j] = a[i, j] - ((a[i, l] * a[k, j]) / a[k, l]);
                    }
                }
            }
            string s = string.Empty;
            for (int i = 0; i < nw.GetLength(0); i++)
            {

                for (int j = 0; j < nw.GetLength(1); j++)
                {
                    s += nw[i, j] + " ";
                }
                s += '\n';
            }
            Console.WriteLine(s);
            return nw;
        }
        public static double[,] FixForF(double[,] a, int[] high, int[] left)
        {
                int l = 0;
                double templ = 0;
                for (int i = 0; i < a.GetLength(1)-1; i++)
                {
                    if (Math.Abs(templ) <= Math.Abs(a[0, i]) && a[0,i]<0)
                    {
                        l = i;
                        templ = a[0, i];
                    }
                }
                int k = 0;
                double[] tempmas = new double[a.GetLength(1)-1];
                for (int i = 1; i < a.GetLength(0); i++)
                {
                    tempmas[i-1] = a[i,a.GetLength(1) - 1] / a[i,l];
                }
                double tempk = double.MaxValue;
                for (int i = 0; i< tempmas.Length;i++)
                {
                    if (tempk > tempmas[i]&& tempmas[i]>0)
                    {
                        k= i+1;
                        tempk = tempmas[i];
                    }
                }
                int tem = left[k-1];
                left[k-1] = high[l];
                high[l] = tem;
                double[,] nw = new double[a.GetLength(0), a.GetLength(1)];
                nw[k, l] = 1 / a[k, l];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    if (i != l) nw[k, i] = a[k, i] * nw[k, l];
                }
                for (int i = 0; i < a.GetLength(1); i++)
                {
                    if (i != k) nw[i, l] = a[i, l] * -nw[k, l];
                }
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        if (i != k && j != l)
                        {
                            nw[i, j] = a[i, j] - ((a[i, l] * a[k, j]) / a[k, l]);
                        }
                    }
                }
                string s = string.Empty;
                for (int i = 0; i < nw.GetLength(0); i++)
                {

                    for (int j = 0; j < nw.GetLength(1); j++)
                    {
                        s += nw[i, j] + " ";
                    }
                    s += '\n';
                }
                Console.WriteLine(s);
                return nw;
        }



        private static bool checkB(double[,] a)
        {
            for (int i = 1; i < a.GetLength(0); i++)
            {
                if (a[i, a.GetLength(1) - 1] < 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool CheckF(double[,] a) 
        {
            for (int i = 0; i < a.GetLength(1)-1; i++)
            {
                if (a[0, i] < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    
}
