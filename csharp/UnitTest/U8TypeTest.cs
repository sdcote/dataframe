using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class U8TypeTest {

        /// The data type under test.
        U8Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new U8Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void U8Type() {
            byte value = byte.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void U8TypeName() {
            Assert.True( datatype.TypeName.Equals( "U8" ) );
        }




        [Test]
        public void U8IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void U8Size() {
            Assert.True( datatype.Size == 1 );
        }





        [Test]
        public void U8Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, byte.MaxValue );
            TestUtil.ValidateField( field, DataField.U8, fieldName );
        }



        
        [Test]
        public virtual void U8Decode() {
            byte[] data = new byte[1];
            byte value = 255;
            data[0] = value; // should be single octet of all 1's

            object obj = datatype.Decode( data );
            Assert.True( obj is byte );
            Assert.True( ((byte)obj) == 255 );

            //test for overflow
            value++;
            data[0] = value;
            obj = datatype.Decode( data );
            Assert.True( obj is byte );
            Assert.True( ((byte)obj) == 0 );

            value++;
            data[0] = value;
            obj = datatype.Decode( data );
            Assert.True( obj is byte );
            Assert.True( ((byte)obj) == 1 );

        }



        
        [Test]
        public virtual void U8Encode() {
            byte value = (byte)254;
            byte[] data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 254 );

            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 255 );

            // Test for overflow
            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 0 );

            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 1 );

        }




    }

}