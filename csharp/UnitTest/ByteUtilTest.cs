using Coyote.DataFrame;
using NUnit.Framework;
using System;
using System.Diagnostics;


namespace DataFrameTests
{
    [TestFixture]
    public class ByteUtilTest
    {


        [Test]
        public void testRenderDate()
        {
            // Here is our test data:

            //Wed Aug 11 10:26:04 465ms EDT 2004
            //Time: 1092234364465
            //+000:00--+001:01--+002:02--+003:03--+004:04--+005:05--+006:06--+007:07--+
            //|00000000|00000000|00000000|11111110|01001110|00111101|11000110|00110001|
            //|000:00: |000:00: |000:00: |254:fe: |078:4e:N|061:3d:=|198:c6: |049:31:1|
            //+--------+--------+--------+--------+--------+--------+--------+--------+

            // Create the datetime which matches our know test data
            DateTime date = new DateTime(2004, 8, 11, 10, 26, 4, 465);

            // Get the bytes representing the date
            byte[] data = ByteUtil.RenderDate(date);

            Assert.True(data.Length == 8);

            // Make sure each byte is as expected
            Assert.True(data[0] == 0, "Byte0=" + data[0]);
            Assert.True(data[1] == 0, "Byte1=" + data[1]);
            Assert.True(data[2] == 0, "Byte2=" + data[2]);
            Assert.True(data[3] == 254, "Byte3=" + data[3]);
            Assert.True(data[4] == 78, "Byte4=" + data[4]);
            Assert.True(data[5] == 61, "Byte5=" + data[5]);
            Assert.True(data[6] == 198, "Byte6=" + data[6]);
            Assert.True(data[7] == 49, "Byte7=" + data[7]);

        }



        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testShortByte()
        {
            ushort value = 255;
            byte[] data = new byte[1];
            data[0] = (byte)value;

            Debug.WriteLine(ByteUtil.Dump(data));
            Debug.WriteLine(ByteUtil.Dump(BitConverter.GetBytes(value)));

            Assert.True(ByteUtil.RetrieveUnsignedByte(data, 0) == 255);
        }




        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testShort()
        {
            short minvalue = short.MinValue;
            byte[] mindata = null;
            mindata = ByteUtil.RenderShort(minvalue);

            Debug.WriteLine(ByteUtil.Dump(mindata));
            Debug.WriteLine(ByteUtil.Dump(BitConverter.GetBytes(minvalue)));



            //Assert.True(ByteUtil.RetrieveShort(data, 0) == short.MinValue);
            //Assert.True(ByteUtil.RetrieveUnsignedShort(data, 0) == 32768);

           short value = short.MaxValue;
          byte[]  data = ByteUtil.RenderShort(value);

            Assert.True(ByteUtil.RetrieveShort(data, 0) == short.MaxValue);
            Assert.True(ByteUtil.RetrieveUnsignedShort(data, 0) == 32767);

            value = -1;
            data = ByteUtil.RenderShort(value);

            Assert.True(ByteUtil.RetrieveShort(data, 0) == -1);
            Assert.True(ByteUtil.RetrieveUnsignedShort(data, 0) == 65535);

            // Test the maximum unsigned value in the unsigned renderer
            ushort intvalue = 65535;
            data = ByteUtil.RenderUnsignedShort(intvalue);

            Assert.True(ByteUtil.RetrieveShort(data, 0) == -1);
            Assert.True(ByteUtil.RetrieveUnsignedShort(data, 0) == 65535);
        }





        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testInt()
        {
            int value = int.MinValue;
            byte[] data = null;
            data = ByteUtil.RenderInt(value);

            Assert.True(ByteUtil.RetrieveInt(data, 0) == int.MinValue);
            Assert.True(ByteUtil.RetrieveUnsignedInt(data, 0) == 2147483648L);

            value = int.MaxValue;
            data = ByteUtil.RenderInt(value);

            Assert.True(ByteUtil.RetrieveInt(data, 0) == int.MaxValue);
            Assert.True(ByteUtil.RetrieveUnsignedInt(data, 0) == 2147483647L);

            value = -1;
            data = ByteUtil.RenderInt(value);

            Assert.True(ByteUtil.RetrieveInt(data, 0) == -1);
            Assert.True(ByteUtil.RetrieveUnsignedInt(data, 0) == 4294967295L);

            // Test the maximum unsigned value in the unsigned renderer
            uint longvalue = 4294967295;
            data = ByteUtil.RenderUnsignedInt(longvalue);

            Assert.True(ByteUtil.RetrieveInt(data, 0) == -1);
            Assert.True(ByteUtil.RetrieveUnsignedInt(data, 0) == 4294967295L);
        }





        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testLong()
        {
            long value = long.MinValue;
            byte[] data = null;
            data = ByteUtil.RenderLong(value);

            Assert.True(ByteUtil.RetrieveLong(data, 0) == long.MinValue);

            // Assert.True(ByteUtil.retrieveUnsignedLong(data,0) == 2147483648L);

            value = long.MaxValue;
            data = ByteUtil.RenderLong(value);

            Assert.True(ByteUtil.RetrieveLong(data, 0) == long.MaxValue);

            // Assert.True(ByteUtil.retrieveUnsignedLong(data,0) == 2147483647L);

            value = -1;
            data = ByteUtil.RenderLong(value);

            Assert.True(ByteUtil.RetrieveLong(data, 0) == -1);
            // Assert.True(ByteUtil.retrieveUnsignedLong(data,0) == 4294967295L);

            // Test the maximum unsigned value in the unsigned renderer
            // long longvalue = 4294967295L;
            // data = ByteUtil.renderUnsignedILong(longvalue);
            // Assert.True(ByteUtil.retrieveLong(data,0) == -1);
            // Assert.True(ByteUtil.retrieveUnsignedLong(data,0) == 4294967295L);

        }





        



        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void testBoolean()
        {
            bool value = true;
            byte[] data = null;
            data = ByteUtil.RenderBoolean(value);

            Assert.True(ByteUtil.RetrieveBoolean(data, 0) == true);

            value = false;
            data = ByteUtil.RenderBoolean(value);

            Assert.True(ByteUtil.RetrieveBoolean(data, 0) == false);
        }





        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Ignore("This test should be ignored since it is broke")]
        public void testDate()
        {
            DateTime value = new DateTime();
            byte[] data = null;
            data = ByteUtil.RenderDate(value);

            // Console.WriteLine(ByteUtil.Dump(data));


            //Assert.True(ByteUtil.RetrieveDate(data, 0).Ticks == value.Ticks);

            //data = ByteUtil.RenderDate(null);

            //Assert.True(ByteUtil.RetrieveDate(data, 0).getTime() == 0);
        }


    }
}