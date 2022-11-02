using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.API.Data;
using TechnicalTest.API.DTO;
using TechnicalTest.API.Models;
using Document = TechnicalTest.API.Models.Document;

namespace TechnicalTest.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventsController : ControllerBase
    {
        public readonly EventDbContext _eventDbContext;

        public EventsController(EventDbContext eventDbContext)
        {
            _eventDbContext = eventDbContext;
        }

         [HttpGet]
         [ProducesResponseType(typeof(List<EventDto>), (int)HttpStatusCode.OK)]
         public async Task<ActionResult> GetEvents()
         {

             if (_eventDbContext.Events.Count() == 0)
             {
                 return NotFound("No events found");
             }

             var dateNow = DateTime.Now;
             dateNow = DateTime.SpecifyKind(dateNow, DateTimeKind.Utc);
             var events = await _eventDbContext.Events.ToListAsync();
             var documents = await _eventDbContext.Documents.ToListAsync();
             var eventDtos = events.Select(e => new Event
             {

                 Id = e.Id,
                 Description = e.Description,
                 DeclarationDateTime = dateNow,
                 DeclaredBy = e.DeclaredBy,
                 Documents = e.Documents.Select(d => new Document
                    {
                        S3Key = d.S3Key,
                        Description = d.Description,
                        EventId = d.EventId
                    }).ToList()
             }).ToList();
             return Ok(eventDtos);
         }

        [Route("/api/v1/[controller]/filter")]
        [HttpGet]
        [ProducesResponseType(typeof(List<EventDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<EventDto>>> GetEvents([FromQuery] EventFilterDto eventFilterDto)
        {
            var events = await _eventDbContext.Events.ToListAsync();
            var documents = await _eventDbContext.Documents.ToListAsync();

            events = eventFilterDto.HasDocument switch
            {
                true => events.Where(x => x.Documents.Count > 0).ToList(),
                false => events.Where(x => x.Documents.Count == 0).ToList(),
                null => events
            };

            if (eventFilterDto.Date != null)
            {
                events = events.Where(x => x.DeclarationDateTime == eventFilterDto.Date).ToList();
            }

            if (eventFilterDto.Order != null)
            {

                if(eventFilterDto.Order != "asc" && eventFilterDto.Order != "desc")
                {
                    return BadRequest("Order string must be asc or desc");
                }

                events = eventFilterDto.Order switch
                {
                    "asc" => events.OrderBy(x => x.DeclarationDateTime).ToList(),
                    "desc" => events.OrderByDescending(x => x.DeclarationDateTime).ToList(),_ => events
                };
            }

            if (events.Count == 0)
            {
                return NotFound("No events found");
            }

            return Ok(events);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _eventDbContext.Events.FindAsync(id);
            var eventExists = await _eventDbContext.Events.AnyAsync(x => x.Id == id);

            if (!eventExists)
            {
                return NotFound("Event not found");
            }

            if (eventToDelete.Description == null)
            {
                _eventDbContext.Events.Remove(eventToDelete);
                await _eventDbContext.SaveChangesAsync();
                return Ok("Event deleted");
            }
            else
            {
                return(Conflict("Event has description, cannot be deleted"));
            }
        }
    }
}