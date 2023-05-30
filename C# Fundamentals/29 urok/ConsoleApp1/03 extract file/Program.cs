string filePath = Console.ReadLine();

string fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1, filePath.LastIndexOf(".") - 
    filePath.LastIndexOf(@"\") - 1);
string fileExt = filePath.Substring(filePath.LastIndexOf(".") + 1);

Console.WriteLine("File name: {0}", fileName);

Console.WriteLine("File extension: {0}", fileExt);
