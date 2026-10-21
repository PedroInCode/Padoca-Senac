using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class Produto
{
    private int _id;
    private string _nome;
    private decimal _preco;
    private string _categoria;
    private bool _fabricado;
    private int _marcaid;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public decimal Preco { get => _preco; set => _preco = value; }
    public string Categoria { get => _categoria; set => _categoria = value; }
    public bool Fabricado { get => _fabricado; set => _fabricado = value; }
    public int MarcaId { get => _marcaid; set => _marcaid = value; }

    public Produto(int id, string nome, decimal preco, string categoria, bool fabricado, int marcaid)
    {
        this._id = id;
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaid = marcaid;
    }

    public Produto(string nome, decimal preco, string categoria, bool fabricado, int marcaid)
    {
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaid = marcaid;
    }

    public static int Inserir(Produto produto)
    {
        string sql = "INSERT INTO Produto (`Nome`, `Preco`, `Categoria`, `Fabricado`, `MarcaId`) VALUES (@Nome, @Preco, @Categoria, @Fabricado, @MarcaId)";

        using (var cmd = new MySqlCommand(sql))
        {
            // Vinculação de Dados: Pega as propriedades do objeto C#
            // e substitui de forma segura as marcações `@` da string SQL com os valores reais.

            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@Preco", produto.Preco);
            cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
            cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
            cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);


            // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
            // ele envia o comando configurado para o método genérico `executarComando` da classe `Banco`.
            // 0 metodo retorna 1 (se a marca for inserida com sucesso) ou -1 (se houver erro no banco).
            return Banco.ExecutarComando(cmd);
        }
    }

    public static int Deletar(Produto produto)
    {
        string sql = "DELETE FROM `Produto` WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("id", produto.Id);
            return Banco.ExecutarComando(cmd);
        }
    }

    public static List<Produto> ListarProdutos()
    {
        string cmdSql = "SELECT * FROM `Produto`";

        List<Produto> listaProdutos = new();

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

                    decimal preco = reader.GetDecimal("Preco");
                    //int pontos = reader.GetInt32("Pontos");

                    string categoria = ValidarCampo(reader["Categoria"]);

                    bool fabricado = reader.GetBoolean("Fabricado");

                    int marcaid = reader.GetInt32("MarcaId");

                    // Cria o objeto Cliente com os dados extraidos
                    Produto produto = new(id, nome, preco, categoria, fabricado, marcaid );

                    // Adiciona este cliente a nossa lista
                    listaProdutos.Add(produto);
                }
            }

            return listaProdutos;
        }
    }

    public static Produto? ListarPorId(int id)
    {
        string sql = "SELECT * FROM `Produto` WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Id", id);

            MySqlDataReader reader = Banco.ExecutarComRetornoDeDados(cmd);

            Produto produto = null;

            while (reader.Read())
            {
                // Extrai os dados da linha atual
                int idBanco = reader.GetInt32("Id");
                string nome = ValidarCampo(reader["Nome"]);
                decimal preco = reader.GetDecimal("Preco");
                string categoria = ValidarCampo(reader["Categoria"]);
                bool fabricado = reader.GetBoolean("Fabricado");
                int marcaid = reader.GetInt32("MarcaId");

                // Cria o objeto Cliente com os dados extraidos
                produto = new(id, nome, preco, categoria, fabricado, marcaid);
            }
            return produto;
        }
    }

    public static int AtualizarProduto(Produto produto)
    {
        string sql = "UPDATE `Produto` SET `Nome` = @Nome, `Preco` = @Preco, `Categoria` = @Categoria, `Fabricado` = @Fabricado, `MarcaId` = @MarcaId WHERE `Id` = @Id";

        using (var cmd = new MySqlCommand(sql))
        {
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@Preco", produto.Preco);
            cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
            cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
            cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);

            // A chave de busca: Quem será atualizado?
            cmd.Parameters.AddWithValue("@Id", produto.Id);

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
