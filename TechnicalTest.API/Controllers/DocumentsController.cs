using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.API.Data;
using TechnicalTest.API.DTO;
using TechnicalTest.API.Models;

namespace TechnicalTest.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly EventDbContext _eventDbContext;

        public DocumentsController(EventDbContext eventDbContext)
        {
            _eventDbContext = eventDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments()
        {
            var documents = await _eventDbContext.Documents.ToListAsync();
            return Ok(documents);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DocumentDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DocumentDto>> CreateDocument([FromBody] DocumentDto documentDto)
        {
            var eventExists = await _eventDbContext.Events.AnyAsync(e => e.Id == documentDto.EventId);
            if (!eventExists)
            {
                return BadRequest("Event does not exist");
            }

            var document = new Models.Document()
            {
                S3Key = documentDto.S3Key,
                Description = documentDto.Description,
                EventId = documentDto.EventId
            };
            await _eventDbContext.Documents.AddAsync(document);
            await _eventDbContext.SaveChangesAsync();
            return Ok(document);
        }
    }


}