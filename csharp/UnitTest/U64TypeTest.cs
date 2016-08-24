using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class U64TypeTest {

        /// The data type under test.
        static U64Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new U64Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void U64Type() {

            long value = long.MaxValue;
            Assert.False( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.True( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void U64Decode() {
            Object obj = null;
            byte[] data = new byte[8];

            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 0;
            data[7] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is ulong );
            Assert.True( ((ulong)obj) == 0L );

            // 9223372036854775808 = 0x8000000000000000 = 10000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
            data[0] = (byte)128;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 0;
            data[7] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is ulong );
            Assert.True( ((ulong)obj) == 9223372036854775808L );

            // 9223372036854775807 = 0x7FFFFFFFFFFFFFFF = 01111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            data[0] = (byte)127;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            data[4] = 255;
            data[5] = 255;
            data[6] = 255;
            data[7] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is ulong );
            Assert.True( ((ulong)obj) == 9223372036854775807L );

            // 0 = 0x0000000000000000 = 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 0;
            data[7] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is ulong );
            Assert.True( ((ulong)obj) == 0 );
            Assert.True( ((ulong)obj) == ulong.MinValue );

            // 18446744073709551615 = 0xFFFFFFFFFFFFFFFF = 11111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            data[4] = 255;
            data[5] = 255;
            data[6] = 255;
            data[7] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is ulong );
            Assert.True( ((ulong)obj) == 18446744073709551615L );
            Assert.True( ((ulong)obj) == ulong.MaxValue );
        }




        [Test]
        public void U64Encode() {

            // 9223372036854775807 = 0x7FFFFFFFFFFFFFFF = 01111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            ulong value = 9223372036854775807L;
            byte[] data = datatype.Encode( value );
            Assert.True( data.Length == 8 );
            Assert.True( data[0] == 127 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );
            Assert.True( data[4] == 255 );
            Assert.True( data[5] == 255 );
            Assert.True( data[6] == 255 );
            Assert.True( data[7] == 255 );

            // 9223372036854775808 = 0x8000000000000000 = 10000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 8 );
            Assert.True( data[0] == 128 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );
            Assert.True( data[4] == 0 );
            Assert.True( data[5] == 0 );
            Assert.True( data[6] == 0 );
            Assert.True( data[7] == 0 );

            // 0 = 0x0000000000000000 = 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
            value = 0;
            data = datatype.Encode( value );
            Assert.True( data.Length == 8 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );
            Assert.True( data[4] == 0 );
            Assert.True( data[5] == 0 );
            Assert.True( data[6] == 0 );
            Assert.True( data[7] == 0 );

            // 9223372036854775807 = 0xFFFFFFFFFFFFFFFF = 11111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            value--;
            data = datatype.Encode( value );
            Assert.True( data.Length == 8 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );
            Assert.True( data[4] == 255 );
            Assert.True( data[5] == 255 );
            Assert.True( data[6] == 255 );
            Assert.True( data[7] == 255 );

        }




        [Test]
        public void U64TypeName() {
            Assert.True( datatype.TypeName.Equals( "U64" ) );
        }




        [Test]
        public void U64IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void U64GetSize() {
            Assert.True( datatype.Size == 8 );
        }




        [Test]
        public void U64Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, ulong.MaxValue );
            TestUtil.ValidateField( field, DataField.U64, fieldName );
        }

    }
}