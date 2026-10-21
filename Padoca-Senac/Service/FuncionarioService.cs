using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

internal static class FuncionarioService
{
    public static void MenuFuncionario()
    { 
        bool sair = false;
        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== GESTÃO DE FUNCIONARIOS ===== ");
            Console.WriteLine("1 - Cadastrar Funcionaio");
            Console.WriteLine("2 - Listar Todos os Funcionarios");
            Console.WriteLine("3 - Buscar Funcionario por ID");
            Console.WriteLine("4 - Atualizar Funcionario");
            Console.WriteLine("5 - Excluir Funcionario");
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
                        CadastrarFuncionario(); break;
                    case "2":
                        ListarFuncionarios(); break;
                    case "3":
                        ListarFuncionarioPorId(); break;
                    case "4":
                        AtualizarFuncionario(); break;
                    case "5":
                        DeletarFuncionario(); break;
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

    private static void CadastrarFuncionario()
    {
        Console.WriteLine("-=-=-=-=-=- CADASTRAR Funcionario -=-=-=-=-=-");
        Console.Write("Digite o nome do Funcionario: ");
        string nomeFuncionario = Console.ReadLine()!;

        Console.Write("Digite o cpf do Funcionario: ");
        string cpfFuncionario = Console.ReadLine()!;

        Console.Write("Digite o cargo do Funcionario: ");
        string cargoFuncionario = Console.ReadLine()!;

        Funcionario novoFuncionario = new Funcionario(nome : nomeFuncionario, cpf : cpfFuncionario, cargo : cargoFuncionario
        );

        int resultadoInsert = Funcionario.Inserir(novoFuncionario);
        Console.Clear();

        if (resultadoInsert > 0)
            Console.WriteLine($"Funcionario cadastrado com sucesso!!");
        VoltarParaOMenu();
    }
    private static void ListarFuncionarios()
    {
        Console.WriteLine("------ LISTA DE Funcionarios ------");

        List<Funcionario> lista = Funcionario.ListarTodosFuncionarios();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum funcionario cadastrado no sistema!");
        }

        else
        {
            foreach (var f in lista)
            {
                Console.WriteLine($"ID: {f.Id}");
                Console.WriteLine($"Nome: {f.Nome} - CPF: {f.Cpf} - Cargo: {f.Cargo}");
            }
        }

        VoltarParaOMenu();
    }
    private static void ListarFuncionarioPorId()
    {
        int idFunc;
        Funcionario? funcionario = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ BUSCAR FUNCIONARIO POR ID ------");
            Console.Write("\nDigite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idFunc))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idFunc == 0)
            {
                VoltarParaOMenu();
                return;
            }

            funcionario = Funcionario.ListarPorId(idFunc);

            if (funcionario == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum funcionario encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como funcionario continua null, o loop continuará
            }

            Console.WriteLine($"\nID: {funcionario.Id} | Nome: {funcionario.Nome} | CPF: {funcionario.Cpf} | Cargo: {funcionario.Cargo}");

            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("1 - Buscar outro funcionario");
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
    private static void AtualizarFuncionario()
    {
        int idFunc;
        Funcionario? funcionario = null;

        // Loop que continuará rodando enquanto o cliente não for encontrado
        while (funcionario == null)
        {
            Console.Clear();
            Console.WriteLine("------ ATUALIZAR FUNCIONARIO ------");
            Console.Write("Digite o [ID] (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idFunc))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idFunc == 0)
            {
                VoltarParaOMenu();
                return;
            }

            funcionario = Funcionario.ListarPorId(idFunc);

            if (funcionario == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum funcionario encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como funcionario continua null, o loop continuará
            }
        }

        // Se chegou aqui, o funcionario foi encontrado com sucesso!
        Console.WriteLine($"\nID: {funcionario.Id} | Nome: {funcionario.Nome} | CPF: {funcionario.Cpf} | Cargo: {funcionario.Cargo}");

        Console.WriteLine("\n-- O QUE VOCÊ DESEJA ALTERAR? --");
        Console.WriteLine("1 - Nome do Funcionario");
        Console.WriteLine("2 - Cpf");
        Console.WriteLine("3 - Cargo");
        Console.WriteLine("0 - Cancelar");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine()!;
        bool alterado = true;

        switch (opcao)
        {
            case "1":
                Console.Write("Novo Nome: ");
                funcionario.Nome = Console.ReadLine()!;
                break;

            case "2":
                Console.Write("Novo CPF: ");
                funcionario.Cpf = Console.ReadLine()!;
                break;

            case "3":
                Console.Write("Novo Cargo: ");
                funcionario.Cargo = Console.ReadLine()!;
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
            int resultado = Funcionario.AtualizarFuncionario(funcionario);

            if (resultado > 0)
                Console.WriteLine("\nFuncionario atualizado com sucesso!");
            else
                Console.WriteLine("\nErro ao atualizar o funcionario no banco de dados.");
        }

        VoltarParaOMenu();
    }
    private static void DeletarFuncionario()
    {
        int idFunc;
        Funcionario? funcionario = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ EXCLUINDO FUNCIONARIO ------");
            Console.Write("\nDigite o [ID] (ou 0 para cancelar): ");

            if (!int.TryParse(Console.ReadLine(), out idFunc))
            {
                Console.WriteLine("\n[ERRO] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            if (idFunc == 0)
            {
                VoltarParaOMenu();
                return;
            }

            funcionario = Funcionario.ListarPorId(idFunc);

            if (funcionario == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum funcionario encontrado no sistema!");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            Console.WriteLine($"\nFuncionario encontrado: ID: {funcionario.Id} | Nome: {funcionario.Nome}");
            Console.Write("\nTem certeza que deseja excluir este funcionario? (S/N): ");
            //Trim remove espaços e ToUpper joga tudo pra maiuscula
            string confirmacao = Console.ReadLine()!.Trim().ToUpper();

            if (confirmacao == "S")
            {
                Funcionario.Deletar(funcionario);
                Console.WriteLine("\nFuncionario excluído com sucesso!");
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

