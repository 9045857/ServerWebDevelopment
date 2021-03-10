namespace L2T1Excel
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string name, string surname, int age, string phoneNumber)
        {
            Name = name;
            Surname = surname;
            Age = age;
            PhoneNumber = phoneNumber;
        }
    }
}
