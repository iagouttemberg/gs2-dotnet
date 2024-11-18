namespace API.DTO.Request;

public class ConsumoEnergeticoRequest
{
    public string Mes { get; set; }
    
    public int Ano { get; set; }
    
    public double ConsumoKWh { get; set; }  
    
    public int UsuarioId { get; set; }
}