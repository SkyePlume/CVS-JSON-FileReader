using FileHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medica.Uk.TechnicalDemonstration.Models
{
    [DelimitedRecord(",")]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CustomerRecord
    {
        [JsonProperty("CustomerID")]
        public int CustomerID { get; set; }

        [JsonProperty("CustomerName")]
        public string? CustomerName { get; set; }

        [JsonProperty("AccountBalance")]
        public decimal AccountBalance { get; set; }

        [JsonProperty("DateCreated")]
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime DateCreated { get; set; }   
    }
}
