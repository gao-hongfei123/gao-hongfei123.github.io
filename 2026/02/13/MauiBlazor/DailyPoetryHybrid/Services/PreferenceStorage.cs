using DailyPoetryHybrid.Library.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPoetryHybrid.Services
{
    //接口在library项目中，因为Preference类是Muai中自带的类，而library项目不能引用Maui
    //所以将接口放在library项目中，而实现类放在Hybrid项目中，这样完成了底层对上层的引用
    //PoetryStorage会调用Preferences来存储数据库版本
    public class PreferenceStorage : IPreferencesStorage
    {
        public int Get(string key, int defaultValue)
        {
            return Preferences.Get(key, defaultValue);
        }

        public void Set(string key, int value)
        {
            Preferences.Set(key, value);
        }
    }
}
