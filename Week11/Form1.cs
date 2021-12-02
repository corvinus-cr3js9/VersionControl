using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week11.Entitites;

namespace Week11
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<Birthprobability> BirthProbabilities = new List<Birthprobability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            for (int year = 2005; year <= 2024 ; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    Simstep(year, Population[i]);
                }

                int nmbrOfMales = (from x in Population
                                   where x.Gender == Gender.Male && x.IsAlive
                                   select x).Count();
                int nmbrOfFemales = (from x in Population
                                     where x.Gender == Gender.Female && x.IsAlive
                                     select x).Count();

                Console.WriteLine(string.Format("Év: {0} Fiúk: {1} Lányok: {2}", year, nmbrOfMales, nmbrOfFemales));



            }




        }

        public void Simstep(int year, Person person)
        {
            if (!person.IsAlive) return;
            byte age = (byte)(year - person.BirthYear);

            double pDeath =(from x in DeathProbabilities
                           where x.Age == age && x.Gender == person.Gender
                           select x.P).FirstOrDefault();
            if (rng.NextDouble() <= pDeath)
            {
                person.IsAlive = false;
            }

            if (person.IsAlive && person.Gender == Gender.Female)
            {
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();


                //Születik gyerek?
                if (rng.NextDouble() <=pBirth)
                {
                    Person újszülött = new Person()
                    {
                        BirthYear = year,
                        NbrOfChildren = 0,
                        Gender = (Gender)(rng.Next(1, 3))
                    };
                    Population.Add(újszülött);
                }

            }

            


        }

        public List<Person> GetPopulation(string csvPath)
        {
            List<Person> population = new List<Person>();
            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }
            return population;
        }
        public List<Birthprobability> GetBirthProbabilities(string csvPath)
        {
            List<Birthprobability> birthprobabilities = new List<Birthprobability>();
            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthprobabilities.Add(new Birthprobability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = Double.Parse(line[2])
                    });
                }
            }
            return birthprobabilities;
            
        }
        public List<DeathProbability> GetDeathProbabilities(string csvPath)
        {
            List<DeathProbability> deathprobabilities = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathprobabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        P = Double.Parse(line[2])
                    });
                }
            }
            return deathprobabilities;
        }

    }
}
