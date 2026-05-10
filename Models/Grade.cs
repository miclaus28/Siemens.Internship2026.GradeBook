using System.Text.Json.Serialization;

namespace Siemens.Internship2026.GradeBook.Models;

public class Grade
{
    public int Id { get; set; }

    [JsonPropertyName("value")]
    public decimal Score { get; set; }

    public bool IsActive { get; set; } = true;
}