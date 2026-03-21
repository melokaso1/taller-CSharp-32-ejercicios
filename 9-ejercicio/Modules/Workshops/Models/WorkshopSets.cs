namespace BreakLineEvent.Modules.Workshops.Models;

public static class WorkshopSets
{
    public static List<Taller> Talleres { get; } = new();

    public static HashSet<InscripcionTaller> Inscripciones { get; } = new();
}
