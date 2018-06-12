using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.Models;

namespace xBountyHunterShared.Extras
{
    public class databaseManager
    {
        static int DATABASE_VERSION = 1;
        SQLiteConnection db;
        int oldVersion = 0;

        public databaseManager()
        {
            db = DependencyService.Get<ISQLite>().GetConnection();
            if(Application.Current.Properties.ContainsKey("DATABASE_VERSION"))
            {
                oldVersion = (int)Application.Current.Properties["DATABASE_VERSION"];
            }
            if(DATABASE_VERSION != oldVersion && oldVersion != 0)
            {
                onUpgrade();
            }
            if(oldVersion == 0)
            {
                CreateTable();
            }
        }

        private void CreateTable()
        {
            db.CreateTable<mFugitivos>();
            Application.Current.Properties["DATABASE_VERSION"] = DATABASE_VERSION;
        }

        private void onUpgrade()
        {
            db.DropTable<mFugitivos>();
            CreateTable();
        }

        public List<mFugitivos> selectNoCaptured()
        {
            List<mFugitivos> result = db.Query<mFugitivos>("SELECT * FROM [mFugitivos]" +
                " WHERE [Capturado] = 0 or [Capturado] is null");
            return result;
        }

        public List<mFugitivos> selectCaptured()
        {
            List<mFugitivos> result = db.Query<mFugitivos>("SELECT * FROM [mFugitivos]" +
                " WHERE [Capturado] = 1");
            return result;
        }

        public List<mFugitivos> selectAll()
        {
            List<mFugitivos> result = db.Query<mFugitivos>("SELECT * FROM [mFugitivos]");
            return result;
        }

        public int insertItem(mFugitivos item)
        {
            int result = db.Insert(item);
            return result;
        }

        public int updateItem(mFugitivos item)
        {
            int result = db.Update(item);
            return result;
        }

        public int deleteItem(int id)
        {
            int result = db.Delete<mFugitivos>(id);
            return result;
        }

        public void closeConnection()
        {
            db.Close();
        }
    }
}
