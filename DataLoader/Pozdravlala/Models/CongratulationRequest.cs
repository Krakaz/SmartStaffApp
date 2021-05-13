using System;
using System.Collections.Generic;
using System.Text;

namespace DataLoader.Pozdravlala.Models
{
    public class CongratulationRequest
    {
        public CongratulationTypeEnum Type { get; set; }
        public SexEnum Sex { get; set; }
        public AppealEnum Appeal { get; set; }
        public ContgatulationLengthEnum Length { get; set; }
    }
}
