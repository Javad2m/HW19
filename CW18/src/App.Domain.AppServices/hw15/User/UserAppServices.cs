using App.Domain.Core.hw15.Card.Services;
using App.Domain.Core.hw15.User.AppServices;
using App.Domain.Core.hw15.User.Data.Repository;
using App.Domain.Core.hw15.User.Services;
using App.Domain.Services.hw15.Card;
using App.Domain.Services.hw15.User;
using App.Infra.Data.Repos.Ef.hw15.Card;
using App.Infra.Data.Repos.Ef.hw15.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.hw15.User;

public class UserAppServices : IUserAppServices
{
    private readonly IUserServices _userRepository;
    private readonly ICardServices _cardRepository;

    public UserAppServices()
    {
        _userRepository = new UserServices();
        _cardRepository = new CardServices();
    }


    public string GenerateVerificationCode(int userId, string fullName)
    {
        var existingVerification = _userRepository.GetVerificationDataById(userId);

        if (existingVerification != null && existingVerification.DateVerificationCode > DateTime.Now)
        {
            return "Code Already Sent";
        }

        var random = new Random();
        int verificationCode = random.Next(10000, 99999);
        DateTime expirationTime = DateTime.Now.AddMinutes(5);
        _userRepository.GenerateAndSaveVerificationCode(userId, fullName, verificationCode, expirationTime);
        return $"Code Send";
    }

    public bool ValidateVerificationCode(int userId, string fullName, int verificationCode)
    {
        var verificationDto = _userRepository.GetVerificationDataById(userId);
        if (verificationDto != null &&
            verificationDto.FullName == fullName &&
            verificationDto.VerificationCode == verificationCode)
        {
            if (verificationDto.DateVerificationCode > DateTime.Now)
            {
                verificationDto.DateVerificationCode = DateTime.Now.AddMinutes(-1);
                _userRepository.SaveVerificationData(verificationDto);
                return true;
            }
        }
        return false;
    }
}
