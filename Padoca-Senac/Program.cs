using Padoca_Senac.Banco;
using Padoca_Senac.Models;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Cliente cliente1 = new Cliente(nome: "", cpf: "1234", telefone: "131313", endereco: "Lula", pontos: 100);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
