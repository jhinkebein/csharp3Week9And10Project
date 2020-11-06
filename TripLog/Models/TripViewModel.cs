using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripLog.Models
{
    public class TripViewModel
    {
        public Trip Trip { get; set; }
        public int PageNumber { get; set; }
    }
}
