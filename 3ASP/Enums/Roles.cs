using System.Text.Json.Serialization;

namespace _3ASP.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Roles
{
    User,
    Admin,
}