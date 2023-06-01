Dictionary<string, List<decimal>> final = new();

int n = int.Parse(Console.ReadLine());

for (int i = 0; i < n; i++)
{
    string[] gradesByStudents = Console.ReadLine()
        .Split();

     //    7
     //John 5.20
     //Maria 5.50
     //John 3.20
     //Maria 2.50
     //Sam 2.00
     //Maria 3.46
     //Sam 3.00

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