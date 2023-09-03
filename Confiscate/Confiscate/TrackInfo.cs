using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confiscate
{
    internal class TrackInfo
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public string Id { get; set; }
    }
}
