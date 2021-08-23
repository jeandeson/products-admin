namespace Demo.API.Domain.Model
{
    public class Endereco
    {
        public long IdEndereco { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public long Cep { get; set; }
    }
}
