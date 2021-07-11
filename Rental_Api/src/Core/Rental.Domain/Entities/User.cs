using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using Microsoft.VisualBasic;

namespace Rental.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Name { get; private set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; private set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; private set; }
        public string Role { get; private set; }


        private User()
        {
        }

        private User(int id, string userName, string name, byte[] passwordHash, byte[] passwordSalt, string role)
        {
            Id = id;
            UserName = userName;
            Name = name;
            Role = role;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public static Result<User> Create(int id, string userName, string name, byte[] passwordHash, byte[] passwordSalt, string role)
            => ValidateFields(userName, name, passwordHash, passwordSalt, role)
                .Map(() => new User(id, userName, name, passwordHash,  passwordSalt, role));

        private static Result ValidateFields(string userName, string name, byte[] passwordHash, byte[] passwordSalt, string role)
            => Result.FailureIf(string.IsNullOrEmpty(userName) , "Username not valid")
                .Bind(() => Result.FailureIf(string.IsNullOrWhiteSpace(name), "Name not valid"))
                .Bind(() => Result.FailureIf(passwordHash is null, "Password not valid"))
                .Bind(() => Result.FailureIf(passwordSalt is null, "Password not valid"))
                .Bind(() => Result.FailureIf(string.IsNullOrWhiteSpace(role), "Role not valid"));
    }
}
