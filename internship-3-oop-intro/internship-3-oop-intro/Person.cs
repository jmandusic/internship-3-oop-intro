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
            Console.WriteLine("Unesite ime eventa na koji želite dodati osobu");
            var eventName = Console.ReadLine();
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
                        addPerson = false;
                    }
                }
            }
        }

        public static void RemovePersonFromEvent(Dictionary<Event, List<Person>> dict)
        {
            Console.WriteLine("Izabrali ste uklanjanje osobe sa eventa");
            Console.WriteLine("Unesite ime eventa sa kojeg želite ukloniti osobu");
            var eventNameRemovePerson = Console.ReadLine();
            Console.WriteLine("Unesite podatke osobe:");
            Console.WriteLine("Ime:");
            var removeFirstName = Console.ReadLine();
            Console.WriteLine("Prezime:");
            var removeLastName = Console.ReadLine();
            Console.WriteLine("OIB:");
            var removeID = int.Parse(Console.ReadLine());
            Console.WriteLine("Broj mobitela:");
            var removePhoneNumber = int.Parse(Console.ReadLine());
            var removePerson = false;
            foreach (var item in dict)
            {
                if (item.Key.Name == eventNameRemovePerson)
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        if (removeID == item.Value[i].ID && Event.PermissionToContinue())
                        {
                            item.Value.Remove(item.Value[i]);
                            removePerson = true;
                        }
                    }
                }
            }
            if (removePerson == false)
            {
                Console.WriteLine("Uneseni ID ne postoji na eventu pa ga se ne može ukloniti");
            }
        }
    }
}
