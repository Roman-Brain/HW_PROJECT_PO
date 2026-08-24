using System;

namespace Parikmakherskaja.Core
{
    /// <summary>Запись клиента к мастеру на услугу.</summary>
    public class Booking
    {
        public int Id { get; }
        public int ClientId { get; }
        public int MasterId { get; }
        public int ServiceId { get; }
        public DateTime StartTime { get; }
        public int DurationMinutes { get; }

        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);

        public Booking(int id, int clientId, int masterId, int serviceId, DateTime startTime, int durationMinutes)
        {
            Id = id;
            ClientId = clientId;
            MasterId = masterId;
            ServiceId = serviceId;
            StartTime = startTime;
            DurationMinutes = durationMinutes;
        }

        /// <summary>Пересекается ли данная запись по времени с другой записью того же мастера.</summary>
        public bool OverlapsWith(Booking other)
        {
            if (other.MasterId != MasterId) return false;
            return StartTime < other.EndTime && other.StartTime < EndTime;
        }
    }
}
