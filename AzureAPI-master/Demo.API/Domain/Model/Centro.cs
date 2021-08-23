using System.Collections.Generic;

namespace Demo.API.Domain.Model
{
    public class Centro
    {
        public long IdCentro { get; set; }
        public long CodigoCentro { get; set; }
        public string NomeCentro { get; set; }
        public long IdEndereco { get; set; }

        public List<Endereco> Endereco { get; set; }
    }
}
