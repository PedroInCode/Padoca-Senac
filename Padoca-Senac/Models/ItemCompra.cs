using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

public class ItemCompra
{
    private int _id;
    private int _compraId;
    private int _produtoId;
    private int _quantidadeItem;
    private decimal _precoCobrado;

    public int Id { get => _id; set => _id = value; }
    public int CompraId { get => _compraId; set => _compraId = value; }
    public int ProdutoId { get => _produtoId; set => _produtoId = value; }
    public int QuantidadeItem { get => _quantidadeItem; set => _quantidadeItem = value; }
    public decimal PrecoCobrado { get => _precoCobrado; set => _precoCobrado = value; }


    //construtor base para validação e criação de um novo item em memória
    public ItemCompra(int produtoId, int quantidadeItem, decimal precoCobrado)
    {
        if (produtoId <= 0) throw new ArgumentOutOfRangeException("Produto Inválido");
        if (quantidadeItem <= 0) throw new ArgumentOutOfRangeException("A quantidade deve ser maior que 0.");
        if (precoCobrado <= 0) throw new ArgumentOutOfRangeException("O preço cobrado deve ser maior que 0.");

        this.ProdutoId = produtoId;
        this.QuantidadeItem = quantidadeItem;
        this.PrecoCobrado = precoCobrado;
    }

    //Contrutor completo (chamado ao ler da base de dados) 
    public ItemCompra(int id, int compraId, int produtoId, int quantidadeItem, decimal precoCobrado) : this(produtoId, quantidadeItem, precoCobrado)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException("Id do item inválido");
        if (compraId <= 0) throw new ArgumentOutOfRangeException("Id da compra inválido");
        
        this.Id = id;
        this.CompraId = compraId;
    }

    public decimal CalcularSubtotal()
    {
        return this.PrecoCobrado * this.QuantidadeItem;
    }

    public int Inserir(ItemCompra itemCompra)
    {
        using (var cmd = new MySqlCommand("sp_InserirItemCompra"))
        {
            //Configura o comando como StoredProcedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("p_CompraID", itemCompra.CompraId);
            cmd.Parameters.AddWithValue("p_ProdutoID", itemCompra.ProdutoId);
            cmd.Parameters.AddWithValue("p_Quantidade", itemCompra.QuantidadeItem);
            cmd.Parameters.AddWithValue("p_PrecoCobradro", itemCompra.PrecoCobrado);

            return Banco.ExecutarComando(cmd);
        }
    }
}
