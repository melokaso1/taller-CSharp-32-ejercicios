using BreakLineEvent.Modules.Workshops.Models;

namespace BreakLineEvent.Modules.Workshops.Services.Models;

public sealed class WorkshopReconciliationSummary
{
    public HashSet<InscripcionTaller> ValidRegistrations { get; init; } = new();
    public List<string> RejectedRegistrations { get; init; } = new();
    public List<Taller> FullWorkshops { get; init; } = new();
}
