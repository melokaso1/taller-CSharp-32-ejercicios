while (true)
{
    Console.Clear();
    Console.Write("Ingrese un arreglo de numeros separado por (,) para salir oprime (E): ");

    string? text_origin = Console.ReadLine();

    if (text_origin!.Equals("e") || text_origin!.Equals("E"))
        break;

    string[] arr_num = text_origin!.Split(',');
    HashSet<string> vistos = new HashSet<string>();
    HashSet<string> repetidos = new HashSet<string>();

    foreach (string i in arr_num)
    {
        if (!vistos.Add(i))
        {
            repetidos.Add(i);
        }
    }

    Console.Write($"Los numeros repetidos son: ");
    foreach (string j in repetidos)
        Console.Write($"[{j}]");

    Console.WriteLine("\noprima una tecla para continuar...");
    Console.ReadKey();
}
