using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Padoca_Senac.banco;

namespace Padoca_Senac.Models;

internal class Cliente
{
    private int _id;
    private string _nome;
    private string _cpf;
    private string _telefone;
    private string _endereco;
    private int _pontos;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public string Cpf { get => _cpf; set => _cpf = value; }
    public string Telefone { get => _telefone; set => _telefone = value; }
    public string Endereco { get => _endereco; set => _endereco = value; }
    public int Pontos { get => _pontos; set => _pontos = value; }

    public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)
    {
        if (nome == "") throw new Exception("O nome do cliente não pode ser vazio!");

        this.Nome = nome;
        this.Cpf = cpf;
        this.Telefone = telefone;
        this.Endereco = endereco;
        this.Pontos = pontos;
    }

    public Cliente(int id, string nome, string cpf, string endereco, string telefone, int pontos)
    {
        this.Id = id;
        this.Nome = nome;
        this.Cpf = cpf;
        this.Endereco = endereco;
        this.Telefone = telefone;
        this.Pontos = pontos;
    }

    public void AdicionarPontos(int pontos)
    {
        this.Pontos += pontos;
    }

    public int Inserir(Cliente cliente)
    {
        // Instrução SQL: Define o comando de inserção (INSERT).
        // O uso de marcações com '@' (ex: @Nome) e uma pratica de seguranca crucial chamada de "Parametrizacao"
        // Ela impede ataques de SQL Injection, pois o banco entende o que é comando e o que é apenas texto do usuário.

        string sql = "INSERT INTO Cliente (`Nome`, `CPF`, `Telefone`, `Endereco`, `Pontos`) VALUES (@Nome, @CPF, @Telefone, @Endereco, @Pontos)";

        // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
        // O 'using' garante que os recursos de memoria ocupados pelo comando sejam liberados ao final do bloco.
        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Dados: Pega as propriedades do objeto C# (cliente.Nome, uuario.Email, etc.)
            // e substitui de forma segura as marcações '@' da string SQL com os valores reais.

            cmd.Parameters.AddWithValue("@Nome",  cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
            cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
            cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
            cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);

            // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
            // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
            // 0 metodo retorna 1 (se o usuario for inserido com sucesso) ou -1 (se houver erro no banco).
            return Banco.ExecutarComando(cmd);
        }
    }

    public static int Deletar(Cliente cliente)
    {
        string cmdSql = "Delete From `Cliente` WHERE `Id` = @id";

        using (var cmd = new MySqlCommand(cmdSql))
        {
            cmd.Parameters.AddWithValue("@id", cliente._id);

            return Banco.ExecutarComando(cmd);
        }
    }

    public static List<Cliente> ListarTodos()
    {
        string cmdSql = "SELECT * FROM `Cliente`";

        List<Cliente> listaClientes = new();

        using(var cmd = new MySqlCommand(cmdSql))
        {
            // Executa a leitura. 0 bloco 'using' aqui é FUNDAMENTAL para garantir que o
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
                    string telefone = ValidarCampo(reader["Telefone"]);
                    string endereco = ValidarCampo(reader["Endereco"]);
                    int pontos = reader.GetInt32("Pontos");

                    // Cria o objeto Cliente com os dados extraidos
                    Cliente cliente = new(id, nome, cpf, endereco, telefone, pontos);

                    // Adiciona este cliente a nossa lista
                    listaClientes.Add(cliente);
                }
            }

            return listaClientes;
        }
    }

    public static Cliente? listarPorId(int id)
    {
        string sql = "SELECT * FROM `Cliente` WHERE `Id` = @Id";

        using(var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Cliente cliente = null;
            while (reader.Read())
            {
                // Extrai os dados da linha atual
                int idBanco = reader.GetInt32("Id");

                string nome = ValidarCampo(reader["Nome"]);

                string cpf = ValidarCampo(reader["CPF"]);
                string telefone = ValidarCampo(reader["Telefone"]);
                string endereco = ValidarCampo(reader["Endereco"]);
                int pontos = reader.GetInt32("Pontos");

                // Cria o objeto Cliente com os dados extraidos
                cliente = new(id, nome, cpf, endereco, telefone, pontos);
            }
            return cliente;
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
