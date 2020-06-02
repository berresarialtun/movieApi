using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Actor
    {
        public Actor()
        {
            Casts = new HashSet<Cast>();
        }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
    }
}
