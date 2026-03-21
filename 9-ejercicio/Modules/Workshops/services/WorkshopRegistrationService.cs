using BreakLineEvent.Modules.Participants.Models;
using BreakLineEvent.Modules.Workshops.Models;
using BreakLineEvent.Modules.Workshops.Services.Models;

namespace BreakLineEvent.Modules.Workshops.Services;

public sealed class WorkshopRegistrationService
{
    private readonly HashSet<Participant> _authorized;
    private readonly HashSet<InscripcionTaller> _inscripciones;

    public List<string> RejectedMessages { get; } = new();

    public WorkshopRegistrationService(HashSet<Participant> authorized)
    {
        _authorized = authorized;
        _inscripciones = WorkshopSets.Inscripciones;
    }

    public bool TryRegister(Participant participant, Taller workshop)
    {
        // 1. Debe estar autorizado
        if (!_authorized.Contains(participant))
        {
            RejectedMessages.Add(
                $"{participant.NombreCompleto} -> {workshop.Nombre} | Motivo: no autorizado"
            );

            return false;
        }

        // 2. Capacidad
        int currentCount = _inscripciones.Count(i => i.Taller.Id == workshop.Id);
        if (currentCount >= workshop.Capacidad)
        {
            RejectedMessages.Add(
                $"{participant.NombreCompleto} -> {workshop.Nombre} | Motivo: sin cupo"
            );

            return false;
        }

        // 3. Cruce de horario con otros talleres del mismo participante
        bool hasOverlap = _inscripciones.Any(i =>
            i.Participante.Equals(participant) && Overlaps(i.Taller, workshop)
        );

        if (hasOverlap)
        {
            RejectedMessages.Add(
                $"{participant.NombreCompleto} -> {workshop.Nombre} | Motivo: cruce de horario"
            );

            return false;
        }

        // Si pasa todas las validaciones, agregamos al HashSet
        _inscripciones.Add(new InscripcionTaller(participant, workshop));
        return true;
    }

    private static bool Overlaps(Taller a, Taller b)
    {
        // Se cruzan si el inicio de uno es antes del fin del otro y viceversa
        return a.HoraInicio < b.HoraFin && b.HoraInicio < a.HoraFin;
    }

    public WorkshopReconciliationSummary BuildSummary()
    {
        // Talleres llenos
        var full = WorkshopSets
            .Talleres.Where(t =>
            {
                int count = WorkshopSets.Inscripciones.Count(i => i.Taller.Id == t.Id);
                return count >= t.Capacidad;
            })
            .ToList();

        return new WorkshopReconciliationSummary
        {
            ValidRegistrations = new HashSet<InscripcionTaller>(WorkshopSets.Inscripciones),
            RejectedRegistrations = new List<string>(RejectedMessages),
            FullWorkshops = full,
        };
    }
}
