using System;
using System.ComponentModel;
using System.Reflection;

namespace Anita
{
    public static class EnumUtils
    {
        /// <summary>
        /// 获取枚举值上的Description特性的说明
        /// </summary>
        /// <param name="en">枚举值</param>
        /// <returns>特性的说明</returns>
        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();                                   // 获取类型  
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   // 获取成员  
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   // 获取描述特性  
                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;                        // 返回当前描述
                }
            }
            return en.ToString();
        }
    }
}