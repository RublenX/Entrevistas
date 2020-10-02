using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class TestMountainTop
    {
        public int GetNumberOfTopsForeach(int[] trackerMountain)
        {
            int? topPrevious = null;
            int? topCandidate = null;
            int result = 0;

            foreach (var point in trackerMountain)
            {
                // Comprueba que no es el primer elemento, ya que ni este ni el último pueden ser cimas al no tener anterior o posterior con el que comparar
                if (topCandidate != null && topPrevious != null)
                {
                    if (topCandidate > topPrevious && topCandidate > point)
                    {
                        result++;
                    }
                }

                // Al finalizar la comprobación establece los siguientes puntos de control
                topPrevious = topCandidate;
                topCandidate = point;
            }

            return result;
        }

        public int GetNumberOfTopsFor(int[] trackerMountain)
        {
            int result = 0;

            for (int i = 0; i < trackerMountain.Length; i++)
            {
                // Comprueba que no es el primer elemento, ya que ni este ni el último pueden ser cimas al no tener anterior o posterior con el que comparar
                if (i > 1)
                {
                    int topRear = trackerMountain[i];
                    int topCandidate = trackerMountain[i - 1];
                    int topPrevious = trackerMountain[i - 2];
                    if (topCandidate > topPrevious && topCandidate > topRear)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public int GetNumberOfTopsPartialFor(int[] trackerMountain, int start, int end)
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

        public int GetNumberOfTopsByProcessorParallel(int[] trackerMountain)
        {
            int result = 0;
            int numProcesors = Environment.ProcessorCount;
            int numMax = trackerMountain.Count();
            int numMaxPorHilo = (int)Math.Ceiling((decimal)numMax / numProcesors);
            if (numMaxPorHilo < 3)
            {
                // Se descarta el procesamiento por hilos si no hay los suficientes valores
                numProcesors = 1;
                numMaxPorHilo = numMax;
            }

            var allTask = new List<Task<int>>();

            Parallel.For(0, numProcesors, i =>
            {
                int start = (i * numMaxPorHilo) - (i > 0 ? 1 : 0);
                int end = (i + 1) * numMaxPorHilo + (i + 1 == numProcesors ? 0 : 1);
                int numberOfTops = GetNumberOfTopsPartialFor(trackerMountain, start, end);
                Interlocked.Add(ref result, numberOfTops);
            });

            return result;
        }

        public async Task<int> GetNumberOfTopsByProcessor(int[] trackerMountain)
        {
            int result = 0;
            int numProcesors = Environment.ProcessorCount;
            int numMax = trackerMountain.Length;
            int numMaxPorHilo = (int)Math.Ceiling((decimal)numMax / numProcesors);
            if (numMaxPorHilo < 3)
            {
                numProcesors = 1;
                numMaxPorHilo = numMax;
            }

            var allTask = new List<Task<int>>();

            // Reparte el recuento por cada hilo del procesador
            for (int i = 0; i < numProcesors; i++)
            {
                int start = (i * numMaxPorHilo) - (i > 0 ? 1 : 0);
                int end = (i + 1) * numMaxPorHilo + (i + 1 == numProcesors ? 0 : 1);
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

        public int GetNumberOfTopsParallelFor(int[] trackerMountain)
        {
            int result = 0;

            Parallel.For(0, trackerMountain.Length, i =>
            {
                // Comprueba que no es el primer elemento, ya que ni este ni el último pueden ser cimas al no tener anterior o posterior con el que comparar
                if (i > 1)
                {
                    int topRear = trackerMountain[i];
                    int topCandidate = trackerMountain[i - 1];
                    int topPrevious = trackerMountain[i - 2];
                    if (topCandidate > topPrevious && topCandidate > topRear)
                    {
                        Interlocked.Add(ref result, 1); ;
                    }
                }
            });

            return result;
        }
    }
}
