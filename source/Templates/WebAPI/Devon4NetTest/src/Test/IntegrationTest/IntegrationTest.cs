using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Devon4Net.Business.Common;
using Devon4Net.Domain.Entities;
using Devon4Net.Infrastructure.Test;
using System;
using System.IO;

namespace Devon4Net.Test.xUnit.Test.Integration
{
    public class IntegrationTest : DatabaseManagementTest<ModelContext>
    {
        public override void ConfigureContext()
        {
            try
            {
                var conn = $"DataSource={Directory.GetCurrentDirectory()}/Database/Resources/IntegrationTest.db";
                var connection = new SqliteConnection(conn);

                var builder = new DbContextOptionsBuilder<ModelContext>();
                builder.UseSqlite(connection);
                ContextOptions = builder.Options;
                Context = new ModelContext(ContextOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} : {ex.InnerException}");
            }
        }

        public override void ConfigureMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
           Mapper = mockMapper.CreateMapper();
        }
    }
}
