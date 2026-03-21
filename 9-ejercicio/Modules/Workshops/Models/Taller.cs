namespace BreakLineEvent.Modules.Workshops.Models;

public sealed class Taller
{
    public Guid Id { get; }
    public string Nombre { get; }
    public TimeOnly HoraInicio { get; }
    public TimeOnly HoraFin { get; }
    public int Capacidad { get; }

    public Taller(string nombre, TimeOnly inicio, TimeOnly fin, int capacidad)
    {
        Id = Guid.NewGuid();
        Nombre = nombre;
        HoraInicio = inicio;
        HoraFin = fin;
        Capacidad = capacidad;
    }

    public override string ToString() =>
        $"{Nombre} ({HoraInicio:HH\\:mm}-{HoraFin:HH\\:mm}) Capacidad: {Capacidad}";
}
