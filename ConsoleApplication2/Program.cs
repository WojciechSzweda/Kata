using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class GeneticAlgorithm
    {
        private Random random = new Random();

        public string Generate(int length)
        {
            var chromosome = new char[length];
            for (int i = 0; i < length; i++)
            {
                chromosome[i] = (char)random.Next(2);
            }
            return new string(chromosome);
        }
        
        public string Select(IEnumerable<string> population, IEnumerable<double> fitnesses, double sum = 0)
        {
            var tournamentPool = new List<string>();
            var fitnessesPool = new List<double>();
            int tournamenSize = 10;
            for (int i = 0; i < tournamenSize; i++)
            {
                var rngIndex = random.Next(population.Count());
                tournamentPool.Add(population.ElementAt(rngIndex));
                fitnessesPool.Add(fitnesses.ElementAt(rngIndex));
            }
            return tournamentPool.Zip(fitnessesPool, (x, y) => new { dna = x, score = y }).OrderByDescending(x => x.score).FirstOrDefault().dna;
        }

        public string Mutate(string chromosome, double probability)
        {
            var chrom = chromosome.ToCharArray();
            for (int i = 0; i < chrom.Length; i++)
            {
                if (random.NextDouble() < probability)
                    chrom[i] = chrom[i] == '0' ? '1' : '0';
            }
            return new string(chrom);
        }

        public IEnumerable<string> Crossover(string chromosome1, string chromosome2)
        {
            var length = chromosome2.Length;
            var child1 = new char[length];
            var child2 = new char[length];
            for (int i = 0; i < length; i++)
            {
                if (random.NextDouble() > 0.5)
                {
                    child1[i] = chromosome1[i];
                    child2[i] = chromosome2[i];
                }
                else
                {
                    child1[i] = chromosome2[i];
                    child2[i] = chromosome1[i];
                }
            }
            return new string[] { new string(child1), new string(child2) };
        }


        public string Run(Func<string, double> fitness, int length, double p_c, double p_m, int iterations = 100)
        {
            var population = new List<string>();
            var fitnesses = new List<double>();
            int populationSize = 100;
            for (int i = 0; i < populationSize; i++)
            {
                var dna = Generate(length);
                population.Add(dna);
                fitnesses.Add(fitness(dna));
            }

            for (int k = 0; k < iterations; k++)
            {
                var newPopulation = new List<string>();
                for (int i = 0; i < populationSize / 2; i++)
                {
                    newPopulation.AddRange(Crossover(Select(population, fitnesses), Select(population, fitnesses)));
                }
                population = newPopulation;
                fitnesses.Clear();
                foreach (var dna in population)
                {
                    fitnesses.Add(fitness(dna));
                }
            }
            return population.Zip(fitnesses, (x, y) => new { dna = x, score = y }).OrderByDescending(x => x.score).FirstOrDefault().dna;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
