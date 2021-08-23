using Demo.API.Domain.Data.Repository;
using Demo.API.Domain.Model;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Service
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly CentroService _centroService;
        private readonly BlobFileService _blobFileService;

        public ProdutoService(ProdutoRepository produtoRepository, CentroService centroService, BlobFileService blobFileService)
        {
            _produtoRepository = produtoRepository;
            _centroService = centroService;
            _blobFileService = blobFileService;
        }

        #region Change Data

        public Produto Insert(Produto produto)
        {
            try
            {
                if (produto.IdProduto == 0)
                {
                    if (!string.IsNullOrEmpty(produto.BlobFile?.Data))
                    {
                        produto.BlobFile = _blobFileService.Insert(produto.BlobFile);
                        produto.FileID = produto.BlobFile.ID;
                    }

                    produto = _produtoRepository.Insert(produto);
                }
                else
                {
                    throw new Exception("ID diferente de 0, avalie a utilização do PUT");
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(produto.FileID) && produto.IdProduto == 0)
                {
                    _blobFileService.Delete(produto.FileID);
                }

                throw ex;
            }

            return produto;
        }

        public Produto Update(Produto produto)
        {
            try
            {
                if (produto.IdProduto == 0)
                {
                    throw new Exception("ID diferente de 0, avalie a utilização do POST");
                }
                else
                {
                    if (!string.IsNullOrEmpty(produto.BlobFile?.Data))
                    {
                        if (!string.IsNullOrEmpty(produto.BlobFile?.ID))
                        {
                            produto.BlobFile = _blobFileService.Update(produto.BlobFile);
                        }
                        else
                        {
                            produto.BlobFile = _blobFileService.Insert(produto.BlobFile);
                        }
                        produto.FileID = produto.BlobFile.ID;
                    }

                    produto = _produtoRepository.Update(produto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return produto;
        }

        public void Delete(long IdProduto)
        {
            Produto produto;

            try
            {
                if (IdProduto == 0)
                {
                    throw new Exception("ID inválido");
                }
                else
                {
                    produto = Get(IdProduto);
                    if (produto != null)
                    {
                        if (!string.IsNullOrEmpty(produto.FileID))
                        {
                            _blobFileService.Delete(produto.FileID);
                        }
                        _produtoRepository.Delete(IdProduto);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Retrieve Repository

        public Produto Get(long IdProduto)
        {
            Produto produto;

            try
            {
                produto = _produtoRepository.Get(IdProduto);
                produto.Centro = _centroService.Get(IdProduto: IdProduto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return produto;
        }

        public List<Produto> Get(long? IdProduto = null)
        {
            List<Produto> produtos;

            try
            {
                produtos = _produtoRepository.Get(IdProduto: IdProduto);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return produtos;
        }

        #endregion
    }
}
