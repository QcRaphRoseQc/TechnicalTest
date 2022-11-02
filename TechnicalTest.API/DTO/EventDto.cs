using System.Collections.ObjectModel;
using TechnicalTest.API.Models;

namespace TechnicalTest.API.DTO
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public User DeclaredBy { get; set; }




    }
}