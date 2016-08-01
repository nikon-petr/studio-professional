using SQLitePCL;
using Studio_Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studio_Professional.Popups;

namespace Studio_Professional.Repository
{
    /// <summary>
    /// Предоставляет доступ к локальной базе данных
    /// </summary>
    public class UserRepository
    {
        private SQLiteConnection connection;
        private User userInfo;

        public UserRepository(SQLiteConnection connection)
        {
            this.connection = connection;
            CreateTable();
        }

        /// <summary>
        /// Предоставляет доступ и хранит информацию о пользователе во время выполнения приложения
        /// </summary>
        public User Data
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

        /// <summary>
        /// Создает таблицу User в базе данных, если еще не создана
        /// </summary>
        private void CreateTable()
        {
            try
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
            catch (SQLiteException)
            {
                Messages.ShowDatabaseErrorMessage();
            }
            catch (Exception e)
            {
                Messages.ShowErrorMessage(e.Message);
            }
        }

        /// <summary>
        /// Получает информацию о пользователе из базы данных
        /// </summary>
        /// <returns></returns>
        private User GetUser()
        {
            try
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
            catch (SQLiteException)
            {
                Messages.ShowDatabaseErrorMessage();
            }
            catch (Exception e)
            {
                Messages.ShowErrorMessage(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Вставляет данные о пользователе в базу
        /// </summary>
        public void Insert(User user)
        {
            try
            {
                using (var statement = connection.Prepare("INSERT INTO User(Name, Number, LastLogin) VALUES(?, ?, ?)"))
                {
                    statement.Bind(1, user.Name);
                    statement.Bind(2, user.Number);
                    statement.Bind(3, user.LastLogin.Ticks);
                    statement.Step();
                }
            }
            catch (SQLiteException)
            {
                Messages.ShowDatabaseErrorMessage();
            }
            catch(Exception e)
            {
                Messages.ShowErrorMessage(e.Message);
            }
        }

        /// <summary>
        /// Обновляет дату последнего входа в приложение
        /// </summary>
        public void UpdateDate()
        {
            try
            {
                using (var statement = connection.Prepare("UPDATE User Set LastLogin=? WHERE Number=?"))
                {
                    statement.Bind(1, DateTime.Now.Ticks);
                    statement.Bind(2, Data.Number);
                    statement.Step();
                }
            }
            catch (SQLiteException)
            {
                Messages.ShowDatabaseErrorMessage();
            }
            catch (Exception e)
            {
                Messages.ShowErrorMessage(e.Message);
            }
        }

        /// <summary>
        /// Удаляет данные о текущем пользователе
        /// </summary>
        public void Delete()
        {
            try
            {
                if (Data != null)
                {
                    using (var statement = connection.Prepare("DELETE FROM User WHERE Number=?"))
                    {
                        statement.Bind(1, Data.Number);
                        statement.Step();
                        userInfo = null;
                    }
                }
            }
            catch (SQLiteException)
            {
                Messages.ShowDatabaseErrorMessage();
            }
            catch(Exception e)
            {
                Messages.ShowErrorMessage(e.Message);
            }
        }
    }
}
