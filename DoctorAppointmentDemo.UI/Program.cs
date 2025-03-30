using System.Reflection.Metadata;
using System.Xml.Serialization;
using DoctorAppointmentDemo.Service.Services;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
        }
        public void Menu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Show doctors");
                Console.WriteLine("2. Add doctor");
                Console.WriteLine("3. Exit");

                if (!Enum.TryParse(Console.ReadLine(), out MenuOption option))
                {
                    Console.WriteLine("Invalid option. Try again.");
                    continue;
                }

                switch (option)
                {
                    case MenuOption.ShowDoctors:
                        Console.WriteLine("Current doctors list:");
                        var docs = _doctorService.GetAll();
                        foreach (var doc in docs)
                        {
                            Console.WriteLine(doc.Name);
                        }
                        break;

                    case MenuOption.AddDoctor:
                        Console.WriteLine("Adding doctor:");
                        var newDoctor = new Doctor
                        {
                            Name = "Vasya",
                            Surname = "Petrov",
                            Experience = 20,
                            DoctorType = Domain.Enums.DoctorTypes.Dentist
                        };
                        _doctorService.Create(newDoctor);
                        Console.WriteLine("Doctor added successfully.");
                        break;

                    case MenuOption.Exit:
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Unknown option. Try again.");
                        break;
                }
            }
        }
        public enum MenuOption
        {
            ShowDoctors = 1,
            AddDoctor = 2,
            Exit = 3
        }
        public static class Program
        {
            public static void Main()
            {
                Console.WriteLine("Select data format");
                Console.WriteLine("1. XML");
                Console.WriteLine("2. JSON");
                string? choice = Console.ReadLine();
                DoctorAppointment? doctorAppointment = null;
                while (true)
                {
                    if (choice.Equals("1"))
                    {
                        doctorAppointment = new DoctorAppointment(Constants.XmlAppSettingsPath, new XmlDataSerializerService());
                        break;
                    }
                    else if (choice.Equals("2"))
                    {
                        doctorAppointment = new DoctorAppointment(Constant.JsonAppSettingsPath, new JsonDataSerializerService());
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong choice.");
                        choice = Console.ReadLine();
                    }
                }
            }
        }
    }
}