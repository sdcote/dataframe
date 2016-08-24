using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;
using System.Globalization;

namespace UnitTest {

    [TestFixture]
    public class DateTypeTest {

        /// The data type under test.
        DateType datatype = null;
        
        internal static byte[] datedata = new byte[8];
        internal static DateTime cal = new DateTime();


        [SetUp]
        public void SetUp() {
            datatype = new DateType();

            datedata[0] = 0;
            datedata[1] = 0;
            datedata[2] = 0;
            datedata[3] = 191;
            datedata[4] = 169;
            datedata[5] = 97;
            datedata[6] = 245;
            datedata[7] = 248;

            // All date values are represented in UTC
            cal = new DateTime( 1996, 2, 1, 13, 15, 23, 0, DateTimeKind.Utc);
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void DateType() {
            DateTime value = new DateTime();
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void DateTypeName() {
            Assert.True( datatype.TypeName.Equals( "DAT" ) );
        }




        [Test]
        public void DateIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public void DateSize() {
            Assert.True( datatype.Size == 8 );
        }





        [Test]
        public void DateField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, new DateTime() );
            TestUtil.ValidateField( field, DataField.DATE, fieldName );
        }









        [Test]
        public virtual void testGetTypeName() {
            Assert.True( datatype.TypeName.Equals( "DAT" ) );
        }




        [Test]
        public virtual void testIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public virtual void testGetSize() {
            Assert.True( datatype.Size == 8 );
        }


        [Test]
        public virtual void testCheckType() {
            Assert.True( datatype.CheckType( new DateTime() ) );
            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }




        [Test]
        public virtual void DateTypeDecode() {
            Debug.WriteLine( "=================================================================================\r\n" );
            Debug.WriteLine( "Decoding test data:\r\n" + ByteUtil.Dump( datedata ) );
            object obj = datatype.Decode( datedata );
            Assert.True( obj is DateTime );
            DateTime date = (DateTime)obj;
            Debug.WriteLine( "Decoded as: " + date );
            //Debug.WriteLine( "a long value of: " + date.getTime() );
            Assert.True( cal.Equals( date ) );
            Debug.WriteLine( "Test Completed Successfully =====================================================\r\n" );
        }





        [Test]
        public virtual void FrameTypeEncode() {
            Debug.WriteLine( "=================================================================================\r\n" );

            DateTime date = cal;
            Debug.WriteLine( "Encoding date '" + date.ToString() + "'" );
            Debug.WriteLine( "Expecting an encoding of:\r\n" + ByteUtil.Dump( datedata ) );
            byte[] data = datatype.Encode( date );
            Assert.NotNull( data );
            Debug.WriteLine( "Encoded as:\r\n" + ByteUtil.Dump( data ) );
            Assert.True( data.Length == 8 );
            for ( int i = 0; i < datedata.Length; i++ ) {
                Assert.True( data[i] == datedata[i], "element " + i + " should be " + datedata[i] + " but is '" + data[i] + "'" );
            }
            Debug.WriteLine( "Test Completed Successfully =====================================================\r\n" );
        }



    }

}