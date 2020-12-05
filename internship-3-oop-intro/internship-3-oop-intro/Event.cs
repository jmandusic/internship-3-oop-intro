using System;
using System.Collections.Generic;
using System.Linq;
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
            Coffee = 1,
            Lecture = 2,
            Concert = 3,
            StudySession = 4
        }

        static void EditMenu(string name)
        {
            Console.WriteLine();
            Console.WriteLine("Što želite urediti na eventu" + ' ' + name);
            Console.WriteLine("1 - Ime");
            Console.WriteLine("2 - Tip");
            Console.WriteLine("3 - Trajanje, odnosno početak i kraj");
            Console.WriteLine("4 - Povratak na glavni izbornik");
            Console.WriteLine();
        }
        public static void AddEvent(Dictionary<Event, List<Person>> dict)
        {
            var startTime = new DateTime(2020, 12, 4, 14, 0, 0);
            var endTime = new DateTime(2020, 12, 4, 16, 30, 0);
            Console.WriteLine("Izabrali ste opciju dodavanja novog eventa");
            var eventName = "";
            var addDefined = true;
            while (addDefined)
            {
                Console.WriteLine("Unesite ime eventa koji želite dodati");
                eventName = Console.ReadLine();
                foreach (var item in dict)
                {
                    if (item.Key.Name == eventName)
                    {
                        addDefined = false;
                    }
                }
                if (!addDefined)
                {
                    Console.WriteLine("Već postoji event pod tim imenom. Molim ponoviti unos!");
                    addDefined = true;
                }
                else
                {
                    addDefined = false;
                }
            }
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
                        }                                                                                                       //moglo se sa logičkim operatorom ILI pa jedan IF, ali mi je ovako bilo preglednije
                        if (endTime.Ticks >= item.Key.EndTime.Ticks && startTime.Ticks <= item.Key.StartTime.Ticks)
                        {
                            //endTime i startTime su izvan intervala starta i enda drugog eventa, pa ih ne smijemo dodati jer bi se u jednom periodu održavala oba eventa istovremeno
                            Console.WriteLine("Ne možete dodati kraj eventa za vrijeme drugog eventa. Ponoviti unos!");
                            endDefined = false;
                        }
                    }
                    if (endTime.Ticks <= startTime.Ticks)
                    {
                        endDefined = false;
                        Console.WriteLine("Kraj eventa mora biti nakon početka eventa. Ponoviti unos!");
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
            var newEvent = new Event(eventName, newEventType, startTime, endTime);
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

        public static void EditEvent(Dictionary<Event, List<Person>> dict)
        {
            Console.WriteLine("Izabrali ste edit eventa");
            var whatEventEdit = "";
            var editDefined = true;
            while (editDefined)
            {
                Console.WriteLine("Unesite ime eventa koji želite urediti");
                whatEventEdit = Console.ReadLine();
                foreach (var item in dict)
                {
                    if (item.Key.Name == whatEventEdit)
                    {
                        editDefined = false;
                    }
                }
                if (editDefined)
                {
                    Console.WriteLine("Ne postoji event pod tim imenom. Molim ponoviti unos!");
                }
            }
            var eventEditName = whatEventEdit;
            var runEdit = true;
            var editMenuDefined = true;
            while (runEdit)
            {
                if (editMenuDefined)
                {
                    EditMenu(eventEditName);
                    editMenuDefined = true;
                }
                var editPick = Console.ReadLine();
                switch (editPick)
                {
                    case "1":
                        Console.WriteLine("Izabrali ste urediti ime eventa");
                        var editedName = "";
                        var editNameDefined = true;
                        while (editNameDefined)
                        {
                            Console.WriteLine("Unesite novo, uređeno ime eventa");
                            editedName = Console.ReadLine();
                            foreach (var item in dict)
                            {
                                if (item.Key.Name == editedName)
                                {
                                    editNameDefined = false;
                                }
                            }
                            if (!editNameDefined)
                            {
                                Console.WriteLine("Već postoji event pod tim imenom. Molim ponoviti unos!");
                                editNameDefined = true;
                            }
                            else
                            {
                                editNameDefined = false;
                            }
                        }
                        var listOfKeys2 = dict.Keys.ToList();
                        var listOfValues2 = dict.Values.ToList();
                        dict.Clear();
                        for (int i = 0; i < listOfKeys2.Count; i++)
                        {
                            if (listOfKeys2[i].Name == eventEditName)
                            {
                                listOfKeys2[i].Name = editedName;
                            }
                        }
                        for (int i = 0; i < listOfKeys2.Count; i++)
                        {
                            dict.Add(listOfKeys2[i], listOfValues2[i]);
                        }
                        Console.WriteLine("Uredili ste ime eventa");
                        eventEditName = editedName;
                        editMenuDefined = true;
                        break;
                    case "2":
                        Console.WriteLine("Izabrali ste urediti tip eventa");
                        var eventEnum = true;
                        var editedEventType = -1;
                        while (eventEnum)
                        {
                            Console.WriteLine("Unesite novi tip eventa (Coffee, Lecture, Concert, StudySession)");
                            var editedEventTypeEnum = Console.ReadLine();
                            switch (editedEventTypeEnum.ToLower())
                            {
                                case "coffee":
                                    editedEventType = (int)TypeOfEvent.Coffee;
                                    eventEnum = false;
                                    break;
                                case "lecture":
                                    editedEventType = (int)TypeOfEvent.Lecture;
                                    eventEnum = false;
                                    break;
                                case "concert":
                                    editedEventType = (int)TypeOfEvent.Concert;
                                    eventEnum = false;
                                    break;
                                case "studysession":
                                    editedEventType = (int)TypeOfEvent.StudySession;
                                    eventEnum = false;
                                    break;
                                default:
                                    Console.WriteLine("Pogrešan unos");
                                    break;
                            }
                        }
                        var listOfKeys = dict.Keys.ToList();
                        var listOfValues = dict.Values.ToList();
                        dict.Clear();
                        for (int i = 0; i < listOfKeys.Count; i++)
                        {
                            if (listOfKeys[i].Name == eventEditName)
                            {
                                listOfKeys[i].EventType = editedEventType;
                                Console.WriteLine(listOfKeys[i].EventType);
                            }
                        }
                        for (int i = 0; i < listOfKeys.Count; i++)
                        {
                            dict.Add(listOfKeys[i], listOfValues[i]);
                        }
                        Console.WriteLine("Uredili ste tip eventa");
                        editMenuDefined = true;
                        break;
                    case "3":
                        var startTime = new DateTime(2020, 12, 4, 14, 0, 0);
                        var endTime = new DateTime(2020, 12, 4, 16, 30, 0);
                        Console.WriteLine("Izabrali ste urediti trajanje, odnosno početak i kraj eventa");
                        var listOfKeysEdited = dict.Keys.ToList();
                        while (true)
                        {
                            var startDefined = true;
                            Console.WriteLine("Unesi start time eventa u formatu DD/MM/YYYY HH:mm:ss");
                            try
                            {
                                startTime = DateTime.Parse(Console.ReadLine());
                                foreach (var item in dict)
                                {
                                    if (item.Key.Name == eventEditName)
                                    {
                                        listOfKeysEdited.Remove(item.Key);
                                    }
                                }
                                foreach (var item in listOfKeysEdited)
                                {
                                    if (startTime.Ticks >= item.StartTime.Ticks && startTime.Ticks <= item.EndTime.Ticks)
                                    {
                                        Console.WriteLine("Ne možete dodati početak eventa za vrijeme drugog eventa");
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
                                foreach (var item in listOfKeysEdited)
                                {
                                    if (endTime.Ticks >= item.StartTime.Ticks && endTime.Ticks <= item.EndTime.Ticks)
                                    {
                                        Console.WriteLine("Ne možete dodati kraj eventa za vrijeme drugog eventa. Ponoviti unos!");
                                        endDefined = false;
                                    }                                                                                                       
                                    if (endTime.Ticks >= item.EndTime.Ticks && startTime.Ticks <= item.StartTime.Ticks)
                                    {
                                        Console.WriteLine("Ne možete dodati kraj eventa za vrijeme drugog eventa. Ponoviti unos!");
                                        endDefined = false;
                                    }
                                }
                                if (endTime.Ticks <= startTime.Ticks)
                                {
                                    endDefined = false;
                                    Console.WriteLine("Kraj eventa mora biti nakon početka eventa. Ponoviti unos!");
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
                        var listOfKeys3 = dict.Keys.ToList();
                        var listOfValues3 = dict.Values.ToList();
                        dict.Clear();
                        for (int i = 0; i < listOfKeys3.Count; i++)
                        {
                            if (listOfKeys3[i].Name == eventEditName)
                            {
                                listOfKeys3[i].StartTime = startTime;
                                listOfKeys3[i].EndTime = endTime;
                            }
                        }
                        for (int i = 0; i < listOfKeys3.Count; i++)
                        {
                            dict.Add(listOfKeys3[i], listOfValues3[i]);
                        }
                        Console.WriteLine("Uredili ste početak i kraj eventa");
                        editMenuDefined = true;
                        break;
                    case "4":
                        Console.WriteLine("Izabrali ste izlazak iz edit event podmenija");
                        if (PermissionToContinue())
                        {
                            runEdit = false;
                        }
                        editMenuDefined = true;
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos");
                        Console.WriteLine("Za ponovni pokušaj unosa unijeti 0, za izbornik bilo što");
                        var optionEditMenu = Console.ReadLine();
                        if (optionEditMenu == "0")
                        {
                            editMenuDefined = false;
                        }
                        break;
                }
            }
        }
    }
}
