using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker
{
    public static class DBConfig
    {
        private static readonly Lazy<string> cs = new(() =>
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build()
                .GetConnectionString("SeminarskiSoftveri")
            ?? throw new InvalidOperationException("Nedostaje konekcioni string 'SeminarskiSoftveri'."));

        public static string ConnectionString => cs.Value;
    }
}
