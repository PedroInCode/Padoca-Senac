using System;
using System.Collections.Generic;
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
}
