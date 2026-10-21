using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

internal class MarcaService
{
    public static void MenuMarca()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== GESTÃO DE MARCAS ===== ");
            Console.WriteLine("1 - Cadastrar Marca");
            Console.WriteLine("2 - Listar Todos as Marcas");
            Console.WriteLine("3 - Buscar Marca por ID");
            Console.WriteLine("4 - Atualizar Marca");
            Console.WriteLine("5 - Excluir Marca");
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
                        CadastrarMarca(); break;
                    case "2":
                        ListarMarcas(); break;
                    case "3":
                        ListarMarcaPorId(); break;
                    case "4":
                        AtualizarMarca(); break;
                    case "5":
                        DeletarMarcas(); break;
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

    private static void CadastrarMarca()
    {
        Console.WriteLine("-=-=-=-=-=- CADASTRAR MARCA -=-=-=-=-=-");
        Console.Write("Digite o nome do Marca: ");
        string nomeMarca = Console.ReadLine()!;

        Console.Write("Digite o telefone do Marca: ");
        string telMarca = Console.ReadLine()!;


        Marca novaMarca = new Marca(nome: nomeMarca, telefone: telMarca
        );

        int resultadoInsert = Marca.Inserir(novaMarca);
        Console.Clear();

        if (resultadoInsert > 0)
            Console.WriteLine($"Marca cadastrada com sucesso!!");
        VoltarParaOMenu();
    }
    private static void ListarMarcas()
    {
        Console.WriteLine("------ LISTA DE MARCAS ------");

        List<Marca> lista = Marca.ListarMarcas();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhuma marca cadastrada no sistema!");
        }

        else
        {
            foreach (var m in lista)
            {
                Console.WriteLine($"ID: {m.Id} | Nome: {m.Nome} | Tel: {m.Telefone}");
            }
        }

        VoltarParaOMenu();
    }
    private static void ListarMarcaPorId()
    {
        int idMarca;
        Marca? marca = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ BUSCAR MARCA POR ID ------");
            Console.Write("\nDigite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idMarca))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idMarca == 0)
            {
                VoltarParaOMenu();
                return;
            }

            marca = Marca.ListarPorId(idMarca);

            if (marca == null)
            {
                Console.WriteLine("\n[ERRO] Nenhuma marca encontrada com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como fornecedor continua null, o loop continuará
            }

            Console.WriteLine($"\nID: {marca.Id} | Nome: {marca.Nome} | Tel: {marca.Telefone}");

            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("1 - Buscar outra marca");
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
    private static void AtualizarMarca()
    {
        int idMarca;
        Marca? marca = null;

        // Loop que continuará rodando enquanto a marca não for encontrada
        while (marca == null)
        {
            Console.Clear();
            Console.WriteLine("------ ATUALIZAR MARCA ------");
            Console.Write("Digite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idMarca))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idMarca == 0)
            {
                VoltarParaOMenu();
                return;
            }

            marca = Marca.ListarPorId(idMarca);

            if (marca == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum marca encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como marca continua null, o loop continuará
            }
        }

        // Se chegou aqui, a marca foi encontrada com sucesso!
        Console.WriteLine($"\nID: {marca.Id} | Nome: {marca.Nome} | Tel: {marca.Telefone}");

        Console.WriteLine("\n-- O QUE VOCÊ DESEJA ALTERAR? --");
        Console.WriteLine("1 - Nome da Marca");
        Console.WriteLine("2 - Telefone da Marca");
        Console.WriteLine("0 - Cancelar");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine()!;
        bool alterado = true;

        switch (opcao)
        {
            case "1":
                Console.Write("Novo Nome: ");
                marca.Nome = Console.ReadLine()!;
                break;

            case "2":
                Console.Write("Novo Telefone: ");
                marca.Telefone = Console.ReadLine()!;
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
            int resultado = Marca.AtualizarMarca(marca);

            if (resultado > 0)
                Console.WriteLine("\nMarca atualizada com sucesso!");
            else
                Console.WriteLine("\nErro ao atualizar a marca no banco de dados.");
        }

        VoltarParaOMenu();
    }
    private static void DeletarMarca()
    {
        int idMarca;
        Marca? marca = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ EXCLUINDO MARCA ------");
            Console.Write("\nDigite o [ID] (ou 0 para cancelar): ");

            if (!int.TryParse(Console.ReadLine(), out idMarca))
            {
                Console.WriteLine("\n[ERRO] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            if (idMarca == 0)
            {
                VoltarParaOMenu();
                return;
            }

            marca = Marca.ListarPorId(idMarca);

            if (marca == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum marca encontrado no sistema!");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            Console.WriteLine($"\nMarca encontrada: ID: {marca.Id} | Nome: {marca.Nome}");
            Console.Write("\nTem certeza que deseja excluir este fornecedor? (S/N): ");
            //Trim remove espaços e ToUpper joga tudo pra maiuscula
            string confirmacao = Console.ReadLine()!.Trim().ToUpper();

            if (confirmacao == "S")
            {
                Marca.Deletar(marca);
                Console.WriteLine("\nMarca excluída com sucesso!");
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
