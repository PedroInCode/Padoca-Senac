using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class Marca
{
    private int _id;
    private string _nome;
    private string _telefone;


    public int Id { get => _id; set => _id = value;}
    public string Nome { get => _nome; set => _nome = value;}
    public string Telefone { get => _telefone; set => _telefone = value;}

    public Marca(int id, string nome, string telefone)
    {
        this._id = id;
        this._nome = nome;
        this._telefone = telefone;
    }

    public Marca(string nome, string telefone)
    {
        this._nome = nome;
        this.Telefone = telefone;
    }

    public int Inserir(Marca marca)
    {
        string sql = "INSERT INTO Marca (`Nome`, `Telefone`) VALUES (@Nome, @Telefone)";

        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Dados: Pega as propriedades do objeto C#
            // e substitui de forma segura as marcações `@` da string SQL com os valores reais.

            cmd.Parameters.AddWithValue("@Nome", marca.Nome);
            cmd.Parameters.AddWithValue("@CPF", marca.Telefone);


            // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
            // ele envia o comando configurado para o método genérico `executarComando` da classe `Banco`.
            // 0 metodo retorna 1 (se a marca for inserida com sucesso) ou -1 (se houver erro no banco).
            return Banco.ExecutarComando(cmd);
        }
    }

    public static int Deletar(Marca marca)
    {
        string sql = "DELETE FROM `Marca` WHERE `Id` = @Id";

        using(var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("id", marca.Id);
            return Banco.ExecutarComando(cmd);
        }
    }

    public static List<Marca> ListarMarcas()
    {
        string cmdSql = "SELECT * FROM `Marca`";

        List<Marca> listaMarcas = new();

        using (var cmd = new MySqlCommand(cmdSql))
        {
            // Executa a leitura. 0 bloco `using` aqui é FUNDAMENTAL para garantir que o
            // reader seja fechado no final, o que por sua vez fecha a conexao com o banco.
            using (MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd))
            {
                // O while vai rodar uma vez para CADA linha que existir na tabela Cliente
                while (reader.Read())
                {
                    // Extrai os dados da linha atual
                    int id = reader.GetInt32("Id");

                    string nome = ValidarCampo(reader["Nome"]);

                    string telefone = ValidarCampo(reader["Telefone"]);

                    // Cria o objeto Cliente com os dados extraidos
                    Marca marca = new(id, nome, telefone);

                    // Adiciona este cliente a nossa lista
                    listaMarcas.Add(marca);
                }
            }

            return listaMarcas;
        }
    }

    public static Marca? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Marca` WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Marca marca = null;

            while (reader.Read())
            {
                // Extrai os dados da linha atual
                int idBanco = reader.GetInt32("Id");
                string nome = ValidarCampo(reader["Nome"]);
                string telefone = ValidarCampo(reader["Telefone"]);

                // Cria o objeto Cliente com os dados extraidos
                marca = new(id, nome, telefone);
            }
            return marca;
        }
    }

    public static int AtualizarMarca(Marca marca)
    {
        string sql = "UPDATE `Marca` SET `Nome` = @Nome, `Telefone` = @Telefone WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", marca.Nome);
            cmd.Parameters.AddWithValue("@CPF", marca.Telefone);

            // A chave de busca: Quem será atualizado?
            cmd.Parameters.AddWithValue("@Id", marca.Id);

            // Execução: O UPDATE também é uma instrução de ação (como INSERT e DELETE).
            // Ele vai retornar 1 (se encontrou  o ID e atualizou com sucesso) ou 0 (se o ID não existia).
            return Banco.ExecutarComando(cmd);
        }
    }

    private static string ValidarCampo(object campo)
    {
        if (campo == DBNull.Value || campo == null)
        {
            return "";
        }
        else
        {
            return campo.ToString();
        }
    }
}
