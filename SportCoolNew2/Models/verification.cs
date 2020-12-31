using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportCoolNew2.Models
{
    public class verification : ValidationAttribute
    {
       

        public string IDChk(string vid)
        {
            List<string> FirstEng = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
            string aa = vid.ToUpper();
            bool chackFirstEnd = false;
            if (aa.Trim().Length == 10)
            {
                byte firstNo = Convert.ToByte(aa.Trim().Substring(1, 1));
                if (firstNo > 2 || firstNo < 1)
                {
                    return "2";
                }
                else
                {
                    int x;
                    for (x = 0; x < FirstEng.Count; x++)
                    {
                        if (aa.Substring(0, 1) == FirstEng[x])
                        {
                            aa = string.Format("{0}{1}", x + 10, aa.Substring(1, 9));
                            chackFirstEnd = true;
                            break;
                        }

                    }
                    if (!chackFirstEnd)
                        return "3";

                    int i = 1;
                    int ss = int.Parse(aa.Substring(0, 1));
                    while (aa.Length > i)
                    {
                        ss = ss + (int.Parse(aa.Substring(i, 1)) * (10 - i));
                        i++;
                    }
                    aa = ss.ToString();
                    if (vid.Substring(9, 1) == "0")
                    {
                        if (aa.Substring(aa.Length - 1, 1) == "0")
                        {
                            return "0";
                        }
                        else
                        {
                            return "4";
                        }
                    }
                    else
                    {
                        if (vid.Substring(9, 1) == (10 - int.Parse(aa.Substring(aa.Length - 1, 1))).ToString())
                        {

                            return "0";
                        }
                        else
                        {
                            return "4";
                        }
                    }
                }
            }
            else
            {

                return "1";
            }
        }
        //回傳1 代表字數不到10  
        //回傳2代表第二碼非1,2  
        //回傳3 代表首碼有誤  
        //回傳4代表檢查碼不對  
    }
}