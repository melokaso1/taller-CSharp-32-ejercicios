using BreakLineEvent.Modules.Participants.Services.Models;

namespace BreakLineEvent.Menus;

public static class MainMenu
{
    public static void ShowParticipantsReport(ParticipantReconciliationSummary summary)
    {
        Console.Clear();
        Console.WriteLine("===== REPORTE FINAL (PARTICIPANTES) =====\n");

        Console.WriteLine($"Preregistrados:   {summary.PreRegisteredCount}");
        Console.WriteLine($"Registro manual:  {summary.ManualRegisteredCount}");
        Console.WriteLine($"Invitados VIP:    {summary.VipInvitedCount}");
        Console.WriteLine($"Lista negra:      {summary.BlackListCount}");
        Console.WriteLine($"Autorizados:      {summary.Authorized.Count}");
        Console.WriteLine($"Asistentes reales:{summary.RealAttendeesCount}");
        Console.WriteLine();

        Console.WriteLine("No autorizados:");
        if (summary.NotAuthorized.Count == 0)
        {
            Console.WriteLine("- (ninguno)");
        }
        else
        {
            foreach (var p in summary.NotAuthorized)
                Console.WriteLine($"- {p}");
        }

        Console.WriteLine("\nAutorizados ausentes:");
        if (summary.Absents.Count == 0)
        {
            Console.WriteLine("- (ninguno)");
        }
        else
        {
            foreach (var p in summary.Absents)
                Console.WriteLine($"- {p}");
        }

        Console.WriteLine("\nDuplicados detectados en carga:");
        if (summary.DuplicatedDocuments.Count == 0 && summary.DuplicatedEmails.Count == 0)
        {
            Console.WriteLine("- (ninguno)");
        }
        else
        {
            foreach (var doc in summary.DuplicatedDocuments)
                Console.WriteLine($"- Documento repetido: {doc}");
            foreach (var mail in summary.DuplicatedEmails)
                Console.WriteLine($"- Email repetido: {mail}");
        }
    }
}
