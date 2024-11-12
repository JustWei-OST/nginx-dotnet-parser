using System;
using System.Collections.Generic;
using System.Text;

namespace NginxDotnetParser
{
    public interface NgxEntry
    {
        //TODO:要添加一个方法,用来获取路径
        string Dump();
    }
}
