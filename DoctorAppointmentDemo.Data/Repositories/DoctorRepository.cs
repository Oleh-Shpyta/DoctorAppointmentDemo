using DoctorAppointmentDemo.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public DoctorRepository(string appSetings, ISerializationService serializationService): base(appSetings, serializationService)
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

        public override void ShowInfo(Doctor doctor)
        {
            Console.WriteLine("Інформація про лікаря:");
            Console.WriteLine($"ID: {doctor.Id}");
            Console.WriteLine($"Ім'я: {doctor.Name}");
            Console.WriteLine($"Email: {doctor.Email}");
            Console.WriteLine($"Телефон: {doctor.Phone}");
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;
            serializationService.Serialize(AppSettings, result);

           // File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }
    }
}
