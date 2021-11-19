using System;
using Npgsql;

namespace Automated.Testing.System.DataAccess.Postgres.Extensions
{
    public static class NpgsqlParameterCollectionExtensions
    {
        public static void AddWithNullValue(this NpgsqlParameterCollection str, string parameterName, object value)
        {
            str.AddWithValue(parameterName, value ?? DBNull.Value);
        }
    }
}