using System;
using System.Collections.Generic;

namespace ProjetoProdutos_Api
{
    public partial class TbClientes
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string SobrenomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public int IdEndereco { get; set; }

        public virtual TbEnderecos IdEnderecoNavigation { get; set; }
    }
}
