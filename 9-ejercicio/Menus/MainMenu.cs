using BreakLineEvent.Modules.Participants.Models;
using BreakLineEvent.Modules.Participants.Services;
using BreakLineEvent.Modules.Participants.Services.Models;
using BreakLineEvent.Modules.Workshops.Models;
using BreakLineEvent.Modules.Workshops.Services;
using BreakLineEvent.Modules.Workshops.Services.Models;

namespace BreakLineEvent.Menus;

public static class MainMenu
{
    // Servicio de talleres (se crea una vez y se reutiliza)
    private static WorkshopRegistrationService? _workshopService;
    private static ParticipantReconciliationSummary? _participantSummary;
    private static WorkshopReconciliationSummary? _workshopSummary;

    public static bool ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("===== BREAKLINE EVENT =====");
        Console.WriteLine("1) Ver resumen de participantes");
        Console.WriteLine("2) Listar talleres");
        Console.WriteLine("3) Inscribir participante en un taller");
        Console.WriteLine("4) Ver reporte final");
        Console.WriteLine("5) Salir");
        Console.Write("\nSeleccione una opción: ");

        string? opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                MostrarResumenParticipantes();
                return true;
            case "2":
                ListarTalleres();
                return true;
            case "3":
                InscribirParticipanteEnTaller();
                return true;
            case "4":
                MostrarReporteFinal();
                return true;
            case "5":
                return false;
            default:
                Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                Console.ReadKey();
                return true;
        }
    }

    private static void AsegurarConciliacion()
    {
        // Calcula participantes autorizados / ausentes / etc. una sola vez
        _participantSummary ??= ParticipantReconciliationService.Reconcile();

        // Inicializa el servicio de talleres si aún no existe
        _workshopService ??= new WorkshopRegistrationService(_participantSummary.Authorized);
    }

    private static void MostrarResumenParticipantes()
    {
        AsegurarConciliacion();

        var s = _participantSummary!;

        Console.Clear();
        Console.WriteLine("===== RESUMEN PARTICIPANTES =====\n");
        Console.WriteLine($"Preregistrados:   {s.PreRegisteredCount}");
        Console.WriteLine($"Registro manual:  {s.ManualRegisteredCount}");
        Console.WriteLine($"Invitados VIP:    {s.VipInvitedCount}");
        Console.WriteLine($"Lista negra:      {s.BlackListCount}");
        Console.WriteLine($"Autorizados:      {s.Authorized.Count}");
        Console.WriteLine($"Asistentes reales:{s.RealAttendeesCount}");
        Console.WriteLine();

        Console.WriteLine("No autorizados:");
        if (s.NotAuthorized.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var p in s.NotAuthorized)
                Console.WriteLine($"- {p}");

        Console.WriteLine("\nAutorizados ausentes:");
        if (s.Absents.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var p in s.Absents)
                Console.WriteLine($"- {p}");

        Console.WriteLine("\nDuplicados detectados en carga:");
        if (s.DuplicatedDocuments.Count == 0 && s.DuplicatedEmails.Count == 0)
        {
            Console.WriteLine("- (ninguno)");
        }
        else
        {
            foreach (var doc in s.DuplicatedDocuments)
                Console.WriteLine($"- Documento repetido: {doc}");
            foreach (var mail in s.DuplicatedEmails)
                Console.WriteLine($"- Email repetido: {mail}");
        }

        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    private static void ListarTalleres()
    {
        Console.Clear();
        Console.WriteLine("===== TALLERES DISPONIBLES =====\n");

        for (int i = 0; i < WorkshopSets.Talleres.Count; i++)
        {
            Taller t = WorkshopSets.Talleres[i];
            int inscritos = WorkshopSets.Inscripciones.Count(x => x.Taller.Id == t.Id);
            Console.WriteLine(
                $"{i + 1}) {t.Nombre} | {t.HoraInicio:HH\\:mm}-{t.HoraFin:HH\\:mm} | "
                    + $"Capacidad: {t.Capacidad} | Inscritos: {inscritos}"
            );
        }

        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }

    private static void InscribirParticipanteEnTaller()
    {
        AsegurarConciliacion();
        var s = _participantSummary!;
        var service = _workshopService!;

        Console.Clear();
        Console.WriteLine("===== INSCRIPCIÓN A TALLER =====\n");

        // 1. Listar participantes autorizados con índice
        var listaAutorizados = s.Authorized.ToList();
        for (int i = 0; i < listaAutorizados.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {listaAutorizados[i]}");
        }

        Console.Write("\nSeleccione participante (número) o 0 para cancelar: ");
        if (
            !int.TryParse(Console.ReadLine(), out int indiceP)
            || indiceP < 0
            || indiceP > listaAutorizados.Count
        )
        {
            Console.WriteLine("Entrada inválida. Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }
        if (indiceP == 0)
            return;

        Participant participante = listaAutorizados[indiceP - 1];

        // 2. Listar talleres
        Console.Clear();
        Console.WriteLine("===== SELECCIONAR TALLER =====\n");
        for (int i = 0; i < WorkshopSets.Talleres.Count; i++)
        {
            Taller t = WorkshopSets.Talleres[i];
            int inscritos = WorkshopSets.Inscripciones.Count(x => x.Taller.Id == t.Id);
            Console.WriteLine(
                $"{i + 1}) {t.Nombre} | {t.HoraInicio:HH\\:mm}-{t.HoraFin:HH\\:mm} | "
                    + $"Capacidad: {t.Capacidad} | Inscritos: {inscritos}"
            );
        }

        Console.Write("\nSeleccione taller (número) o 0 para cancelar: ");
        if (
            !int.TryParse(Console.ReadLine(), out int indiceT)
            || indiceT < 0
            || indiceT > WorkshopSets.Talleres.Count
        )
        {
            Console.WriteLine("Entrada inválida. Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }
        if (indiceT == 0)
            return;

        Taller taller = WorkshopSets.Talleres[indiceT - 1];

        // 3. Intentar inscripción y mostrar resultado
        bool ok = service.TryRegister(participante, taller);

        Console.WriteLine();
        if (ok)
        {
            Console.WriteLine(
                $"Inscripción EXITOSA: {participante.NombreCompleto} -> {taller.Nombre}"
            );
        }
        else
        {
            // Tomamos el último mensaje de rechazo
            string mensaje =
                service.RejectedMessages.LastOrDefault()
                ?? "Inscripción rechazada (motivo desconocido)";
            Console.WriteLine(mensaje);
        }

        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void MostrarReporteFinal()
    {
        AsegurarConciliacion();
        var service = _workshopService!;
        _workshopSummary = service.BuildSummary();

        Console.Clear();
        Console.WriteLine("===== REPORTE FINAL =====\n");

        var ps = _participantSummary!;
        var ws = _workshopSummary;

        Console.WriteLine($"Preregistrados:   {ps.PreRegisteredCount}");
        Console.WriteLine($"Registro manual:  {ps.ManualRegisteredCount}");
        Console.WriteLine($"Invitados VIP:    {ps.VipInvitedCount}");
        Console.WriteLine($"Lista negra:      {ps.BlackListCount}");
        Console.WriteLine($"Autorizados:      {ps.Authorized.Count}");
        Console.WriteLine($"Asistentes reales:{ps.RealAttendeesCount}");
        Console.WriteLine();

        Console.WriteLine("No autorizados:");
        if (ps.NotAuthorized.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var p in ps.NotAuthorized)
                Console.WriteLine($"- {p}");

        Console.WriteLine("\nAutorizados ausentes:");
        if (ps.Absents.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var p in ps.Absents)
                Console.WriteLine($"- {p}");

        Console.WriteLine("\nInscripciones rechazadas:");
        if (ws!.RejectedRegistrations.Count == 0)
            Console.WriteLine("- (ninguna)");
        else
            foreach (var msg in ws.RejectedRegistrations)
                Console.WriteLine($"- {msg}");

        Console.WriteLine("\nTalleres llenos:");
        if (ws.FullWorkshops.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var w in ws.FullWorkshops)
                Console.WriteLine($"- {w.Nombre} (capacidad {w.Capacidad})");

        Console.WriteLine("\nDuplicados detectados en carga:");
        if (ps.DuplicatedDocuments.Count == 0 && ps.DuplicatedEmails.Count == 0)
        {
            Console.WriteLine("- (ninguno)");
        }
        else
        {
            foreach (var doc in ps.DuplicatedDocuments)
                Console.WriteLine($"- Documento repetido: {doc}");
            foreach (var mail in ps.DuplicatedEmails)
                Console.WriteLine($"- Email repetido: {mail}");
        }

        Console.WriteLine("\nPresione una tecla para volver al menú...");
        Console.ReadKey();
    }
}
