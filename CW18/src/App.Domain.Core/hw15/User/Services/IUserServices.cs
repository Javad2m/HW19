using App.Domain.Core.hw15.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.User.Services;
using App.Domain.Core.hw15.User.Entity;

public interface IUserServices
{
    public User Get(int id);
    public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime);
    public VerificationDto GetVerificationDataById(int userId);
    public void SaveVerificationData(VerificationDto verificationDto);
}
