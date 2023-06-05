using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int age = 19;
            string name = "Pesho";

            Person firstPersone = new Person();
            Person secondPersone = new Person(age);
            Person thirdPersone = new Person(name, age);



        }
    }
}
