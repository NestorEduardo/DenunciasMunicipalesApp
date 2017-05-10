using System;

namespace DenunciasMunicipalesApp.Models
{
    public class Complaint
    {
        public int ComplaintId { get; set; }

        public string Description { get; set; }

        public string CaseAddress { get; set; }

        public DateTime Date { get; set; }

        public string CreatedBy { get; set; }

        public string Image { get; set; }

        public byte[] ImageArray { get; set; }

        public int ComplaintTypeId { get; set; }

        public string ComplaintTypeName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
