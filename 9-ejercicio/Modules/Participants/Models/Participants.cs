namespace BreakLineEvent.Modules.Participants.Models;

public sealed class Participant : IEquatable<Participant>
{
    public Guid Id { get; }
    public string Documento { get; }
    public string NombreCompleto { get; }
    public string Email { get; }
    public bool EsVip { get; }

    public string EmailNormalizado => Email.Trim().ToLowerInvariant();

    public Participant(string documento, string nombreCompleto, string email, bool esVip = false)
    {
        Id = Guid.NewGuid();
        Documento = documento;
        NombreCompleto = nombreCompleto;
        Email = email;
        EsVip = esVip;
    }

    public bool Equals(Participant? other)
    {
        if (other is null)
            return false;

        if (
            !string.IsNullOrWhiteSpace(Documento)
            && !string.IsNullOrWhiteSpace(other.Documento)
            && Documento == other.Documento
        )
            return true;

        if (
            !string.IsNullOrWhiteSpace(Email)
            && !string.IsNullOrWhiteSpace(other.Email)
            && EmailNormalizado == other.EmailNormalizado
        )
            return true;

        return false;
    }

    public override bool Equals(object? obj) => Equals(obj as Participant);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (Documento?.GetHashCode() ?? 0);
            hash = hash * 23 + (EmailNormalizado?.GetHashCode() ?? 0);
            return hash;
        }
    }

    public override string ToString() =>
        $"{NombreCompleto} | DOC: {Documento} | EMAIL: {EmailNormalizado}";
}
