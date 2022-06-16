using System.Text;

internal static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        var sb = new StringBuilder();
        
        sb.Append(id != null ? $"[{id}] - " : "");
        sb.Append($"{name} - ");
        sb.Append(department != null ? department.ToUpper() : "OWNER");

        return sb.ToString();
    }
}
