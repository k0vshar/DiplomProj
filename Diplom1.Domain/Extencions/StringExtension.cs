﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesMarket.Domain.Extencions
{
    public static class StringExtension
    {
        public static string Join(this List<string> words)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < words.Count; i++)
            {
                sb.Append($"{i + 1}: {words[i]} ");
            }

            return sb.ToString();
        }
    }
}