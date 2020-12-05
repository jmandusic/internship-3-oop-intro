using System;
using System.Collections.Generic;
using System.Text;

namespace internship_3_oop_intro
{
    class Event
    {
        public Event(string name, int type, DateTime startTime, DateTime endTime)
        {
            Name = name;
            EventType = type;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string Name { get; set; }
        public int EventType { get; set; }
        public  DateTime StartTime { get; set; }
        public  DateTime EndTime { get; set; }


        public static bool PermissionToContinue()
        {
            Console.WriteLine("Jeste li sigurni da želite nastaviti?");
            while (true)
            {
                Console.WriteLine("Unesi: y/n");
                var choice = Console.ReadLine();
                if (choice.ToLower() == "y")
                {
                    return true;
                }
                else if (choice.ToLower() == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Pogrešan unos");
                }
            }

        }

        public enum TypeOfEvent
        {
            Coffee,
            Lecture,
            Concert,
            StudySession
        }
        public static void AddEvent(Dictionary<Event, List<Person>> dict)
        {
            var startTime = new DateTime(2020, 12, 4, 14, 0, 0);
            var endTime = new DateTime(2020, 12, 4, 16, 30, 0 );
            Console.WriteLine("Izabrali ste opciju dodavanja novog eventa");
            Console.WriteLine("Unesite ime novog eventa");
            var newEventName = Console.ReadLine();
            var eventEnum = true;
            var newEventType = -1;
            while (eventEnum)
            {
                Console.WriteLine("Unesite tip novog eventa (Coffee, Lecture, Concert, StudySession)");
                var newEventTypeEnum = Console.ReadLine();
                switch (newEventTypeEnum.ToLower())
                {
                    case "coffee":
                        newEventType = (int)TypeOfEvent.Coffee;
                        eventEnum = false;
                        break;
                    case "lecture":
                        newEventType = (int)TypeOfEvent.Lecture;
                        eventEnum = false;
                        break;
                    case "concert":
                        newEventType = (int)TypeOfEvent.Concert;
                        eventEnum = false;
                        break;
                    case "studysession":
                        newEventType = (int)TypeOfEvent.StudySession;
                        eventEnum = false;
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos");
                        break;
                }
            }
            while (true)
            {
                var startDefined = true;
                Console.WriteLine("Unesi start time eventa u formatu DD/MM/YYYY HH:mm:ss");
                try
                {
                    startTime = DateTime.Parse(Console.ReadLine());
                    foreach (var item in dict)
                    {
                        if (startTime.Ticks >= item.Key.StartTime.Ticks && startTime.Ticks <= item.Key.EndTime.Ticks)
                        {
                            Console.WriteLine("Ne možete dodati početak eventa za vrijeme drugog eventa. Ponoviti unos!");
                            startDefined = false;
                        }
                    }
                    if (startDefined)
                    {
                        break;

                    }
                }
                catch
                {
                    Console.WriteLine("Pogrešan unos. Paziti na format unosa!");
                }
            }
            while (true)
            {
                var endDefined = true;
                Console.WriteLine("Unesi end time eventa u formatu DD/MM/YYYY HH:mm:ss");
                try
                {
                    endTime = DateTime.Parse(Console.ReadLine());
                    foreach (var item in dict)
                    {
                        if (endTime.Ticks >= item.Key.StartTime.Ticks && endTime.Ticks <= item.Key.EndTime.Ticks)          
                        {
                            //startTime nije u intervalu drugog eventa, ali endTime je
                            Console.WriteLine("Ne možete dodati kraj eventa za vrijeme drugog eventa. Ponoviti unos!");
                            endDefined = false;
                        }
                        if (endTime.Ticks >= item.Key.EndTime.Ticks && startTime.Ticks <= item.Key.StartTime.Ticks)
                        {
                            //endTime i startTime su izvan intervala starta i enda drugog eventa, pa ih ne smijemo dodati jer bi se u jednom periodu održavala oba eventa istovremeno
                            Console.WriteLine("Ne možete dodati kraj eventa za vrijeme drugog eventa. Ponoviti unos!");
                            endDefined = false;
                        }
                    }
                    if (endTime.Ticks < startTime.Ticks)
                    {
                        endDefined = false;
                        Console.WriteLine("Kraj eventa ne može biti prije početka eventa. Ponoviti unos!");
                    }
                    if (endDefined)
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Pogrešan unos. Paziti na format unosa!");
                }
            }
            var newEvent = new Event(newEventName, newEventType, startTime, endTime);
            dict.Add(newEvent, new List<Person>());
            Console.WriteLine("Event dodan");
        }

        public static void DeleteEvent(Dictionary<Event, List<Person>> dict)
        {
            Console.WriteLine("Izabrali ste opciju brisanja eventa");
            var eventName = "";
            var delDefined = true;
            while (delDefined)
            {
                Console.WriteLine("Unesite ime eventa koji želite izbrisati");
                eventName = Console.ReadLine();
                foreach (var item in dict)
                {
                    if (item.Key.Name == eventName)
                    {
                        delDefined = false;
                    }
                }
                if (delDefined)
                {
                    Console.WriteLine("Ne postoji event pod tim imenom. Molim ponoviti unos!");
                }
            }
            foreach (var item in dict)
            {
                if (item.Key.Name == eventName && PermissionToContinue()) 
                {
                    Console.WriteLine("Event izbrisan");
                    dict.Remove(item.Key);
                }
            }
        }
    }
}
