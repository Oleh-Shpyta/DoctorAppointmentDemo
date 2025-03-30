using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentDemo.Data.Interfaces
{
   public interface ISerializationService
    {
        void Serialize<T>(string path, T data);
        T Deserialize<T>(string path);
    }
}
