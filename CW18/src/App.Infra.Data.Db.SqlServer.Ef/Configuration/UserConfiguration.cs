﻿using App.Domain.Core.hw15.User.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Db.SqlServer.Ef.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Users");

        builder.HasData(new List<User>()
        {
            new User() { Id = 1, FirstName = "javad", LastName = "moradi" },
            new User() { Id = 2, FirstName = "saeid", LastName = "lak" }
        });
    }
}
