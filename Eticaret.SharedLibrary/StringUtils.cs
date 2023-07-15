namespace Eticaret.SharedLibrary
{
    public class StringUtils
    {
        public static string ListToString(List<string> list)
        {
            var str = "";
            foreach (string item in list)
            {
                string itemWithQuota = "'" + item + "'";
                str += itemWithQuota + ',';
            }
            str = str.TrimEnd(',');

            return str;
        }
    }
}
