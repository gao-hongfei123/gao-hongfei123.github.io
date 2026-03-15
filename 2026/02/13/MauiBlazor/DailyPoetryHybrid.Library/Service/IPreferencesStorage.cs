using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPoetryHybrid.Library.Service
{
    //用来进行数据库的版本存储，偏好存储，键值对存储
    //其实现类在Hybrid项目中，因为需要使用Maui中自带的preferences类的偏好存储功能来存储数据库版本
    //这样完成了底层对上层的引用
    public interface IPreferencesStorage
    {
        //获取数据库版本
        public int Get(string key,int defaultValue);
        //设置数据库版本
        public void Set(string key,int value);
    }
}
