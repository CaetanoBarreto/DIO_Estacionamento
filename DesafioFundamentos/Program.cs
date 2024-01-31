using DesafioFundamentos.Models;
using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        decimal precoInicial = 0;
        decimal precoPorHora = 0;
        Estacionamento es = null;

        Console.WriteLine("Seja bem-vindo ao sistema de estacionamento!\n");

        while (precoInicial <= 0)
        {
            Console.WriteLine("Digite o preço inicial:");
            if (!decimal.TryParse(Console.ReadLine(), out precoInicial) || precoInicial <= 0)
            {
                Console.WriteLine("Preço inicial inválido. Por favor, insira um valor numérico válido e maior que zero.");
            }
        }

        while (precoPorHora <= 0)
        {
            Console.WriteLine("Agora, digite o preço por hora:");
            if (!decimal.TryParse(Console.ReadLine(), out precoPorHora) || precoPorHora <= 0)
            {
                Console.WriteLine("Preço por hora inválido. Por favor, insira um valor numérico válido e maior que zero.");
            }
        }

        
        es = new Estacionamento(precoInicial, precoPorHora);

        string opcao = string.Empty;
        bool exibirMenu = true;

        while (exibirMenu)
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Limpar Lista de Veículos");
            Console.WriteLine("5 - Encerrar");

            switch (Console.ReadLine())
            {
                case "1":
                    es.AdicionarVeiculo();
                    break;

                case "2":
                    es.RemoverVeiculo();
                    break;

                case "3":
                    es.ListarVeiculos();
                    break;

                case "4":
                    es.LimparDados();
                    Console.WriteLine("Dados limpos.");
                    break;

                case "5":
                    exibirMenu = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadLine();
        }

        Console.WriteLine("O programa se encerrou");
    }
}
