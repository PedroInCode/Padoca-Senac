using MySqlConnector;
using Padoca_Senac.banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

public class Compra
{
    private int _id;
    private DateTime _dataHora;
    private int _funcionarioId;
    private int _clienteId;
    private List<ItemCompra> _itens = new();

    public int Id { get => _id; set => _id = value; }
    public DateTime DataHora { get => _dataHora; set => _dataHora = value; }
    public int FuncionarioId { get => _funcionarioId; set => _funcionarioId = value; }
    public int ClienteId { get => _clienteId; set => _clienteId = value; }

    public Compra(DateTime dataHora, int funcionarioId, int clienteId)
    {
        if (funcionarioId <= 0) throw new ArgumentOutOfRangeException("O funcionario responsavel é inválido");
        

        this._dataHora = dataHora;
        this._funcionarioId = funcionarioId;
        this._clienteId = clienteId;
    }

    public Compra(int id, DateTime dataHora, int funcionarioId, int clienteId, List<ItemCompra> itens) : this(dataHora, funcionarioId, clienteId)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException("ID da compra invalido");

        this._id = id;
    }

    public decimal CalcularTotal()
    {
        return _itens.Sum(it => it.CalcularSubtotal());
    }

    public void AdicionarItem(Produto produto, int quantidade)
    {
        if (produto == null) throw new Exception("Produto Inexistente");

        ItemCompra novoItem = new ItemCompra(produto.Id, quantidade, produto.Preco);
        this._itens.Add(novoItem);
    }

    public void RemoverItem(ItemCompra item)
    {
        this._itens.Remove(item);
    }

    public static int Inserir(Compra compra)
    {
        if (compra.ClienteId <= 0) compra.ClienteId = 1000;

        using(var cmd = new MySqlCommand("sp_InserirCompra"))
        {
            cmd.Parameters.AddWithValue("p_DataHora", compra.DataHora);
            cmd.Parameters.AddWithValue("p_FuncionarioId", compra.FuncionarioId);
            cmd.Parameters.AddWithValue("p_ClienteId", compra.ClienteId);

            object resultado = Banco.ExecutarComandoComRetornoId(cmd);

            if(resultado != null && int.TryParse(resultado.ToString(), out int idGerado))
            {
                compra.Id = idGerado;
                return idGerado;
            }
            return -1;
        }

        
    }
}
