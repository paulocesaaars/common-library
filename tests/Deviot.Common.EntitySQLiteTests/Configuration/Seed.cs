using Deviot.Common.EntitySQLiteTests.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Common.EntitySQLiteTests.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class Seed
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            // User
            var users = new List<User>();
            users.Add(new User(new Guid("7011423f65144a2fb1d798dec19cf466"), "Paulo César de Souza"));
            users.Add(new User(new Guid("6805b2ad65e748018b0d9465a41cf27f"), "Bruna Stefano Marques"));
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}