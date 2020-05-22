using System;
using System.Collections.Generic;
using System.Text;

namespace RCCS.Database.ViewModel
{
    public class ProgressReportViewModel
    {
        //Citizen
        public string Name { get; set; }
        public long CPR { get; set; }

        //RespiteCareHome
        public string RespiteCareHomeName { get; set; }

        //ResidenceInformation
        public DateTime PlannedDischargeDate { get; set; }
    }
}
