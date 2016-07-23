using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_Professional.Repository
{
    public class AppRepository
    {
        private SQLiteConnection connection;
        private UserRepository user;
        private AboutContentRepository aboutPage;

        public UserRepository User
        {
            get
            {
                if (user == null)
                {
                    user = new UserRepository(connection);
                }
                return user;
            }
        }

        public AboutContentRepository AboutPage
        {
            get
            {
                if(aboutPage == null)
                {
                    aboutPage = new AboutContentRepository(connection);
                }
                return aboutPage;
            }
        }

        public AppRepository()
        {
            connection = new SQLiteConnection("app.db");
        }
    }
}
