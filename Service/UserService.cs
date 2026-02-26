using pract12_trpo.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract12_trpo.Service
{
    public class UsersService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<User> Users { get; set; } = new();
        public UsersService()
        {
            GetAll();
        }
        public void Add(User user)
        {
            var _user = new User
            {
                Login = user.Login,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreateAt = DateTime.Now,
            };
            _db.Add<User>(_user);
            Commit();
            Users.Add(_user);
        }
        public int Commit() => _db.SaveChanges();
        public void GetAll()
        {
            var users = _db.Users.ToList();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
        public void Remove(User user)
        {
            _db.Remove<User>(user);
            if (Commit() > 0)
                if (Users.Contains(user))
                    Users.Remove(user);
        }
    }
}
