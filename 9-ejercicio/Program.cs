using BreakLineEvent.Menus;
using BreakLineEvent.Modules.Participants.Services;
using BreakLineEvent.Modules.Participants.Services.Models;

namespace BreakLineEvent;

public class Program
{
    public static void Main(string[] args)
    {
        // Inicializar datos de prueba
        ParticipantDataService.LoadSampleData();

        // Procesar conciliación
        var summary = ParticipantReconciliationService.Reconcile();

        // Mostrar menú / reporte principal
        MainMenu.ShowParticipantsReport(summary);

        Console.WriteLine("\nPresione una tecla para salir...");
        Console.ReadKey();
    }
}
