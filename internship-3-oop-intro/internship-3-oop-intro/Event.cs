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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

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
            Console.WriteLine("Izabrali ste opciju dodavanja novog eventa");
            Console.WriteLine("Unesite ime novog eventa");
            var newEventName = Console.ReadLine();
            Console.WriteLine("Unesite tip novog eventa (Coffee, Lecture, Concert, StudySession)");
            var newEventTypeEnum = Console.ReadLine();
            var newEventType = -1;
            switch (newEventTypeEnum.ToLower())
            {
                case "coffee":
                    newEventType = (int)TypeOfEvent.Coffee;
                    break;
                case "lecture":
                    newEventType = (int)TypeOfEvent.Lecture;
                    break;
                case "concert":
                    newEventType = (int)TypeOfEvent.Concert;
                    break;
                case "studysession":
                    newEventType = (int)TypeOfEvent.StudySession;
                    break;
                default:
                    Console.WriteLine("Pogrešan unos");
                    break;
            }
            Console.WriteLine("Unesi start time eventa u formatu DD/MM/YYYY HH:mm:ss");
            DateTime startTime = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Unesi end time eventa u formatu DD/MM/YYYY HH:mm:ss");
            DateTime endTime = DateTime.Parse(Console.ReadLine());
            //provjerit unose??
            var newEvent = new Event(newEventName, newEventType, startTime, endTime);
            dict.Add(newEvent, new List<Person>());
        }

        public static void DeleteEvent(Dictionary<Event, List<Person>> dict)
        {
            Console.WriteLine("Izabrali ste opciju brisanja eventa");
            Console.WriteLine("Unesite ime eventa koji želite izbrisati");
            var deleteEvent = Console.ReadLine();
            foreach (var item in dict)
            {
                if (item.Key.Name == deleteEvent && PermissionToContinue()) 
                {
                    dict.Remove(item.Key);
                }
            }
        }
    }
}
