namespace API.DTO.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string FirebaseId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateOnly DataCadastro { get; set; }
    public ICollection<ConsumoEnergeticoResponse> ConsumosEnergeticos { get; set; }
}
