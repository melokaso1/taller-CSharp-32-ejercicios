using System;
using ejercicio.services;

namespace ejercicio.utils;

public class Run_program
{
    public static HashSet<string>? db = new HashSet<string>();

    public static void initialize()
    {
        Initialize();
    }

    private static void Initialize()
    {
        db = new HashSet<string>
        {
            // Autos
            "Toyota",
            "Honda",
            "Nissan",
            "Mazda",
            "Mitsubishi",
            "Subaru",
            "Suzuki",
            "Hyundai",
            "Kia",
            "Chevrolet",
            "Ford",
            "Dodge",
            "Jeep",
            "Chrysler",
            "GMC",
            "Buick",
            "Cadillac",
            "Lincoln",
            "Volkswagen",
            "Audi",
            "BMW",
            "Mercedes-Benz",
            "Porsche",
            "Mini",
            "Volvo",
            "Saab",
            "Renault",
            "Peugeot",
            "Citroën",
            "Fiat",
            "Alfa Romeo",
            "Lancia",
            "Ferrari",
            "Lamborghini",
            "Maserati",
            "Seat",
            "Skoda",
            "Opel",
            "Vauxhall",
            "Tesla",
            "Infiniti",
            "Lexus",
            "Acura",
            "Genesis",
            "Jaguar",
            "Land Rover",
            "Bentley",
            "Rolls-Royce",
            "Aston Martin",
            // Camiones / buses
            "Volvo Trucks",
            "Scania",
            "MAN",
            "DAF",
            "Iveco",
            "Renault Trucks",
            "Mack Trucks",
            "Freightliner",
            "Peterbilt",
            "Kenworth",
            "Western Star",
            "manuel-epstein",
            "International Trucks",
            "Hino",
            "Isuzu",
            "Fuso",
            "Tata Motors",
            "Ashok Leyland",
            "Navistar",
            "Kamaz",
            // Motos
            "Yamaha",
            "Honda Motorcycles",
            "Kawasaki",
            "Suzuki Motorcycles",
            "Ducati",
            "Harley-Davidson",
            "Triumph",
            "KTM",
            "Aprilia",
            "BMW Motorrad",
            // Aviones (fabricantes)
            "Boeing",
            "Airbus",
            "Embraer",
            "Bombardier",
            "Cessna",
            "Dassault Aviation",
            "Gulfstream",
            "Lockheed Martin",
            "Northrop Grumman",
            "Sukhoi",
            "Pilatus",
            "De Havilland Canada",
            "COMAC",
            "Mitsubishi Aircraft Corporation",
            "Cirrus Aircraft",
            "Piper Aircraft",
            "Textron Aviation",
            // Helicópteros
            "Bell Helicopter",
            "Sikorsky",
            "Airbus Helicopters",
            "Robinson Helicopter",
            "MD Helicopters",
            "Leonardo Helicopters",
            // Trenes / metro
            "Alstom",
            "Siemens Mobility",
            "Bombardier Transportation",
            "CAF",
            "Hitachi Rail",
            "CRRC",
            "Stadler Rail",
            // Barcos / transporte marítimo (marcas grandes)
            "Maersk",
            "MSC",
            "CMA CGM",
            "Hapag-Lloyd",
            "Evergreen Marine",
            "COSCO Shipping",
            // Transporte urbano y servicios
            "Uber",
            "Lyft",
            "Bolt",
            "Cabify",
        };

        Logic.load_view(db);
    }
}
