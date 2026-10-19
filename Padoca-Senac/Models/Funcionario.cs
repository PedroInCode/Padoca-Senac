using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Padoca_Senac.banco;

namespace Padoca_Senac.Models;

internal class Funcionario
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _cargo;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Cpf { get => _cpf; set => _cpf = value; }
    public string Cargo { get => _cargo; set => _cargo = value; }

    public Funcionario(string nome, string cpf, string cargo)
    {
        if (nome == "") throw new Exception("O nome do funcionario não pode ser vazio!");

        this.Nome = nome;
        this.Cpf = cpf;
        this.Cargo = cargo;
    }

    public Funcionario(int id, string nome, string cpf, string cargo)
    {
        this.Id = id;
        this.Nome = nome;
        this.Cpf = cpf;
        this.Cargo = cargo;
    }

   
    public int Inserir(Funcionario funcionario)
    {
        // Instrução SQL: Define o comando de inserção (INSERT).
        // O uso de marcações com `@` (ex: @Nome) e uma pratica de seguranca crucial chamada de "Parametrizacao"
        // Ela impede ataques de SQL Injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Funcionario (`Nome`, `CPF`, `Cargo`) VALUES (@Nome, @CPF, @Cargo)";

        // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
        // O `using` garante que os recursos de memoria ocupados pelo comando sejam liberados ao final do bloco.
        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, uuario.Email, etc.)
            // e substitui de forma segura as marcações `@` da string SQL com os valores reais.

            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
            

            // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
            // ele envia o comando configurado para o método genérico `executarComando` da classe `Banco`.
            // 0 metodo retorna 1 (se o usuario for inserido com sucesso) ou -1 (se houver erro no banco).
            return Banco.ExecutarComando(cmd);
        }
    }

    public static int Deletar(Funcionario funcionario)
    {
        string cmdSql = "Delete From `Funcionario` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(cmdSql))
        {
            cmd.Parameters.AddWithValue("@Id", funcionario.Id);

            return Banco.ExecutarComando(cmd);
        }
    }

    public static List<Funcionario> ListarTodosFuncionarios()
    {
        string cmdSql = "SELECT * FROM `Funcionario`";

        List<Funcionario> listaFuncionario = new();

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

                    string cpf = ValidarCampo(reader["CPF"]);
                    string cargo = ValidarCampo(reader["Cargo"]);

                    // Cria o objeto Cliente com os dados extraidos
                    Funcionario funcionario = new(id, nome, cpf, cargo);

                    // Adiciona este cliente a nossa lista
                    listaFuncionario.Add(funcionario);
                }
            }

            return listaFuncionario;
        }
    }

    public static Funcionario? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Funcionario` WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Funcionario funcionario = null;
            while (reader.Read())
            {
                // Extrai os dados da linha atual
                int idBanco = reader.GetInt32("Id");

                string nome = ValidarCampo(reader["Nome"]);

                string cpf = ValidarCampo(reader["CPF"]);
                string cargo = ValidarCampo(reader["Cargo"]);

                // Cria o objeto Cliente com os dados extraidos
                funcionario = new(id, nome, cpf, cargo);
            }
            return funcionario;
        }
    }

    public static int AtualizarFuncionario(Funcionario funcionario)
    {
        string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `Cpf` = @CPF, `Cargo` = @Cargo WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
            cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
            cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);

            // A chave de busca: Quem será atualizado?
            cmd.Parameters.AddWithValue("@Id", funcionario.Id);

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
