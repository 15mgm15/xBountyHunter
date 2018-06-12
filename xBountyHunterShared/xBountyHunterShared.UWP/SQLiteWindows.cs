using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using Xamarin.Forms;
using xBountyHunterShared.DependencyServices;
using xBountyHunterShared.UWP;
using System.IO;
using Windows.Storage;

[assembly : Dependency (typeof(SQLiteWindows))]

namespace xBountyHunterShared.UWP
{
    public class SQLiteWindows : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "mFugitivos.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            var platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
            SQLiteConnection conn = new SQLiteConnection(platform, path);
            return conn;
        }
    }
}
