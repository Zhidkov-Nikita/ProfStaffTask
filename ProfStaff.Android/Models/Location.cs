using System.Collections.Generic;

namespace ProfStaff.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int? Parent_ID { get; set; }
        public string Name { get; set; }
        public List<Location> Children { get; set; } = new List<Location>();
        public bool IsExpanded { get; set; } = true;
        public int Level { get; set; }
    }
}