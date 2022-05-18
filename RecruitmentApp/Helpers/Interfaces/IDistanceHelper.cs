using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Helpers.Interfaces
{
    public interface IDistanceHelper
    {
        public  DistanceResult CalculateDistance(Coordinates coordinates, TargetCoordinates targetCoordinates);
        public DistanceResult DetermineClosestVehicle(IList<DistanceResult> distanceResult);
    }
}
