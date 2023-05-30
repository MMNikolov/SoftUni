
string[] articlePArts = Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();

string title = articlePArts[0];
string content = articlePArts[1];
string author = articlePArts[^1];

Article article = new Article(title, content, author);

int numberOfCommands = int.Parse(Console.ReadLine());

for (int i = 0; i < numberOfCommands; i++)
{
    string[] commandTokens = Console.ReadLine()
        .Split(": ", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    string command = commandTokens[0]; 
    string newValue = commandTokens[1];

    
    if (command == "Edit")
    {
        article.Edit(newValue);
    }
    else if (command == "ChangeAuthor")
    {
        article.ChangeAuthor(newValue);
    }
    else
    {
        article.Rename(command);
    }

}

Console.WriteLine(article);

public class Article
{
    public Article(string title, string content, string author)
    {
        Title = title;
        Content = content;
        Author = author;
    }

    public string Title { get; set; }

    public string Content { get; set; }    

    public string Author { get; set; }  

    public void Edit(string content)
    {
        Content = content;
    }

    public void ChangeAuthor(string author)
    {
        Author = author;
    }
    
    public void Rename(string title)
    {
        Title = title;
    }

    public override string ToString()
    {
        return $"{Title} - {Content}: {Author}";
    }

}
