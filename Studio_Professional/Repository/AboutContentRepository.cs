using SQLitePCL;
using Studio_Professional.Json;
using Studio_Professional.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Studio_Professional.Repository
{
    public class AboutContentRepository
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
                    Task.Run(async () =>
                    {
                        aboutPageContent = await GetAboutPageContent();
                    });
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
        private async Task<AboutPage> GetAboutPageContent()
        {
            using (var statement = connection.Prepare("SELECT * FROM AboutPageContent"))
            {
                if (statement.Step() != SQLiteResult.ROW)
                {
                    return null;
                }
                aboutPageContent = new AboutPage
                {
                    Id = statement.GetInteger("Id"),
                    Image1 = await AboutPage.ConvertBytesToBitmapImage(statement.GetBlob("Image1-1")),
                    Image2 = await AboutPage.ConvertBytesToBitmapImage(statement.GetBlob("Image1-2")),
                    Image3 = await AboutPage.ConvertBytesToBitmapImage(statement.GetBlob("Image1-3")),
                    Image4 = await AboutPage.ConvertBytesToBitmapImage(statement.GetBlob("Image1-4")),
                    TextHeader = statement.GetText("TextHeader"),
                    TextContent = statement.GetText("TextContent"),
                    YouTubeId1 = statement.GetText("YouTubeId1"),
                    TextContent2 = statement.GetText("TextContent2"),
                    Image5 = await AboutPage.ConvertBytesToBitmapImage(statement.GetBlob("Image2-1")),
                    Image6 = await AboutPage.ConvertBytesToBitmapImage(statement.GetBlob("Image2-2")),
                    YouTubeId2 = statement.GetText("YouTubeId2"),
                    AdressHeader = statement.GetText("AdressText"),
                    AdressText = statement.GetText("AdressText"),
                    ContactHeader = statement.GetText("ContactHeader"),
                    ContactText = statement.GetText("ContactText"),
                    PhoneHeader = statement.GetText("PhoneHeader"),
                    PhoneText = statement.GetText("PhoneText"),
                    SocialLinkVk = statement.GetText("SocialLinkVk"),
                    SocialLinkTw = statement.GetText("SocialLinkTw"),
                    SocialLinkInst = statement.GetText("SocialLinkInst"),
                    SocialLinkFb = statement.GetText("SocialLinkFb"),
                    MapX = statement.GetFloat("MapX"),
                    MapY = statement.GetFloat("MapY"),
                    Utd = new DateTime(statement.GetInteger("Utd"))
                };
                return Content;
            }
        }

        /// <summary>
        /// Сохраняет данные страницы в базу данных
        /// </summary>
        /// <param name="aboutPage">Объект для сохранения</param>
        public async void Insert(AboutAnswer aboutPage)
        {
            await Task.Run(() =>
            {
                string query = @"INSERT INTO AboutPageContent VALUES
                (
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
                )";
                using (var statement = connection.Prepare(query))
                {
                    statement.Bind("Id", 42);
                    statement.Bind("Image1_1", aboutPage.Image1);
                    statement.Bind("Image1_2", aboutPage.Image2);
                    statement.Bind("Image1_3", aboutPage.Image3);
                    statement.Bind("Image1_4", aboutPage.Image4);
                    statement.Bind("TextHeader", aboutPage.TextHeader);
                    statement.Bind("TextContent", aboutPage.TextContent);
                    statement.Bind("YouTubeId1", aboutPage.YouTubeId1);
                    statement.Bind("Image2_1", aboutPage.Image5);
                    statement.Bind("Image2_2", aboutPage.Image6);
                    statement.Bind("YouTubeId2", aboutPage.YouTubeId2);
                    statement.Bind("AdressHeader", aboutPage.AdressHeader);
                    statement.Bind("AdressText", aboutPage.AdressText);
                    statement.Bind("ContactHeader", aboutPage.ContactHeader);
                    statement.Bind("ContactText", aboutPage.ContactText);
                    statement.Bind("PhoneHeader", aboutPage.PhoneHeader);
                    statement.Bind("PhoneText", aboutPage.PhoneText);
                    statement.Bind("SocialLinkVk", aboutPage.SocialLinkVk);
                    statement.Bind("SocialLinkTw", aboutPage.SocialLinkTw);
                    statement.Bind("SocialLinkInst", aboutPage.SocialLinkInst);
                    statement.Bind("SocialLinkFb", aboutPage.SocialLinkFb);
                    statement.Bind("MapX", aboutPage.MapX);
                    statement.Bind("MapY", aboutPage.MapY);
                    statement.Bind("Utd", aboutPage.Utd);
                    statement.Step();
                }
            });
        }

        /// <summary>
        /// Обновляет данные страницы в базу данных
        /// </summary>
        public void UpdatePageContent(AboutAnswer aboutPage)
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
                statement.Bind("Image1_1", aboutPage.Image1);
                statement.Bind("Image1_2", aboutPage.Image2);
                statement.Bind("Image1_3", aboutPage.Image3);
                statement.Bind("Image1_4", aboutPage.Image4);
                statement.Bind("TextHeader", aboutPage.TextHeader);
                statement.Bind("TextContent", aboutPage.TextContent);
                statement.Bind("YouTubeId1", aboutPage.YouTubeId1);
                statement.Bind("TextContent2", aboutPage.TextContent2);
                statement.Bind("Image2_1", aboutPage.Image5);
                statement.Bind("Image2_2", aboutPage.Image6);
                statement.Bind("YouTubeId2", aboutPage.YouTubeId2);
                statement.Bind("AdressHeader", aboutPage.AdressHeader);
                statement.Bind("AdressText", aboutPage.AdressText);
                statement.Bind("ContactHeader", aboutPage.ContactHeader);
                statement.Bind("ContactText", aboutPage.ContactText);
                statement.Bind("PhoneHeader", aboutPage.PhoneHeader);
                statement.Bind("PhoneText", aboutPage.PhoneText);
                statement.Bind("SocialLinkVk", aboutPage.SocialLinkVk);
                statement.Bind("SocialLinkTw", aboutPage.SocialLinkTw);
                statement.Bind("SocialLinkInst", aboutPage.SocialLinkInst);
                statement.Bind("SocialLinkFb", aboutPage.SocialLinkFb);
                statement.Bind("MapX", aboutPage.MapX);
                statement.Bind("MapX", aboutPage.MapY);
                statement.Bind("Utd", aboutPage.Utd);
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
    }
}
