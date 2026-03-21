using BreakLineEvent.Modules.Participants.Services.Models;
using BreakLineEvent.Modules.Workshops.Services.Models;

namespace BreakLineEvent.Menus;

public static class MainMenu
{
    public static void ShowFinalReport(
        ParticipantReconciliationSummary participantSummary,
        WorkshopReconciliationSummary workshopSummary
    )
    {
        Console.Clear();
        Console.WriteLine("===== REPORTE FINAL =====\n");

        Console.WriteLine($"Preregistrados:   {participantSummary.PreRegisteredCount}");
        Console.WriteLine($"Registro manual:  {participantSummary.ManualRegisteredCount}");
        Console.WriteLine($"Invitados VIP:    {participantSummary.VipInvitedCount}");
        Console.WriteLine($"Lista negra:      {participantSummary.BlackListCount}");
        Console.WriteLine($"Autorizados:      {participantSummary.Authorized.Count}");
        Console.WriteLine($"Asistentes reales:{participantSummary.RealAttendeesCount}");
        Console.WriteLine();

        Console.WriteLine("No autorizados:");
        if (participantSummary.NotAuthorized.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var p in participantSummary.NotAuthorized)
                Console.WriteLine($"- {p}");

        Console.WriteLine("\nAutorizados ausentes:");
        if (participantSummary.Absents.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var p in participantSummary.Absents)
                Console.WriteLine($"- {p}");

        Console.WriteLine("\nInscripciones rechazadas:");
        if (workshopSummary.RejectedRegistrations.Count == 0)
            Console.WriteLine("- (ninguna)");
        else
            foreach (var msg in workshopSummary.RejectedRegistrations)
                Console.WriteLine($"- {msg}");

        Console.WriteLine("\nTalleres llenos:");
        if (workshopSummary.FullWorkshops.Count == 0)
            Console.WriteLine("- (ninguno)");
        else
            foreach (var w in workshopSummary.FullWorkshops)
                Console.WriteLine($"- {w.Nombre} (capacidad {w.Capacidad})");

        Console.WriteLine("\nDuplicados detectados en carga:");
        if (
            participantSummary.DuplicatedDocuments.Count == 0
            && participantSummary.DuplicatedEmails.Count == 0
        )
        {
            Console.WriteLine("- (ninguno)");
        }
        else
        {
            foreach (var doc in participantSummary.DuplicatedDocuments)
                Console.WriteLine($"- Documento repetido: {doc}");
            foreach (var mail in participantSummary.DuplicatedEmails)
                Console.WriteLine($"- Email repetido: {mail}");
        }
    }
}
