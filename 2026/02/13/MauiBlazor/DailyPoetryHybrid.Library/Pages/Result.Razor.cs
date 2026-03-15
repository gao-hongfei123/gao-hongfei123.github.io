using DailyPoetryHybrid.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DailyPoetryHybrid.Library.Pages
{
    public partial class Result
    {
        private Expression<Func<Poetry, bool>> _where = p => true;

        public const string Loading = "正在载入";
        public const string NoResult = "没有满足条件的结果";
        public const string NoMoreResult = "没有更多的结果";

        private string _status = string.Empty;

        public const int PageSize = 20;
        //使用List是因为它可以在无限滚动时添加数据,而IEnumerable不行
        private List<Poetry> _poetries = new();

        //页面渲染完成时就LoadMoreAsync，否则因为没有数据显示空白
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

                //初始化数据库
                await _poetryStroage.InitializedAsync();

                //第一次渲染就触发
                //滚动加载
                await LoadMoreAsync();
            
        }

        //无限滚动加载数据
        public async Task LoadMoreAsync()
        {
            var poetries = await _poetryStroage.GetPoetriesAsync(_where,_poetries.Count,PageSize);
            _poetries.AddRange(poetries);
        }


    }
}
