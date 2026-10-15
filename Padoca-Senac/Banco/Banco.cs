
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Padoca_Senac.banco;

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

    //Declaração do método: público  e estático, retorna um número inteiro (linhas afetadas) e recebe um objeto MySqlCommand com a instrução SQL já configurada como parâmetro
    public static int executarComanda(MySqlCommand cmd)
    {
        //O bloco using garante que a coneção será automaticamente fechada e liberada da memória ao final do bloco, mesmo que ocorram erros no meio do processo.

        //Cria a conexão utilizando a variavel de ambiente
        using (var conexao = new MySqlConnection(stringConnection))
        {
           //Vinculação: Diz ao comando SQL qm qual conexão ele deve ser executado.
            cmd.Connection = conexao;

            //Tratamento de erros: Inicia um bloco de tentativa para proteger o programa de falhas abruptas.
            try
            {
                // Abertura da conexão: Estabelece efetivamente a comunicação com o servidor Mysql
                conexao.Open();

                // Executa o comando SQL (Insert, Update ou Delete) no banco de dados.
                // Retorna um número inteiro correspondente ao total de linhas que foram modificadas.
                return cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no Banco de dados: {ex.Message}");
                return -1;
            }
        }
    }
}