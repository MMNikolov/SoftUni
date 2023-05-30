using System;
internal class Program
{
    private static void Main(string[] args)
    {
        string name = Console.ReadLine();

        double sumOfGrades = 0;
        int excluded = 0;
        int grade = 1;

        while (grade <= 12)
        {
            double currentGrade = double.Parse(Console.ReadLine());

            if (currentGrade < 4)
            {
                excluded++;
            }
            if (excluded == 2)
            {
                Console.WriteLine($"{name} has been excluded at {grade - 1} grade");
                break;
            }


            sumOfGrades += currentGrade;
            grade++;
        }
        double average = sumOfGrades / 12;


        if (excluded < 2)
        {
            Console.WriteLine($"{name} graduated. Average grade: {average:f2}" );
        }

    }
}