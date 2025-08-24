using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Model
{
    public class Session
    {
        public Guid SessionId { get; set; }
        public DateTime CreationTime { get; set; }   // e.g., session created on March 18 2025
        public DateTime Duration { get; set; }       // in HH:MM:SS
        public string? Description { get; set; }     // e.g. "Studying maths"
        
    }
}
