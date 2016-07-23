using SQLitePCL;
using Studio_Professional.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Repository
{
    public class ContentRepository
    {
        private SQLiteConnection connection;
        private AboutPage aboutPageContent;

        public ContentRepository(string name)
        {
            connection = new SQLiteConnection(name);
        }

        /// <summary>
        /// Предоставляет доступ и хранит данные страницы "О нас" во время выполнения приложения
        /// </summary>
        public AboutPage AboutPageContent
        {
            get
            {
                if (aboutPageContent != null)
                {
                    return aboutPageContent;
                }
                aboutPageContent = GetAboutPageContent();
                return aboutPageContent;
            }
        }

        /// <summary>
        /// Создает тадлицу AboutPageContent, если еще не создана
        /// </summary>
        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS
                                AboutPageContent (
                                      Id INTEGER PRIMARY KEY NOT NULL,
                                      Image1_1 BLOB NOT NULL,
                                      Image1_2 BLOB NOT NULL,
                                      Image1_3 BLOB NOT NULL,
                                      Image1_4 BLOB NOT NULL,
                                      TextHeader TEXT NOT NULL,
                                      TextContent TEXT NOT NULL,
                                      YouTubeId1 TEXT NOT NULL,
                                      TextContent2 TEXT NOT NULL,
                                      Image2_1 BLOB NOT NULL,
                                      Image2_2 BLOB NOT NULL,
                                      YouTubeId2 TEXT NOT NULL,
                                      AdressHeader TEXT NOT NULL,
                                      AdressText TEXT NOT NULL,
                                      ContactHeader TEXT NOT NULL,
                                      ContactText TEXT NOT NULL,
                                      PhoneHeader TEXT NOT NULL,
                                      PhoneText TEXT NOT NULL,
                                      SocialLinkVk TEXT NOT NULL,
                                      SocialLinkTw TEXT NOT NULL,
                                      SocialLinkInst TEXT NOT NULL,
                                      SocialLinkFb TEXT NOT NULL,
                                      MapX REAL NOT NULL,
                                      MapY REAL NOT NULL,
                                      Utd INTEGER NOT NULL,
                            );";
            using (var statement = connection.Prepare(query))
            {
                statement.Step();
            }
        }

        /// <summary>
        /// Получает данные страницы из базы данных
        /// </summary>
        /// <returns></returns>
        private AboutPage GetAboutPageContent()
        {
            using (var statement = connection.Prepare("SELECT * FROM AboutPageContent"))
            {
                if (statement.Step() != SQLiteResult.ROW)
                {
                    return null;
                }
                // TODO
                return new AboutPage { };
            }
        }

        /// <summary>
        /// Сохраняет данные страницы в базу данных
        /// </summary>
        /// <param name="aboutPage">Объект для сохранения</param>
        public void Insert(AboutPage aboutPage)
        {
            using (var statement = connection.Prepare("INSERT INTO AboutPageContent() VALUES(?, ?, ?)"))
            {
                // TODO
                statement.Step();
            }
        }

        /// <summary>
        /// Обновляет данные страницы в базу данных
        /// </summary>
        /// <param name="aboutPage"></param>
        public void UpdatePageContent(AboutPage aboutPage)
        {
            using (var statement = connection.Prepare("UPDATE AboutPageContent Set WHERE "))
            {
                // TODO
                statement.Step();
            }
        }

        /// <summary>
        /// Удаляет данные страницы из базы данных
        /// </summary>
        public void Delete()
        {
            if (AboutPageContent != null)
            {
                using (var statement = connection.Prepare("DELETE FROM AboutPageContent WHERE Id=?"))
                {
                    statement.Bind(1, AboutPageContent.Id);
                    statement.Step();
                    aboutPageContent = null;
                }
            }
        }

        /// <summary>
        /// Возвращает дату последнего обновлеия данных страцины из базы данных
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastUpdateDate()
        {
            using (var statement = connection.Prepare("SELECT Utd FROM AboutPageContent"))
            {
                if (statement.Step() != SQLiteResult.ROW)
                {
                    return new DateTime(0);
                }
                return new DateTime((long)statement[0]);
            }
        }
    }
}
