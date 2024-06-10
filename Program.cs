using System;
using System.Collections.Generic;
using System.IO;

namespace _2lab_7
{
    public class Person : IComparable
    {
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string NameFromDad { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double MonthIncome { get; set; }
        public bool HasEmployed { get; set; }
        public bool DoRegularSport { get; set; }

        public double GetYearIncome()
        {
            return MonthIncome * 12;
        }

        public double GetRecomendedWeight()
        {
            return (Height - 100) * 1.15;
        }

        public Person()
        {
        }

        public Person(string firsName, string lastName, string nameFromDad, int age, double height, double weight, double monthIncome, bool hasEmployed, bool doRegularSport)
        {
            FirsName = firsName;
            LastName = lastName;
            NameFromDad = nameFromDad;
            Age = age;
            Height = height;
            Weight = weight;
            MonthIncome = monthIncome;
            HasEmployed = hasEmployed;
            DoRegularSport = doRegularSport;
        }

        public string Info()
        {
            return FirsName + ", " + LastName + ", " + NameFromDad;
        }

        public int CompareTo(object obj)
        {
            Person t = obj as Person;
            return string.Compare(this.FirsName, t.FirsName);
        }
    }
    class Program
    {
        static List<Person> persons = new List<Person>();

        static void PrintPersons()
        {
            foreach (Person person in persons)
            {
                Console.WriteLine(person.Info().Replace('і', 'i'));
            }
        }

        static void Main(string[] args)
        {
            persons = new List<Person>();
            FileStream fs = new FileStream("try2.persons", FileMode.Open);
            BinaryReader reader = new BinaryReader(fs);

            try
            {
                Person person;
                Console.WriteLine("      Читаємо данi з файлу...\n");

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    person = new Person();
                    for (int i = 0; i <= 8; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                person.FirsName = reader.ReadString();
                                break;
                            case 1:
                                person.LastName = reader.ReadString();
                                break;
                            case 2:
                                person.NameFromDad = reader.ReadString();
                                break;
                            case 3:
                                person.Age = reader.ReadInt32();
                                break;
                            case 4:
                                person.Height = reader.ReadDouble();
                                break;
                            case 5:
                                person.Weight = reader.ReadDouble();
                                break;
                            case 6:
                                person.MonthIncome = reader.ReadDouble();
                                break;
                            case 7:
                                person.HasEmployed = reader.ReadBoolean();
                                break;
                            case 8:
                                person.DoRegularSport = reader.ReadBoolean();
                                break;
                        }    
                    }
                    persons.Add(person); 
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Сталась помилка: {0}", ex.Message);
            }
            finally
            {
                reader.Close();
            }
            Console.WriteLine("     Несортований перелiк персон: {0}", persons.Count);
            PrintPersons();
            persons.Sort();
            Console.WriteLine("     Сортований перелiк персон: {0}", persons.Count);
            PrintPersons();
            Console.WriteLine("     Додаємо новий запис: Базюн");
            Person personDid = new Person("Дiд", "Шинобi", "Бордюрович", 30, 185, 90, 1488, true, true);
            persons.Add(personDid);
            persons.Sort();
            Console.WriteLine("     Перелiк персон: {0}", persons.Count);
            PrintPersons();
            Console.WriteLine("     Видаляємо останнє значення");
            persons.RemoveAt(persons.Count - 1);
            Console.WriteLine("     Перелiк персон: {0}", persons.Count);

            PrintPersons();
            Console.ReadKey();
        }
    }
}
