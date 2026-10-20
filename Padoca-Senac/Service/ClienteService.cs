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
                        CadastrarCliente();
                        break;
                    case "2":
                        ListarClientes();
                        break;
                    case "3":
                        ListarClientePorId();
                        break;
                }
            }
            catch (Exception ex)
            {

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
    }
    private static void ListarClientes()
    {
        Console.WriteLine("------ LISTA DE CLIENTES ------");

        List<Cliente> lista = Cliente.ListarTodos();

        if(lista.Count == 0) 
            Console.WriteLine("Nenhum cliente cadastrado no sistema!");
        else
        {
            foreach(var cliente in lista)
            {
                Console.WriteLine($"ID: {cliente.Id}");
                Console.WriteLine($"Nome: {cliente.Nome} - Pontos: {cliente.Pontos}");
            }
        }

        Console.Write("\nPressione qualquer tecla para voltar ao menu principal:");
        Console.ReadKey();
        Console.Clear();

        
    }
    private static void ListarClientePorId()
    {
        Console.WriteLine("------ BUSCAR CLIENTE POR ID ------");
        Console.Write("\nDigite o Id do Cliente: ");
        int idCliente = Int32.Parse(Console.ReadLine());

        var clienteBusca = Cliente.ListarPorId(idCliente);

        if (clienteBusca == null)
            Console.WriteLine("Error: Nenhum cliente encontrado no sistema!");
        else
            Console.WriteLine($"ID: {clienteBusca.Id}");
            Console.WriteLine($"Nome: {clienteBusca.Nome} - Pontos: {clienteBusca.Pontos}");

        Console.Write("\nPressione qualquer tecla para voltar ao menu principal:");
        Console.ReadKey();
        Console.Clear();
    }
}
