using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using internet_shop.Models;
using internet_shop.Helpers;
using internet_shop.Entities;

namespace internet_shop.Services
{
    public class UserService
    {
        public UserService(IOptions<AppSettings> appSettings, BaseDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        private readonly AppSettings _appSettings;
        private readonly BaseDbContext _context;

        private DbSet<Profile> Profiles => _context.Profiles;
        private DbSet<User> Users => _context.Users;

        public List<Profile> GetAllProfiles()
        {
            return Profiles.ToList();
        }

        public Profile GetProfileById(int id)
        {
            var profile = Profiles.SingleOrDefault((Profile profile) => profile.Id == id);
            if (profile == null)
            {
                return null;
            }
            return profile;
        }
        public User Authenticate(string username, string password)
        {
            Profile profile = null;
            var user = Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user != null)
            {
                var data = Profiles.SingleOrDefault(x => x.Username == username);
                if (data != null)
                    profile = ToEntity(data.FirstName, data.LastName, data.Email, data.Address, data.Username, data.Password);
            }
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }
        public Profile AddUser(string fn, string ln, string em, string addr, string username, string password)
        {
            var data = Users.SingleOrDefault(x => x.Username == username);
            Profile newdata = null;
            if (data != null)
                return null;
            else
                newdata=ToEntity(fn,  ln, em, addr, username, password);
            Profiles.Add(newdata);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }
            User newuser = ToEntityUser(username, password);
            Users.Add(newuser);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }
            return newdata;
        }
        
        public Profile ToEntity(string fn, string ln, string em, string addr, string username, string password)
        {
            return new Profile
            {
                FirstName = fn,
                LastName = ln,
                Email = em,
                Address = addr,
                Username = username,
                Password = password,
            };
        }
        public User ToEntityUser(string username, string password)
        {
            return new User
            {
                Username = username,
                Password = password,
                Token = null,
            };
        }
        public IEnumerable<User> GetAll()
        {
            return Users.WithoutPasswords();
        }

        public (bool result, Exception exception) DeleteProfileById(int id)
        {
            var profile = Profiles.SingleOrDefault((Profile profile) => profile.Id == id);

            if (profile == null)
            {
                return (false, new ArgumentNullException($"Promo with id: {id} not found"));
            }

            EntityEntry<Profile> result = Profiles.Remove(profile);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return (false, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (result.State == EntityState.Deleted, null);
        }

        public (Profile profile, Exception exception) Updateprofile(Profile _profile)
        {
            Profile profile = Profiles.SingleOrDefault((Profile profile) => profile.Id == _profile.Id);

            if (profile == null)
            {
                return (null, new ArgumentNullException($"promos with id: {_profile.Id} not found"));
            }

            if (_profile.Id != 0)
            {
                profile.FirstName = _profile.FirstName;
                profile.LastName = _profile.LastName;
                profile.Email = _profile.Email;
                profile.Address = _profile.Address;
                profile.Username = _profile.Username;
                profile.Password = _profile.Password;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (profile, null);
        }
    }
}
