using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asklepios.Web.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the Description from the DescriptionAttribute.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        //public static string GetDescription(this Enum enumValue)
        //{
        //    return enumValue.GetType()
        //               .GetMember(enumValue.ToString())
        //               .First()
        //               .GetCustomAttribute<DescriptionAttribute>()?
        //               .Description ?? string.Empty;
        //}

    }
}
