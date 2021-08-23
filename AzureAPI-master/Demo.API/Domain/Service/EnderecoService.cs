using Demo.API.Domain.Data.Repository;
using Demo.API.Domain.Model;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Service
{
    public class EnderecoService
    {
        private readonly EnderecoRepository _enderecoRepository;

        public EnderecoService(EnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        #region Change Data

        public Endereco Insert(Endereco endereco)
        {
            try
            {
                if (endereco.IdEndereco == 0)
                {
                    endereco = _enderecoRepository.Insert(endereco);
                }
                else
                {
                    throw new Exception("IdEndereco diferente de 0, avalie a utilização do PUT");
                }
            }
            catch
            {
                throw;
            }

            return endereco;
        }

        public Endereco Update(Endereco endereco)
        {
            try
            {
                if (endereco.IdEndereco == 0)
                {
                    throw new Exception("IdEndereco diferente de 0, avalie a utilização do POST");
                }
                else
                {
                    endereco = _enderecoRepository.Update(endereco);
                }
            }
            catch
            {
                throw;
            }

            return endereco;
        }

        public void Delete(long id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("IdEndereco inválido");
                }
                else
                {
                    _enderecoRepository.Delete(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Retrieve Repository

        public Endereco Get(long IdEndereco)
        {
            Endereco endereco;

            try
            {
                endereco = _enderecoRepository.Get(IdEndereco);
            }
            catch
            {
                throw;
            }

            return endereco;
        }

        public List<Endereco> Get(long? IdEndereco = null, long? IdCliente = null, long? IdCentro = null)
        {
            List<Endereco> enderecos;

            try
            {
                enderecos = _enderecoRepository.Get(IdEndereco: IdEndereco, IdCliente: IdCliente, IdCentro: IdCentro);
            }
            catch
            {
                throw;
            }

            return enderecos;
        }

        public List<Endereco> Get(long? IdCliente = null) 
        {
            List<Endereco> enderecos;

            try
            {
                enderecos = _enderecoRepository.Get(IdCliente);
            }
            catch
            {
                throw;
            }

            return enderecos;
        }
        public List<Endereco> Get(long? IdCentro = null, long? name = null)
        {
            List<Endereco> enderecos;

            try
            {
                enderecos = _enderecoRepository.Get(IdCentro, name);
            }
            catch
            {
                throw;
            }

            return enderecos;
        }

        #endregion
    }
}
