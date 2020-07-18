using System.Data;

namespace Infrastructure.SqlConnections
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetConnection();
    }
}