using RecruitmentApp.Helpers.Interfaces;
using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Helpers.Implementations
{
    public class DistanceHelper : IDistanceHelper
    {
        public  DistanceResult CalculateDistance(Coordinates coordinates, TargetCoordinates targetCoordinates)
        {
            if (!coordinates.Longitude.Equals(null) || !coordinates.Latitude.Equals(null))
            {
                var d1 = coordinates.Latitude * (Math.PI / 180.0);
                var num1 = coordinates.Longitude * (Math.PI / 180.0);
                var d2 = targetCoordinates.Latitude * (Math.PI / 180.0);
                var num2 = targetCoordinates.Longitude * (Math.PI / 180.0) - num1;
                var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

                var distance = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));



                return new DistanceResult
                {
                    Distance = distance,

                    VehicleId = coordinates.PositionId,
                    VehicleRegistration = coordinates.VehicleRegistration,
                    VehicleLatitude = coordinates.Latitude,
                    VehicleLongitude = coordinates.Longitude,

                    Position = targetCoordinates.Position,
                    Longitude = targetCoordinates.Longitude,
                    Latitude = targetCoordinates.Latitude,
                };
            }
            else
            {
                return null;
            }
            

        }

        public DistanceResult DetermineClosestVehicle(IList<DistanceResult> distanceResult)
        {
            return distanceResult.OrderBy(x => x.Distance).FirstOrDefault();
        }
    }
}
