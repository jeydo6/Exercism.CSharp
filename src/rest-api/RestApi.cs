using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class RestApi
{
    private readonly Database _database;
    
    public RestApi(string database) => _database = new Database(JsonSerializer.Deserialize<IEnumerable<User>>(database));

    public string Get(string url, string payload = null) => url switch
    {
        "/users" => SendRequest<GetUsersRequest, IEnumerable<User>>(payload, GetUsers),
        _ => throw new InvalidOperationException()
    };

    public string Post(string url, string payload) => url switch
    {
        "/add" => SendRequest<CreateUserRequest, User>(payload, CreateUser),
        "/iou" => SendRequest<CreateIouRequest, IEnumerable<User>>(payload, CreateIou),
        _ => throw new InvalidOperationException()
    };

    private IEnumerable<User> GetUsers(GetUsersRequest request) =>
        _database.GetUsers(request.Names);

    private User CreateUser(CreateUserRequest request) =>
        _database.CreateUser(request.Name);
    
    private IEnumerable<User> CreateIou(CreateIouRequest request) =>
        _database.CreateIou(request.LenderName, request.BorrowerName, request.Amount);

    private class Database
    {
        private readonly IDictionary<string, User> _users = new SortedDictionary<string, User>();

        public Database(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                _users[user.Name] = user;
            }
        }

        public IEnumerable<User> GetUsers(IEnumerable<string> names) => _users
            .Where(kvp => names.Contains(kvp.Key))
            .Select(kvp => new User(kvp.Value))
            .ToArray();

        public User CreateUser(string name)
        {
            _users[name] = new User
            {
                Name = name
            };

            return _users[name];
        }
        
        public IEnumerable<User> CreateIou(string lenderName, string borrowerName, decimal amount)
        {
            Lend(lenderName, borrowerName, amount);
            Borrow(lenderName, borrowerName, amount);

            return GetUsers(new string[] { lenderName, borrowerName });
        }

        private void Lend(string lenderName, string borrowerName, decimal amount)
        {
            var remaining = amount;
            
            if (_users[lenderName].Owes.ContainsKey(borrowerName))
            {
                remaining = _users[lenderName].Owes[borrowerName] - amount;
                if (remaining > 0)
                {
                    _users[lenderName].Owes[borrowerName] = remaining;
                    return;
                }

                _users[lenderName].Owes.Remove(borrowerName);
                remaining *= -1;
            }

            if (remaining <= 0)
            {
                return;
            }

            if (!_users[lenderName].OwedBy.ContainsKey(borrowerName))
            {
                _users[lenderName].OwedBy[borrowerName] = 0m;
            }

            _users[lenderName].OwedBy[borrowerName] += remaining;
        }
        
        private void Borrow(string lenderName, string borrowerName, decimal amount)
        {
            var remaining = amount;
            
            if (_users[borrowerName].OwedBy.ContainsKey(lenderName))
            {
                remaining = _users[borrowerName].OwedBy[lenderName] - amount;
                if (remaining > 0)
                {
                    _users[borrowerName].OwedBy[lenderName] = remaining;
                    return;
                }

                _users[borrowerName].OwedBy.Remove(lenderName);
                remaining *= -1;
            }

            if (remaining <= 0)
            {
                return;
            }

            if (!_users[borrowerName].Owes.ContainsKey(lenderName))
            {
                _users[borrowerName].Owes[lenderName] = 0m;
            }

            _users[borrowerName].Owes[lenderName] += remaining;
        }
    }

    private class User
    {
        public User() { }

        public User(User user) => (Name, Owes, OwedBy) = (user.Name, new SortedDictionary<string, decimal>(user.Owes), new SortedDictionary<string, decimal>(user.OwedBy));

        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        [JsonPropertyName("owes")]
        public IDictionary<string, decimal> Owes { get; set; } = new SortedDictionary<string, decimal>();

        [JsonPropertyName("owed_by")]
        public IDictionary<string, decimal> OwedBy { get; set; } = new SortedDictionary<string, decimal>();

        [JsonPropertyName("balance")]
        public decimal Balance => OwedBy.Values.Sum() - Owes.Values.Sum();
    }

    private static string SendRequest<TRequest, TResponse>(string payload, Func<TRequest, TResponse> func) where TRequest : new()
    {
        var request = payload is null ?
            new TRequest() :
            JsonSerializer.Deserialize<TRequest>(payload)!;

        var response = func(request);
        
        return JsonSerializer.Serialize(response);
    }
    
    private class GetUsersRequest
    {
        [JsonPropertyName("users")]
        public IEnumerable<string> Names { get; set; }
    }

    private class CreateUserRequest
    {
        [JsonPropertyName("user")]
        public string Name { get; set; }
    }

    private class CreateIouRequest
    {
        [JsonPropertyName("lender")]
        public string LenderName { get; set; }
        
        [JsonPropertyName("borrower")]
        public string BorrowerName { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

    }
}
