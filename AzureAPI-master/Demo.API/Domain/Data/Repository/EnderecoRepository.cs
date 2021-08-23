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
    public class EnderecoRepository
    {
        private readonly ISqlHelper _dataConnection;

        public EnderecoRepository(ISqlHelper sqlHelper)
        {
            _dataConnection = sqlHelper;
        }

        #region LoadModel

        private List<Endereco> Load(DataSet data)
        {
            List<Endereco> enderecos;
            Endereco endereco;

            try
            {
                enderecos = new List<Endereco>();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    endereco = new Endereco();

                    endereco.IdEndereco = row.Field<long>("IdEndereco");
                    endereco.Estado = row.Field<string>("Estado");
                    endereco.Cidade = row.Field<string>("Cidade");
                    endereco.Bairro = row.Field<string>("Bairro");
                    endereco.Rua = row.Field<string>("Rua");
                    endereco.Cep = row.Field<long>("Cep");

                    enderecos.Add(endereco);
                }
            }
            catch
            {
                throw;
            }

            return enderecos;
        }

        #endregion

        #region Change Data

        public Endereco Insert(Endereco endereco)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($@" INSERT INTO Enderecos
											    (    
                                                     Estado
												    ,Cidade
                                                    ,Bairro
                                                    ,Rua
                                                    ,Cep
											    )
										     OUTPUT inserted.IdEndereco 
										     VALUES
											    (
												     @Estado
                                                    ,@Cidade
                                                    ,@Bairro
                                                    ,@Rua
                                                    ,@Cep
											    )");

                command.Parameters.AddWithValue("Estado", endereco.Estado.AsDbValue());
                command.Parameters.AddWithValue("Cidade", endereco.Cidade.AsDbValue());
                command.Parameters.AddWithValue("Bairro", endereco.Bairro.AsDbValue());
                command.Parameters.AddWithValue("Rua", endereco.Rua.AsDbValue());
                command.Parameters.AddWithValue("Cep", endereco.Cep.AsDbValue());

                endereco.IdEndereco = (long)_dataConnection.ExecuteScalar(command);
            }
            catch
            {
                throw;
            }

            return endereco;
        }

        public Endereco Update(Endereco endereco)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($" UPDATE Enderecos SET " +
                                         $" Estado = @Estado," +
                                         $" Cidade = @Cidade," +
                                         $" Bairro = @Bairro," +
                                         $" Rua = @Rua," +
                                         $" Cep = @Cep" +
                                         $" WHERE IdEndereco = @IdEndereco");

                command.Parameters.AddWithValue("IdEndereco", endereco.IdEndereco.AsDbValue());
                command.Parameters.AddWithValue("Estado", endereco.Estado.AsDbValue());
                command.Parameters.AddWithValue("Cidade", endereco.Cidade.AsDbValue());
                command.Parameters.AddWithValue("Bairro", endereco.Bairro.AsDbValue());
                command.Parameters.AddWithValue("Rua", endereco.Rua.AsDbValue());
                command.Parameters.AddWithValue("Cep", endereco.Cep.AsDbValue());

                _dataConnection.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }

            return endereco;
        }

        public bool Delete(long IdEndereco)
        {
            SqlCommand command;
            int result;

            try
            {
                command = new SqlCommand($@" DELETE from Enderecos where IdEndereco = @IdEndereco ");

                command.Parameters.AddWithValue("IdEndereco", IdEndereco.AsDbValue());

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

        public Endereco Get(long IdEndereco)
        {
            SqlCommand command;
            DataSet dataSet;

            Endereco endereco;

            try
            {
                command = new SqlCommand($" SELECT * FROM Enderecos WHERE IdEndereco = @IdEndereco ");
                command.Parameters.AddWithValue("IdEndereco", IdEndereco.AsDbValue());

                dataSet = _dataConnection.ExecuteDataSet(command);

                endereco = Load(dataSet).FirstOrDefault();
            }
            catch
            {
                throw;
            }

            return endereco;
        }

        public List<Endereco> Get(long? IdCliente = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Endereco> enderecos;
            List<string> clauses;

            try
            {
                command = new SqlCommand($" SELECT distinct E.* " +
                                         $" FROM " +
                                         $" Enderecos E LEFT JOIN " +
                                         $" Clientes C ON E.IdEndereco = C.IdEndereco ");

                clauses = new List<string>();
                if (IdCliente.HasValue)
                {
                    clauses.Add($"IdCliente = @IdCliente");
                    command.Parameters.AddWithValue("IdCliente", IdCliente.AsDbValue());
                }

                if (clauses.Count > 0)
                {
                    command.CommandText += $" WHERE {string.Join(" and ", clauses)}";
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                enderecos = Load(dataSet);
            }
            catch
            {
                throw;
            }

            return enderecos;
        }

        public List<Endereco> Get(long? IdCentro = null,long? name = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Endereco> enderecos;
            List<string> clauses;

            try
            {
                command = new SqlCommand($" SELECT distinct E.* " +
                                         $" FROM " +
                                         $" Enderecos E LEFT JOIN " +
                                         $" Centros C ON E.IdEndereco = C.IdEndereco"); 

                clauses = new List<string>();
                if (IdCentro.HasValue)
                {
                    clauses.Add($"IdCentro = @IdCentro"); 
                    command.Parameters.AddWithValue("IdCentro", IdCentro.AsDbValue());
                }

                if (clauses.Count > 0)
                {
                    command.CommandText += $" WHERE {string.Join(" and ", clauses)}";
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                enderecos = Load(dataSet);
            }
            catch
            {
                throw;
            }

            return enderecos;
        }

        public List<Endereco> Get(long? IdEndereco = null, long? IdCliente = null, long? IdCentro = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Endereco> enderecos;

            try
            {
                command = new SqlCommand($" SELECT * " +
                                         $" FROM " +
                                         $" Enderecos ");

                if (IdEndereco.HasValue)
                {
                    command.Parameters.AddWithValue("IdEndereco", IdEndereco.AsDbValue());
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                enderecos = Load(dataSet);
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
