namespace _03.Cards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            string[] input = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < input.Length; i++)
            {
                string[] tokens = input[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    Card card = new Card();
                    card.CreateCard(tokens[0], tokens[1]);
                    cards.Add(card);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var card in cards)
            {
                Console.Write($"{card.ToString()} ");
            }
        }

    }
    public class Card
    {
        public string CardFace { get; set; }
        public string CardSuit { get; set; }

        public void CreateCard(string face, string suit)
        {
            string[] validFaces = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] validSuits = new string[] { "S", "H", "D", "C" };

            if (validFaces.Contains(face))
            {
                CardFace = face;
            }
            else
            {
                throw new ArgumentException("Invalid card!");
            }

            if (validSuits.Contains(suit))
            {
                CardSuit = suit;
            }
            else
            {
                throw new ArgumentException("Invalid card!");
            }
        }

        public override string ToString()
        {

            string utfSuit = string.Empty;
            switch (CardSuit)
            {
                case "S": utfSuit = "\u2660"; break;
                case "H": utfSuit = "\u2665"; break;
                case "D": utfSuit = "\u2666"; break;
                case "C": utfSuit = "\u2663"; break;
            }

            return $"[{CardFace}{utfSuit}]";
        }
    }
}