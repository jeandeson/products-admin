namespace Demo.API.Domain.Model
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string SobrenomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public long IdEndereco { get; set; }
        public object Endereco { get; internal set; }
    }
}
