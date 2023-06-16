Dictionary<string, List<decimal>> final = new();

int n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    string[] gradesByStudents = Console.ReadLine()
        .Split();

    string studentName = gradesByStudents[0];
    decimal grade = decimal.Parse(gradesByStudents[1]);

    if (!final.ContainsKey(studentName))
    {
        final.Add(studentName, new List<decimal>());
    }

    final[studentName].Add(grade);
}

foreach (var student in final)
{
    Console.Write($"{student.Key} -> ");

    foreach (var grade in student.Value)
    {
        Console.Write($"{grade:f2} ");
    }

    Console.WriteLine($"(avg: {student.Value.Average():f2})");
}