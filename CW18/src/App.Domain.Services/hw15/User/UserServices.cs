using App.Domain.Core.hw15.Result;
using App.Domain.Core.hw15.Transaction.Data.Repositories;
using App.Domain.Core.hw15.User.Data.Repository;
using App.Domain.Core.hw15.User.Services;
using App.Infra.Data.Repos.Ef.hw15.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.hw15.User
{
    public class UserServices : IUserServices
    {

        private readonly IUserRepository userServices;
        public UserServices(IUserRepository userServicess)
        {
            userServices = userServicess;
        }
        public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime)
        {
            userServices.GenerateAndSaveVerificationCode(userId, fullName, verificationCode, expirationTime);
        }

        public Core.hw15.User.Entity.User Get(int id)
        {
            return userServices.Get(id);
        }

        public VerificationDto GetVerificationDataById(int userId)
        {
            return userServices.GetVerificationDataById(userId);
        }

        public void SaveVerificationData(VerificationDto verificationDto)
        {
            userServices.SaveVerificationData(verificationDto);
        }
    }
}
