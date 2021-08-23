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
    public class ProdutoRepository
    {
        private readonly IConfiguration _config;
        private readonly ISqlHelper _dataConnection;

        public ProdutoRepository(IConfiguration configuration, ISqlHelper sqlHelper)
        {
            _config = configuration;
            _dataConnection = sqlHelper;
        }

        #region LoadModel

        private List<Produto> Load(DataSet data)
        {
            List<Produto> candidates;
            Produto produto;

            try
            {
                candidates = new List<Produto>();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    produto = new Produto();

                    produto.IdProduto = row.Field<long>("IdProduto");
                    produto.CodigoProduto = row.Field<long>("CodigoProduto");
                    produto.NomeProduto = row.Field<string>("NomeProduto");
                    produto.QuantidadeProduto = row.Field<int>("QuantidadeProduto");
                    produto.ValorProduto = row.Field<string>("ValorProduto");
                    produto.IdCentro = row.Field<long>("IdCentro");
                    produto.FileID = row.Field<string>("FileID");

                    produto.LoadUrls(_config);

                    candidates.Add(produto);
                }
            }
            catch
            {
                throw;
            }

            return candidates;
        }

        #endregion

        #region Change Data

        public Produto Insert(Produto produto)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($@" INSERT INTO Produtos
											    (
												     CodigoProduto
												    ,NomeProduto
                                                    ,QuantidadeProduto
                                                    ,ValorProduto
                                                    ,IdCentro
                                                    ,FileID
											    )
										     OUTPUT inserted.IdProduto 
										     VALUES
											    (
												     @CodigoProduto
												    ,@NomeProduto
                                                    ,@QuantidadeProduto
                                                    ,@ValorProduto
                                                    ,@IdCentro
                                                    ,@FileID
											    )");

                command.Parameters.AddWithValue("CodigoProduto", produto.CodigoProduto.AsDbValue());
                command.Parameters.AddWithValue("NomeProduto", produto.NomeProduto.AsDbValue());
                command.Parameters.AddWithValue("QuantidadeProduto", produto.QuantidadeProduto.AsDbValue());
                command.Parameters.AddWithValue("ValorProduto", produto.ValorProduto.AsDbValue());
                command.Parameters.AddWithValue("IdCentro", produto.IdCentro.AsDbValue());
                command.Parameters.AddWithValue("FileID", produto.FileID.AsDbValue());

                produto.IdProduto = (long)_dataConnection.ExecuteScalar(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return produto;
        }

        public Produto Update(Produto produto)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($" UPDATE Produtos SET " +
                                         $" CodigoProduto = @CodigoProduto," +
                                         $" NomeProduto = @NomeProduto," +
                                         $" QuantidadeProduto = @QuantidadeProduto," +
                                         $" ValorProduto = @ValorProduto," +
                                         $" IdCentro = @IdCentro," +
                                         $" FileID = @FileID" +
                                         $" WHERE IdProduto = @IdProduto");

                command.Parameters.AddWithValue("CodigoProduto", produto.CodigoProduto.AsDbValue());
                command.Parameters.AddWithValue("IdProduto", produto.IdProduto.AsDbValue());
                command.Parameters.AddWithValue("NomeProduto", produto.NomeProduto.AsDbValue());
                command.Parameters.AddWithValue("QuantidadeProduto", produto.QuantidadeProduto.AsDbValue());
                command.Parameters.AddWithValue("ValorProduto", produto.ValorProduto.AsDbValue());
                command.Parameters.AddWithValue("IdCentro", produto.IdCentro.AsDbValue());
                command.Parameters.AddWithValue("FileID", produto.FileID.AsDbValue());

                _dataConnection.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            produto.LoadUrls(_config);
            return produto;
        }

        public bool Delete(long IdProduto)
        {
            SqlCommand command;
            int result; 

            try
            {
                command = new SqlCommand($@" DELETE from Produtos WHERE IdProduto = @IdProduto ");

                command.Parameters.AddWithValue("IdProduto", IdProduto.AsDbValue());

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

        public Produto Get(long IdProduto)
        {
            SqlCommand command;
            DataSet dataSet;

            Produto produto;

            try
            {
                command = new SqlCommand($" SELECT * FROM Produtos WHERE IdProduto = @IdProduto ");
                command.Parameters.AddWithValue("IdProduto", IdProduto.AsDbValue());

                dataSet = _dataConnection.ExecuteDataSet(command);

                produto = Load(dataSet).FirstOrDefault();
            }
            catch 
            {
                throw;
            }

            return produto;
        }

        public List<Produto> Get(long? IdProduto = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Produto> produtos;

            try
            {
                command = new SqlCommand($" SELECT * " +
                                         $" FROM " +
                                         $" Produtos ");


                if (IdProduto.HasValue)
                {
                    command.Parameters.AddWithValue("IdProduto", IdProduto.AsDbValue());
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                produtos = Load(dataSet);
            }
            catch
            {
                throw;
            }

            return produtos;
        }

        #endregion
    }
}
