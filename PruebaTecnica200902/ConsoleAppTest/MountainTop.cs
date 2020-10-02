using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    /// <summary>
    /// Clase para analizar cumbres en arrays de enteros
    /// </summary>
    public class MountainTop
    {
        /// <summary>
        /// Realiza un recorrido parcial en un bucle for de un array de enteros y obtiene el número de cimas
        /// </summary>
        /// <param name="trackerMountain">Array a recorrer</param>
        /// <param name="start">Valor inicial del recorrido</param>
        /// <param name="end">Número del elemento final del recorrido</param>
        /// <returns>Número de cimas encontradas</returns>
        private int GetNumberOfTopsPartialFor(int[] trackerMountain, int start, int end)
        {
            int result = 0;

            for (int i = start; i < end; i++)
            {
                int point = trackerMountain[i];

                // Comprueba que no es el primer elemento, ya que ni este ni el último pueden ser cimas al no tener anterior o posterior con el que comparar
                if (i > start + 1)
                {
                    int topCandidate = trackerMountain[i - 1];
                    int topPrevious = trackerMountain[i - 2];
                    if (topCandidate > topPrevious && topCandidate > point)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Devuelve el número de cimas encontradas en un array de enteros.
        /// Este método divide el recorrido del array en hilos de ejecución para que pueda realizarse en paralelo,
        /// no sería más rápido que un bluce for en una primera instancia, pero en llamadas recursivas disminuye
        /// los tiempos de ejecución respecto a un bucle for síncrono
        /// </summary>
        /// <param name="trackerMountain">Parámetro con el array de enteros</param>
        /// <returns>Número de cimas encontradas</returns>
        public async Task<int> GetNumberOfTops(int[] trackerMountain)
        {
            int result = 0;
            // Obtiene el número de hilos que el equipo soporta para realizar una división del array equitativa
            int numProcessors = Environment.ProcessorCount;
            int numMax = trackerMountain.Length;
            int numMaxByTask = (int)Math.Ceiling((decimal)numMax / numProcessors);
            if (numMaxByTask < 3)
            {
                // Descarta el proceso multihilo cuando hay un número de valores bajo
                return GetNumberOfTopsPartialFor(trackerMountain, 0, numMax);
            }

            var allTask = new List<Task<int>>();

            // Reparte el recorrido en bucles for por cada hilo del procesador
            for (int i = 0; i < numProcessors; i++)
            {
                int start = (i * numMaxByTask) - (i > 0 ? 1 : 0);
                int end = (i + 1) * numMaxByTask + (i + 1 == numProcessors ? 0 : 1);
                var getNumberOfTopsTask = Task.Run(() => { return GetNumberOfTopsPartialFor(trackerMountain, start, end); });
                allTask.Add(getNumberOfTopsTask);
            }

            while (allTask.Any())
            {
                var taskFinished = await Task.WhenAny(allTask);
                result += taskFinished.Result;
                allTask.Remove(taskFinished);
            }

            return result;
        }
    }
}
