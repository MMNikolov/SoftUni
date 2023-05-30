int numberOfTeams = int.Parse(Console.ReadLine());

List<Team> teams = new List<Team>();

for (int i = 0; i < numberOfTeams; i++)
{
    string[] inputTokens = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    string teamCreator = inputTokens[0];
    string teamName = inputTokens[1];

    if (teams.Select(x => x.Name).Contains(teamName))
    {
        Console.WriteLine($"Team {teamName} was already created!");
        continue;
    }

    if (teams.Select(x => x.Creator).Contains(teamCreator))
    {
        Console.WriteLine($"{teamCreator} cannot create another team!");
    }

    Team currentTeam = new Team(teamName, teamCreator);

    teams.Add(currentTeam);
    Console.WriteLine($"Team {teamName} has been cretated by {teamCreator}!");
}

string input;
while ((input = Console.ReadLine()) != "end of assignment")
{
    string[] inputTokens = Console.ReadLine()
        .Split("->", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    string teamMember = inputTokens[0];
    string teamName = inputTokens[1];

    Team team = teams.FirstOrDefault(x => x.Name == teamName);
    if (team is null)
    {
        Console.WriteLine($"Team {teamName} does not exist!");
        continue;
    }

    if (teams.SelectMany(x=>x.Members).Contains(teamMember))
    {
        Console.WriteLine($"Member {teamMember} cannot join team {teamName}");
    }

    team.Members.Add(teamMember);
}

public class Team
{
    public Team(string name, string creator)
    {
        Name = name;
        Creator = creator;
    }

    public string Name { get; set; }

    public string Creator { get; set; }

    public List<string> Members { get; set; }
}
