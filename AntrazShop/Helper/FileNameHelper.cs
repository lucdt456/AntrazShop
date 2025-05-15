using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AntrazShop.Helper
{
	public class FileNameHelper
	{
		public static string ToSlug(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) return string.Empty;

			//xoá khoảng trắng thừa
			fileName = fileName.Trim();

			// chuyển thành chữ thường
			fileName = fileName.ToLower();

			// Thay thế các ký tự đặc biệt (ví dụ: "đ" thành "d")
			fileName = ReplaceVietnameseChars(fileName);

			//tách dấu ra khỏi chữ cái
			fileName = fileName.Normalize(NormalizationForm.FormD);

			StringBuilder sb = new StringBuilder();

			//Thêm chữ cái khác dấu vào chuỗi sb
			foreach(var c in fileName)
			{
				if(CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}

			//trả về chuỗi k dấu
			fileName = sb.ToString().Normalize(NormalizationForm.FormC);

			//chuyển thành chuỗi slug
			fileName = fileName.Replace(' ', '_');

			//xoá các ký tự k hợp lệ
			fileName = Regex.Replace(fileName, @"[^a-z0-9_]", "");


			return fileName;
		}

		private static string ReplaceVietnameseChars(string input)
		{
			return input
			   .Replace("đ", "d")
			   .Replace("Đ", "D");
		}
	}
}
