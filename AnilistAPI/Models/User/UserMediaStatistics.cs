using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAnilist.Models.User
{
    public class UserMediaStatistics
    {
        public int count { get; set; }
        public int? minutesWatched { get; set; }
        public int? episodesWatched { get; set; }
        public int? volumesRead { get; set; }
        public int? chaptersRead { get; set; }
        public float meanScore { get; set; }
        public List<UserMediaStatusDetails>? statuses { get; set; }
    }
}
