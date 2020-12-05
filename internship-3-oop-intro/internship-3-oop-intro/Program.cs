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
            Console.WriteLine("6 - Ispisi");
            Console.WriteLine("7 - Izlaz iz aplikacije");
            Console.WriteLine();
        }

        static void SubMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Ispis detalja eventa");
            Console.WriteLine("2 - Ispis svih osoba na eventu");
            Console.WriteLine("3 - Ispis svih detalja");
            Console.WriteLine("4 - Izlazak iz podmenija");
            Console.WriteLine();
        }

        static void PrintDetailsEvent(Dictionary<Event, List<Person>> dictionary, string eventName)
        {
            foreach (var item in dictionary)
            {
                if (item.Key.Name == eventName)
                {
                    var duration = (item.Key.EndTime - item.Key.StartTime).ToString();
                    Console.WriteLine("Ime:" + ' ' + item.Key.Name);
                    Console.WriteLine("Tip:" + ' ' + item.Key.EventType);
                    Console.WriteLine("Start time:" + ' ' + item.Key.StartTime);
                    Console.WriteLine("End time:" + ' ' + item.Key.EndTime);
                    Console.WriteLine("Duration:" + ' ' + duration);
                    Console.WriteLine("Broj ljudi na eventu:" + ' ' + item.Value.Count.ToString());
                }
            }
        }

        static void PrintParticipantsEvent(Dictionary<Event, List<Person>> dictionary, string eventName)
        {
            foreach (var item in dictionary)
            {
                if (item.Key.Name == eventName)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        Console.WriteLine((i + 1).ToString() + '.' + ' ' + item.Value[i].FirstName.ToString() + ' ' + item.Value[i].LastName.ToString() + ' ' + item.Value[i].PhoneNumber.ToString());
                    }
                }
            }
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
            var menuDefined = true;
            while (runProgram)
            {
                if (menuDefined)
                {
                    Menu();
                    menuDefined = true;
                }
                var pick = int.Parse(Console.ReadLine());
                switch (pick)
                {
                    case 1:
                        Event.AddEvent(dictionary);
                        menuDefined = true;
                        break;
                    case 2:
                        Event.DeleteEvent(dictionary);
                        menuDefined = true;
                        break;
                    case 4:
                        Person.AddPersonToEvent(dictionary);
                        menuDefined = true;
                        break;
                    case 5:
                        Person.RemovePersonFromEvent(dictionary);
                        menuDefined = true;
                        break;
                    case 6:
                        var runSubMenu = true;
                        var subMenuDefined = true;
                        while (runSubMenu)
                        {
                            if (subMenuDefined)
                            {
                                SubMenu();
                                subMenuDefined = true;
                            }
                            var pickSubMenu = int.Parse(Console.ReadLine());
                            switch (pickSubMenu)
                            {
                                case 1:
                                    Console.WriteLine("Izabrali ste ispis detalja o eventu");
                                    var eventDef = false;
                                    while (!eventDef)
                                    {
                                        Console.WriteLine("Detalje kojeg eventa želite ispisati, unesite ime eventa");
                                        var eventName = Console.ReadLine();
                                        foreach (var item in dictionary)
                                        {
                                            if (eventName == item.Key.Name)
                                            {
                                                eventDef = true;
                                            }
                                        }
                                        if (eventDef)
                                        {
                                            PrintDetailsEvent(dictionary, eventName);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ne postoji event pod tim imenom. Ponoviti unos!");
                                        }
                                    }
                                    subMenuDefined = true;
                                    break;
                                case 2:
                                    Console.WriteLine("Izabrali ste ispis svih osoba na eventu");
                                    var printDef = false;
                                    while (!printDef)
                                    {
                                        Console.WriteLine("Sudionike kojeg eventa želite ispisati, unesite ime eventa");
                                        var eventName = Console.ReadLine();
                                        foreach (var item in dictionary)
                                        {
                                            if (eventName == item.Key.Name)
                                            {
                                                printDef = true;
                                            }
                                        }
                                        if (printDef)
                                        {
                                            PrintParticipantsEvent(dictionary, eventName);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ne postoji event pod tim imenom. Ponoviti unos!");
                                        }
                                    }
                                    subMenuDefined = true;
                                    break;
                                case 3:
                                    Console.WriteLine("Izabrali ste ispis svih detalja na eventu");
                                    var printAll = false;
                                    while (!printAll)
                                    {
                                        Console.WriteLine("Detalje kojeg eventa želite ispisati, unesite ime eventa");
                                        var eventName = Console.ReadLine();
                                        foreach (var item in dictionary)
                                        {
                                            if (eventName == item.Key.Name)
                                            {
                                                printAll = true;
                                            }
                                        }
                                        if (printAll)
                                        {
                                            PrintDetailsEvent(dictionary, eventName);
                                            PrintParticipantsEvent(dictionary, eventName);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ne postoji event pod tim imenom. Ponoviti unos!");
                                        }
                                    }
                                    subMenuDefined = true;
                                    break;
                                case 4:
                                    Console.WriteLine("Izabrali ste izlazak iz podmenija");
                                    if (Event.PermissionToContinue())
                                    {
                                        runSubMenu = false;
                                    }
                                    subMenuDefined = true;
                                    break;
                                default:
                                    Console.WriteLine("Pogrešan unos");
                                    Console.WriteLine("Za ponovni pokušaj unosa unijeti 0, za izbornik bilo što");
                                    var optionSubMenu = Console.ReadLine();
                                    if (optionSubMenu == "0")
                                    {
                                        subMenuDefined = false;
                                    }
                                    break;
                            }
                        }
                         break;
                    case 7:
                        Console.WriteLine("Izabrali ste izlazak iz aplikacije");
                        if (Event.PermissionToContinue())
                        {
                            runProgram = false;
                        }
                        menuDefined = true;
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos");
                        Console.WriteLine("Za ponovni pokušaj unosa unijeti 0, za izbornik bilo što");
                        var option = Console.ReadLine();
                        if (option == "0")
                        {
                            menuDefined = false;
                        }
                        break;
                }
            }
        }
    }
}
