﻿using App.Domain.Core.hw15.Result;
using App.Domain.Core.hw15.User.Data.Repository;
using App.Infra.Data.Db.SqlServer.Ef.Dal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.hw15.User;
using App.Domain.Core.hw15.User.Entity;

public class UserRepository : IUserRepository
{
    string _path = "C:/hw15/users.txt";
    private readonly AppDbContext _appDbContext;
    private List<VerificationDto> codes = new List<VerificationDto>();



    public UserRepository()
    {
        _appDbContext = new AppDbContext();

        var directory = Path.GetDirectoryName(_path);
        if (Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        if (!File.Exists(_path))
        {
            Directory.CreateDirectory(directory);
            File.WriteAllText(_path, "[]");
        }
    }

    public User Get(int id)
    {
        var user = _appDbContext.Users.FirstOrDefault(x => x.Id == id);

        if (user is null)
        {
            throw new Exception($"user not found");
        }
        else
        {
            return user;
        }
    }


    public void GenerateAndSaveVerificationCode(int userId, string fullName, int verificationCode, DateTime expirationTime)
    {
        var data = File.ReadAllText(_path);
        var verificationDataList = JsonConvert.DeserializeObject<List<VerificationDto>>(data);
        if (verificationDataList == null)
        {
            verificationDataList = new List<VerificationDto>();
        }
        var verification = verificationDataList.FirstOrDefault(v => v.Id == userId);
        if (verification != null)
        {
            verification.FullName = fullName;
            verification.VerificationCode = verificationCode;
            verification.DateVerificationCode = expirationTime;
        }
        else
        {
            var verificationDto = new VerificationDto
            {
                Id = userId,
                FullName = fullName,
                VerificationCode = verificationCode,
                DateVerificationCode = expirationTime
            };
            verificationDataList.Add(verificationDto);
        }
        string json = JsonConvert.SerializeObject(verificationDataList);
        File.WriteAllText(_path, json);
    }

    public VerificationDto GetVerificationDataById(int userId)
    {
        var data = File.ReadAllText(_path);
        var verificationCode = JsonConvert.DeserializeObject<List<VerificationDto>>(data);
        return verificationCode.FirstOrDefault(x => x.Id == userId);
    }
    public void SaveVerificationData(VerificationDto verificationDto)
    {
        var data = File.ReadAllText(_path);
        var verificationDataList = JsonConvert.DeserializeObject<List<VerificationDto>>(data);
        if (verificationDataList == null)
        {
            verificationDataList = new List<VerificationDto>();
        }
        var verification = verificationDataList.FirstOrDefault(v => v.Id == verificationDto.Id);
        if (verification != null)
        {
            verification.FullName = verificationDto.FullName;
            verification.VerificationCode = verificationDto.VerificationCode;
            verification.DateVerificationCode = verificationDto.DateVerificationCode;
        }

        else
        {
            verificationDataList.Add(verificationDto);
        }
        var jsonData = JsonConvert.SerializeObject(verificationDataList);
        File.WriteAllText(_path, jsonData);
    }
}
