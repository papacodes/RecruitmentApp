using RecruitmentApp.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using RecruitmentApp.Constants;
using RecruitmentApp.Models;

namespace RecruitmentApp.Helpers.Implementations
{
    public sealed class FileHelper : IFileHelper
    {
        public static BinaryWriter binWriteSave;
        public static BinaryReader binReadSave;


        public IList<T> DeserializeListFromBytes<T>(byte[] bytes)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                try
                {
                    return (List<T>)binFormatter.Deserialize(memStream);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return (List<T>)binFormatter.Deserialize(memStream);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message.ToString());
                    return (List<T>)binFormatter.Deserialize(memStream);
                }

            }
        }

        public byte[] GetArrayOfBytesFromBinaryReader()
        {
            const int intBufferSize = 4096;

            using (MemoryStream memSteamTemp = new MemoryStream())
            {
                byte[] bytBuffer = new byte[intBufferSize];
                int intCount;

                while ((intCount = binReadSave.Read(bytBuffer, 0, bytBuffer.Length)) != (0))
                {
                    memSteamTemp.Write(bytBuffer, 0, intCount);
                }

                return memSteamTemp.ToArray();
            }
        }

        public bool IsFilePresent(string filePath)
        {
            string strSaveFileTypeName = (filePath);

            if (File.Exists(strSaveFileTypeName))
            {
                return true;
            }

            return false;
        }

        public IList<Coordinates> ReadBinaryFile(IList<Coordinates> coordinates)
        {
            using (BinaryReader b = new BinaryReader(
            File.Open(StringLiterals.BinaryFilePath, FileMode.Open)))
            {
                while (b.BaseStream.Position != b.BaseStream.Length)
                {
                    try
                    {
                        Coordinates tempCoordinates = new Coordinates();

                        tempCoordinates.PositionId = b.ReadInt32();
                        tempCoordinates.VehicleRegistration = b.ReadByte().ToString();
                        tempCoordinates.Latitude = BitConverter.ToSingle(b.ReadBytes(4));
                        tempCoordinates.Longitude = BitConverter.ToSingle(b.ReadBytes(4));
                        tempCoordinates.RecordedTimeUTC = b.ReadInt64();

                        coordinates.Add(tempCoordinates);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"an error occured: {ex.Message}");
                    }

                }

                return coordinates;
            }
        }


        public IList<T> ReadFromSaveFile<T>()
        {
            IList<T> lisListToBeRead = new List<T>();
            string strSaveFileTypeName = (StringLiterals.BinaryFilePath);

            Type mytype = typeof(T);

            try
            {
                binReadSave = new BinaryReader(new FileStream(strSaveFileTypeName, FileMode.Open));
            }
            catch (IOException e)
            {
                Console.Write(e.Message.ToString());
            }

            try
            {
                lisListToBeRead = DeserializeListFromBytes<T>(GetArrayOfBytesFromBinaryReader());
            }
            catch (IOException e)
            {
                Console.Write(e.Message.ToString());
            }

            binReadSave.Close();

            return lisListToBeRead;
        }

        public string ReadJsonFile()
        {
            return new StreamReader(StringLiterals.JsonFilePath).ReadToEnd();
        }
    }
}
