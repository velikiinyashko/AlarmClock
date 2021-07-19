using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClock.Core.Models
{
    public class AlarmModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string AlarmSounds { get; set; }
        public bool IsEnable { get; set; }
    }
}
