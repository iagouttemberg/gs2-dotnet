namespace Database.Models;

public class Usuario
{
    public int Id { get; set; }

    public string FirebaseId { get; set; }
    
    public string Nome { get; set; }
    
    public string Email { get; set; }

    public DateOnly DataCadastro { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    public ICollection<ConsumoEnergetico> ConsumosEnergeticos { get; set; } = new List<ConsumoEnergetico>();
}