using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Models;

internal class ProdutoFabricado : Produto
{
    private int _produtoid;
    private string _descricaoProducao;

    public int ProdutoId { get => _produtoid; set => _produtoid = value; } 
    public string DescricaoProdutcao { get => _descricaoProducao; set => _descricaoProducao = value; }

    public ProdutoFabricado(int id, string nome, decimal preco, string categoria, bool fabricado, int marcaid, int produtoid, string descricaoproducao) : base(id, nome, preco, categoria, fabricado, marcaid)
    {
        this.ProdutoId = produtoid;
        this._descricaoProducao = descricaoproducao;
    }
}
