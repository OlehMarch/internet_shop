using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using internet_shop.Helpers;
using internet_shop.Entities;
using internet_shop.DbContexts;
using internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace internet_shop.Services
{
    public class UserService
    {

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User {
                Id = 1, Username = "test", Password = "test"
            },
            new User {
                Id = 2, Username = "alshefer", Password = "alshefer"
            },
        };

        private readonly AppSettings _appSettings;
        private readonly ProfileDbContext _profileDbContext;
        private DbSet<Profile> _Profiles => _profileDbContext.Profiles;

        public List<Profile> GetAllProfiles()
        {
            return _Profiles.ToList();
        }

        public Profile GetProfileById(int id)
        {
            var profile = _Profiles.SingleOrDefault((Profile profile) => profile.Id == id);
            if (profile == null)
            {
                return null;
            }
            return profile;
        }
        public UserService(IOptions<AppSettings> appSettings, ProfileDbContext profileDbContext)
        {
            _appSettings = appSettings.Value;
            _profileDbContext = profileDbContext;
        }
        private DbSet<Profile> _profiles => _profileDbContext.Profiles;
        public User Authenticate(string username, string password)
        {
            
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

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
            var data = _users.SingleOrDefault(x => x.Username == username);
            Profile newdata = null;
            if (data != null)
                return null;
            else
                newdata=ToEntity(fn,  ln, em, addr, username, password);
            _profiles.Add(newdata);
            try
            {
                _profileDbContext.SaveChanges();
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


            public IEnumerable<User> GetAll()
        {
            return _users.WithoutPasswords();
        }

        public (bool result, Exception exception) DeleteProfileById(int id)
        {
            var profile = _Profiles.SingleOrDefault((Profile profile) => profile.Id == id);

            if (profile == null)
            {
                return (false, new ArgumentNullException($"Promo with id: {id} not found"));
            }

            EntityEntry<Profile> result = _Profiles.Remove(profile);

            try
            {
                _profileDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return (false, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (result.State == EntityState.Deleted, null);
        }

        public (Profile profile, Exception exception) Updateprofile(Profile _profile)
        {
            Profile profile = this._Profiles.SingleOrDefault((Profile profile) => profile.Id == _profile.Id);

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
                _profileDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (profile, null);
        }
    }
}
