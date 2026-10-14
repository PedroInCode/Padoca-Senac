
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Padoca_Senac.Banco;

public static class Banco
{
    private static string stringConnection = "Server=10.91.45.46;Port=3306;Database=padaria_bd;Uid=admin;Pwd=12345678";

    public static void connectAndTest()
    {
        using (var connection = new MySqlConnection(stringConnection))
        {
            try
            {
                Console.WriteLine("Tentando conectar ao banco de dados...");
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