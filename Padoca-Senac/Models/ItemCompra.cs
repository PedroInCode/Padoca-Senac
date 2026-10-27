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

    //construtor base para validação e criação de um novo item em memória
    public ItemCompra(int produtoId, int quantidadeItem, decimal precoCobrado)
    {
        if (produtoId <= 0) throw new ArgumentOutOfRangeException("Produto Inválido");
        if (quantidadeItem <= 0) throw new ArgumentOutOfRangeException("A quantidade deve ser maior que 0.");
        if (precoCobrado <= 0) throw new ArgumentOutOfRangeException("O preço cobrado deve ser maior que 0.");

        this._produtoId = produtoId;
        this._quantidadeItem = quantidadeItem;
        this._precoCobrado = precoCobrado;
    }

    //Contrutor completo (chamado ao ler da base de dados) 
    public ItemCompra(int id, int compraId, int produtoId, int quantidadeItem, decimal precoCobrado) : this(produtoId, quantidadeItem, precoCobrado)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException("Id do item inválido");
        if (compraId <= 0) throw new ArgumentOutOfRangeException("Id da compra inválido");
        
        this._id = id;
        this._compraId = compraId;
    }
}
