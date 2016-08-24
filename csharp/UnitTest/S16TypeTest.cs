using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class S16TypeTest {

        /// The data type under test.
        S16Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new S16Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void S16Type() {
            short value = short.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void S16TypeName() {
            Assert.True( datatype.TypeName.Equals( "S16" ) );
        }




        [Test]
        public void S16IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void S16Size() {
            Assert.True( datatype.Size == 2 );
        }





        [Test]
        public void S16Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, short.MaxValue );
            TestUtil.ValidateField( field, DataField.S16, fieldName );
        }






        [Test]
        public virtual void S16Decode() {
            byte[] data = new byte[2];
            // -32768 = 0x8000 = 10000000 00000000
            data[0] = (byte)128;
            data[1] = 0;
            object obj = datatype.Decode( data );
            Assert.True( obj is short );
            Assert.True( ((short)obj) == -32768 );

            // 32767 = 0x7FFF = 01111111 11111111
            data[0] = (byte)127;
            data[1] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is short );
            Assert.True( ((short)obj) == 32767 );

            // 0 = 0x0000 = 00000000 00000000
            data[0] = 0;
            data[1] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is short );
            Assert.True( ((short)obj) == 0 );

            //-1 = 0xFFFF = 11111111 11111111
            data[0] = 255;
            data[1] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is short );
            Assert.True( ((short)obj) == -1 );

        }




        [Test]
        public virtual void S16Encode() {
            // 32767 = 0x7FFF = 01111111 11111111
            short value = (short)32767;
            byte[] data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 127 );
            Assert.True( data[1] == 255 );

            // Overflow to -32768  = 0x7FFF = 10000000 00000000
            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 128 );
            Assert.True( data[1] == 0 );

            // 0 = 0x0000 = 00000000 00000000
            value = 0;
            data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );

            //-1 = 0xFFFF = 11111111 11111111
            value--;
            data = datatype.Encode( value );
            Assert.True( data.Length == 2 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );

        }



    }

}