namespace Database.Models;

public class DicaEconomia
{
    public int Id { get; set; }
    
    public string Titulo { get; set; }
    
    public string Descricao { get; set; }
    
    public DateOnly DataCriacao { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}