string plainText = Console.ReadLine();

char[] encryptedText = new char[plainText.Length];

for (int i = 0; i < plainText.Length; i++)
{
    encryptedText[i] = (char)(plainText[i] + 3);
}
Console.WriteLine(new string(encryptedText));
