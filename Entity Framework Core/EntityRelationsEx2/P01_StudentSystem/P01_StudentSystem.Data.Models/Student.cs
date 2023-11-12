using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string RegisteredOn { get; set; }
        public string Birthday { get; set; }
        public Course Course { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Homework> Homeworks { get; set; }

    }
}
