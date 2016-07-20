using SQLitePCL;
using Studio_Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Repository
{
    public class UserRepository
    {
        private SQLiteConnection connection;
        private User userInfo;

        public UserRepository(string name)
        {
            connection = new SQLiteConnection(name);
            CreateTable();
        }

        public User UserInfo
        {
            get
            {
                if (userInfo != null)
                {
                    return userInfo;
                }
                userInfo = GetUser();
                return userInfo;
            }
        }

        private void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS
                                User (Name VARCHAR(50) NOT NULL,
                                      Number VARCHAR(11) PRIMARY KEY NOT NULL,
                                      LastLogin INTEGER NOT NULL);";
            using (var statement = connection.Prepare(query))
            {
                statement.Step();
            }
        }

        private User GetUser()
        {
            using (var statement = connection.Prepare("SELECT Name, Number, LastLogin FROM User"))
            {
                if (statement.Step() != SQLiteResult.ROW)
                {
                    return null;
                }
                return new User
                {
                    Name = (string)statement[0],
                    Number = (string)statement[1],
                    LastLogin = new DateTime((long)statement[2])
                };
            }
        }

        public void Insert(User user)
        {
            using (var statement = connection.Prepare("INSERT INTO User(Name, Number, LastLogin) VALUES(?, ?, ?)"))
            {
                statement.Bind(1, user.Name);
                statement.Bind(2, user.Number);
                statement.Bind(3, user.LastLogin.Ticks);
                statement.Step();
            }
        }

        public void UpdateDate()
        {
            using (var statement = connection.Prepare("UPDATE User Set LastLogin=? WHERE Number=?"))
            {
                statement.Bind(1, DateTime.Now.Ticks);
                statement.Bind(2, UserInfo.Number);
                statement.Step();
            }
        }

        public void Delete()
        {
            if (UserInfo != null)
            {
                using (var statement = connection.Prepare("DELETE FROM User WHERE Number=?"))
                {
                    statement.Bind(1, UserInfo.Number);
                    statement.Step();
                    userInfo = null;
                }
            }
        }
    }
}
