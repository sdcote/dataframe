using System;

using Coyote.DataFrame;

namespace Cookbook
{
    class StringField
    {
        public void Run()
        {
            DataFrame frame = new DataFrame("MSG", "Hello World!");
            byte[] bytes = frame.Bytes;
            Console.WriteLine(ByteUtil.Dump(bytes));
        }
    }
}
