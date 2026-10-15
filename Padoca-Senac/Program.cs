using Padoca_Senac.banco;
using Padoca_Senac.Models;

internal class Program
{
    static void Main(string[] args)
    {
        Cliente cliente1 = new Cliente(
                nome: "Yvan Jesus",
                cpf: "11122233745",
                telefone: "111111111",
                endereco: "Rua do SENAC",
                pontos: 100
            );

        cliente1.Inserir(cliente1);

    }
}
