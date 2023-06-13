namespace _05.ComparingObjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] info = input.Split();
                people.Add(new Person(info[0], int.Parse(info[1]), info[2]));
            }

            int targetPersonIndex = int.Parse(Console.ReadLine()) - 1;

            Person targetPerson = people[targetPersonIndex];

            if (people.Where(p => p.CompareTo(targetPerson) == 0).Count() > 1)
            {
                Console.Write(people.Where(p => p.CompareTo(targetPerson) == 0).Count() + " ");
                Console.Write(people.Where(p => p.CompareTo(targetPerson) != 0).Count() + " ");
                Console.WriteLine(people.Count());
            }
            else
            {
                Console.WriteLine("No matches");
            }
        }
    }
}