using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;

            while (n % 2 == 0)
            {
                Console.Write("Enter a number : ");
                n = Convert.ToInt32(Console.ReadLine());

                if (n % 2 == 0)
                  Console.WriteLine("Not a valid number, need an odd number!");
            }

            Console.WriteLine("\n Algo 1 : \n");
            PrintUsingAlgo1(n);
            Console.WriteLine("\n Algo 2 : \n");
            PrintUsingAlgo2(n);

            //Test 1

            Console.ReadLine();
        }

        //////////////////////////////////////////////////////////////////////////

        static void PrintUsingAlgo1(int n)
        {
            int mid_index = (int)Math.Ceiling((double)n / 2) - 1;

            for (int j = 0; j < n; ++j)
            {
                for (int i = 0; i < (mid_index + j + 1); ++i)
                {
                    int draw_start_index = mid_index - j;
                    int draw_end_index = mid_index + j;

                    if (j > mid_index)
                    {
                        draw_start_index = -draw_start_index;
                        draw_end_index = (n - 1) - draw_start_index;
                    }

                    if (i >= draw_start_index && i <= draw_end_index)
                        Console.Write("*");
                    else if (i < mid_index)
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        //////////////////////////////////////////////////////////////////////////

        static void PrintUsingAlgo2(int n)
        {
            n = n / 2;

            for(int i = -n; i <= n; ++i)
            {
                for(int j = -n; j <= n; ++j)
                {
                    if (Math.Abs(i) + Math.Abs(j) <= n)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        //////////////////////////////////////////////////////////////////////////

    }
}
