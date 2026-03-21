using BreakLineEvent.Modules.Participants.Models;

namespace BreakLineEvent.Modules.Participants.Services;

public static class ParticipantDataService
{
    public static void LoadSampleData()
    {
        // Función local para agregar y registrar duplicados
        void AddWithDuplicateDetection(HashSet<Participant> set, Participant p)
        {
            if (set.Any(x => !string.IsNullOrWhiteSpace(x.Documento) && x.Documento == p.Documento))
            {
                ParticipantSets.DuplicatedDocuments.Add(p.Documento);
            }

            if (
                set.Any(x =>
                    !string.IsNullOrWhiteSpace(x.Email) && x.EmailNormalizado == p.EmailNormalizado
                )
            )
            {
                ParticipantSets.DuplicatedEmails.Add(p.EmailNormalizado);
            }

            set.Add(p);
        }

        ParticipantSets.PreRegistered.Clear();
        ParticipantSets.ManualRegistered.Clear();
        ParticipantSets.VipInvited.Clear();
        ParticipantSets.BlackList.Clear();
        ParticipantSets.RealAttendees.Clear();
        ParticipantSets.DuplicatedDocuments.Clear();
        ParticipantSets.DuplicatedEmails.Clear();

        // Caso 1: duplicado por documento
        var ana1 = new Participant("123", "Ana Torres", "ana@gmail.com");
        var ana2 = new Participant("123", "Ana T.", "anatorres@gmail.com");
        AddWithDuplicateDetection(ParticipantSets.PreRegistered, ana1);
        AddWithDuplicateDetection(ParticipantSets.PreRegistered, ana2);

        // Caso 2: duplicado por email normalizado
        var luis1 = new Participant("999", "Luis Díaz", "LDiaz@correo.com");
        var luis2 = new Participant("888", "Luis D.", "ldiaz@correo.com ");
        AddWithDuplicateDetection(ParticipantSets.PreRegistered, luis1);
        AddWithDuplicateDetection(ParticipantSets.PreRegistered, luis2);

        // Otros preregistrados
        var laura = new Participant("1122", "Laura Pérez", "laura@correo.com");
        var carlos = new Participant("3344", "Carlos Ruiz", "carlos@correo.com");
        AddWithDuplicateDetection(ParticipantSets.PreRegistered, laura);
        AddWithDuplicateDetection(ParticipantSets.PreRegistered, carlos);

        // Registro manual
        AddWithDuplicateDetection(
            ParticipantSets.ManualRegistered,
            new Participant("5566", "Mario Soto", "mario@correo.com")
        );
        AddWithDuplicateDetection(
            ParticipantSets.ManualRegistered,
            new Participant("6677", "Juan Díaz", "juan@correo.com")
        );

        // Invitados VIP
        var vip1 = new Participant("7711", "VIP One", "vip1@breakline.com", esVip: true);
        AddWithDuplicateDetection(ParticipantSets.VipInvited, vip1);

        // Lista negra (Caso 3)
        ParticipantSets.BlackList.Add(new Participant("3344", "Carlos Ruiz", "carlos@correo.com"));

        // Asistentes reales (Caso 4: uno no autorizado)
        ParticipantSets.RealAttendees.Add(ana1);
        ParticipantSets.RealAttendees.Add(luis1);
        ParticipantSets.RealAttendees.Add(vip1);
        ParticipantSets.RealAttendees.Add(
            new Participant("7788", "Carlos Ruiz", "carlos@correo.com")
        );
    }
}
