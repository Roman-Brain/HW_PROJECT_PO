using System;

namespace Parikmakherskaja.Core
{
    public enum PaymentMethod { Cash, Card }

    /// <summary>Кассовая операция — оплата оказанной услуги.</summary>
    public class CashOperation
    {
        public int Id { get; }
        public int ClientId { get; }
        public int MasterId { get; }
        public int ServiceId { get; }
        public decimal Amount { get; }
        public DateTime Timestamp { get; }
        public PaymentMethod Method { get; }

        public CashOperation(int id, int clientId, int masterId, int serviceId, decimal amount, DateTime timestamp, PaymentMethod method)
        {
            Id = id;
            ClientId = clientId;
            MasterId = masterId;
            ServiceId = serviceId;
            Amount = amount;
            Timestamp = timestamp;
            Method = method;
        }
    }
}
