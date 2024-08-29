using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Models
{
    public class EventRepository : IEventRepository
    {
        private readonly IDbConnection _conn;

        public EventRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _conn.Query<Event>("SELECT * FROM calendar;");
        }

        public Event GetEvent(int id)
        {
            return _conn.QuerySingleOrDefault<Event>("SELECT * FROM calendar WHERE Id = @id", new { id });
        }

        public void InsertEvent(Event eventToInsert)
        {
            _conn.Execute("INSERT INTO calendar (Name, Date, Description) VALUES (@name, @date, @description)",  /*Error here*/
                new { description = eventToInsert.Description, name = eventToInsert.Name, date = eventToInsert.Date });
        }

        public IEnumerable<Event> GetEventsByDate(string date)
        {
            var events = _conn.Query<Event>("Select * from calendar where Date = @eventDate", new { eventDate = date });
            return events;
        }

        public void DeleteEvent(Event calendar)
        {
            _conn.Execute("DELETE FROM calendar WHERE Id = @id",
                new { id = calendar.Id });
        }
    }
}
