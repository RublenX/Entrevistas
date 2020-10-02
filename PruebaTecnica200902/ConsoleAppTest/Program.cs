using System;
using System.Linq;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool repeat = false;

            do
            {
                int[] nums = Enumerable.Range(0, 400000).OrderBy(c => Guid.NewGuid()).ToArray();
                //int[] nums = new int[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 };
                //int[] nums = new int[] { 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1, 5, 1 };
                //int[] nums = new int[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2, 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 };
                //int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 9, 9, 9, 9, 9, 9, 9, 9, 8, 8, 8, 8, 8, 8, 7, 7, 7, 7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 5, 6, 7, 8, 9, 10, 9, 8, 7, 6, 5, 4, 4, 4, 4, 4, 4, 3, 2, 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

                TestMountainTop testMountainTop = new TestMountainTop();
                DateTime tStart = DateTime.Now;
                int resultado = testMountainTop.GetNumberOfTopsForeach(nums);
                Console.WriteLine($"{resultado} GetNumberOfTopsForeach ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");

                tStart = DateTime.Now;
                resultado = testMountainTop.GetNumberOfTopsFor(nums);
                Console.WriteLine($"{resultado} GetNumberOfTopsFor ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");

                tStart = DateTime.Now;
                resultado = testMountainTop.GetNumberOfTopsPartialFor(nums, 0, nums.Length);
                Console.WriteLine($"{resultado} GetNumberOfTopsForPartial ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");

                tStart = DateTime.Now;
                resultado = testMountainTop.GetNumberOfTopsByProcessorParallel(nums);
                Console.WriteLine($"{resultado} GetNumberOfTopsByProcessorParallel ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");

                tStart = DateTime.Now;
                resultado = testMountainTop.GetNumberOfTopsByProcessor(nums).Result;
                Console.WriteLine($"{resultado} GetNumberOfTopsByProcessor ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");

                tStart = DateTime.Now;
                resultado = testMountainTop.GetNumberOfTopsParallelFor(nums);
                Console.WriteLine($"{resultado} GetNumberOfTopsParallelFor ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");

                tStart = DateTime.Now;
                resultado = new MountainTop().GetNumberOfTops(nums).Result;
                Console.WriteLine($"{resultado} GetNumberOfTops [Entregado] ha tardado {(DateTime.Now - tStart).TotalMilliseconds}");


                Console.Write("¿Deseas repetir la ejecución? [S/N] : ");
                char key = Console.ReadKey().KeyChar;
                repeat = key == 's' || key == 'S';
                Console.WriteLine();
            } while (repeat);
        }
    }
}
