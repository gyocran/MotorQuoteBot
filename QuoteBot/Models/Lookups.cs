using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteBot.Models
{
    public class Lookups
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string PopSource { get; set; }
        public string ShortDescription { get; set; }
        public int PopSeq { get; set; }
        public int PopCode { get; set; }
        public string Code { get; set; }
        public string PopMinValue { get; set; }
        public string PopMaxValue { get; set; }
        public string HexCode { get; set; }
    }
}
