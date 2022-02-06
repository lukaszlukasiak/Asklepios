using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.ServiceClasses
{
    public static class MvcExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string controllers = null, string actions = null, string cssClass = "active")
        {
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

            var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
            var acceptedActions = (actions ?? currentAction ?? "").Split(',');

            return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
                ? cssClass
                : "";
        }
        //public static SelectList ReturnSelectListWithSingleEnum(Enum defaultValue ) 
        //{     
        //    var selectList = HtmlHelper.GetEnumSelectList<defaultValue>().ToList();
        //    selectList.Single(x => x.Value == $"{(int)(object)defaultValue}").Selected = true;
        //    return selectList;
        //}
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj, Enum value) where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = e.ToString() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
        public static IEnumerable<SelectListItem> GetEnumSelectListWithOneValue<TEnum>(this IHtmlHelper htmlHelper, TEnum defaultValue)
where TEnum : struct
        {
            List<SelectListItem> selectList = htmlHelper.GetEnumSelectList<TEnum>().ToList();
            string defaultVal = ((int)(object)defaultValue).ToString();
           // selectList = new SelectList( selectList.Where(x => x.Value == defaultVal)).ToList();
            for (int i = selectList.Count()-1; i >= 0; i--)
            {
                string localVal = selectList.ElementAt(i).Value;
                if (localVal!=defaultVal)
                {
                    selectList.RemoveAt(i);
                }
            }
            //selectList.Single(x => x.Value == $"{(int)(object)defaultValue}").Selected = true;
            selectList.ElementAt(0).Selected = true;
            return selectList;
        }


    }
}
