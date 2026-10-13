
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Padoca_Senac.Banco;

public static class Banco
{
    private static readonly string stringConnection =
        new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build()
            .GetConnectionString("Padoca")
        ?? throw new InvalidOperationException(
            "String de conexão não encontrada nos User Secrets.");

    public static void connectAndTest()
    {
        using (var connection = new MySqlConnection(stringConnection))
        {
            try
            {
                connection.Open();

                using (var command = new MySqlCommand(
                    "SELECT VERSION();", connection))
                {
                    string? version = command.ExecuteScalar()?.ToString();
                    Console.WriteLine($"Versão do MariaDB: {version}");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao conectar no banco de dados!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}