using System;
using System.IO;

using Coyote.DataFrame;

namespace Cookbook
{
    class DateField
    {
        public void Run()
        {
            DataField datefield = new DataField("DATE", DateTime.UtcNow);
            byte[] data = datefield.Bytes;
            Console.WriteLine(ByteUtil.Dump(data));

            Console.WriteLine("\r\n*************************************************************\r\n");

            MemoryStream stream = new MemoryStream(data);
            BinaryReader reader = new BinaryReader(stream);
            DataField newfield = new DataField(reader);
            byte[] otherdata = newfield.Bytes;
            Console.WriteLine(ByteUtil.Dump(otherdata));

            Console.WriteLine("\r\n*************************************************************\r\n");

            DateTime time = (DateTime)newfield.ObjectValue;
            Console.WriteLine(time.ToShortDateString() + " " + time.ToShortTimeString());
            Console.WriteLine(ByteUtil.Dump(datefield.Value));
            long millis = ByteUtil.RetrieveLong(datefield.Value, 0);
            Console.WriteLine("Millis: " + millis);

            Console.WriteLine("\r\n*************************************************************\r\n");

            DataFrame frame = new DataFrame();
            frame.Add(datefield);
            DataFrame newframe = new DataFrame();
            newframe.Add("NewDate",newfield);
            frame.Add("newframe", newframe);
            Console.WriteLine(frame.ToString());

        }
    }
}
