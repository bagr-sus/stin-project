﻿using Microsoft.EntityFrameworkCore;
using STINProject_API.Services.PersistenceService.Model;
using STINProject_API.Services.PersistenceService.Models;

namespace STINProject_API.Services.PersistenceService
{
    public class SQLitePersistenceService : IPersistenceService
    {
        private readonly SQLiteDataContext _context;
        public SQLitePersistenceService()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var connectionString = $"Data source={Path.Join(path, "blogging.db")}";
            _context = new SQLiteDataContext(connectionString);
        }

        public bool AddTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            return _context.SaveChanges() == 1;
        }

        public IEnumerable<Account> GetAccounts(Guid userId)
        {
            return _context.Accounts.Where(x => x.OwnerId == userId);
        }

        public IEnumerable<Transaction> GetTransactions(Guid accountId)
        {
            return _context.Transactions.Where(x => x.AccountID == accountId);
        }

        public User GetUser(Guid id)
        {
            return _context.Users.Single(x => x.UserId == id);
        }

        public User GetUser(string username)
        {
            return _context.Users.Single(x => x.Username == username);
        }

        public bool AddAccount(Account account)
        {
            _context.Add(account);
            return _context.SaveChanges() == 1;
        }
    }
}
