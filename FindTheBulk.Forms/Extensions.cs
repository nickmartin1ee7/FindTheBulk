using System;
using System.Text;

namespace FindTheBulk.Forms
{
    public static class Extensions
    {
        public static string PrintProperties(this object obj)
        {
            var sb = new StringBuilder();

            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                sb.Append($"{propertyInfo.Name}: {propertyInfo.GetValue(obj)}{Environment.NewLine}");
            }

            return sb.ToString();
        }
    }
}
