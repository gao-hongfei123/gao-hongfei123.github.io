using DailyPoetryHybrid.Library.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DailyPoetryHybrid.Library.Service
{
    public class PoetryStorage : IPoetryStorage
    {
        public const string DbName = "poetrydb.sqlite3";
        public static readonly string PoetryDbPath = 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Connection => _connection ??= new SQLiteAsyncConnection(PoetryDbPath);

        private readonly IPreferencesStorage _preferencesStorage;
        public PoetryStorage(IPreferencesStorage preferencesStorage)
        {
            _preferencesStorage = preferencesStorage;
        }
        //若为true则表示数据库版本没变，否则需要更新数据库
        //是否初始化数据库
        public bool IsInitialized => _preferencesStorage.Get(PoetryStorageConstant.DbVersionKey, 0) == PoetryStorageConstant.Version;
        //不限制条件查询
        public async Task<IEnumerable<Poetry>> GetPoetriesAsync(Expression<Func<Poetry, bool>> where, int skip, int take)
        {
            return await Connection.Table<Poetry>().Where(where).Skip(skip).Take(take).ToListAsync();
        }
        //单个查询
        public async Task<Poetry> GetPoetryAsync(int id)
        {
            return await  Connection.Table<Poetry>().FirstOrDefaultAsync(p => p.Id == id);
        }

        //初始化数据库
        public async Task InitializedAsync()
        {
            await using var dbFileStream = new FileStream(PoetryDbPath, FileMode.OpenOrCreate);
            await using var dbAssectStream = typeof(PoetryStorage).Assembly.GetManifestResourceStream(DbName);
            await dbAssectStream.CopyToAsync(dbFileStream);
            _preferencesStorage.Set(PoetryStorageConstant.DbVersionKey, PoetryStorageConstant.Version);
        }


        }
}

    //存储键值对
    public static class PoetryStorageConstant
        {
        public const string DbVersionKey =
        nameof(PoetryStorageConstant) + "." + nameof(DbVersionKey);
        
        //当前数据库版本
        public const int Version = 1;


        }

