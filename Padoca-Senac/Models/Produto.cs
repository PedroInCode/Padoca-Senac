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
    private int _fabricado;
    private int _marcaid;
    private string _nomeMarca;

    public int Id { get => _id; set => _id = value; }
    public string Nome { get => _nome; set => _nome = value; }
    public decimal Preco { get => _preco; set => _preco = value; }
    public string Categoria { get => _categoria; set => _categoria = value; }
    public int Fabricado { get => _fabricado; set => _fabricado = value; }
    public int MarcaId { get => _marcaid; set => _marcaid = value; }
    public string NomeMarca { get => _nomeMarca; }

    public Produto(int id, string nome, decimal preco, string categoria, int fabricado, int marcaid)
    {
        this._id = id;
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaid = marcaid;
        this._nomeMarca = NomeMarca;
    }

    public Produto(string nome, decimal preco, string categoria, int fabricado, int marcaid)
    {
        this._nome = nome;
        this._preco = preco;
        this._categoria = categoria;
        this._fabricado = fabricado;
        this._marcaid = marcaid;
        this._nomeMarca = NomeMarca;
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

                    int fabricado = reader.GetInt32("Fabricado");

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

    public static List<Produto> ListarTodosProdutosComMarca()
    {
        string cmdSql = "SELECT p.Id, p.Nome, p.Preco, p.Categoria, p.Fabricado,m.Id as MarcaId, m.Nome AS marcaNome FROM `Produto` as `p` INNER JOIN `Marca` as `m` ON `p`.`MarcaId` = `m`.`Id`;";

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

                    int fabricado = reader.GetInt32("Fabricado");

                    int marcaid = reader.GetInt32("MarcaId");

                    string nomeMarca = ValidarCampo(reader["MarcaNome"]);

                    // Cria o objeto Cliente com os dados extraidos
                    Produto produto = new(id, nome, preco, categoria, fabricado, marcaid);
                    produto._nomeMarca = nomeMarca;

                    // Adiciona este cliente a nossa lista
                    listaProdutos.Add(produto);
                }
            }

            return listaProdutos;
        }
    }

    //public static List<ProdutoFabricado> ListarProdutosFabricados()
    //{
    //    string sql = @"SELECT p.Id, p.Nome, p.Preco, p.Categoria, p.Fabricado, p.MarcaId, pf.ProdutoId, pf.DescricaoProducao FROM Produto AS p INNER JOIN ProdutoFabricado AS pf ON p.Id = pf.ProdutoId WHERE p.Fabricado = 1";

    //    List<ProdutoFabricado> listaProdutos = new();

    //    using (var cmd = new MySqlCommand(sql))
    //    {
    //        using (MySqlDataReader reader =
    //            Banco.ExecutarComRetornoDeDados(cmd))
    //        {
    //            while (reader.Read())
    //            {
    //                int id = reader.GetInt32("Id");
    //                string nome = ValidarCampo(reader["Nome"]);
    //                decimal preco = reader.GetDecimal("Preco");
    //                string categoria = ValidarCampo(reader["Categoria"]);
    //                bool fabricado = reader.GetBoolean("Fabricado");
    //                int marcaId = reader.GetInt32("MarcaId");

    //                int produtoId = reader.GetInt32("ProdutoId");
    //                string nomeMarca = ValidarCampo(reader["MarcaNome"]);
    //                string descricao = ValidarCampo(reader["DescricaoProducao"]);

    //                ProdutoFabricado produto = new(
    //                    id,
    //                    nome,
    //                    preco,
    //                    categoria,
    //                    fabricado,
    //                    marcaId,
    //                    produtoId,
    //                    descricao
    //                );

    //                produto._nomeMarca = nomeMarca;

    //                listaProdutos.Add(produto);
    //            }
    //        }

    //        return listaProdutos;
    //    }
    //}

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
                int fabricado = reader.GetInt32("Fabricado");
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
