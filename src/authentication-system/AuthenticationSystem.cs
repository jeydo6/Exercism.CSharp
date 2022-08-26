using System.Collections.Generic;
using System.Collections.ObjectModel;

internal class Authenticator
{
    private class EyeColor
    {
        public const string Blue = "blue";
        public const string Green = "green";
        public const string Brown = "brown";
        public const string Hazel = "hazel";
        public const string Grey = "grey";
    }

    public Authenticator(Identity admin) => _admin = admin;

    private readonly Identity _admin;

    private readonly IDictionary<string, Identity> _developers = new Dictionary<string, Identity>
    {
        ["Bertrand"] = new Identity
        {
            Email = "bert@ex.ism",
            EyeColor = "blue"
        },
        ["Anders"] = new Identity
        {
            Email = "anders@ex.ism",
            EyeColor = "brown"
        }
    };

    public Identity Admin => new Identity(_admin);

    public IDictionary<string, Identity> GetDevelopers() => new ReadOnlyDictionary<string, Identity>(_developers);
}

internal struct Identity
{
    private Identity(string email, string eyeColor) => (Email, EyeColor) = (email, eyeColor);
    
    public Identity(Identity identity) : this (identity.Email, identity.EyeColor) { }
    
    public string Email { get; set; }

    public string EyeColor { get; set; }
}
