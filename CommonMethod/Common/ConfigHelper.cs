using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMethodLibrary
{
    public class ConfigHelper
    {
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="isDecoding">是否解码</param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key, bool isDecoding = false)
        {
            AppSettingsReader reader = new AppSettingsReader();
            string result = reader.GetValue(key, typeof(string)).ToString();
            return result;
        }
    }
}
