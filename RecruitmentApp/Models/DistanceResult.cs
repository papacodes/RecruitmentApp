using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Models
{
    public  class DistanceResult
    {
        public int Position { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Int32 VehicleId { get; set; }
        public string? VehicleRegistration { get; set; }
        public float VehicleLatitude { get; set; }
        public float VehicleLongitude { get; set; }


        public double Distance { get; set; }
    }
}
