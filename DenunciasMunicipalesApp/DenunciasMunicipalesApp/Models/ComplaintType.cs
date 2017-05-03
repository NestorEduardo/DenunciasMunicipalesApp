using SQLite;

namespace DenunciasMunicipalesApp.Models
{
    public class ComplaintType
    {
        [PrimaryKey]
        public int ComplaintTypeId { get; set; }

        public string Description { get; set; }

        public override int GetHashCode()
        {
            return ComplaintTypeId;
        }
    }
}
