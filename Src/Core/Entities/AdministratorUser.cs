namespace Core.Entities
{
    public class AdministratorUser: User
    {
        public string AdminId { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}
