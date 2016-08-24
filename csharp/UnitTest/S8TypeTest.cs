using NUnit.Framework;
using System;
using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class S8TypeTest {

        /// The data type under test.
        S8Type datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new S8Type();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void S8Type() {
            sbyte value = sbyte.MinValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void S8TypeName() {
            Assert.True( datatype.TypeName.Equals( "S8" ) );
        }




        [Test]
        public void S8IsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void S8Size() {
            Assert.True( datatype.Size == 1 );
        }





        [Test]
        public void S8Field() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, sbyte.MinValue );
            TestUtil.ValidateField( field, DataField.S8, fieldName );
        }




        [Test]
        public virtual void S8Decode() {
            byte[] data = new byte[1];

            // 0x80 = 10000000
            byte value = 128;
            data[0] = value;
            object obj = datatype.Decode( data );
            Assert.True( obj is sbyte );
            Assert.True( ((sbyte)obj) == -128 );

            // 0x81 = 10000001
            value++;
            data[0] = value;
            obj = datatype.Decode( data );
            Assert.True( obj is sbyte );
            Assert.True( ((sbyte)obj) == -127 );

            //0x82 = 10000010
            value++;
            data[0] = value;
            obj = datatype.Decode( data );
            Assert.True( obj is sbyte );
            Assert.True( ((sbyte)obj) == -126 );

            //0x7F = 01111111
            value = 127;
            data[0] = value;
            obj = datatype.Decode( data );
            Assert.True( obj is sbyte );
            Assert.True( ((sbyte)obj) == 127 );

            //test for overflow
            //0x80 = 10000000
            value++;
            data[0] = value;
            obj = datatype.Decode( data );
            Assert.True( obj is sbyte );
            Assert.True( ((sbyte)obj) == -128 );

        }



        
        [Test]
        public virtual void S8Encode() {
            //0x7F = 01111111
            sbyte value = (sbyte)127;
            byte[] data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 127 );

            //
            value++;
            data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 128 );

            //0x80 = 10000000
            value = -128;
            data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 128 );

            value--;
            data = datatype.Encode( value );
            Assert.True( data.Length == 1 );
            Assert.True( data[0] == 127 );

        }




    }

}