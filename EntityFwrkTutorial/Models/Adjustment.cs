using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFwrkTutorial.Models
{
    public partial class Adjustment
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal GpaDelta { get; set; }
        public bool Completed { get; set; }
    }
}
