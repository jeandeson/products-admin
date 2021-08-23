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
    public class UserRepository
    {
        private readonly ISqlHelper _dataConnection;

        public UserRepository(ISqlHelper sqlHelper)
        {
            _dataConnection = sqlHelper;
        }

        #region LoadModel

        private List<User> Load(DataSet data)
        {
            List<User> Users;
            User user;

            try
            {
                Users = new List<User>();

                foreach (DataRow row in data.Tables[0].Rows)
                {
                    user = new User();

                    user.Id = row.Field<long>("Id");
                    user.Email = row.Field<string>("Email");
                    user.Password = row.Field<string>("Password");

                    Users.Add(user);
                }
            }
            catch
            {
                throw;
            }

            return Users;
        }

        #endregion

        #region Change Data

        public User Insert(User user)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($@" SELECT * FROM Users where
												Email = @email
												     AND
                                                Password = @password
											   ");

                command.Parameters.AddWithValue("Email", user.Email.AsDbValue());
                command.Parameters.AddWithValue("Password", user.Password.AsDbValue());

                user.Id = (long)_dataConnection.ExecuteScalar(command);
            }
            catch
            {
                throw;
            }

            return user;
        }

        public User Update(User user)
        {
            SqlCommand command;

            try
            {
                command = new SqlCommand($" UPDATE User SET " +
                                         $" Email = @Email," +
                                         $" Password = @Password" +
                                         $" WHERE Id = @Id");

                command.Parameters.AddWithValue("Id", user.Id.AsDbValue());
                command.Parameters.AddWithValue("Email", user.Email.AsDbValue());
                command.Parameters.AddWithValue("Password", user.Password.AsDbValue());

                _dataConnection.ExecuteNonQuery(command);
            }
            catch
            {
                throw;
            }

            return user;
        }

        public bool Delete(long Id)
        {
            SqlCommand command;
            int result;

            try
            {
                command = new SqlCommand($@" DELETE from Users where Id = @Id ");

                command.Parameters.AddWithValue("Id", Id.AsDbValue());

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

        public User Get(string email)
        {
            SqlCommand command;
            DataSet dataSet;

            User user;

            try
            {
                command = new SqlCommand($" SELECT * FROM Users WHERE email = @email ");
                command.Parameters.AddWithValue("email", email.AsDbValue());

                dataSet = _dataConnection.ExecuteDataSet(command);

                user = Load(dataSet).FirstOrDefault();
            }
            catch
            {
                throw;
            }

            return user;
        }

        public User Get(string email, string password)
        {
            SqlCommand command;
            DataSet dataSet;

            User user;

            try
            {
                command = new SqlCommand($" SELECT Id FROM Users WHERE email = @email and password = @password ");
                command.Parameters.AddWithValue("email", email.AsDbValue());
                command.Parameters.AddWithValue("password", password.AsDbValue());

                dataSet = _dataConnection.ExecuteDataSet(command);

                user = Load(dataSet).FirstOrDefault();
            }
            catch
            {
                throw;
            }

            return user;
        }

        public List<User> Get(long? Id = null)
        {
            SqlCommand command;
            DataSet dataSet;

            List<User> users;

            try
            {
                command = new SqlCommand($" SELECT * " +
                                         $" FROM " +
                                         $" Users ");

                if (Id.HasValue)
                {
                    command.Parameters.AddWithValue("Id", Id.AsDbValue());
                }

                dataSet = _dataConnection.ExecuteDataSet(command);

                users = Load(dataSet);
            }
            catch
            {
                throw;
            }

            return users;
        }

        #endregion
    }
}
