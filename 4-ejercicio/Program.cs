Console.Write(
    @"ingresa un texto y el software analizara cuantas veces se repite un caracter

ingresa el texto aca: "
);

string? texto = Console.ReadLine();

texto = texto!.Replace(" ", string.Empty.ToLower());

Dictionary<char, int> cant = new Dictionary<char, int>();

foreach (char i in texto)
{
    if (cant.ContainsKey(i))
        cant[i]++;
    else
        cant[i] = 1;
}
foreach (var j in cant)
    Console.WriteLine($"Caracter = [{j.Key}] | Cantidad = [{j.Value}]");
