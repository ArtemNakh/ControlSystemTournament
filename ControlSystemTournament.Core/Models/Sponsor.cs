using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Models
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Contribution { get; set; }

        public virtual Tournament Tournament { get; set; }
    }

}
