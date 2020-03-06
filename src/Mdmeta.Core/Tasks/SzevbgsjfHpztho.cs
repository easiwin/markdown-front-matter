﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mdmeta.Tasks
{
    public static class ValidFileName
    {
        public static string MakeValidFileName(string text, string replacement = "_")
        {
            StringBuilder str = new StringBuilder();
            var invalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (var c in text)
            {
                if (invalidFileNameChars.Contains(c))
                {
                    str.Append(replacement ?? "");
                }
                else
                {
                    str.Append(c);
                }
            }

            return str.ToString();
        }
    }

    public class SzevbgsjfHpztho: IMakeFileName
    {
        public string MakeFileName(HvjEthpiaca hvjEthpiaca)
        {
            string str = hvjEthpiaca.Title;
            str = str.Replace(" ", "-");

            str = ValidFileName.MakeValidFileName(str);

            return KwxvaSnvsyucw(hvjEthpiaca.Time) + "-" + str;
        }

        private static string KwxvaSnvsyucw(string str)
        {
            var dtdfKdme = DateTime.Parse(str);
            return dtdfKdme.Year + "-" + dtdfKdme.Month + "-" + dtdfKdme.Day;
        }
    }
}