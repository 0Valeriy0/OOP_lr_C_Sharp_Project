using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3.Class
{
    public class MedicalCard
    {
        public string BloodGroup { get; set; }
        public bool HasAccessToCompete { get; set; }
        public DateTime InspectationDate { get; set; }

        public MedicalCard() {
            BloodGroup = "Невідома";
            HasAccessToCompete = false;
            InspectationDate = DateTime.Today;
        }

        public MedicalCard(string _bloodGroup, bool _hadAccessToCompete, DateTime inspectationDAte) {
            BloodGroup = _bloodGroup;
            HasAccessToCompete = _hadAccessToCompete;
            InspectationDate = inspectationDAte;
        }



    }
}
