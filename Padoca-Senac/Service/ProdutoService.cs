using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

internal class ProdutoService
{
    public static void MenuProduto()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== GESTÃO DE PRODUTOS ===== ");
            Console.WriteLine("1 - Cadastrar Produto");
            Console.WriteLine("2 - Listar Todos os Produtos");
            Console.WriteLine("3 - Buscar Produto por ID");
            Console.WriteLine("4 - Atualizar Produto");
            Console.WriteLine("5 - Excluir Produto");
            Console.WriteLine("0 - Sair do Programa");

            Console.WriteLine(" =============================");
            Console.Write("Digite uma opção: ");

            string opcao = Console.ReadLine();
            Console.Clear();

            try
            {
                switch (opcao)
                {
                    case "1":
                        CadastrarProduto(); break;
                    case "2":
                        ListarProdutos(); break;
                    case "3":
                        ListarProdutoPorId(); break;
                    case "4":
                        AtualizarProduto(); break;
                    case "5":
                        DeletarProduto(); break;
                    case "0":
                        sair = true;
                        Console.WriteLine("Encerrando o programa..."); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    private static void CadastrarProduto()
    {
        Console.WriteLine("-=-=-=-=-=- CADASTRAR PRODUTO -=-=-=-=-=-");
        Console.Write("Digite o Nome do produto: ");
        string nomeProduto = Console.ReadLine()!;

        Console.Write("Digite o Preco do produto: ");
        decimal precoProduto = decimal.Parse(Console.ReadLine()!);

        Console.Write("Digite a Categoria do produto: ");
        string categoriaProduto = Console.ReadLine()!;

        Console.Write("Digite se o Produto foi Fabricado (true/false): ");
        bool fabricadoProduto = bool.Parse(Console.ReadLine()!);

        Console.Write("Digite o [ID] da marca: ");
        int idmarca = int.Parse(Console.ReadLine()!);

        Produto novoProduto = new Produto(nome : nomeProduto, preco : precoProduto, categoria : categoriaProduto, fabricado : fabricadoProduto, marcaid :idmarca
        );

        int resultadoInsert = Produto.Inserir(novoProduto);
        Console.Clear();

        if (resultadoInsert > 0)
            Console.WriteLine($"Produto cadastrado com sucesso!!");
        VoltarParaOMenu();
    }
    private static void ListarProdutos()
    {
        Console.WriteLine("------ LISTA DE PRODUTOS ------");

        List<Produto> lista = Produto.ListarProdutos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado no sistema!");
        }

        else
        {
            Console.WriteLine("----------------------------------------------------------------\n");
            foreach (var p in lista)
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preco: {p.Preco} | Categoria: {p.Categoria} | Fabricado: {p.Fabricado} | Preco: {p.MarcaId}");
                Console.WriteLine("\n----------------------------------------------------------------");
            }
        }

        VoltarParaOMenu();
    }
    private static void ListarProdutoPorId()
    {
        int idProduto;
        Produto? produto = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ BUSCAR PRODUTO POR ID ------");
            Console.Write("\nDigite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idProduto))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idProduto == 0)
            {
                VoltarParaOMenu();
                return;
            }

            produto = Produto.ListarPorId(idProduto);

            if (produto == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum produto encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como fornecedor continua null, o loop continuará
            }

            Console.WriteLine($"ID: {produto.Id} | Nome: {produto.Nome} | Preco: {produto.Preco} | Categoria: {produto.Categoria} | Fabricado: {produto.Fabricado} | MarcaId: {produto.MarcaId}");

            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("1 - Buscar outro Produto");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine()!;

            if (opcao == "0")
            {
                VoltarParaOMenu();
                return; // Sai do método e encerra
            }
            // Se digitar qualquer outra coisa ou "1", o loop `while (true)` roda de novo e limpa a tela para a próxima busca!
        }
    }
    private static void AtualizarProduto()
    {
        int idProduto;
        Produto? produto = null;

        // Loop que continuará rodando enquanto a marca não for encontrada
        while (produto == null)
        {
            Console.Clear();
            Console.WriteLine("------ ATUALIZAR PRODUTO ------");
            Console.Write("Digite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idProduto))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idProduto == 0)
            {
                VoltarParaOMenu();
                return;
            }

            produto = Produto.ListarPorId(idProduto);

            if (produto == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum produto encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como marca continua null, o loop continuará
            }
        }

        // Se chegou aqui, a marca foi encontrada com sucesso!
        Console.WriteLine($"ID: {produto.Id} | Nome: {produto.Nome} | Preco: {produto.Preco} | Categoria: {produto.Categoria} | Fabricado: {produto.Fabricado} | MarcaId: {produto.MarcaId}");

        Console.WriteLine("\n-- O QUE VOCÊ DESEJA ALTERAR? --");
        Console.WriteLine("1 - Nome do Produto");
        Console.WriteLine("2 - Preco do Produto");
        Console.WriteLine("3 - Categoria do Produto");
        Console.WriteLine("4 - Status do Produto Fabricado");
        Console.WriteLine("5 - Marca do Produto");
        Console.WriteLine("0 - Cancelar");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine()!;
        bool alterado = true;

        switch (opcao)
        {
            case "1":
                Console.Write("Novo Nome: ");
                produto.Nome = Console.ReadLine()!;
                break;

            case "2":
                Console.Write("Novo Preco: ");
                produto.Preco = decimal.Parse(Console.ReadLine()!);
                break;

            case "3":
                Console.Write("Nova Categoria: ");
                produto.Categoria = Console.ReadLine()!;
                break;

            case "4":
                Console.Write("Novo status de Fabricação (true/false): ");
                produto.Fabricado = bool.Parse(Console.ReadLine()!);
                break;

            case "5":
                Console.Write("Nova marca do Produto [IDMARCA]: ");
                produto.MarcaId = int.Parse(Console.ReadLine()!);
                break;

            case "0":
                Console.WriteLine("Operação cancelada.");
                alterado = false;
                break;

            default:
                Console.WriteLine("Opção inválida!");
                alterado = false;
                break;
        }

        if (alterado)
        {
            int resultado = Produto.AtualizarProduto(produto);

            if (resultado > 0)
                Console.WriteLine("\nProduto atualizado com sucesso!");
            else
                Console.WriteLine("\nErro ao atualizar o produto no banco de dados.");
        }

        VoltarParaOMenu();
    }
    private static void DeletarProduto()
    {
        int idProduto;
        Produto? produto = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ EXCLUINDO PRODUTO ------");
            Console.Write("\nDigite o [ID] (ou 0 para cancelar): ");

            if (!int.TryParse(Console.ReadLine(), out idProduto))
            {
                Console.WriteLine("\n[ERRO] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            if (idProduto == 0)
            {
                VoltarParaOMenu();
                return;
            }

            produto = Produto.ListarPorId(idProduto);

            if (produto == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum produto encontrado no sistema!");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            Console.WriteLine($"\nProduto encontrado: ID: {produto.Id} | Nome: {produto.Nome}");
            Console.Write("\nTem certeza que deseja excluir este fornecedor? (S/N): ");
            //Trim remove espaços e ToUpper joga tudo pra maiuscula
            string confirmacao = Console.ReadLine()!.Trim().ToUpper();

            if (confirmacao == "S")
            {
                Produto.Deletar(produto);
                Console.WriteLine("\nProduto excluído com sucesso!");
                break;
            }
            else
            {
                Console.WriteLine("\nExclusão cancelada pelo usuário.");
                PressionarParaTentarNovamente();
            }
        }

        VoltarParaOMenu();
    }

    //Perfumaria
    private static void VoltarParaOMenu()
    {
        Console.Write("\nPressione qualquer tecla para voltar ao menu principal:");
        Console.ReadKey();
        Console.Clear();
    }

    // Método auxiliar apenas para pausar antes de limpar a tela no loop
    private static void PressionarParaTentarNovamente()
    {
        Console.Write("\nPressione qualquer tecla para tentar novamente...");
        Console.ReadKey();
    }
}
