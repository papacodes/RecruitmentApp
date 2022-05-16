using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public class Coordinates
    {

        public Int32 PositionId { get; set; }
        public string? VehicleRegistration { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Int64? RecordedTimeUTC { get; set; }


    }
}
