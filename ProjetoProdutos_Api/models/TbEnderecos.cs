using System;
using System.Collections.Generic;

namespace ProjetoProdutos_Api
{
    public partial class TbEnderecos
    {
        public TbEnderecos()
        {
            TbCentros = new HashSet<TbCentros>();
            TbClientes = new HashSet<TbClientes>();
        }

        public int IdEndereco { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Cep { get; set; }

        public virtual ICollection<TbCentros> TbCentros { get; set; }
        public virtual ICollection<TbClientes> TbClientes { get; set; }
    }
}
