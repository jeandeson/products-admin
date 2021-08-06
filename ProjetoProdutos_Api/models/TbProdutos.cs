using System;
using System.Collections.Generic;

namespace ProjetoProdutos_Api
{
    public partial class TbProdutos
    {
        public int IdProduto { get; set; }
        public int CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidadeProduto { get; set; }
        public string ValorProduto { get; set; }
        public string ImagemProduto { get; set; }
        public int IdCentro { get; set; }

        public virtual TbCentros IdCentroNavigation { get; set; }
    }
}
