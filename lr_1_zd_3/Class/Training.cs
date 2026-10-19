using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3.Class
{
    internal class Training
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; } 
        public string Sport { get; set; }
        public Coach Coach { get; set; }
        public Sportsman Sportsman { get; set; }
    }
}
