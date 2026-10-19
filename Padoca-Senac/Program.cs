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


        //Cliente cli01 = Cliente.listarPorId(1);
        //if (cli01 != null)
        //{
        //    Console.WriteLine($"ID: {cli01.Id}");
        //    Console.WriteLine($"Nome: {cli01.Nome}");
        //    Console.WriteLine($"CPF: {cli01.Cpf}");
        //    Console.WriteLine($"Endereco: {cli01.Endereco}");
        //    Console.WriteLine($"Telefone: {cli01.Telefone}");
        //    Console.WriteLine($"Pontos: {cli01.Pontos}");
        //}
        //else
        //{
        //    Console.WriteLine("O cliente não foi encontrado!");
        //}

        //Funcionario? funcionario1 = Funcionario.ListarPorId(1);

        //if (funcionario1 != null)
        //{
        //    funcionario1.Nome = "Pedro Capixaba";
        //    funcionario1.Cpf = "43999977734";
        //    funcionario1.Cargo = "Atendente";

        //    int resultado = Funcionario.AtualizarFuncionario(funcionario1);

        //    if (resultado > 0)
        //    {
        //        Console.WriteLine("Funcionario atualizado com sucesso!");
        //    }
        //}

        //Funcionario? funcionarioPesquisa = Funcionario.ListarPorId(1);
        //if (funcionarioPesquisa != null)
        //{
        //    Console.WriteLine($"ID: {funcionarioPesquisa.Id}");
        //    Console.WriteLine($"Nome: {funcionarioPesquisa.Nome}");
        //    Console.WriteLine($"CPF: {funcionarioPesquisa.Cpf}");
        //    Console.WriteLine($"Cargo: {funcionarioPesquisa.Cargo}");
        //}
        //else
        //{
        //    Console.WriteLine("O cliente não foi encontrado!");
        //}

        //Funcionario funcionario = new("Yvan six seven", "87665432112", "Farmador de aura");

        //var testesla = Funcionario.ListarPorId(3);

        //Funcionario.Deletar(testesla);

        List<Funcionario> funcionario3 = Funcionario.ListarTodosFuncionarios();

        foreach (Funcionario func in funcionario3)
        {
            Console.WriteLine($"ID: {func.Id}");
            Console.WriteLine($"Nome: {func.Nome}");
            Console.WriteLine($"CPF: {func.Cpf}");
            Console.WriteLine($"Endereco: {func.Cargo}");
        }
    }
}
