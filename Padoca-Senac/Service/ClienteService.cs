using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Services;

internal class ClienteService
{
    public static void MenuCliente()
    {
        bool sair = false;

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== GESTÃO DE CLIENTES ===== ");
            Console.WriteLine("1 - Cadastrar Cliente");
            Console.WriteLine("2 - Listar Todos os Clientes");
            Console.WriteLine("3 - Buscar Cliente por ID");
            Console.WriteLine("4 - Atualizar Cliente");
            Console.WriteLine("5 - Excluir Cliente");
            Console.WriteLine("6 - Visualizar pontuação");
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
                        CadastrarCliente(); break;
                    case "2":
                        ListarClientes(); break;
                    case "3":
                        ListarClientePorId(); break;
                    case "4":
                        AtualizarCliente(); break;
                    case "5":
                        DeletarCliente(); break;
                    case "6":
                        VisualizarPontuacao(); break;
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

    private static void CadastrarCliente()
    {
        Console.WriteLine("-=-=-=-=-=- CADASTRAR CLIENTE -=-=-=-=-=-");
        Console.Write("Digite o nome do cliente: ");
        string nomeCliente = Console.ReadLine()!;

        Console.Write("Digite o endereço do cliente: ");
        string enderecoCliente = Console.ReadLine()!;

        Console.Write("Digite o cpf do cliente: ");
        string cpfCliente = Console.ReadLine()!;

        Console.Write("Digite o telefone do cliente: ");
        string telefoneCliente = Console.ReadLine()!;

        Console.Write("Digite a quantidade de pontos: ");
        int qtdPontos = int.Parse(Console.ReadLine()!);

        Cliente novoCliente = new Cliente(
           nome: nomeCliente, cpf: cpfCliente, telefone: telefoneCliente,
           endereco: enderecoCliente, pontos: qtdPontos
        );

        int resultadoInsert = Cliente.Inserir(novoCliente);
        Console.Clear();

        if (resultadoInsert > 0)
            Console.WriteLine($"Cliente cadastrado com sucesso!!");
        VoltarParaOMenu();
    }
    private static void ListarClientes()
    {
        Console.WriteLine("------ LISTA DE CLIENTES ------");

        List<Cliente> lista = Cliente.ListarTodos();

        if(lista.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado no sistema!");
        }
            
        else
        {
            foreach(var cliente in lista)
            {
                Console.WriteLine($"ID: {cliente.Id}");
                Console.WriteLine($"Nome: {cliente.Nome} - Pontos: {cliente.Pontos}");
            }
        }

        VoltarParaOMenu();
    }
    private static void ListarClientePorId()
    {
        int idCli;
        Cliente? cliente = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ BUSCAR CLIENTE POR ID ------");
            Console.Write("\nDigite o Id do Cliente (ou 0 para sair): ");

            if(!int.TryParse(Console.ReadLine(), out idCli))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idCli == 0)
            {
                VoltarParaOMenu();
                return;
            }

            cliente = Cliente.ListarPorId(idCli);

            if (cliente == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum cliente encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como cliente continua null, o loop continuará
            }

            // Se chegou aqui, o cliente foi encontrado com sucesso!
            Console.WriteLine($"\nID: {cliente.Id} | Nome: {cliente.Nome} | Pontos: {cliente.Pontos} | Tel: {cliente.Telefone} | Endereço: {cliente.Endereco}");

            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("1 - Buscar outro cliente");
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
    private static void AtualizarCliente()
    {
        int idCli;
        Cliente? cliente = null;

        // Loop que continuará rodando enquanto o cliente não for encontrado
        while (cliente == null)
        {
            Console.Clear();
            Console.WriteLine("------ ATUALIZAR CLIENTE ------");
            Console.Write("Digite o Id do Cliente (ou 0 para sair): ");

            if (!int.TryParse(Console.ReadLine(), out idCli))
            {
                Console.WriteLine("\n[ERROR] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o início do while
            }

            // Permite ao usuário desistir e voltar ao menu
            if (idCli == 0)
            {
                VoltarParaOMenu();
                return;
            }

            cliente = Cliente.ListarPorId(idCli);

            if (cliente == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum cliente encontrado com esse ID no sistema!");
                PressionarParaTentarNovamente();
                // Como cliente continua null, o loop continuará
            }
        }

        // Se chegou aqui, o cliente foi encontrado com sucesso!
        Console.WriteLine($"\nID: {cliente.Id} | Nome: {cliente.Nome} | Pontos: {cliente.Pontos} | Tel: {cliente.Telefone} | Endereço: {cliente.Endereco}");

        Console.WriteLine("\n-- O QUE VOCÊ DESEJA ALTERAR? --");
        Console.WriteLine("1 - Nome do Cliente");
        Console.WriteLine("2 - Pontos");
        Console.WriteLine("3 - Telefone");
        Console.WriteLine("4 - Endereço");
        Console.WriteLine("0 - Cancelar");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine()!;
        bool alterado = true;

        switch (opcao)
        {
            case "1":
                Console.Write("Novo Nome: ");
                cliente.Nome = Console.ReadLine()!;
                break;

            case "2":
                Console.Write("Nova quantidade de pontos: ");
                if (int.TryParse(Console.ReadLine(), out int novosPontos))
                {
                    cliente.Pontos = novosPontos;
                }
                else
                {
                    Console.WriteLine("Valor de pontos inválido!");
                    alterado = false;
                }
                break;

            case "3":
                Console.Write("Novo Telefone: ");
                cliente.Telefone = Console.ReadLine()!;
                break;

            case "4":
                Console.Write("Novo Endereço: ");
                cliente.Endereco = Console.ReadLine()!;
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
            int resultado = Cliente.AtualizarCliente(cliente);

            if (resultado > 0)
                Console.WriteLine("\nCliente atualizado com sucesso!");
            else
                Console.WriteLine("\nErro ao atualizar o cliente no banco de dados.");
        }

        VoltarParaOMenu();
    }
    private static void DeletarCliente()
    {
        int idCli;
        Cliente? cliente = null;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------ EXCLUINDO CLIENTE ------");
            Console.Write("\nDigite o Id do Cliente (ou 0 para cancelar): ");

            if(!int.TryParse(Console.ReadLine(), out idCli))
            {
                Console.WriteLine("\n[ERRO] ID inválido! Digite apenas números.");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            if(idCli == 0)
            {
                VoltarParaOMenu();
                return;
            }

            cliente = Cliente.ListarPorId(idCli);

            if(cliente == null)
            {
                Console.WriteLine("\n[ERRO] Nenhum cliente encontrado no sistema!");
                PressionarParaTentarNovamente();
                continue; // Volta para o topo do loop
            }

            Console.WriteLine($"\nCliente encontrado: ID: {cliente.Id} | Nome: {cliente.Nome}");
            Console.Write("\nTem certeza que deseja excluir este cliente? (S/N): ");
            //Trim remove espaços e ToUpper joga tudo pra maiuscula
            string confirmacao = Console.ReadLine()!.Trim().ToUpper();

            if(confirmacao == "S")
            {
                Cliente.Deletar(cliente);
                Console.WriteLine("\nCliente excluído com sucesso!");
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
    private static void VisualizarPontuacao()
    {
        Console.WriteLine("------ PONTUAÇÃO DOS CLIENTES ------");

        List<Cliente> lista = Cliente.ListarTodos();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado no sistema!");
        }
        else
        {
            int opcao = 0;
            bool opcaoValida = false;

            // Loop para garantir que o usuário digite uma opção numérica válida (1 ou 2)
            while (!opcaoValida)
            {
                Console.WriteLine("\nComo deseja ordenar a pontuação?");
                Console.WriteLine("1 - Crescente (Menor para Maior)");
                Console.WriteLine("2 - Decrescente (Maior para Menor)");
                Console.Write("Escolha uma opção: ");

                // Valida se o que foi digitado é um número inteiro
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("\n[ERRO] Opção inválida! Digite apenas números.");
                    PressionarParaTentarNovamente();
                    Console.Clear();
                    Console.WriteLine("------ PONTUAÇÃO DOS CLIENTES ------");
                    continue;
                }

                // Valida se o número digitado está entre as opções esperadas (1 ou 2)
                if (opcao == 1 || opcao == 2)
                {
                    opcaoValida = true;
                }
                else
                {
                    Console.WriteLine("\n[ERRO] Escolha apenas 1 ou 2.");
                    PressionarParaTentarNovamente();
                    Console.Clear();
                    Console.WriteLine("------ PONTUAÇÃO DOS CLIENTES ------");
                }
            }


            // Usei IEnumrable para guardar a regra de como ordenar, a ordenação de fato so acontece no momento exato em que o foreach começa a rodar. Isso poupa muito processamento!
            IEnumerable<Cliente> clientesOrdenados; // Variável para armazenar a lista ordenada

            if (opcao == 1)
            {
                // Do menor para o maior
                clientesOrdenados = lista.OrderBy(cliente => cliente.Pontos);
            }
            else
            {
                // Do maior para o menor 
                clientesOrdenados = lista.OrderByDescending(cliente => cliente.Pontos);
            }

            Console.WriteLine("\n------------------------------------");
            foreach (var cliente in clientesOrdenados)
            {
                Console.WriteLine($"{cliente.Nome} - Quantidade de Pontos: {cliente.Pontos}");
            }
            Console.WriteLine("------------------------------------");
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
