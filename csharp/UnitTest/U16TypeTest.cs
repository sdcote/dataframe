using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class U16TypeTest {

        /// The data type under test.
        U16Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new U16Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void U16Type() {
            ushort value = ushort.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void U16TypeName() {
            Assert.True( datatype.TypeName.Equals( "U16" ) );
        }




        [Test]
        public void U16IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void U16Size() {
            Assert.True( datatype.Size == 2 );
        }





        [Test]
        public void U16Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, ushort.MaxValue );
            TestUtil.ValidateField( field, DataField.U16, fieldName );
        }





        [Test]
        public virtual void U16Decode() {
            byte[] data = new byte[2];
            data[0] = 0;
            data[1] = 0;

            // Minimum size for this type
            // 0 = 0x0000 = 00000000 00000000
            object obj = datatype.Decode( data );
            Assert.True( obj is ushort );
            Assert.True( ((ushort)obj) == 0 );

            // Maximum size for this type
            // 65535 = 0xFFFF = 11111111 11111111
            data[0] = 255;
            data[1] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is ushort );
            Assert.True( ((ushort)obj) == 65535 );

        }




        [Test]
        public virtual void U16Encode() {
            // 65535 =0xFFFF = 11111111 11111111
            ushort value = 65535;
            byte[] data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );

            // Overflow to 0
            @value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );

            //0 = 0x0000 = 00000000 00000000
            @value = 0;
            data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );

            // underflow to 65535
            //65535 = 0xFFFF = 11111111 11111111
            @value--;
            data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );

        }




    }

}