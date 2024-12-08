using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Models.Media
{
    public class AiringSchedule
    {
        public List<AiringScheduleNode> nodes { get; set; }
    }
    public class AiringScheduleNode
    {
        public int episode { get; set; }
        public int timeUntilAiring { get; set; }
    }
}
