using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

internal class FornecedorService
{
    public static void MenuFornecedor()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== GESTÃO DE FORNECEDOR ===== ");
            Console.WriteLine("1 - Cadastrar Fornecedor");
            Console.WriteLine("2 - Listar Todos os Fornecedores");
            Console.WriteLine("3 - Buscar Fornecedor por ID");
            Console.WriteLine("4 - Atualizar Fornecedor");
            Console.WriteLine("5 - Excluir Fornecedor");
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
                        CadastrarFornecedor(); break;
                    case "2":
                        ListarFornecedores(); break;
                    case "3":
                        ListarFornecedorPorId(); break;
                    case "4":
                        AtualizarFornecedor(); break;
                    case "5":
                        DeletarFornecedor(); break;
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

    private static void CadastrarFornecedor()
    {
        Console.WriteLine("-=-=-=-=-=- CADASTRAR FORNECEDOR -=-=-=-=-=-");
        Console.Write("Digite o nome do Fornecedor: ");
        string nomeFornecedor = Console.ReadLine()!;

        Console.Write("Digite o telefone do Fornecedor: ");
        string telFornecedor = Console.ReadLine()!;

        Console.Write("Digite a cnpj do Fornecedor: ");
        string cnpjFornecedor = Console.ReadLine()!;


        Fornecedor novoFornecedor = new Fornecedor(nome: nomeFornecedor, telefone : telFornecedor, cnpj: cnpjFornecedor
        );

        int resultadoInsert = Fornecedor.Inserir(novoFornecedor);
        Console.Clear();

        if (resultadoInsert > 0)
            Console.WriteLine($"Fornecedor cadastrado com sucesso!!");
        VoltarParaOMenu();
    }
    private static void ListarFornecedores()
    {
        Console.WriteLine("------ LISTA DE FORNECEDORES ------");

        List<Fornecedor> lista = Fornecedor.ListarFornecedores();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum fornecedor cadastrado no sistema!");
        }

        else
        {
            foreach (var f in lista)
            {
                Console.WriteLine($"ID: {f.Id} | Nome: {f.Nome} | Tel: {f.Telefone} | CNPJ: {f.CNPJ}");
            }
        }

        VoltarParaOMenu();
    }
    private static void ListarFornecedorPorId()
    {
        int idFornecedor;
        Fornecedor? fornecedor = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ BUSCAR FORNECEDOR POR ID ------");
            Console.Write("\nDigite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idFornecedor))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idFornecedor == 0)
            {
                VoltarParaOMenu();
                return;
            }

            fornecedor = Fornecedor.ListarPorId(idFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum fornecedor encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como fornecedor continua null, o loop continuará
            }

            Console.WriteLine($"\nID: {fornecedor.Id} | Nome: {fornecedor.Nome} | Tel: {fornecedor.Telefone} | CNPJ: {fornecedor.CNPJ}");

            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("1 - Buscar outro fornecedor");
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
    private static void AtualizarFornecedor()
    {
        int idFornecedor;
        Fornecedor? fornecedor = null;

        // Loop que continuará rodando enquanto o fornecedor não for encontrado
        while (fornecedor == null)
        {
            Console.Clear();
            Console.WriteLine("------ ATUALIZAR FORNECEDOR ------");
            Console.Write("Digite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idFornecedor))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idFornecedor == 0)
            {
                VoltarParaOMenu();
                return;
            }

            fornecedor = Fornecedor.ListarPorId(idFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum fornecedor encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como fornecedor continua null, o loop continuará
            }
        }

        // Se chegou aqui, o fornecedor foi encontrado com sucesso!
        Console.WriteLine($"\nID: {fornecedor.Id} | Nome: {fornecedor.Nome} | Tel: {fornecedor.Telefone} | CNPJ: {fornecedor.CNPJ}");

        Console.WriteLine("\n-- O QUE VOCÊ DESEJA ALTERAR? --");
        Console.WriteLine("1 - Nome do Fornecedor");
        Console.WriteLine("2 - Telefone do Fornecedor");
        Console.WriteLine("3 - CNPJ do Fornecedor");
        Console.WriteLine("0 - Cancelar");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine()!;
        bool alterado = true;

        switch (opcao)
        {
            case "1":
                Console.Write("Novo Nome: ");
                fornecedor.Nome = Console.ReadLine()!;
                break;

            case "2":
                Console.Write("Novo Telefone: ");
                fornecedor.Telefone = Console.ReadLine()!;
                break;

            case "3":
                Console.Write("Novo CNPJ: ");
                fornecedor.CNPJ = Console.ReadLine()!;
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
            int resultado = Fornecedor.AtualizarFornecedor(fornecedor);

            if (resultado > 0)
                Console.WriteLine("\nFornecedor atualizado com sucesso!");
            else
                Console.WriteLine("\nErro ao atualizar o fornecedor no banco de dados.");
        }

        VoltarParaOMenu();
    }
    private static void DeletarFornecedor()
    {
        int idFornecedor;
        Fornecedor? fornecedor = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ EXCLUINDO FORNECEDOR ------");
            Console.Write("\nDigite o [ID] (ou 0 para cancelar): ");

            if (!int.TryParse(Console.ReadLine(), out idFornecedor))
            {
                Console.WriteLine("\n[ERRO] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            if (idFornecedor == 0)
            {
                VoltarParaOMenu();
                return;
            }

            fornecedor = Fornecedor.ListarPorId(idFornecedor);

            if (fornecedor == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum fornecedor encontrado no sistema!");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            Console.WriteLine($"\nFornecedor encontrado: ID: {fornecedor.Id} | Nome: {fornecedor.Nome}");
            Console.Write("\nTem certeza que deseja excluir este fornecedor? (S/N): ");
            //Trim remove espaços e ToUpper joga tudo pra maiuscula
            string confirmacao = Console.ReadLine()!.Trim().ToUpper();

            if (confirmacao == "S")
            {
                Fornecedor.Deletar(fornecedor);
                Console.WriteLine("\nFornecedor excluído com sucesso!");
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
