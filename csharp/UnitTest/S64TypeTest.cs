using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class S64TypeTest {

        /// The data type under test.
        S64Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new S64Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void S64Type() {
            long value = long.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void S64Decode() {
            Object obj = null;
            byte[] data = new byte[8];
            //-9223372036854775808 = 0x8000000000000000 = 10000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
            data[0] = (byte)128;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 0;
            data[7] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is long );
            Assert.True( ((long)obj) == -9223372036854775808L );

            //9223372036854775807 = 0x7FFFFFFFFFFFFFFF = 01111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            data[0] = (byte)127;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            data[4] = 255;
            data[5] = 255;
            data[6] = 255;
            data[7] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is long );
            Assert.True( ((long)obj) == 9223372036854775807L );

            //0 = 0x0000000000000000 = 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 0;
            data[7] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is long );
            Assert.True( ((long)obj) == 0 );

            //-1 = 0xFFFFFFFFFFFFFFFF = 11111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            data[4] = 255;
            data[5] = 255;
            data[6] = 255;
            data[7] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is long );
            Assert.True( ((long)obj) == -1 );
        }




        [Test]
        public void S64Encode() {
            //9223372036854775807 = 0x7FFFFFFFFFFFFFFF = 01111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
            long value = 9223372036854775807L;
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

            //-9223372036854775808 = 0x8000000000000000 = 10000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
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

            //0 = 0x0000000000000000 = 00000000 00000000 00000000 00000000 00000000 00000000 00000000 00000000
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

            //-1 = 0xFFFFFFFFFFFFFFFF = 11111111 11111111 11111111 11111111 11111111 11111111 11111111 11111111
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
        public void S64TypeName() {
            Assert.True( datatype.TypeName.Equals( "S64" ) );
        }




        [Test]
        public void S64IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void S64Size() {
            Assert.True( datatype.Size == 8 );
        }





        [Test]
        public void S64Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, -1L );
            TestUtil.ValidateField( field, DataField.S64, fieldName );
        }

    }
}