using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class U32TypeTest {

        /// The data type under test.
        U32Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new U32Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void U32Type() {
            uint value = uint.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void U32TypeName() {
            Assert.True( datatype.TypeName.Equals( "U32" ) );
        }




        [Test]
        public void U32IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void U32Size() {
            Assert.True( datatype.Size == 4 );
        }





        [Test]
        public void U32Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, uint.MaxValue );
            TestUtil.ValidateField( field, DataField.U32, fieldName );
        }




        [Test]
        public virtual void U32Decode() {
            byte[] data = new byte[4];
            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;

            // Minimum size for this type
            //0 = 0x00000000 = 00000000 00000000 00000000 00000000
            object obj = datatype.Decode( data );
            Assert.True( obj is uint );
            Assert.True( ((uint)obj) == 0 );

            // Maximum value for this type
            // 4294967295 = 0xFFFFFFFF = 11111111 11111111 11111111 11111111
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is uint );
            Assert.True( ((uint)obj) == 4294967295 );

        }




        [Test]
        public virtual void U32Encode() {
            // 4294967295 =0xFFFFFFFF = 11111111 11111111 11111111 11111111
            uint value = uint.MaxValue;
            byte[] data = datatype.Encode( value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );

            // Overflow to 0
            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );

            //0 = 0x00000000 = 00000000 00000000 00000000 00000000
            value = 0;
            data = datatype.Encode( value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );

            // underflow to 4294967295
            //4294967295 = 0xFFFFFFFF = 11111111 11111111 11111111 11111111
            value--;
            data = datatype.Encode( value );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );

        }






    }

}