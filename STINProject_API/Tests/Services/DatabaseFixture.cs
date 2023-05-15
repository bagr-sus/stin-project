﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using STINProject_API.Services.PersistenceService;
using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Tests.Services
{
    public class DatabaseFixture : IDisposable
    {
        public User TestUser { get; private set; }
        public IEnumerable<Account> TestAccounts { get; private set; }
        public IEnumerable<Transaction> TestTransactions { get; private set; }
        public SQLitePersistenceService InMemoryService { get; private set; }
        public DatabaseFixture()
        {
            TestUser = ContextMockingTools.SampleUsers(1).First();
            TestAccounts = ContextMockingTools.SampleAccounts(3, TestUser.UserId);
            TestTransactions = ContextMockingTools.SampleTransactions(5, TestAccounts.First().AccountId);

            var connection = new SqliteConnection("Data Source =:memory:;");
            connection.Open();

            var options = new DbContextOptionsBuilder<SQLiteDataContext>()
                .UseSqlite(connection)
                .Options;

            var context = new SQLiteDataContext(options);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            InMemoryService = new SQLitePersistenceService(context);

            // ... initialize data in the test database ...
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }

    }
}