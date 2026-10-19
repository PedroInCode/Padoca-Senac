using Padoca_Senac.banco;
using Padoca_Senac.Models;

internal class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Digite o nome do cliente: ");
        //string nomeCliente = Console.ReadLine()!;

        //Console.WriteLine("Digite o endereço do cliente: ");
        //string enderecoCliente = Console.ReadLine()!;

        //Console.WriteLine("Digite o cpf do cliente: ");
        //string cpfCliente = Console.ReadLine()!;

        //Console.WriteLine("Digite o telefone do cliente: ");
        //string telefoneCliente = Console.ReadLine()!;

        //Console.WriteLine("Digite a quantidade de pontos: ");
        //int qtdPontos = int.Parse(Console.ReadLine()!);

        //Cliente cliente1 = new Cliente(
        //        nome: nomeCliente,
        //        cpf: cpfCliente,
        //        telefone: telefoneCliente,
        //        endereco: enderecoCliente,
        //        pontos: qtdPontos
        //    );

        //cliente1.Inserir(cliente1);

        //List<Cliente> clientes = Cliente.ListarTodos();

        //foreach(Cliente cliente in clientes)
        //{
        //    Console.WriteLine($"ID: {cliente.Id}");
        //    Console.WriteLine($"Nome: {cliente.Nome}");
        //    Console.WriteLine($"CPF: {cliente.Cpf}");
        //    Console.WriteLine($"Endereco: {cliente.Endereco}");
        //    Console.WriteLine($"Telefone: {cliente.Telefone}");
        //    Console.WriteLine($"Pontos: {cliente.Pontos}");
        //}


        Cliente cli01 = Cliente.listarPorId(1);
        if (cli01 != null)
        {
            Console.WriteLine($"ID: {cli01.Id}");
            Console.WriteLine($"Nome: {cli01.Nome}");
            Console.WriteLine($"CPF: {cli01.Cpf}");
            Console.WriteLine($"Endereco: {cli01.Endereco}");
            Console.WriteLine($"Telefone: {cli01.Telefone}");
            Console.WriteLine($"Pontos: {cli01.Pontos}");
        }
        else
        {
            Console.WriteLine("O cliente não foi encontrado!");
        }

    }
}
