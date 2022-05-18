using RecruitmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentApp.Helpers.Interfaces
{
    public interface IFileHelper
    {
        public bool IsFilePresent(string filePath);
        public IList<T> ReadFromSaveFile<T>();
        public byte[] GetArrayOfBytesFromBinaryReader();
        public  IList<T> DeserializeListFromBytes<T>(byte[] bytes);
        public IList<Coordinates> ReadBinaryFile(IList<Coordinates> coordinates);
        public string ReadJsonFile();
    }
}
