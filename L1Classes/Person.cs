namespace L1Classes
{
    public class Person
    {
        private readonly string _name;
        private readonly string _surname;

        public Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public override string ToString()
        {
            return $"{_name} {_surname}";
        }
    }
}
