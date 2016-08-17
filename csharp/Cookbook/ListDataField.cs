using System;

using Coyote.DataFrame;

namespace Cookbook
{
    class ListDataField
    {
        public void Run()
        {
            // Create an array of objects
            object[] oary = new object[8];
            oary[0] = (byte)1;
            oary[1] = (short)2;
            oary[2] = (int)3;
            oary[3] = (long)4;
            oary[4] = (float)5;
            oary[5] = (double)6;
            oary[6] = "7";
            oary[7] = null;

            DataField field = new DataField("LIST", oary);

            byte[] bytes = field.Bytes; // get the wire format - 50 bytes of data
            Console.WriteLine(ByteUtil.Dump(bytes)); 

            DataField dfield = new DataField(bytes);
            object[] oary1 = (object[])dfield.ObjectValue;

            Console.WriteLine("Array Contains " + oary1.Length + " entries");
            foreach (object obj in oary1)
            {
                if (obj != null)
                    Console.WriteLine(obj.GetType().Name + " = " + obj.ToString());
                else
                    Console.WriteLine("NULL");
            }
        }

    }
}
