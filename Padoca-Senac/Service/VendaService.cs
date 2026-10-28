using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padoca_Senac.Service;

public static class VendaService
{
    public static void MenuVendas()
    {
        bool sair = false;
        while (!sair)
        {
            Console.Clear();

            Console.WriteLine("-=-=-=-= PDV - Padaria Pão & Cia =-=-=-=-");
            Console.WriteLine("\n1 - Registrar Venda");
            Console.WriteLine("0 - Voltar ao Menu Principal");
            Console.Write("\nEscolha uma opção: ");

            string opcao = Console.ReadLine();

            try
            {
                switch (opcao)
                {
                    case "1":
                        RegistrarVenda();
                        break;
                    case "0":
                        sair = true;
                        return;
                    default:
                        PressionarParaVoltar();
                        MenuVendas();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro na venda: {ex.Message}");
                PressionarParaVoltar();
            }
            
        }
    }

    public static void RegistrarVenda()
    {
        
    }

    public static void PressionarParaVoltar()
    {
        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }
} 
