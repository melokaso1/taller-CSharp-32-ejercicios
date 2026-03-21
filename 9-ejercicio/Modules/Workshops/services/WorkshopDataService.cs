using BreakLineEvent.Modules.Workshops.Models;

namespace BreakLineEvent.Modules.Workshops.Services;

public static class WorkshopDataService
{
    public static void LoadSampleWorkshops()
    {
        WorkshopSets.Talleres.Clear();
        WorkshopSets.Inscripciones.Clear();

        var t1 = new Taller(
            "Microservicios Avanzados",
            new TimeOnly(9, 0),
            new TimeOnly(10, 30),
            capacidad: 2
        );
        var t2 = new Taller("Docker Pro", new TimeOnly(10, 0), new TimeOnly(11, 30), capacidad: 1);
        var t3 = new Taller(
            "Kubernetes 101",
            new TimeOnly(11, 0),
            new TimeOnly(12, 30),
            capacidad: 3
        );

        WorkshopSets.Talleres.AddRange(new[] { t1, t2, t3 });
    }
}
