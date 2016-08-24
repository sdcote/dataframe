using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class FrameTypeTest {

        /// The data type under test.
        FrameType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new FrameType();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void FrameType() {
            Assert.True( datatype.CheckType( new DataFrame() ) );
            Assert.False( datatype.CheckType( ulong.MaxValue ) );
        }


        [Test]
        public void FrameTypeName() {
            Assert.True( datatype.TypeName.Equals( "FRM" ) );
        }




        [Test]
        public void FrameIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public void FrameSize() {
            Assert.True( datatype.Size == -1 );
        }





        [Test]
        public void FrameField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, new DataFrame() );
            TestUtil.ValidateField( field, DataField.FRAME, fieldName );
        }






        [Test]
        public virtual void FrameTypeDecode() {
            byte[] data = new byte[14];
            data[0] = 4; // name of the field is 4 bytes long
            data[1] = 116; // t
            data[2] = 101; // e
            data[3] = 115; // s
            data[4] = 116; // t
            data[5] = 3; // data type code is 3 - String
            data[6] = 0; // first byte of unsigned integer for length
            data[7] = 0; // second byte of unsigned integer for length
            data[8] = 0; // third byte of unsigned integer for length
            data[9] = 4; // fourth byte of unsigned integer for length
            data[10] = 97; // a
            data[11] = 98; // b
            data[12] = 99; // c
            data[13] = 100; // d
            object value = datatype.Decode( data );
            Assert.True( value is DataFrame );
            DataFrame frame = (DataFrame)value;
            Assert.True( frame.Contains( "test" ) );
            Assert.True( frame.Size == 1 );
            DataField field = frame.Field[0]; // get the first field
            Assert.NotNull( field );
            Assert.True( field.Type == 3 );
            object fieldvalue = field.ObjectValue;
            Assert.NotNull( fieldvalue );
            Assert.True( fieldvalue is string );
            string stringvalue = (string)fieldvalue;
            Assert.True( stringvalue.Equals( "abcd" ) );
        }




        [Test]
        public virtual void testEncode() {
            // empty frame should yield no bytes
            DataFrame data = new DataFrame();
            byte[] value = datatype.Encode( data );
            Assert.True( value.Length == 0 );

            //other tests actually depend on the encoding of each field and their field types.

            // Add a string with the name of test
            data.Add( "test", "abc" );
            value = datatype.Encode( data );
            Assert.True( value.Length == 13 );
            Assert.True( value[0] == 4 ); // name of the field is 4 bytes long
            Assert.True( value[1] == 116 ); // t
            Assert.True( value[2] == 101 ); // e
            Assert.True( value[3] == 115 ); // s
            Assert.True( value[4] == 116 ); // t
            Assert.True( value[5] == 3 ); // data type code is 3 - String
            Assert.True( value[6] == 0 ); // first byte of unsigned integer for length
            Assert.True( value[7] == 0 ); // second byte of unsigned integer for length
            Assert.True( value[8] == 0 ); // third byte of unsigned integer for length
            Assert.True( value[9] == 3 ); // fourth byte of unsigned integer for length
            Assert.True( value[10] == 97 ); // a
            Assert.True( value[11] == 98 ); // b
            Assert.True( value[12] == 99 ); // c
        }



    }

}