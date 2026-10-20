using Padoca_Senac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

internal class FuncionarioService
{
    public static void MenuCliente()
    {
        bool sair = false;

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("===== GESTÃO DO SISTEMA ===== ");
            Console.WriteLine("0 - Sair do Programa");

            Console.WriteLine(" =============================");
            Console.Write("Digite uma opção: ");

            string opcao = Console.ReadLine();
            Console.Clear();

            try
            {
                switch (opcao)
                {
                    
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


    private static void VoltarParaOMenu()
    {
        Console.Write("\nPressione qualquer tecla para voltar ao menu principal:");
        Console.ReadKey();
        Console.Clear();
    }
}
