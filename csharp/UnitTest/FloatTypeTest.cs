using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class FloatTypeTest {

        /// The data type under test.
        FloatType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new FloatType();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void FloatType() {
            float value = float.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void FloatTypeName() {
            Assert.True( datatype.TypeName.Equals( "FLT" ) );
        }




        [Test]
        public void FloatIsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void FloatSize() {
            Assert.True( datatype.Size == 4 );
        }





        [Test]
        public void FloatField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, float.MaxValue );
            TestUtil.ValidateField( field, DataField.FLOAT, fieldName );
        }



        [Test]
        public virtual void FloatDecode() {
            object obj = null;
            byte[] data = new byte[4];
            data[0] = 255;
            data[1] = 255;
            data[2] = 127;
            data[3] = 127;
            obj = datatype.Decode( data );
            Assert.True( obj is float );
            Assert.True( ((float)obj) == float.MaxValue );

            data[0] = 255;
            data[1] = 255;
            data[2] = 127;
            data[3] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is float );
            Assert.True( ((float)obj) == float.MinValue );

            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is float );
            Assert.True( ((float)obj) == 0 );

            data[0] = 0;
            data[1] = 0;
            data[2] = 128;
            data[3] = 191;
            obj = datatype.Decode( data );
            Assert.True( obj is float );
            Assert.True( ((float)obj) == -1 );
        }



        [Test]
        public virtual void FloatEncode() {
            float value = float.MaxValue;
            byte[] data = datatype.Encode( value );
            //Debug.WriteLine( "Max: " + value + "\r\n" + ByteUtil.Dump( data ) );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 127 );
            Assert.True( data[3] == 127 );

            value = float.MinValue;
            data = datatype.Encode( value );
            //Debug.WriteLine( "Min: " + value + "\r\n" + ByteUtil.Dump( data ) );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 127 );
            Assert.True( data[3] == 255 );

            value = 0;
            data = datatype.Encode( value );
            //Debug.WriteLine( "Zero: " + value + "\r\n" + ByteUtil.Dump( data ) );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );

            value = -1;
            data = datatype.Encode( value );
            //Debug.WriteLine( "Negative: " + value + "\r\n" + ByteUtil.Dump( data ) );
            Assert.True( data.Length == 4 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 128 );
            Assert.True( data[3] == 191 );
        }
        
    }

}