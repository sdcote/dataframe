using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Coyote.DataFrame;

namespace Cookbook
{
    class DataFields
    {
        public void Run()
        {
            DataField field = new DataField("Hello");
            byte[] bytes = field.Bytes;
            Console.WriteLine("Here is a field by itself:");
            Console.WriteLine(ByteUtil.Dump(bytes));
            Console.WriteLine();

            DataFrame frame = new DataFrame();
            Console.WriteLine("Here is an empty frame:");
            Console.WriteLine(ByteUtil.Dump(frame.Bytes));
            Console.WriteLine();

            frame.Add("msg", "Hello");
            Console.WriteLine("Here is a frame with one field:");
            Console.WriteLine(ByteUtil.Dump(frame.Bytes));
            Console.WriteLine();

            frame.Add("msg", "World!");
            Console.WriteLine("Here is a frame with two fields:");
            Console.WriteLine(ByteUtil.Dump(frame.Bytes));
            Console.WriteLine();




        }
    }
}
