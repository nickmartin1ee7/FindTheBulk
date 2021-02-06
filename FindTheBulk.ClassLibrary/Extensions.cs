using System;
using System.IO;
using System.Text;

namespace FindTheBulk.ClassLibrary
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

        public static string GetFileSizeString(this FileInfo file)
        {
            var loops = 0;
            var length = file.Length;

            while (length > 1024)
            {
                loops++;
                length /= 1024;
            }


            var unit = string.Empty;
            switch (loops)
            {
                case 0:
                    unit = "Bytes";
                    break;
                case 1:
                    unit = "KB";
                    break;
                case 2:
                    unit = "MB";
                    break;
                case 3:
                    unit = "GB";
                    break;
                case 4:
                    unit = "TB";
                    break;
                case 5:
                    unit = "PB";
                    break;
                default:
                    break;
            }

            return $"{length} {unit}";
        }
    }
}
