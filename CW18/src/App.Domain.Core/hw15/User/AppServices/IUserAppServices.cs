using App.Domain.Core.hw15.User.Data.Repository;
using App.Domain.Core.hw15.User.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.User.AppServices;

public interface IUserAppServices
{

    public string GenerateVerificationCode(int userId, string fullName);
    public bool ValidateVerificationCode(int userId, string fullName, int verificationCode);
}
