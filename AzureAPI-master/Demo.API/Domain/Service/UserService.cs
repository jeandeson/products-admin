
using Demo.API.Domain.Data.Repository;
using Demo.API.Domain.Model;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Change Data

        public User Insert(User user)
        {
            try
            {
                if (user.Id == 0)
                {
                    user = _userRepository.Insert(user);
                }
                else
                {
                    throw new Exception("Id diferente de 0, avalie a utilização do PUT");
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        public User Update(User user)
        {
            try
            {
                if (user.Id == 0)
                {
                    throw new Exception("Id diferente de 0, avalie a utilização do POST");
                }
                else
                {
                    user = _userRepository.Update(user);
                }
            }
            catch
            {
                throw;
            }

            return user;
        }

        public void Delete(long Id)
        {
            try
            {
                if (Id == 0)
                {
                    throw new Exception("Id inválido");
                }
                else
                {
                    _userRepository.Delete(Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Retrieve Repository

        public User Get(string email)
        {
            User user;

            try
            {
                user = _userRepository.Get(email);
            }
            catch
            {
                throw;
            }

            return user;
        }

        public List<User> Get(long? Id = null)
        {
            List<User> users;

            try
            {
                users = _userRepository.Get(Id: Id);
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
