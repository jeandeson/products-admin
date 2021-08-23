using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Demo.API.Domain.Data.Base;
using RauchTech.Common.Configuration;

namespace Demo.API.Domain.Model
{
    public class Produto
    {
        public long IdProduto { get; set; }
        public long CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidadeProduto { get; set; }
        public string ValorProduto { get; set; }
        public long IdCentro { get; set; }
        public string FileID { get; set; }
        public string File { get; private set; }
        public BlobFile BlobFile { get; set; }
        public List<Centro> Centro { get; internal set; }

        public void LoadUrls(IConfiguration config)
        {
            if (!string.IsNullOrEmpty(FileID)) 
            {
                File = config.GetValue("Blob:BaseURL")[0] + FileID;
            }
        }
    }

}
