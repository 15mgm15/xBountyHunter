using System;
using xBountyHunterShared.iOS;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using System.IO;
using SQLite;

[assembly : Dependency (typeof(SQLiteiOS))]

namespace xBountyHunterShared.iOS
{
    public class SQLiteiOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "mFugitivos.db3";
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libPath = Path.Combine(docPath, "..", "Library");
            string path = Path.Combine(docPath, sqliteFilename);

            SQLiteConnection conn = new SQLiteConnection(path);
            return conn;
        }
    }
}