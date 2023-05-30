List<Article> articles = new List<Article>();

int numberOfArtciles = int.Parse(Console.ReadLine());

for (int i = 0; i < numberOfArtciles; i++)
{
    string[] articleParts = Console.ReadLine()
    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
    .ToArray();

    string title = articleParts[0];
    string content = articleParts[1];
    string author = articleParts[2];

    Article article = new Article(title, content, author);

    articles.Add(article);
}

foreach (var article in articles)
{
    Console.WriteLine(article);
}



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

    public override string ToString()
    {
        return $"{Title} - {Content}: {Author}";
    }

}
