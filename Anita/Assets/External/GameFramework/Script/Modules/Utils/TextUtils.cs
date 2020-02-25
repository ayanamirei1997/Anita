// using System.Collections.Generic;
// using System.Numerics;

// namespace Anita
// {
//     public static class TextUtils
//     {
//         private static char START_CHAR = '[';
//         private static char END_CHAR = ']';
//         private static char SLASH_CHAR = '/';
//         private static string[] UBB_CLOSE_TAGS = { "color", "size", "URL", "B", "I" };
//         private static string ELLIPSIS = "...";
//         private static Dictionary<int, Vector2> wordPixelsDic = new Dictionary<int, Vector2>();

//         /// <summary> 
//         /// 安全的截断字符串 暂时只支持字体大小 颜色 粗体 斜体 URL
//         /// </summary> 
//         /// <param name="input">输入串</param> 
//         /// <param name="maxLen">最大长度</param> 
//         /// <returns>maxLen长度的字符串</returns> 
//         public static string SubStringUBB(string input, int maxLen)
//         {
//             if (string.IsNullOrEmpty(input) || input.Length <= maxLen)
//             {
//                 return input;
//             }
//             int inputLen = input.Length;
//             string result = string.Empty;

//             //声明堆栈用来放标签 
//             Stack<string> tags = new Stack<string>();
//             int charNum = 0;
//             int i = 0;
//             while(charNum < inputLen)
//             {
//                 char c = input[i];
//                 if (c == START_CHAR)
//                 {
//                     string tag = string.Empty;
//                     int tagIndex = i + 1;
//                     bool isTagEnd = false;
//                     bool isTagNameEnd = false;
//                     result += c;
//                     bool hasMarkTagInStack = false;
//                     char temp;
//                     while (tagIndex < inputLen)
//                     {
//                         temp = input[tagIndex];
//                         result += temp;
//                         tagIndex++;
//                         if (tag == string.Empty && temp == SLASH_CHAR)
//                         {
//                             isTagEnd = true;
//                             continue;
//                         }
//                         if (!isTagNameEnd)
//                         {
//                             if (char.IsLetter(temp) || char.IsNumber(temp))
//                             {
//                                 tag += temp;
//                             }
//                             else
//                             {
//                                 isTagNameEnd = true;
//                             }
//                         }

//                         if (!string.IsNullOrEmpty(tag))
//                         {
//                             if (isTagNameEnd && !hasMarkTagInStack)
//                             {
//                                 if (isTagEnd)
//                                 {
//                                     tags.Pop();
//                                 }
//                                 else
//                                 {
//                                     tags.Push(tag);
//                                 }
//                                 hasMarkTagInStack = true;
//                             }
//                         }

//                         if (isTagNameEnd)
//                         {
//                             if (temp == END_CHAR)
//                             {
//                                 i = tagIndex-1;
//                                 break;
//                             }
//                         }

//                     }
//                 }
//                 else
//                 {
//                     result += c;
//                     charNum++;
//                     if (charNum >= maxLen)
//                     {
//                         break;
//                     }
                    
//                 }
//                 i++;
//                 if (i >= inputLen)
//                 {
//                     break;
//                 }
//             }

//             //UBB语法补全
//             while (tags.Count > 0)
//             {
//                 string tag = tags.Pop();
//                 bool isMustCloseTag = false;
//                 foreach (string mustCloseTag in UBB_CLOSE_TAGS)
//                 {
//                     if (string.Compare(mustCloseTag, tag, true) == 0)
//                     {
//                         isMustCloseTag = true;
//                         break;
//                     }
//                 }
//                 if (isMustCloseTag)
//                 {
//                     result += (START_CHAR.ToString() + SLASH_CHAR + tag + END_CHAR);
//                 }
//             }

//             //无需截断
//             if (i >= inputLen)
//             {
//                 return result;
//             }
//             return result + ELLIPSIS;
//         }

//         /// <summary>
//         /// 根据字体大小获取单个字幕所占的像素
//         /// </summary>
//         /// <param name="size"></param>
//         /// <returns></returns>
//         public static Vector2 GetWorldPixelsBySize(int size)
//         {
//             if (wordPixelsDic.ContainsKey(size))
//             {
//                 return wordPixelsDic[size];
//             }
//             Vector2 vec2 = new Vector2();
//             switch (size)
//             {
//                 case 10:
//                     vec2.X = 7f;
//                     vec2.Y = 17f;
//                     break;
//                 case 11:
//                     vec2.X = 7.3f;
//                     vec2.Y = 18f;
//                     break;
//                 case 12:
//                     vec2.X = 8.3f;
//                     vec2.Y = 19f;
//                     break;
//                 case 13:
//                     vec2.X = 8.7f;
//                     vec2.Y = 20f;
//                     break;
//                 case 14:
//                     vec2.X = 9.4f;
//                     vec2.Y = 20f;
//                     break;
//                 case 15:
//                     vec2.X = 9.8f;
//                     vec2.Y = 21f;
//                     break;
//                 case 16:
//                     vec2.X = 10.2f;
//                     vec2.Y = 22f;
//                     break;
//                 case 17:
//                     vec2.X = 10.6f;
//                     vec2.Y = 23f;
//                     break;
//                 case 18:
//                     vec2.X = 11.0f;
//                     vec2.Y = 25f;
//                     break;
//                 case 19:
//                     vec2.X = 11.6f;
//                     vec2.Y = 26f;
//                     break;
//                 case 20:
//                     vec2.X = 12.3f;
//                     vec2.Y = 27f;
//                     break;
//                 case 21:
//                     vec2.X = 12.7f;
//                     vec2.Y = 28f;
//                     break;
//                 case 22:
//                     vec2.X = 13.3f;
//                     vec2.Y = 29f;
//                     break;
//                 default:
//                     vec2.X = 9.4f;
//                     vec2.Y = 20f;
//                     return vec2;

//             }
//             wordPixelsDic.Add(size, vec2);
//             return vec2;
//         }


//         public static string GetPlayerFullName(string allianceCode, string nickName)
//         {
//             if (string.IsNullOrEmpty(allianceCode))
//             {
//                 return nickName;
//             }
//             return string.Format("[{0}]{1}", allianceCode, nickName);
//         }
//     }
// }