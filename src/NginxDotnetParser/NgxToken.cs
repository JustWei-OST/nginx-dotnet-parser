using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser
{
    public class NgxToken(string token)
    {
        private string _token = token;

        public override string ToString() => _token;

        public string Token => _token;

    }
}
