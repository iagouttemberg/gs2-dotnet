namespace Database.Models;

public class ConsumoEnergetico
{
    public int Id { get; set; }
    
    public string Mes { get; set; }
    
    public int Ano { get; set; }
    
    public double ConsumoKWh { get; set; }  
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}