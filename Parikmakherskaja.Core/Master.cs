namespace Parikmakherskaja.Core
{
    /// <summary>Мастер (сотрудник) парикмахерской.</summary>
    public class Master
    {
        public int Id { get; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public bool IsPresentToday { get; set; } = true;

        public Master(int id, string fullName, string specialization)
        {
            Id = id;
            FullName = fullName;
            Specialization = specialization;
        }
    }
}
