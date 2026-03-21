using BreakLineEvent.Modules.Participants.Models;

namespace BreakLineEvent.Modules.Participants.Services.Models;

public sealed class ParticipantReconciliationSummary
{
    public HashSet<Participant> Authorized { get; init; } = new();
    public HashSet<Participant> NotAuthorized { get; init; } = new();
    public HashSet<Participant> Absents { get; init; } = new();

    public HashSet<string> DuplicatedDocuments { get; init; } = new();
    public HashSet<string> DuplicatedEmails { get; init; } = new();

    public int PreRegisteredCount { get; init; }
    public int ManualRegisteredCount { get; init; }
    public int VipInvitedCount { get; init; }
    public int BlackListCount { get; init; }
    public int RealAttendeesCount { get; init; }
}
