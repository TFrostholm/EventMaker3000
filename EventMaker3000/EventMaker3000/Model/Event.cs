using System;

namespace EventMaker3000.Model
{
    public class Event
    {
        // Properties:

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }


        //Constructor:
        public Event(int id, string name, string description, string place, DateTime dateTime)
        {
            Id = id;
            Name = name;
            Description = description;
            Place = place;
            DateTime = dateTime;
        }
    }
}