Random rng = new Random();

Console.Clear();

char[] caracteres = new char[]
{
    // Letras minúsculas
    'a',
    'b',
    'c',
    'd',
    'e',
    'f',
    'g',
    'h',
    'i',
    'j',
    'k',
    'l',
    'm',
    'n',
    'o',
    'p',
    'q',
    'r',
    's',
    't',
    'u',
    'v',
    'w',
    'x',
    'y',
    'z',
    'ñ',
    // Letras mayúsculas
    'A',
    'B',
    'C',
    'D',
    'E',
    'F',
    'G',
    'H',
    'I',
    'J',
    'K',
    'L',
    'M',
    'N',
    'O',
    'P',
    'Q',
    'R',
    'S',
    'T',
    'U',
    'V',
    'W',
    'X',
    'Y',
    'Z',
    'Ñ',
    // Vocales acentuadas y diéresis
    'á',
    'é',
    'í',
    'ó',
    'ú',
    'ü',
    'Á',
    'É',
    'Í',
    'Ó',
    'Ú',
    'Ü',
    // Números
    '0',
    '1',
    '2',
    '3',
    '4',
    '5',
    '6',
    '7',
    '8',
    '9',
    // Signos y símbolos (sin espacios extra)
    '.',
    ',',
    ';',
    ':',
    '?',
    '!',
    '¿',
    '¡',
    '(',
    ')',
    '[',
    ']',
    '{',
    '}',
    '\"',
    '\'',
    '+',
    '-',
    '*',
    '/',
    '=',
    '%',
    '&',
    '|',
    '_',
    '@',
    '#',
    '$',
    '^',
    '<',
    '>',
    '\\',
};

List<char> password = new List<char>();

char generate_password()
{
    int index = rng.Next(0, caracteres.Length);

    return caracteres[index];
}

int n = rng.Next(15, 87);

for (int i = 0; i < n; i++)
{
    char caracter = generate_password();

    password.Add(caracter);
}

Console.Write($"Tu contraseña segura es: ");

foreach (char j in password)
    Console.Write(j);
