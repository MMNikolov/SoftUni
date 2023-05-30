int countOfStudents = int.Parse(Console.ReadLine());

List<Student> students = new List<Student>();

for (int i = 0; i < countOfStudents; i++)
{
    string[] currentStudentToken = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .ToArray();

    string firstName = currentStudentToken[0];
    string lastName = currentStudentToken[1];
    double grade = double.Parse(currentStudentToken[2]);

    Student student = new Student(firstName, lastName, grade);
    students.Add(student);
}



foreach (var student in students.OrderByDescending(x => x.Grade))
{
    Console.WriteLine(student);
}

public class Student 
{
    public Student(string firstname, string lastName, double grade)
    {
        FirstName = firstname;
        LastName = lastName;
        Grade = grade;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }   
    
    public double Grade { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}: {Grade:f2}";
    }
}
