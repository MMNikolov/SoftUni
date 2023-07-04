namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IIndividual> entities = new List<IIndividual>();

            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                string[] documents = input.Split();
                if (documents.Length == 2)
                {
                    string name = documents[0];
                    string id = documents[1];

                    entities.Add(new Robot(name, id));
                }
                else if (documents.Length == 3)
                {
                    string name = documents[0];
                    int age = int.Parse(documents[1]);
                    string id = documents[2];

                    entities.Add(new Human(name, age, id));
                }
            }

            string fakeSubstring = Console.ReadLine();

            foreach (IIndividual entity in entities)
            {
                entity.CheckId(fakeSubstring);
            }
        }
    }
}