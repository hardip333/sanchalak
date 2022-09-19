using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MSUISApi.Models
{
    public class Validation
    {
        
        public bool checkIsNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        public bool validateDigit(string strNumber)
        {
            if (string.IsNullOrEmpty(strNumber))
            {
                return false;
            }
            else
            {
                int numberOfChar = strNumber.Count();
                if (numberOfChar > 0)
                {
                    bool r = strNumber.All(char.IsDigit);
                    return r;
                }
                else
                {
                    return false;
                }
            }


        }

        public bool IsNumeric(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            else
            {
                /*if(Regex.IsMatch(s, @"^\d+\.?\d*$"))
                {
                    return false;
                }
                else
                {
                    return true;
                }*/

                Regex r = new Regex(@"^\d+\.?\d*$");

                return r.IsMatch(s);


            }
        }

        public bool validateAlphaNumeric(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            else
            {
                if (Regex.IsMatch(s, "^[a-zA-Z0-9]*$"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}