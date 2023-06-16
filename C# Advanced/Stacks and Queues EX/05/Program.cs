int[] nums = (Console.ReadLine()
    .Split()
    .Select(int.Parse)
    .ToArray());

Stack<int> stack = new(nums);

int rackHold = int.Parse(Console.ReadLine());
int capacityOfRackEd = rackHold;
int allRacks = 0;
int allClothes = stack.Count();


for (int i = 0; i < allClothes; i++)
{
    if (rackHold > stack.Peek())
    {
        rackHold = rackHold - stack.Pop();
        if (stack.Count == 0)
        {
            allRacks++;
        }
    }
    else if (rackHold == stack.Peek())
    {
        stack.Pop();
        allRacks++;
        rackHold = capacityOfRackEd;
    }
    else if (rackHold < stack.Peek())
    {
        allRacks++;
        rackHold = capacityOfRackEd;
        rackHold -= stack.Pop();

        if (stack.Count == 0)
        {
            allRacks++;
        }
    }
}

Console.WriteLine(allRacks);