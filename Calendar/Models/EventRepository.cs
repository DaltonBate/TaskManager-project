using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbConnection _conn; //store database connection

        // Constructor accepting an IDbConnection and assigning it to the private field
        public EventRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        // retieve all events from database
        public IEnumerable<Event> GetAllEvents()
        {
            //maps all rows of calendar & maps to Event objects
            return _conn.Query<Event>("SELECT * FROM calendar;");
        }
        // retrieve one event by its ID
        public Event GetEvent(int id)
        {
            return _conn.QuerySingleOrDefault<Event>("SELECT * FROM calendar WHERE Id = @id", new { id });
        }
        //inserts new event to database
        public void InsertEvent(Event eventToInsert)
        {
            // Executes an insert command with parameters for the event's name, date, and description
            _conn.Execute("INSERT INTO calendar (Name, Date, Description) VALUES (@name, @date, @description)",  
                new { description = eventToInsert.Description, name = eventToInsert.Name, date = eventToInsert.Date });
        }
        // retrive events that occur on a specific date
        public IEnumerable<Event> GetEventsByDate(string date)
        {
            var events = _conn.Query<Event>("Select * from calendar where Date = @eventDate", new { eventDate = date });
            return events;
        }
        //deletes event based on ID
        public void DeleteEvent(Event calendar)
        {
            _conn.Execute("DELETE FROM calendar WHERE Id = @id",
                new { id = calendar.Id });
        }
    }
}
