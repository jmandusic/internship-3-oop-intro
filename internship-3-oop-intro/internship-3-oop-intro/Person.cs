using System;
using System.Collections.Generic;
using System.Text;

namespace internship_3_oop_intro
{
    class Person
    {
        public Person(string firstname, string lastname, int id, int phonenumber)
        {
            FirstName = firstname;
            LastName = lastname;
            ID = id;
            PhoneNumber = phonenumber;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; set; }
        public int PhoneNumber { get; set; }

        public static void AddPersonToEvent(Dictionary<Event, List<Person>> dict)
        {
            Console.WriteLine("Izabrali ste dodavanje osobe na event");
            var eventName = "";
            var addDefined = true;
            while (addDefined)
            {
                Console.WriteLine("Unesite ime eventa na koji želite dodati osobu");
                eventName = Console.ReadLine();
                foreach (var item in dict)
                {
                    if (item.Key.Name == eventName)
                    {
                        addDefined = false;
                    }
                }
                if (addDefined)
                {
                    Console.WriteLine("Ne postoji event pod tim imenom. Molim ponoviti unos!");
                }
            }
            Console.WriteLine("Unesite podatke osobe:");
            Console.WriteLine("Ime:");
            var newFirstName = Console.ReadLine();
            Console.WriteLine("Prezime:");
            var newLastName = Console.ReadLine();
            Console.WriteLine("OIB:");
            var newID = int.Parse(Console.ReadLine());
            Console.WriteLine("Broj mobitela:");
            var newPhoneNumber = int.Parse(Console.ReadLine());
            var addPerson = true;
            foreach (var item in dict)
            {
                if (item.Key.Name == eventName)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        if (newID == item.Value[i].ID)
                        {
                            Console.WriteLine("OIB osobe već postoji na eventu.");
                            Console.WriteLine("Ne možete dodati ovu osobu na event.");
                            addPerson = false;
                        }
                    }
                }
            }
            while (addPerson)
            {
                var eventParticipant = new Person(newFirstName, newLastName, newID, newPhoneNumber);
                foreach (var item in dict)
                {
                    if (item.Key.Name == eventName)
                    {
                        item.Value.Add(eventParticipant);
                        Console.WriteLine(eventParticipant.FirstName + ' ' + eventParticipant.LastName + ' ' + "dodan(a) na event");
                        addPerson = false;
                    }
                }
            }
        }

        public static void RemovePersonFromEvent(Dictionary<Event, List<Person>> dict)
        {
            Console.WriteLine("Izabrali ste uklanjanje osobe sa eventa");
            var eventNameRemovePerson = "";
            var removeDefined = true;
            while (removeDefined)
            {
                Console.WriteLine("Unesite ime eventa sa kojeg želite ukloniti osobu");
                eventNameRemovePerson = Console.ReadLine();
                foreach (var item in dict)
                {
                    if (item.Key.Name == eventNameRemovePerson)
                    {
                        removeDefined = false;
                    }
                }
                if (removeDefined)
                {
                    Console.WriteLine("Ne postoji event pod tim imenom. Molim ponoviti unos!");
                }
            };
            Console.WriteLine("Unesite OIB osobe:");
            Console.WriteLine("OIB:");
            var removeID = int.Parse(Console.ReadLine());
            var removePerson = false;
            foreach (var item in dict)
            {
                if (item.Key.Name == eventNameRemovePerson)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        if (removeID == item.Value[i].ID && Event.PermissionToContinue())
                        {
                            Console.WriteLine(item.Value[i].FirstName + ' ' + item.Value[i].LastName + ' ' + "uklonjen(a) s eventa");
                            item.Value.Remove(item.Value[i]);
                            removePerson = true;
                            break;
                        }
                        else if (removeID == item.Value[i].ID)
                        {
                            removePerson = true;
                        }
                    }
                }
            }
            if (removePerson == false)
            {
                Console.WriteLine("Uneseni OIB ne predstavlja osobu na eventu, pa ju se ne može ukloniti");
            }
        }
    }
}
