using BreakLineEvent.Menus;
using BreakLineEvent.Modules.Participants.Models;
using BreakLineEvent.Modules.Participants.Services;
using BreakLineEvent.Modules.Workshops.Models;
using BreakLineEvent.Modules.Workshops.Services;

namespace BreakLineEvent;

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Cargar datos de participantes y talleres
        ParticipantDataService.LoadSampleData();
        WorkshopDataService.LoadSampleWorkshops();

        // 2. Conciliar participantes
        var participantSummary = ParticipantReconciliationService.Reconcile();

        // 3. Validar inscripciones a talleres
        var workshopService = new WorkshopRegistrationService(participantSummary.Authorized);

        // Demo de Casos 5 y 6 usando los datos cargados
        var ana = participantSummary.Authorized.First(p => p.Documento == "123");
        var luis = participantSummary.Authorized.FirstOrDefault(p =>
            p.EmailNormalizado == "ldiaz@correo.com"
        );
        var t1 = WorkshopSets.Talleres[0]; // Microservicios
        var t2 = WorkshopSets.Talleres[1]; // Docker Pro

        // Caso 5: cruce de talleres (Ana en t1 y t2 que se solapan)
        workshopService.TryRegister(ana, t1);
        workshopService.TryRegister(ana, t2); // debe ser rechazo por cruce

        // Caso 6: taller sin cupo (capacidad 1 en t2)
        if (luis is not null)
            workshopService.TryRegister(luis, t2); // puede quedar sin cupo según orden

        var workshopSummary = workshopService.BuildSummary();

        // 4. Mostrar reporte consolidado
        MainMenu.ShowFinalReport(participantSummary, workshopSummary);

        Console.WriteLine("\nPresione una tecla para salir...");
        Console.ReadKey();
    }
}
