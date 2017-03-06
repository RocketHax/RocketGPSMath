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
            Console.Write("Enter a number : ");
            
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Value entered is : {0}", n);

            if(n % 2 == 0)
            {
                Console.WriteLine("Not a valid number, need an odd number!");
                return;
            }

            int mid_index = (int)Math.Ceiling((double)n / 2) - 1;

            for (int j = 0; j < n; ++j)
            {
                for (int i = 0; i < n; ++i)
                {
                    int draw_start_index = 0;
                    int draw_end_index = 0; 

                    if (j <= mid_index)
                    {
                        draw_start_index = mid_index - j;
                        draw_end_index = mid_index + j;
                    }
                    else
                    {
                        draw_start_index = j - mid_index;
                        draw_end_index = (n - 1) - (j - mid_index);
                    }

                    if (i >= draw_start_index && i <= draw_end_index)
                        Console.Write("*");
                    else if(i < mid_index)
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
