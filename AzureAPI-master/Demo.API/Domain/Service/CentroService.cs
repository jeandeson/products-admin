
using Demo.API.Domain.Data.Repository;
using Demo.API.Domain.Model;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Service
{
    public class CentroService
    {
        private readonly CentroRepository _centroRepository;
        private readonly EnderecoService _enderecosService;

        public CentroService(CentroRepository centroRepository, EnderecoService enderecosService)
        {
            _centroRepository = centroRepository;
            _enderecosService = enderecosService;
        }

        #region Change Data

        public Centro Insert(Centro centro)
        {
            try
            {
                if (centro.IdCentro == 0)
                {
                    centro = _centroRepository.Insert(centro);
                }
                else
                {
                    throw new Exception("IdCliente diferente de 0, avalie a utilização do PUT");
                }
            }
            catch
            {
                throw;
            }

            return centro;
        }

        public Centro Update(Centro centro)
        {
            try
            {
                if (centro.IdCentro == 0)
                {
                    throw new Exception("IdCliente diferente de 0, avalie a utilização do POST");
                }
                else
                {
                    centro = _centroRepository.Update(centro);
                }
            }
            catch
            {
                throw;
            }

            return centro;
        }

        public void Delete(long IdCentro)
        {
            try
            {
                if (IdCentro == 0)
                {
                    throw new Exception("IdCliente inválido");
                }
                else
                {
                    _centroRepository.Delete(IdCentro);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Retrieve Repository

        public Centro Get(long IdCentro, long name)
        {
            Centro centro;

            try
            {
                centro = _centroRepository.Get(IdCentro);
                centro.Endereco = _enderecosService.Get(IdCentro: IdCentro, name: name);
            }
            catch
            {
                throw;
            }

            return centro;
        }

        public List<Centro> Get(long? IdCentro = null)
        {
            List<Centro> centros;

            try
            {
                centros = _centroRepository.Get(IdCentro: IdCentro);
            }
            catch
            {
                throw;
            }

            return centros;
        }

        public List<Centro> Get(long? IdProduto = null, long? name = null)
        {
            List<Centro> centros;

            try
            {
                centros = _centroRepository.Get(IdProduto);
            }
            catch
            {
                throw;
            }

            return centros;
        }

        #endregion
    }
}
