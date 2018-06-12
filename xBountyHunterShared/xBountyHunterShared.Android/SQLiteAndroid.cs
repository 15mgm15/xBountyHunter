using SQLite;
using xBountyHunterShared.Droid;
using Xamarin.Forms;
using System.IO;

[assembly : Dependency (typeof(SQLiteAndroid))]

namespace xBountyHunterShared.Droid
{
    public class SQLiteAndroid : DependencyServices.ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "mFugitivos.db3";
            string docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(docPath, sqliteFilename);

            SQLiteConnection conn = new SQLiteConnection(path);
            return conn;
        }
    }
}