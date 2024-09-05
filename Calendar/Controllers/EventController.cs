
using Calendar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Calendar.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository repo;

        public EventController(IEventRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string date)
        {
            ViewBag.Date = date;
            var events = repo.GetEventsByDate(date);
            return View(events);
        }

        public IActionResult ViewEvent(string eventDate)
        {
            ViewBag.Date = eventDate;
            var events = repo.GetEventsByDate(eventDate);

            return View(events);
        }
        public IActionResult GetAllEvents()
        {
            var daysWithEvents = JsonSerializer.Serialize(repo.GetAllEvents());
            return Ok(daysWithEvents);
        }

        [HttpPost]
        public IActionResult CreateEvent(Event eventToCreate)
        {
            repo.InsertEvent(eventToCreate);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                var eventToDelete = repo.GetEvent(id);
                if (eventToDelete != null)
                {
                    repo.DeleteEvent(eventToDelete);
                }
                else
                {
                    TempData["ErrorMessage"] = "Event not found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the event.";
            }

            return RedirectToAction("Index", "Home"); // Redirect to the static page
        }

        // New action method for the static page
        public IActionResult StaticPage()
        {
            return View(); // Returns the StaticPage view
        }
    }
}
