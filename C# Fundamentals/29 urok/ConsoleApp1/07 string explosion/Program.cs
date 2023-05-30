using System.Text;

string input = Console.ReadLine();
StringBuilder sb = new StringBuilder();
int explosionStrength = 0;

for (int i = 0; i < input.Length; i++)
{
	if (input[i] == '>')
	{
		explosionStrength += int.Parse(input[i + 1].ToString());
		sb.Append(input[i]);	
	}
	else if (explosionStrength > 0)
	{
		explosionStrength--;
	}
	else
	{
		sb.Append(input[i]);
	}
}
Console.WriteLine(sb.ToString());
