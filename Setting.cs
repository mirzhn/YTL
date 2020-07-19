using System;
using System.Collections.Generic;
using System.Text;

namespace YTL
{
    class Setting
    {
        public string url { get; }
        public string token { get; }

        public string path { get; }

        public Setting(string url, string token, string path)
        {
            this.url = url;
            this.token = token;
            this.path = path;
        }
    }
}
