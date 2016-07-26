using SQLitePCL;
using Studio_Professional.Json;
using Studio_Professional.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Studio_Professional.Repository
{
    public class AboutContentRepository : IDisposable
    {
        private SQLiteConnection connection;
        private AboutPage aboutPageContent;

        public AboutContentRepository(SQLiteConnection connection)
        {
            this.connection = connection;
            CreateTable();
        }

        /// <summary>
        /// Предоставляет доступ и хранит данные страницы "О нас" во время выполнения приложения
        /// </summary>
        public AboutPage Content
        {
            get
            {
                if (aboutPageContent == null)
                {
                    aboutPageContent = GetAboutPageContent();
                }
                return aboutPageContent;
            }
        }

        /// <summary>
        /// Создает тадлицу AboutPageContent, если еще не создана
        /// </summary>
        public void CreateTable()
        {
            string query = @"CREATE TABLE IF NOT EXISTS AboutPageContent 
            (
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
                Utd INTEGER NOT NULL
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
            using (var statement = connection.Prepare(@"SELECT Id, Image1_1, Image1_2, Image1_3, Image1_4, TextHeader, TextContent,
                YouTubeId1, TextContent2,Image2_1, Image2_2, YouTubeId2, AdressHeader, AdressText, ContactHeader, ContactText, PhoneHeader,
                PhoneText, SocialLinkVk, SocialLinkTw, SocialLinkInst, SocialLinkFb, MapX, MapY, Utd FROM AboutPageContent WHERE Id=42"))
            {
                if (statement.Step() != SQLiteResult.ROW)
                {
                    return null;
                }
                aboutPageContent = new AboutPage
                {
                    Id = statement.GetInteger(0),
                    Image1 = statement.GetBlob(1),
                    Image2 = statement.GetBlob(2),
                    Image3 = statement.GetBlob(3),
                    Image4 = statement.GetBlob(4),
                    TextHeader = (string)statement[5],
                    TextContent = (string)statement[6],
                    YouTubeId1 = (string)statement[7],
                    TextContent2 = (string)statement[8],
                    Image5 = statement.GetBlob(9),
                    Image6 = statement.GetBlob(10),
                    YouTubeId2 = (string)statement[11],
                    AdressHeader = (string)statement[12],
                    AdressText = (string)statement[13],
                    ContactHeader = (string)statement[14],
                    ContactText = (string)statement[15],
                    PhoneHeader = (string)statement[16],
                    PhoneText = (string)statement[17],
                    SocialLinkVk = (string)statement[18],
                    SocialLinkTw = (string)statement[19],
                    SocialLinkInst = (string)statement[20],
                    SocialLinkFb = (string)statement[21],
                    MapX = statement.GetFloat(22),
                    MapY = statement.GetFloat(23),
                    Utd = new DateTime(statement.GetInteger(24))
                };
                return Content;
            }
        }

        /// <summary>
        /// Сохраняет данные страницы в базу данных
        /// </summary>
        /// <param name="aboutPage">Объект для сохранения</param>
        public void Insert(AboutPage aboutPage)
        {
            string query = @"INSERT INTO AboutPageContent(   
                    Id,
                    Image1_1,
                    Image1_2,
                    Image1_3,
                    Image1_4,
                    TextHeader,
                    TextContent,
                    YouTubeId1,
                    TextContent2,
                    Image2_1,
                    Image2_2,
                    YouTubeId2,
                    AdressHeader,
                    AdressText,
                    ContactHeader,
                    ContactText,
                    PhoneHeader,
                    PhoneText,
                    SocialLinkVk,
                    SocialLinkTw,
                    SocialLinkInst,
                    SocialLinkFb,
                    MapX,
                    MapY,
                    Utd
                ) 
                VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            using (var statement = connection.Prepare(query))
            {
                statement.Bind(1, 42);
                statement.Bind(2, aboutPage.Image1);
                statement.Bind(3, aboutPage.Image2);
                statement.Bind(4, aboutPage.Image3);
                statement.Bind(5, aboutPage.Image4);
                statement.Bind(6, aboutPage.TextHeader);
                statement.Bind(7, aboutPage.TextContent);
                statement.Bind(8, aboutPage.YouTubeId1);
                statement.Bind(9, aboutPage.TextContent2);
                statement.Bind(10, aboutPage.Image5);
                statement.Bind(11, aboutPage.Image6);
                statement.Bind(12, aboutPage.YouTubeId2);
                statement.Bind(13, aboutPage.AdressHeader);
                statement.Bind(14, aboutPage.AdressText);
                statement.Bind(15, aboutPage.ContactHeader);
                statement.Bind(16, aboutPage.ContactText);
                statement.Bind(17, aboutPage.PhoneHeader);
                statement.Bind(18, aboutPage.PhoneText);
                statement.Bind(19, aboutPage.SocialLinkVk);
                statement.Bind(20, aboutPage.SocialLinkTw);
                statement.Bind(21, aboutPage.SocialLinkInst);
                statement.Bind(22, aboutPage.SocialLinkFb);
                statement.Bind(23, aboutPage.MapX);
                statement.Bind(24, aboutPage.MapY);
                statement.Bind(25, aboutPage.Utd.Ticks);
                statement.Step();
            }
        }

        /// <summary>
        /// Обновляет данные страницы в базу данных
        /// </summary>
        public void UpdatePageContent(AboutPage aboutPage)
        {
            string query = @"
                UPDATE AboutPageContent Set 
                    Image1_1=?,
                    Image1_2=?,
                    Image1_3=?,
                    Image1_4=?,
                    TextHeader=?,
                    TextContent=?,
                    YouTubeId1=?,
                    TextContent2=?,
                    Image2_1=?,
                    Image2_2=?,
                    YouTubeId2=?,
                    AdressHeader=?,
                    AdressText=?,
                    ContactHeader=?,
                    ContactText=?,
                    PhoneHeader,
                    PhoneText=?,
                    SocialLinkVk=?,
                    SocialLinkTw=?,
                    SocialLinkInst=?,
                    SocialLinkFb=?,
                    MapX=?,
                    MapY=?,
                    Utd=?
                WHERE Id=42 ";
            using (var statement = connection.Prepare(query))
            {
                statement.Bind(1, aboutPage.Image1);
                statement.Bind(2, aboutPage.Image2);
                statement.Bind(3, aboutPage.Image3);
                statement.Bind(4, aboutPage.Image4);
                statement.Bind(5, aboutPage.TextHeader);
                statement.Bind(6, aboutPage.TextContent);
                statement.Bind(7, aboutPage.YouTubeId1);
                statement.Bind(8, aboutPage.TextContent2);
                statement.Bind(9, aboutPage.Image5);
                statement.Bind(10, aboutPage.Image6);
                statement.Bind(11, aboutPage.YouTubeId2);
                statement.Bind(12, aboutPage.AdressHeader);
                statement.Bind(13, aboutPage.AdressText);
                statement.Bind(14, aboutPage.ContactHeader);
                statement.Bind(15, aboutPage.ContactText);
                statement.Bind(16, aboutPage.PhoneHeader);
                statement.Bind(17, aboutPage.PhoneText);
                statement.Bind(18, aboutPage.SocialLinkVk);
                statement.Bind(19, aboutPage.SocialLinkTw);
                statement.Bind(20, aboutPage.SocialLinkInst);
                statement.Bind(21, aboutPage.SocialLinkFb);
                statement.Bind(22, aboutPage.MapX);
                statement.Bind(23, aboutPage.MapY);
                statement.Bind(24, aboutPage.Utd.Ticks);
                statement.Step();
            }
        }

        /// <summary>
        /// Удаляет данные страницы из базы данных
        /// </summary>
        public void Delete()
        {
            if (Content != null)
            {
                using (var statement = connection.Prepare("DELETE FROM AboutPageContent WHERE Id=?"))
                {
                    statement.Bind("Id", Content.Id);
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
                return new DateTime(statement.GetInteger("Utd"));
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
