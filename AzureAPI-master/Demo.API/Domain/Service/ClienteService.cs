
using Demo.API.Domain.Data.Repository;
using Demo.API.Domain.Model;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Service
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly EnderecoService _enderecosService;

        public ClienteService(ClienteRepository clienteRepository, EnderecoService enderecosService)
        {
            _clienteRepository = clienteRepository;
            _enderecosService = enderecosService;
        }

        #region Change Data

        public Cliente Insert(Cliente Cliente)
        {
            try
            {
                if (Cliente.IdCliente == 0)
                {
                    Cliente = _clienteRepository.Insert(Cliente);
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

            return Cliente;
        }

        public Cliente Update(Cliente Cliente)
        {
            try
            {
                if (Cliente.IdCliente == 0)
                {
                    throw new Exception("IdCliente diferente de 0, avalie a utilização do POST");
                }
                else
                {
                    Cliente = _clienteRepository.Update(Cliente);
                }
            }
            catch
            {
                throw;
            }

            return Cliente;
        }

        public void Delete(long IdCliente)
        {
            try
            {
                if (IdCliente == 0)
                {
                    throw new Exception("IdCliente inválido");
                }
                else
                {
                    _clienteRepository.Delete(IdCliente);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Retrieve Repository

        public Cliente Get(long IdCliente)
        {
            Cliente cliente;

            try
            {
                cliente = _clienteRepository.Get(IdCliente);
                cliente.Endereco = _enderecosService.Get(IdCliente: IdCliente);
            }
            catch
            {
                throw;
            }

            return cliente;
        }

        public List<Cliente> Get(long? IdCliente = null)
        {
            List<Cliente> clientes;

            try
            {
                clientes = _clienteRepository.Get(IdCliente: IdCliente);
            }
            catch
            {
                throw;
            }

            return clientes;
        }

        #endregion
    }
}
