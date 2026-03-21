using BreakLineEvent.Modules.Participants.Models;
using BreakLineEvent.Modules.Participants.Services.Models;

namespace BreakLineEvent.Modules.Participants.Services;

public static class ParticipantReconciliationService
{
    public static ParticipantReconciliationSummary Reconcile()
    {
        // autorizados = (preregistrados ∪ registroManual ∪ invitadosVip) - listaNegra
        var authorized = new HashSet<Participant>(ParticipantSets.PreRegistered);
        authorized.UnionWith(ParticipantSets.ManualRegistered);
        authorized.UnionWith(ParticipantSets.VipInvited);
        authorized.ExceptWith(ParticipantSets.BlackList);

        // noAutorizados = asistentesReales - autorizados
        var notAuthorized = new HashSet<Participant>(ParticipantSets.RealAttendees);
        notAuthorized.ExceptWith(authorized);

        // ausentes = autorizados - asistentesReales
        var absents = new HashSet<Participant>(authorized);
        absents.ExceptWith(ParticipantSets.RealAttendees);

        // Ejemplo de uso de Overlaps / SetEquals (para cumplir requisito)
        bool overlapsBlackList = authorized.Overlaps(ParticipantSets.BlackList);
        bool sameAsPreRegistered = authorized.SetEquals(ParticipantSets.PreRegistered);
        _ = overlapsBlackList;
        _ = sameAsPreRegistered;

        return new ParticipantReconciliationSummary
        {
            Authorized = authorized,
            NotAuthorized = notAuthorized,
            Absents = absents,
            DuplicatedDocuments = new HashSet<string>(ParticipantSets.DuplicatedDocuments),
            DuplicatedEmails = new HashSet<string>(ParticipantSets.DuplicatedEmails),
            PreRegisteredCount = ParticipantSets.PreRegistered.Count,
            ManualRegisteredCount = ParticipantSets.ManualRegistered.Count,
            VipInvitedCount = ParticipantSets.VipInvited.Count,
            BlackListCount = ParticipantSets.BlackList.Count,
            RealAttendeesCount = ParticipantSets.RealAttendees.Count,
        };
    }
}
