using System;
using System.Collections.Generic;
using System.IO;

namespace DesafioFundamentos.Models
{
     public class Estacionamento
    {
        private decimal precoCarro = 0;
        private decimal precoMoto = 0;
        private decimal precoCaminhao = 0;
        private List<Veiculo> veiculos = new List<Veiculo>();
        private string caminhoArquivo = "dados.txt";

        public Estacionamento()
        {
            InicializarEstacionamento();
            CarregarDados();
        }

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            ConfigurarPrecos(precoInicial, precoPorHora);
            CarregarDados();
        }

        private void InicializarEstacionamento()
        {
            Console.WriteLine("Digite o valor fixo:");
            while (!decimal.TryParse(Console.ReadLine(), out precoCarro) || precoCarro < 0)
            {
                Console.WriteLine("Valor fixo inválido. Por favor, insira um valor numérico válido e maior ou igual a zero.");
            }

            Console.WriteLine("Digite o valor por hora (carro e moto):");
            while (!decimal.TryParse(Console.ReadLine(), out precoMoto) || precoMoto < 0)
            {
                Console.WriteLine("Valor por hora (carro e moto) inválido. Por favor, insira um valor numérico válido e maior ou igual a zero.");
            }

            Console.WriteLine("Digite o valor por hora para caminhão:");
            while (!decimal.TryParse(Console.ReadLine(), out precoCaminhao) || precoCaminhao < 0)
            {
                Console.WriteLine("Valor por hora para caminhão inválido. Por favor, insira um valor numérico válido e maior ou igual a zero.");
            }

            Console.Clear();
            Console.WriteLine("Configurações do estacionamento:");
            Console.WriteLine($"Valor fixo: {precoCarro}");
            Console.WriteLine($"Valor por hora (carro e moto): {precoMoto}");
            Console.WriteLine($"Valor por hora (caminhão): {precoCaminhao}");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();

            if (!string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("Digite o tipo de veículo (carro, moto, caminhao):");
                string tipo = Console.ReadLine();

                decimal precoHora = 0;

                switch (tipo.ToLower())
                {
                    case "carro":
                        precoHora = precoCarro;
                        break;
                    case "moto":
                        precoHora = precoMoto;
                        break;
                    case "caminhao":
                        precoHora = precoCaminhao;
                        break;
                    default:
                        Console.WriteLine("Tipo de veículo inválido. Não foi possível adicionar o veículo.");
                        return;
                }

                Veiculo novoVeiculo = new Veiculo { Placa = placa, Tipo = (TipoVeiculo)Enum.Parse(typeof(TipoVeiculo), tipo, true), PrecoHora = precoHora };
                veiculos.Add(novoVeiculo);
                Console.WriteLine($"Veículo com placa {placa} adicionado ao estacionamento.");
                SalvarDados();
            }
            else
            {
                Console.WriteLine("Placa inválida. Não foi possível adicionar o veículo.");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = Console.ReadLine();

            if (veiculos.Exists(x => x.Placa.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                if (int.TryParse(Console.ReadLine(), out int horas))
                {
                    
                    decimal valorTotal = CalcularValorTotal(horas, placa);

                    
                    veiculos.RemoveAll(x => x.Placa.ToUpper() == placa.ToUpper());

                    Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
                    SalvarDados();
                }
                else
                {
                    Console.WriteLine("Quantidade de horas inválida. Não foi possível calcular o valor.");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Verifique se digitou a placa corretamente");
            }
        }

        private decimal CalcularValorTotal(int horas, string placa)
        {
            Veiculo veiculo = veiculos.Find(x => x.Placa.ToUpper() == placa.ToUpper());
            decimal valorTotal = veiculo.PrecoHora + (veiculo.PrecoHora * horas);

            
            AplicarDescontoAcrecimoTipoVeiculo(ref valorTotal, veiculo.Tipo, horas);

            return valorTotal;
        }

        private void AplicarDescontoAcrecimoTipoVeiculo(ref decimal valorTotal, TipoVeiculo tipo, int horas)
        {
            switch (tipo)
            {
                case TipoVeiculo.Carro:
                    
                    break;

                case TipoVeiculo.Moto:
                    decimal descontoMoto = CalcularDescontoMoto(horas);
                    valorTotal = valorTotal - (valorTotal * descontoMoto);
                    break;

                case TipoVeiculo.Caminhao:
                    decimal acrescimoCaminhao = CalcularAcrescimoCaminhao(horas);
                    valorTotal = valorTotal + (valorTotal * acrescimoCaminhao);
                    break;

                default:
                    Console.WriteLine("Tipo de veículo não reconhecido.");
                    break;
            }
        }

        private decimal CalcularDescontoMoto(int horas)
        {
            decimal descontoBase = 0.15m;

            if (horas > 10)
            {
                return descontoBase + 0.1m; 
            }

            return descontoBase;
        }

        private decimal CalcularAcrescimoCaminhao(int horas)
        {
            decimal acrescimoBase = 0.15m;

            if (horas > 10)
            {
                return acrescimoBase + 0.1m; 
            }

            return acrescimoBase;
        }

        private void SalvarDados()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(caminhoArquivo))
                {
                    foreach (var veiculo in veiculos)
                    {
                        writer.WriteLine($"{veiculo.Placa}|{veiculo.Tipo}|{veiculo.PrecoHora.ToString("0.00")}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
            }
        }

        private void CarregarDados()
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    veiculos.Clear();

                    foreach (string line in File.ReadLines(caminhoArquivo))
                    {
                        string[] partes = line.Split('|');
                        if (partes.Length == 3)
                        {
                            Veiculo veiculo = new Veiculo
                            {
                                Placa = partes[0],
                                Tipo = (TipoVeiculo)Enum.Parse(typeof(TipoVeiculo), partes[1], true),
                                PrecoHora = decimal.TryParse(partes[2], out decimal preco) ? preco : 0
                            };
                            veiculos.Add(veiculo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
            }
        }

        public void ListarVeiculos()
        {
            Console.WriteLine("Lista de Veículos Estacionados:");
            foreach (var veiculo in veiculos)
            {
                Console.WriteLine($"Placa: {veiculo.Placa}, Tipo: {veiculo.Tipo}, Preço por Hora: R$ {veiculo.PrecoHora.ToString("0.00")}");
            }
        }

        public void LimparDados()
        {
            veiculos.Clear();
            SalvarDados();
        }

        public void ConfigurarPrecos(decimal precoInicial, decimal precoPorHora)
        {
            if (ValidarPreco(precoInicial) && ValidarPreco(precoPorHora))
            {
                this.precoCarro = precoInicial;
                this.precoMoto = precoPorHora;
                this.precoCaminhao = precoPorHora;
            }
            else
            {
                Console.WriteLine("Preços inválidos. Certifique-se de inserir valores não negativos.");
            }
        }

        private bool ValidarPreco(decimal preco)
        {
            return preco >= 0;
        }
    }
}