using System;
using System.Collections.Generic;
using System.Linq;

namespace Parikmakherskaja.Core
{
    /// <summary>
    /// Требование ТЗ п.2.4: кассовый журнал, отчёт по выручке за период, в т.ч. по мастерам.
    /// </summary>
    public class CashJournal
    {
        private readonly List<CashOperation> _operations = new List<CashOperation>();
        private int _nextId = 1;

        public IReadOnlyList<CashOperation> Operations => _operations;

        public CashOperation Register(int clientId, int masterId, int serviceId, decimal amount, DateTime timestamp, PaymentMethod method)
        {
            var op = new CashOperation(_nextId++, clientId, masterId, serviceId, amount, timestamp, method);
            _operations.Add(op);
            return op;
        }

        /// <summary>Суммарная выручка за период [from, to] включительно.</summary>
        public decimal GetRevenue(DateTime from, DateTime to)
        {
            return _operations.Where(o => o.Timestamp.Date >= from.Date && o.Timestamp.Date <= to.Date)
                               .Sum(o => o.Amount);
        }

        /// <summary>Отчёт по выручке за период с группировкой по мастеру.</summary>
        public Dictionary<int, decimal> GetRevenueByMaster(DateTime from, DateTime to)
        {
            return _operations
                .Where(o => o.Timestamp.Date >= from.Date && o.Timestamp.Date <= to.Date)
                .GroupBy(o => o.MasterId)
                .ToDictionary(g => g.Key, g => g.Sum(o => o.Amount));
        }
    }
}
