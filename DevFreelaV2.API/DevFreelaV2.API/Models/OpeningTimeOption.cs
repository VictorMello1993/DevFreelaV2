using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreelaV2.API.Models
{
    public class OpeningTimeOption
    {
        public TimeSpan StartAt { get; set; }
        public TimeSpan FinishAt { get; set; }
    }
}
