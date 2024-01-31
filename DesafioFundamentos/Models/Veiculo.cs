using System;

namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        public string Placa { get; set; }
        public int HorasEstacionado { get; set; }
        public TipoVeiculo Tipo { get; set; }
        public decimal PrecoHora { get; set; }
    }

    public enum TipoVeiculo
    {
        Carro,
        Moto,
        Caminhao
    }
}
