namespace BreakLineEvent.Modules.Participants.Models;

public static class ParticipantSets
{
    public static HashSet<Participant> PreRegistered { get; } = new();
    public static HashSet<Participant> ManualRegistered { get; } = new();
    public static HashSet<Participant> VipInvited { get; } = new();
    public static HashSet<Participant> BlackList { get; } = new();
    public static HashSet<Participant> RealAttendees { get; } = new();

    public static HashSet<string> DuplicatedDocuments { get; } = new();
    public static HashSet<string> DuplicatedEmails { get; } = new();
}
