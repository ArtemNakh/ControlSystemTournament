using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } // Назва місця 
        public string Address { get; set; } // Адреса, якщо це фізичне місце
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsVirtual { get; set; } // Чи є місце віртуальним (онлайн)
    }

}
