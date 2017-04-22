using System;

namespace DenunciasMunicipalesApp.Models
{
    class Complaint
    {
        public int ComplaintId { get; set; }

        public string Description { get; set; }

        public string CaseAddress { get; set; }

        public DateTime Date { get; set; }

        public string CreatedBy { get; set; }
    }
}
