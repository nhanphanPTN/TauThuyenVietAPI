using System;
using System.Text.RegularExpressions;

namespace TauThuyenViet.Utilities
{
    public static class Common
    {
        public static string GetFirstImage(this string value)
        {
            string result = value.Split('\n')[0];
            return result;
        }

        public static string[] GetListImage(this string value)
        {
            string[] result = value.Split('\n');
            return result;
        }

        public static string RemoveSign(this string value)
        {
            string result = value;

            //Hàm xử lý mã hóa link
            string[] charList = new string[]
            {
                "aáàảãạăắằẳẵặâấầẩẫậ", "oóòỏõọôốổỗộơớờởỡợ", "eéèẻẽẹêếềểễệ", "iíìỉĩị", "uúùủũụ", "yýỳỷỹỵ", "dđ"
            };

            for (int i = 0; i < charList.Length; i++)
            {
                for (int j = 1; j < charList[i].Length; j++)
                {
                    result = result.Replace(charList[i][j], charList[i][0]);
                    result = result.Replace(charList[i][j].ToString().ToUpper(), charList[i][0].ToString().ToUpper());
                }
            }

            //char[] a = new char[]
            //{
            //    'á', 'à', 'ạ', 'ả', 'ã', 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ', 'â', 'ấ', 'ầ', 'ẩ','ẫ', 'ậ'
            //};

            //for (int i = 0; i < a.Length; i++)
            //{
            //    result = result.Replace(a[i], 'a');
            //}
            ////..

            //char[] o = new char[]
            //{
            //    'ó', 'ò', 'ọ', 'ỏ', 'õ', 'ơ', 'ớ', 'ở', 'ỡ', 'ờ', 'ợ', 'ố', 'ồ', 'ộ', 'ỗ','ổ', 'ô'
            //};

            //for (int i = 0; i < o.Length; i++)
            //{
            //    result = result.Replace(o[i], 'o');
            //}
            ////..

            //char[] e = new char[]
            //{
            //    'é', 'è', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ế', 'ề', 'ể', 'ễ', 'ệ'
            //};

            //for (int i = 0; i < e.Length; i++)
            //{
            //    result = result.Replace(e[i], 'e');
            //}
            ////..

            //char[] iArray = new char[]
            //{
            //    'i', 'í', 'ì', 'ĩ', 'ị'
            //};

            //for (int i = 0; i < iArray.Length; i++)
            //{
            //    result = result.Replace(iArray[i], 'i');
            //}
            ////..


            //char[] u = new char[]
            //{
            //    'u', 'ú', 'ù', 'ủ', 'ũ'
            //};

            //for (int i = 0; i < u.Length; i++)
            //{
            //    result = result.Replace(u[i], 'u');
            //}
            ////..

            //char[] y = new char[]
            //{
            //    'y', 'ý', 'ỳ', 'ỷ', 'ỹ'
            //};

            //for (int i = 0; i < y.Length; i++)
            //{
            //    result = result.Replace(y[i], 'y');
            //}
            ////..

            //char[] dArray = new char[]
            //{
            //    'đ'
            //};

            //for (int i = 0; i < dArray.Length; i++)
            //{
            //    result = result.Replace(dArray[i], 'd');
            //}
            ////..

            //char[] symbolArray = new char[]
            //{
            //    '/', '\\', ',', '.', '?', '!',
            //};

            //for (int i = 0; i < symbolArray.Length; i++)
            //{
            //    result = result.Replace(symbolArray[i].ToString(), "");
            //}
            ////..

            //result = result.Replace(' ', '-');

            return result;
        }

        public static string ToUrlFormat(this string value)
		{
			////Chưa tối ưu
			//value = value.Replace(" ", "");
			//value = value.Replace("%", "");
			//value = value.Replace("@", "");
			//value = value.Replace("#", "");
			//value = value.Replace("$", "");

			value = value.ToLower();
			value = value.RemoveSign();
            //Tối ưu
            string charList = " ~!@#$%^&*()_+/\\:'|{},.?<>;[]";
			for (int i = 0; i < charList.Length; i++)
			{
                value = value.Replace(charList[i], '-');
			}

            //Thay thế dáu - trùng lắp
            Regex regex = new Regex("-+"); //  Regex C# 
            value = regex.Replace(value, "-");

            return value;
		}
    }
}
