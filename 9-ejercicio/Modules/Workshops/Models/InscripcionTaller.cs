using BreakLineEvent.Modules.Participants.Models;

namespace BreakLineEvent.Modules.Workshops.Models;

public sealed class InscripcionTaller : IEquatable<InscripcionTaller>
{
    public Participant Participante { get; }
    public Taller Taller { get; }

    public InscripcionTaller(Participant participante, Taller taller)
    {
        Participante = participante;
        Taller = taller;
    }

    public bool Equals(InscripcionTaller? other)
    {
        if (other is null)
            return false;

        return Participante.Equals(other.Participante) && Taller.Id == other.Taller.Id;
    }

    public override bool Equals(object? obj) => Equals(obj as InscripcionTaller);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + Participante.GetHashCode();
            hash = hash * 23 + Taller.Id.GetHashCode();
            return hash;
        }
    }

    public override string ToString() => $"{Participante.NombreCompleto} -> {Taller.Nombre}";
}
