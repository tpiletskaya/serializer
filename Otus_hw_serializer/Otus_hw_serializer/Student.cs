namespace Otus_hw_serializer
{

    public class Student
    {
        public int studentNumber;
        public string gender;

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }

        public Student Get() => new Student()
        {
            gender = "male",
            studentNumber = 1234,
            FirstName = "Ivan",
            Surname = "Ivanov",
            Age = 19,
            Group = "108"
        };
    }
}
