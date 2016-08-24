using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class S32TypeTest {

        /// The data type under test.
        S32Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new S32Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void S32Type() {
            int value = int.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }





        [Test]
        public virtual void S32Decode() {
            object obj = null;
            byte[] data = new byte[4];

            //-2147483648 = 0x80000000 = 10000000 00000000 00000000 00000000
            data[0] = 128;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;

            obj = datatype.Decode( data );
            Assert.True( obj is int );
            Assert.True( ((int)obj) == -2147483648 );

            // 2147483647 = 0x7FFFFFFF = 01111111 11111111 11111111 11111111
            data[0] = (byte)127;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is int );
            Assert.True( ((int)obj) == 2147483647 );

            //0 = 0x00000000 = 00000000 00000000 00000000 00000000
            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is int );
            Assert.True( ((int)obj) == 0 );

            //-1 = 0xFFFFFFFF = 11111111 11111111 11111111 11111111
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is int );
            Assert.True( ((int)obj) == -1 );
            
        }




        [Test]
        public virtual void S32Encode() {
            //2147483647 = 0x7FFFFFFF = 01111111 11111111 11111111 11111111
            int @value = 2147483647;
            byte[] data = datatype.Encode( @value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 127 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );

            // Overflow to -2147483648 = 0x80000000 = 10000000 00000000 00000000 00000000
            @value++;
            data = datatype.Encode( @value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 128 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );

            //0 = 0x00000000 = 00000000 00000000 00000000 00000000
            @value = 0;
            data = datatype.Encode( @value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );

            //-1 = 0xFFFFFFFF = 11111111 11111111 11111111 11111111
            @value--;
            data = datatype.Encode( @value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );

        }



        [Test]
        public void S32TypeName() {
            Assert.True( datatype.TypeName.Equals( "S32" ) );
        }




        [Test]
        public void S32IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void S32Size() {
            Assert.True( datatype.Size == 4 );
        }





        [Test]
        public void S32Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, -1 );
            TestUtil.ValidateField( field, DataField.S32, fieldName );
        }

    }

}