using SQLite;

namespace xBountyHunterShared.DependencyServices
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
