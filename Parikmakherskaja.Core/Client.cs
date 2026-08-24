namespace Parikmakherskaja.Core
{
    /// <summary>Клиент парикмахерской.</summary>
    public class Client
    {
        public int Id { get; }
        public string FullName { get; set; }
        public string Phone { get; }

        public Client(int id, string fullName, string phone)
        {
            Id = id;
            FullName = fullName;
            Phone = phone;
        }
    }
}
