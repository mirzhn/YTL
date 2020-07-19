using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YTL
{
    static class ParamReader
    {
        static string FlagType = @"\type";
        static string FlagFilter = @"\filter";
        public static Param getParam(string[] args)
        {
            string Type = getValue(args, FlagType);
            string Filter = getValue(args, FlagFilter);

            Param NewParam = new Param(Type, Filter);
            return NewParam;
        }

        public static string getValue(string[] args, string Flag)
        {
            return args.Where(arg => arg.StartsWith(Flag)).First().Substring(Flag.Count() + 1);
        }

    }
}
