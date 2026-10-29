using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

public static class VendaService
{
    public static void MenuVendas()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();

            Console.WriteLine("-=-=-=-= PDV - Padaria Pão & Cia =-=-=-=-");
            Console.WriteLine("\n1 - Registrar Venda");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine();

            try
            {
                switch (opcao)
                {
                    case "1":
                        RegistrarVenda();
                        break;
                    case "0":
                        sair = true;
                        return;
                    default:
                        PressionarParaVoltar();
                        MenuVendas();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro na venda: {ex.Message}");
                PressionarParaVoltar();
            }
            
        }
    }

    public static void RegistrarVenda()
    {
        Console.WriteLine("----- NOVA VENDA -----");

        int funcionarioid = 2;

        Console.Write("Id do cliente (Pressione ENTER se não quiser se identificar): ");
        int idCliente = 0;
        int.TryParse(Console.ReadLine(), out idCliente);

        Cliente? clienteVenda = Cliente.ListarPorId(idCliente);

        Compra compraAtiva = new(DateTime.Now, funcionarioid, idCliente);

        var adicionarMais = true;

        while (adicionarMais)
        {
            Console.WriteLine($"\n--- Adicionar Produto ---");
            Console.WriteLine($"\nID do produto ou '0' para finalizar/pagar");

            int.TryParse(Console.ReadLine(), out int idProduto);

            if(idProduto == 0)
            {
                adicionarMais = false;
                continue;
            }

            Produto? produto = Produto.ListarPorId(idProduto)!;

            if(produto == null)
            {
                Console.WriteLine("Produto não encontrado... Tente Novamente");
                continue;
            }

            Console.Write($"[ {produto.Nome} | {produto.Preco} ] - Unidades: ");
            int.TryParse(Console.ReadLine(), out int qtdProduto);

            if(qtdProduto > 0)
            {
                compraAtiva.AdicionarItem(produto, qtdProduto);
                Console.WriteLine($"Subtotal: {produto.Preco * qtdProduto}");
            }

        }

        if(compraAtiva.Itens.Count == 0)
        {
            Console.WriteLine("\nVenda cancelada (Nenhum item foi adicionado).");
        }
        else
        {
            Console.WriteLine("\n----- RESUMO DA VENDA -----");
            foreach(var item in compraAtiva.Itens)
            {
                Console.WriteLine($"ID: {item.ProdutoId}" +
                    $"| Qtd: {item.QuantidadeItem} " +
                    $"| Preço Unit: {item.PrecoCobrado} " +
                    $"| Subtotal: {item.CalcularSubtotal():C}");
            }

            decimal totalFinal = compraAtiva.CalcularTotal();

            Console.WriteLine("------------------------------");
            Console.WriteLine($"TOTAL A PAGAR: {totalFinal}");
            Console.WriteLine("------------------------------");
        }

    }

    public static void PressionarParaVoltar()
    {
        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
} 
