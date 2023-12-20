namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();
            
            //verifica se o valor atribuído a placa foi nulo ou se é válido
        if (!string.IsNullOrEmpty(placa))
        {
            veiculos.Add(placa);
            Console.WriteLine($"Veículo com placa {placa} adicionado ao estacionamento.");
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

    // Verifica se o veículo existe
    if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
    {
        Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

        if (int.TryParse(Console.ReadLine(), out int horas))
        {
            // Calculando o valor total
            decimal valorTotal = precoInicial + (precoPorHora * horas);
            
            //removendo da lista
            veiculos.Remove(placa);

            Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
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

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                
                foreach (var placa in veiculos)
                {
                    Console.WriteLine(placa);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
