using System;
using System.Collections.Generic;

namespace internship_3_oop_intro
{
    class Program
    {
        static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Dodavanje eventa");
            Console.WriteLine("2 - Brisanje eventa");
            Console.WriteLine("3 - Edit eventa");
            Console.WriteLine("4 - Dodavanje osobe na event");
            Console.WriteLine("5 - Uklanjanje osobe sa eventa");
            Console.WriteLine("6 - Ispis detalja eventa");
            Console.WriteLine("7 - Izlaz iz aplikacije");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var person1 = new Person("Marko", "Markić", 100, 045782);
            var person2 = new Person("Ante", "Antić", 101, 736251);
            var person3 = new Person("Jure", "Jurić", 102, 672451);
            var person4 = new Person("Ivan", "Ivanić", 103, 982541);
            var person5 = new Person("Luka", "Lukić", 104, 972451);
            var person6 = new Person("Mario", "Marić", 105, 725413);
            var person7 = new Person("Frane", "Franić", 106, 983625);

            var event1StartTime = new DateTime(2020, 5, 8, 14, 45, 0);
            var event1EndTime = new DateTime(2020, 5, 8, 15, 30, 0);
            var event2StartTime = new DateTime(2020, 4, 7, 20, 45, 0);
            var event2EndTime = new DateTime(2020, 4, 7, 23, 30, 0);
            var event1 = new Event("A", 1, event1StartTime, event1EndTime);
            var event2 = new Event("B", 2, event2StartTime, event2EndTime);

            var dictionary = new Dictionary<Event, List<Person>>()
            {
                {event1, new List<Person>() {person1, person5, person7 } },
                {event2, new List<Person>() {person2, person3, person4, person6} }
            };

            var runProgram = true;
            while (runProgram)
            {
                Menu();
                var pick = int.Parse(Console.ReadLine());
                switch (pick)
                {
                    case 1:
                        Event.AddEvent(dictionary);
                        break;
                    case 2:
                        Event.DeleteEvent(dictionary);
                        break;
                    case 4:
                        Person.AddPersonToEvent(dictionary);
                        break;
                    case 5:
                        Person.RemovePersonFromEvent(dictionary);
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos");
                        break;
                }
                foreach (var item in dictionary)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        Console.WriteLine(item.Key.Name + ' ' + item.Value[i].FirstName + ' ' + item.Value[i].LastName);

                    }
                }
            }
        }
    }
}
