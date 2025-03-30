using System;
using System.IO;
using System.Xml.Serialization;
using DoctorAppointmentDemo.Data.Interfaces;

namespace DoctorAppointmentDemo.Service.Services
{
    public class XmlDataSerializerService : ISerializationService
    {
        public T Deserialize<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Файл {path} не знайдено.");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
        public void Serialize<T>(string path, T data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }
        }
    }
}
