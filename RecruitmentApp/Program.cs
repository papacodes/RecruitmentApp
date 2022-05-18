using System;
using RecruitmentApp.Models;
using RecruitmentApp.Helpers.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentApp.Helpers.Implementations;
using RecruitmentApp.Constants;
using System.Linq;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static void Main (string[] args)
        {
            Console.WriteLine("starting project ... ");

            var host = CreateHostBuilder(args).Build();

            Console.WriteLine("checking if files exist ... ");

            var BinaryfileExists = host.Services.GetService<IFileHelper>().IsFilePresent(StringLiterals.BinaryFilePath);

            var JsonfileExists = host.Services.GetService<IFileHelper>().IsFilePresent(StringLiterals.JsonFilePath);

            if (BinaryfileExists && JsonfileExists)
            {
                Console.WriteLine("files found, running services ...");

                IList<Coordinates> coordinates = new List<Coordinates>();

                coordinates = host.Services.GetService<IFileHelper>().ReadBinaryFile(coordinates);

                var targets = JsonConvert.DeserializeObject<List<TargetCoordinates>>(host.Services.GetService<IFileHelper>().ReadJsonFile());

                CalculateDistance(coordinates, targets, host);
            }
            else
            {
                Console.WriteLine("Binary file or Json file doesn't exist, please check the string literals to ensure the file paths are correct.");
            }                
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            Console.WriteLine("registering services ... ");

            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFileHelper, FileHelper>();
                    services.AddSingleton<IDistanceHelper, DistanceHelper>();
                });

            return hostBuilder;
        }

        private static void CalculateDistance(IList<Coordinates> coordinates, IList<TargetCoordinates> targets, IHost host)
        {
            Console.WriteLine("calculating distance ... ");

            IList<DistanceResult> distanceResult = new List<DistanceResult>();

            foreach (TargetCoordinates target in targets)
            {
                Console.WriteLine($"calculating distance for position: {target.Position} ... ");

                foreach (Coordinates coordinate in coordinates)
                {
                    var response = host.Services.GetService<IDistanceHelper>().CalculateDistance(coordinate, target);

                    distanceResult.Add(response);
                }

                Console.WriteLine($"Distance calculated, determining closest distance for position: {target.Position} ... ");

                var closestPoint = host.Services.GetService<IDistanceHelper>().DetermineClosestVehicle(distanceResult);

                PrintResult(closestPoint);

                distanceResult.Clear();
            }
        }

        private static void PrintResult(DistanceResult closestPoint)
        {
            Console.WriteLine($"============================================ |{closestPoint.Position}| =========================================================");
            Console.WriteLine($"Closest vehicle to point position: {closestPoint.Position} is : Vehicle ID: {closestPoint.VehicleId}");
            Console.WriteLine($"Target Coordinates -> Latitude : {closestPoint.Latitude}, Longitude {closestPoint.Longitude}");
            Console.WriteLine($"Vehicle Coordinates -> Latitude : {closestPoint.VehicleLatitude}, Longitude : {closestPoint.VehicleLongitude}");

            Console.WriteLine($"============================================ |end| =========================================================");

            Console.WriteLine("///////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            Console.WriteLine("");
        }

    }
}