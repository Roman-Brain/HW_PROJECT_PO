namespace Parikmakherskaja.Core
{
    /// <summary>Позиция прайс-листа услуг.</summary>
    public class Service
    {
        public int Id { get; }
        public string Name { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public Service(int id, string name, int durationMinutes, decimal price)
        {
            Id = id;
            Name = name;
            DurationMinutes = durationMinutes;
            Price = price;
        }
    }
}
