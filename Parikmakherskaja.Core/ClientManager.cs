using System.Collections.Generic;
using System.Linq;

namespace Parikmakherskaja.Core
{
    /// <summary>
    /// Учёт клиентов. Требование ТЗ п.3: "исключение дублирования одного и того же
    /// клиента под разными записями (поиск по телефону перед созданием новой карточки)".
    /// </summary>
    public class ClientManager
    {
        private readonly List<Client> _clients = new List<Client>();
        private int _nextId = 1;

        public IReadOnlyList<Client> Clients => _clients;

        /// <summary>Найти клиента по телефону, либо создать нового, если такого телефона ещё нет.</summary>
        public Client AddOrFindByPhone(string fullName, string phone)
        {
            var existing = _clients.FirstOrDefault(c => c.Phone == phone);
            if (existing != null)
            {
                return existing;
            }

            var client = new Client(_nextId++, fullName, phone);
            _clients.Add(client);
            return client;
        }
    }
}
