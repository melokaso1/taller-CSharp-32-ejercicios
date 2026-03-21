using System;
using System.Linq.Expressions;
using ejercicio.utils;
using ejercicio.Views;

namespace ejercicio.services;

public class Logic
{
    public static void load_view(HashSet<string> db)
    {
        Desing.intro();
        string? search = Console.ReadLine();

        search ??= string.Empty;

        FilterTable(db, search);
    }

    public static void FilterTable(HashSet<string> db, string search)
    {
        bool encontrado = false;

        int indice = 1;
        foreach (var marca in db)
        {
            if (marca.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{indice}: {marca}");
                encontrado = true;
                indice++;
            }
        }

        if (!encontrado)
            Console.WriteLine($"\nNo hay resultados para: {search}");

        Console.WriteLine("\nOprima cualquier tecla para volver a filtrar... \n");
        Console.ReadKey();
        Console.Clear();
    }
}
