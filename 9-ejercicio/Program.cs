using BreakLineEvent.Menus;
using BreakLineEvent.Modules.Participants.Services;
using BreakLineEvent.Modules.Workshops.Services;

namespace BreakLineEvent;

public class Program
{
    public static void Main(string[] args)
    {
        // Cargar datos iniciales
        ParticipantDataService.LoadSampleData();
        WorkshopDataService.LoadSampleWorkshops();

        // Bucle principal de menú
        bool continuar = true;
        while (continuar)
        {
            continuar = MainMenu.ShowMainMenu();
        }
    }
}
