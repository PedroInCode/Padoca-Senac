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
            Console.WriteLine("Digite uma opção: ");

            string opcao = Console.ReadLine();
            Console.Clear();
        }
    }
}
