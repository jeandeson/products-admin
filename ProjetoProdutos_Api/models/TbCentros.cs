using System;
using System.Collections.Generic;

namespace ProjetoProdutos_Api
{
    public partial class TbCentros
    {
        public TbCentros()
        {
            TbProdutos = new HashSet<TbProdutos>();
        }

        public int IdCentro { get; set; }
        public int CodigoCentro { get; set; }
        public string NomeCentro { get; set; }
        public int IdEndereco { get; set; }

        public virtual TbEnderecos IdEnderecoNavigation { get; set; }
        public virtual ICollection<TbProdutos> TbProdutos { get; set; }
    }
}
