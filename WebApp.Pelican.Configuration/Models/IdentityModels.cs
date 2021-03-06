﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApp.Pelican.Configuration.Models
{
    // Modify the User class to add extra user information
    public class User : IUser
    {
        public User()
            : this(String.Empty) {}

        public User(string userName)
        {
            UserName = userName;
            Id = Guid.NewGuid()
                     .ToString();
        }

        public string UserName { get; set; }

        [Key]
        public string Id { get; set; }
    }

    public class UserLogin : IUserLogin
    {
        public UserLogin() {}

        public UserLogin(string userId,
                         string loginProvider,
                         string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            UserId = userId;
        }

        [Key]
        [Column(Order = 0)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ProviderKey { get; set; }

        public string UserId { get; set; }
    }

    public class UserSecret : IUserSecret
    {
        public UserSecret() {}

        public UserSecret(string userName,
                          string secret)
        {
            UserName = userName;
            Secret = secret;
        }

        [Key]
        public string UserName { get; set; }

        public string Secret { get; set; }
    }

    public class UserRole : IUserRole
    {
        [Key]
        [Column(Order = 0)]
        public string RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
    }

    public class Role : IRole
    {
        public Role()
            : this(String.Empty) {}

        public Role(string roleName)
        {
            Id = roleName;
        }

        [Key]
        public string Id { get; set; }
    }

    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext()
            : base("DefaultConnection") {}

        public IdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) {}

        // This method ensures that user names are always unique

        public DbSet<User> Users { get; set; }
        public DbSet<UserSecret> Secrets { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
                                                                   IDictionary<object, object> items)
        {
            if (entityEntry.State != EntityState.Added)
            {
                return base.ValidateEntity(entityEntry,
                                           items);
            }

            var user = entityEntry.Entity as User;
            // Check for uniqueness of user name
            if (user == null || !Users.Any(u => String.Equals(u.UserName,
                                                              user.UserName,
                                                              StringComparison.CurrentCultureIgnoreCase)))
            {
                return base.ValidateEntity(entityEntry,
                                           items);
            }

            var result = new DbEntityValidationResult(entityEntry,
                                                      new List<DbValidationError>());
            result.ValidationErrors.Add(new DbValidationError("User",
                                                              "User name must be unique."));
            return result;
        }
    }
}