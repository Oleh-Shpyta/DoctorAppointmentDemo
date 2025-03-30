using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorAppointmentDemo.Data.Interfaces;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Repositories
{
    public class PatientsRepository : GenericRepository<Patient>, IPatientsRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public PatientRepository(string appSetings, ISerializationService serializationService) : base(appSetings, serializationService)
        {
            this.serializationService = serializationService;
            var settings = AppSettings.Load();

            if (settings?.Database?.Doctors == null)
            {
                throw new Exception("Помилка: Невірна структура конфігурації.");
            }

            Path = settings.Database.Doctors.Path;
            LastId = settings.Database.Doctors.LastId;
        }

        public override void ShowInfo(Doctor patient)
        {
            Console.WriteLine("Інформація про лікаря:");
            Console.WriteLine($"ID: {patient.Id}");
            Console.WriteLine($"Ім'я: {patient.Name}");
            Console.WriteLine($"Email: {patient.Email}");
            Console.WriteLine($"Телефон: {patient.Phone}");
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;
            serializationService.Serialize(AppSettings, result);
        }
    }
}
