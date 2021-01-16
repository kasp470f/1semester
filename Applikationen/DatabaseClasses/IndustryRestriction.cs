using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applikationen.DatabaseClasses
{    
    public class IndustryRestriction
    {
        public int RI_ID { get; set; }
        public int RI_R_ID { get; set; }
        public int RI_M_ID { get; set; }
        public int RI_I_ID { get; set; }
        public string RI_Text { get; set; }
        public DateTime RI_StartDate { get; set; }
        public DateTime RI_EndDate { get; set; }

        // Industry variables
        public string I_Name { get; set; }
        public string I_Code { get; set; }
        public string I_Description { get; set; }

        // Restriction variables
        public string R_Text { get; set; }
    }
}

