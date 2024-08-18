using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlSystemTournament.Core.Models
{
    //public enum PlayerRole
    //{
    //    Сoach,/       /тренер
    //    Captain,   // Капітан команди
    //    Shooter,   // Стрілець
    //    Support,   // Підтримка
    //    Tank,      // Танк
    //    Healer,    // Лікар
    //    Strategist // Стратег
    //}

    public class PlayerRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
