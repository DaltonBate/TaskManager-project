
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public interface IEventRepository                                  //Method Signatures
    {
        public IEnumerable<Event> GetAllEvents();
        public Event GetEvent(int id);
        public void InsertEvent(Event eventToInsert);
        public void DeleteEvent(Event eventToDelete);
        public IEnumerable<Event> GetEventsByDate(string date);
        
    }
}
