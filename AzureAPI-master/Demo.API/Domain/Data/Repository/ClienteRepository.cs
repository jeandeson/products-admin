using Demo.API.Domain.Data.Base;
using Demo.API.Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RauchTech.DataExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Demo.API.Domain.Data.Repository
{
    public class ClienteRepository
    {
        private readonly ISqlHelper _dataConnection;

        public ClienteRepository(ISqlHelper sqlHelper)
        {
            _dataConnection = sqlHelper;
        }

        #region LoadModel

        private List<Cliente> Load(DataSet data)
        {
            List<Cliente> Clientes;
            Cliente cliente;

            try
            {
                Clientes = new List<Cliente>();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    cliente = new Cliente();

                    cliente.IdCliente = row.Field<long>("IdCliente");
                    cliente.NomeCliente = row.Field<string>("NomeCliente");
                    cliente.SobrenomeCliente = row.Field<string>("SobrenomeCliente");
                    cliente.EmailCliente = row.Field<string>("EmailCliente");
                    cliente.IdEndereco = row.Field<long>("IdEndereco");

                    Clientes.Add(cliente);
                }
            }
            catch
            {
                throw;
            }

            return Clientes;
        }

        #endregion

        #region Change Data

        public Cliente Insert(Cliente cliente)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($@" INSERT INTO Clientes
											    (
												     NomeCliente
												    ,SobrenomeCliente
                                                    ,EmailCliente
                                                    ,IdEndereco
											    )
										     OUTPUT inserted.IdCliente 
										     VALUES
											    (
												      @NomeCliente
												    ,@SobrenomeCliente
                                                    ,@EmailCliente
                                                    ,@IdEndereco
											    )");

                command.Parameters.AddWithValue("NomeCliente", cliente.NomeCliente.AsDbValue());
                command.Parameters.AddWithValue("SobrenomeCliente", cliente.SobrenomeCliente.AsDbValue());
                command.Parameters.AddWithValue("EmailCliente", cliente.SobrenomeCliente.AsDbValue());
                command.Parameters.AddWithValue("IdEndereco", cliente.IdEndereco.AsDbValue());

                cliente.IdCliente = (long)_dataConnection.ExecuteScalar(command);
            }
            catch
            {
                throw;
            }

            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($" UPDATE Clientes SET " +
                                         $" NomeCliente = @NomeCliente," +
                                         $" SobrenomeCliente = @SobrenomeCliente," +
                                         $" EmailCliente = @EmailCliente," +
                                         $" IdEndereco = @IdEndereco" +
                                         $" WHERE IdCliente = @IdCliente");

                command.Parameters.AddWithValue("IdCliente", cliente.IdCliente.AsDbValue());
                command.Parameters.AddWithValue("NomeCliente", cliente.NomeCliente.AsDbValue());
                command.Parameters.AddWithValue("SobrenomeCliente", cliente.SobrenomeCliente.AsDbValue());
                command.Parameters.AddWithValue("EmailCliente", cliente.EmailCliente.AsDbValue());
                command.Parameters.AddWithValue("IdEndereco", cliente.IdEndereco.AsDbValue());

                _dataConnection.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }

            return cliente;
        }

        public bool Delete(long IdCliente)
        {
            SqlCommand command;
            int result;

            try
            {
                command = new SqlCommand($@" DELETE from Clientes where IdCliente = @IdCliente ");

                command.Parameters.AddWithValue("IdCliente", IdCliente.AsDbValue());

                result = _dataConnection.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }

            return result > 0;
        }

        #endregion

        #region Retrieve Data

        public Cliente Get(long IdCliente)
        {
            SqlCommand command;
            DataSet dataSet;

            Cliente cliente;

            try
            {
                command = new SqlCommand($" SELECT * FROM Clientes WHERE IdCliente = @IdCliente ");
                command.Parameters.AddWithValue("IdCliente", IdCliente.AsDbValue());

                dataSet = _dataConnection.ExecuteDataSet(command);

                cliente = Load(dataSet).FirstOrDefault();
            }
            catch
            {
                throw;
            }

            return cliente;
        }

        public List<Cliente> Get(long? IdCliente = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Cliente> clientes;

            try
            {
                command = new SqlCommand($" SELECT * " +
                                         $" FROM " +
                                         $" Clientes ");

                if (IdCliente.HasValue)
                {
                    command.Parameters.AddWithValue("IdCliente", IdCliente.AsDbValue());
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                clientes = Load(dataSet);
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
