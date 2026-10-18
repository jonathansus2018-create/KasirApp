namespace KasirApp.Models
{
    public interface IAuditable
    {
        bool Stsrc { get; set; }
        DateTime Datein { get; set; }
        string Userin { get; set; }
        DateTime? Dateup { get; set; }
        string? Userup { get; set; }
    }
}