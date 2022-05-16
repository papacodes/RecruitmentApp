using System;
using RecruitmentApp.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public List<Coordinates>? Coordinates { get; set; }

        static void Main(string[] args)
        {
            List<Coordinates> coordinates = new List<Coordinates>();

            ReadBinaryFile(coordinates); 
        }

        static void ReadBinaryFile(List<Coordinates> coordinates)
        {

            using (BinaryReader b = new BinaryReader(
            File.Open("../../../Resources/VehiclePositions.dat", FileMode.Open)))
            {
                while (b.BaseStream.Position != b.BaseStream.Length)
                {
                    try
                    {
                        Coordinates tag = new Coordinates();

                        tag.PositionId = b.ReadInt32();
                        tag.VehicleRegistration = b.ReadByte().ToString();

                        tag.Latitude = BitConverter.ToSingle(b.ReadBytes(4));

                        tag.Longitude = BitConverter.ToSingle(b.ReadBytes(4));
                        tag.RecordedTimeUTC = b.ReadInt64();

                        coordinates.Add(tag);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"an error occured: {ex.Message}");
                    }


                } 
                
            }
        }
    }
}