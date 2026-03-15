using DailyPoetryHybrid.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DailyPoetryHybrid.Library.Service
{
    
    public interface IPoetryStorage
    {
        //数据库是否初始化
        public bool IsInitialized { get; }

        //执行数据库初始化,文件操作必须使用异步
        Task InitializedAsync();

        Task<Poetry> GetPoetryAsync(int id);

        //不限制查询条件
        Task<IEnumerable<Poetry>> GetPoetriesAsync(
            Expression<Func<Poetry, bool>> where, int skip, int take);

            
    }
}
