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
    public class CentroRepository
    {
        private readonly ISqlHelper _dataConnection;

        public CentroRepository(ISqlHelper sqlHelper)
        {
            _dataConnection = sqlHelper;
        }

        #region LoadModel

        private List<Centro> Load(DataSet data)
        {
            List<Centro> centros;
            Centro centro;

            try
            {
                centros = new List<Centro>();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    centro = new Centro();

                    centro.IdCentro = row.Field<long>("IdCentro");
                    centro.CodigoCentro = row.Field<long>("CodigoCentro");
                    centro.NomeCentro = row.Field<string>("NomeCentro");
                    centro.IdEndereco = row.Field<long>("IdEndereco");

                    centros.Add(centro);
                }
            }
            catch
            {
                throw;
            }

            return centros;
        }

        #endregion

        #region Change Data

        public Centro Insert(Centro centro)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($@" INSERT INTO Centros
											    (    
                                                     CodigoCentro
												    ,NomeCentro
                                                    ,IdEndereco
											    )
										     OUTPUT inserted.IdCentro 
										     VALUES
											    (
												     @CodigoCentro
                                                    ,@NomeCentro
                                                    ,@IdEndereco
											    )");

                command.Parameters.AddWithValue("CodigoCentro", centro.CodigoCentro.AsDbValue());
                command.Parameters.AddWithValue("NomeCentro", centro.NomeCentro.AsDbValue());
                command.Parameters.AddWithValue("IdEndereco", centro.IdEndereco.AsDbValue());

                centro.IdCentro = (long)_dataConnection.ExecuteScalar(command);
            }
            catch
            {
                throw;
            }

            return centro;
        }

        public Centro Update(Centro centro)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($" UPDATE Centros SET " +
                                         $" NomeCentro = @NomeCentro," +
                                         $" CodigoCentro = @CodigoCentro," +
                                         $" IdEndereco = @IdEndereco" +
                                         $" WHERE IdCentro = @IdCentro");

                command.Parameters.AddWithValue("IdCentro", centro.IdCentro.AsDbValue());
                command.Parameters.AddWithValue("NomeCentro", centro.NomeCentro.AsDbValue());
                command.Parameters.AddWithValue("CodigoCentro", centro.CodigoCentro.AsDbValue());
                command.Parameters.AddWithValue("IdEndereco", centro.IdEndereco.AsDbValue());

                _dataConnection.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }

            return centro;
        }

        public bool Delete(long IdCentro)
        {
            SqlCommand command;
            int result;

            try
            {
                command = new SqlCommand($@" DELETE from Centros where IdCentro = @IdCentro ");

                command.Parameters.AddWithValue("IdCentro", IdCentro.AsDbValue());

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

        public Centro Get(long IdCentro)
        {
            SqlCommand command;
            DataSet dataSet;

            Centro centro;

            try
            {
                command = new SqlCommand($" SELECT * FROM Centros WHERE IdCentro = @IdCentro ");
                command.Parameters.AddWithValue("IdCentro", IdCentro.AsDbValue());

                dataSet = _dataConnection.ExecuteDataSet(command);

                centro = Load(dataSet).FirstOrDefault();
            }
            catch
            {
                throw;
            }

            return centro;
        }

        public List<Centro> Get(long? IdCentro = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Centro> centros;

            try
            {
                command = new SqlCommand($" SELECT * " +
                                         $" FROM " +
                                         $" Centros ");


                if (IdCentro.HasValue)
                {
                    command.Parameters.AddWithValue("IdCentro", IdCentro.AsDbValue());
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                centros = Load(dataSet);
            }
            catch
            {
                throw;
            }

            return centros;
        }

        public List<Centro> Get(long? IdProduto = null, long? name = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<Centro> centros;
            List<string> clauses;

            try
            {
                command = new SqlCommand($" SELECT distinct C.* " +
                                         $" FROM " +
                                         $" Centros C LEFT JOIN " +
                                         $" Produtos P ON C.IdCentro = P.IdCentro ");

                clauses = new List<string>();
                if (IdProduto.HasValue)
                {
                    clauses.Add($"IdProduto = @IdProduto");
                    command.Parameters.AddWithValue("IdProduto", IdProduto.AsDbValue());
                }

                if (clauses.Count > 0)
                {
                    command.CommandText += $" WHERE {string.Join(" and ", clauses)}";
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                centros = Load(dataSet);
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
