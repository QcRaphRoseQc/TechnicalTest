using System.Collections.ObjectModel;
using TechnicalTest.API.DTO;

namespace TechnicalTest.API.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? DeclarationDateTime { get; set; }
        public User DeclaredBy { get; set; }
        public Guid DeclaredById { get; set; }
        public List<Document> Documents { get; set; }


        public Event()
        {
            Documents = new List<Document>();
        }
    }
}