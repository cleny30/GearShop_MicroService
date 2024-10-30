using BusinessObject.Models.Entity;
using System.Text.Json.Serialization;

namespace BusinessObject.Model.Entity;

public class DeliveryAddressModel
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Fullname { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string? Specific { get; set; } = null!;

    public bool IsDefault { get; set; }
}