using Database.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Database;

public class UsuarioRepositoryTest
{
    private readonly DbContextOptions<OracleDbContext> _options;

    public UsuarioRepositoryTest()
    {
        // Configura um banco de dados em memória para os testes
        _options = new DbContextOptionsBuilder<OracleDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task Add_ShouldAddEntityToDatabase()
    {
        try
        {
            // Arrange
            using (var context = new OracleDbContext(_options))
            {
                var repository = new Repository<Usuario>(context);
                var usuario = new Usuario { FirebaseId = "UID1",Nome = "Test User", Email = "test@example.com"};

                // Act
                await repository.AddAsync(usuario);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new OracleDbContext(_options))
            {
                var count = context.Set<Usuario>().Count();
                Assert.Equal(1, count); // Verifica se um usuário foi adicionado

                var addedUser = context.Set<Usuario>().First();
                Assert.Equal("UID1", addedUser.FirebaseId);
                Assert.Equal("Test User", addedUser.Nome); // Verifica se os dados estão corretos
                Assert.Equal("test@example.com", addedUser.Email);
            }
        }
        catch (Exception ex)
        {
            // Adiciona logging para qualquer exceção que ocorra
            Console.WriteLine($"Erro durante o teste: {ex.Message}");
            throw;
        }
    }

}