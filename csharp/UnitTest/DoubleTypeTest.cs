using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class DoubleTypeTest {

        /// The data type under test.
        DoubleType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new DoubleType();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void DoubleType() {
            double value = double.MaxValue;
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void DoubleTypeName() {
            Assert.True( datatype.TypeName.Equals( "DBL" ) );
        }




        [Test]
        public void DoubleIsNumeric() {
            Assert.True( datatype.IsNumeric );
        }




        [Test]
        public void DoubleSize() {
            Assert.True( datatype.Size == 8 );
        }





        [Test]
        public void DoubleField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, double.MaxValue );
            TestUtil.ValidateField( field, DataField.DOUBLE, fieldName );
        }




        [Test]
        public virtual void DoubleDecode() {
            object obj = null;
            byte[] data = new byte[8];
            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            data[4] = 255;
            data[5] = 255;
            data[6] = 239;
            data[7] = 127;
            obj = datatype.Decode( data );
            Assert.True( obj is double );
            Assert.True( ((double)obj) == double.MaxValue );

            data[0] = 255;
            data[1] = 255;
            data[2] = 255;
            data[3] = 255;
            data[4] = 255;
            data[5] = 255;
            data[6] = 239;
            data[7] = 255;
            obj = datatype.Decode( data );
            Assert.True( obj is double );
            Assert.True( ((double)obj) == double.MinValue );

            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 0;
            data[7] = 0;
            obj = datatype.Decode( data );
            Assert.True( obj is double );
            Assert.True( ((double)obj) == 0 );

            data[0] = 0;
            data[1] = 0;
            data[2] = 0;
            data[3] = 0;
            data[4] = 0;
            data[5] = 0;
            data[6] = 240;
            data[7] = 191;
            obj = datatype.Decode( data );
            Assert.True( obj is double );
            Assert.True( ((double)obj) == -1 );

      

        }




        [Test]
        public virtual void DoubleEncode() {
            double value = double.MaxValue;
            byte[] data = datatype.Encode( value );
            //Debug.WriteLine( "Max: "+value+"\r\n"+ByteUtil.Dump( data ) );
            Assert.True( data.Length == 8 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );
            Assert.True( data[4] == 255 );
            Assert.True( data[5] == 255 );
            Assert.True( data[6] == 239 );
            Assert.True( data[7] == 127 );

            value = double.MinValue;
            data = datatype.Encode( value );
            //Debug.WriteLine( "Min: " + value + "\r\n" + ByteUtil.Dump( data ) );
                        Assert.True( data.Length == 8 );
            Assert.True( data[0] == 255 );
            Assert.True( data[1] == 255 );
            Assert.True( data[2] == 255 );
            Assert.True( data[3] == 255 );
            Assert.True( data[4] == 255 );
            Assert.True( data[5] == 255 );
            Assert.True( data[6] == 239 );
            Assert.True( data[7] == 255 );

            value = 0;
            data = datatype.Encode( value );
            //Debug.WriteLine( "Zero: " + value + "\r\n" + ByteUtil.Dump( data ) );
                        Assert.True( data.Length == 8 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );
            Assert.True( data[4] == 0 );
            Assert.True( data[5] == 0 );
            Assert.True( data[6] == 0 );
            Assert.True( data[7] == 0 );

            value = -1;
            data = datatype.Encode( value );
            //Debug.WriteLine( "Negative1: " + value + "\r\n" + ByteUtil.Dump( data ) );
            Assert.True( data.Length == 8 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 0 );
            Assert.True( data[2] == 0 );
            Assert.True( data[3] == 0 );
            Assert.True( data[4] == 0 );
            Assert.True( data[5] == 0 );
            Assert.True( data[6] == 240 );
            Assert.True( data[7] == 191 );
        }





    }

}