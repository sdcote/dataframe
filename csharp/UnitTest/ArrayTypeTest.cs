using NUnit.Framework;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class ArrayTypeTest {

        /// The data type under test.
        ArrayType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new ArrayType();
        }




        [TearDown]
        public void TearDown() {
            datatype = null;
        }




        [Test]
        public virtual void ArrayCheckType() {
            string[] array = new string[3];
            Assert.True( datatype.CheckType( array ) );
        }




        [Test]
        public virtual void ArrayTypeName() {
            Assert.True( datatype.TypeName.Equals( "ARY" ) );
        }




        [Test]
        public virtual void ArrayIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public virtual void ArraySize() {
            Assert.True( datatype.Size == -1 );
        }




        [Test]
        public virtual void ArrayDecode() {
            byte[] data = new byte[18];
            data[0] = 3; // type of string
            data[1] = 0; // first byte of U32 length
            data[2] = 0; // second byte of U32 length
            data[3] = 0; // third byte of U32 length
            data[4] = 1; // fourth byte of U32 length
            data[5] = 65; // UTF-8 Capital 'A'
            data[6] = 3;
            data[7] = 0;
            data[8] = 0;
            data[9] = 0;
            data[10] = 1;
            data[11] = 66;
            data[12] = 3;
            data[13] = 0;
            data[14] = 0;
            data[15] = 0;
            data[16] = 1;
            data[17] = 67;

            object obj = datatype.Decode( data );
            Assert.True( obj is object[] );
            object[] array = (object[])obj;
            Assert.True( array.Length == 3 );
            object value1 = array[0];
            Assert.True( value1 is string );
        }




        [Test]
        public virtual void ArrayEncode() {
            string[] array = new string[3];
            array[0] = "A";
            array[1] = "B";
            array[2] = "C";
            byte[] payload = datatype.Encode( array );
            System.Diagnostics.Debug.WriteLine( ByteUtil.Dump( payload ) );
            Assert.True( payload.Length == 18 );
        }







        [Test]
        //[Ignore( "Under development" )]
        public virtual void ArrayRoundTrip() {
            byte[] bytes = new byte[1];
            bytes[0] = 255;
            //Debug.WriteLine( ByteUtil.dump( bytes ) );

            object[] values = new object[10];
            values[0] = "test";
            values[1] = (byte)255; //U8 type5
            values[2] = (short)-32768; //S16 type6
            values[3] = 65535; //U16 type7
            values[4] = -2147483648; //S32 type8
            values[5] = 4294967296L; //U32 type9
            values[6] = -9223372036854775808L; //S64 type10
            values[7] = 9223372036854775807L; //U64 type11
            values[8] = 123456.5F; //type12
            values[9] = 123456.5D; //type13

            ArrayType subject = new ArrayType();
            byte[] payload = subject.Encode( values );
            Debug.WriteLine( ByteUtil.Dump( payload ) );
            Assert.True( payload.Length == 65 );

            object obj = subject.Decode( payload );
            Assert.True( obj is object[] );
            object[] array = (object[])obj;
            Assert.True( array.Length == 10 );

            object element = array[0];
            Debug.WriteLine( "Element 0 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[0] );
            Assert.True( element is string );
            Assert.True( "test".Equals( (string)element ) );

            element = array[1];
            Debug.WriteLine( "Element 1 is " + element.GetType().ToString() + " value=>" + element.ToString() + " Original=>" + values[1] );
            Assert.True( element is byte );
            Assert.True( 255 == ((byte)element) );

            element = array[2];
            Debug.WriteLine( "Element 2 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[2] );
            Assert.True( element is short );
            Assert.True( -32768 == ((short)element) );

            element = array[3];
            Debug.WriteLine( "Element 3 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[3] );
            Assert.True( element is int );
            Assert.True( 65535 == ((int)element) );

            element = array[4];
            Debug.WriteLine( "Element 4 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[4] );
            Assert.True( -2147483648 == ((int)element) );

            element = array[5];
            Debug.WriteLine( "Element 5 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[5] );
            Assert.True( element is long );
            Assert.True( 4294967296L == ((long)element) );

            element = array[6];
            Debug.WriteLine( "Element 6 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[6] );
            Assert.True( element is long );
            Assert.True( -9223372036854775808L == ((long)element) );

            element = array[7];
            Debug.WriteLine( "Element 7 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[7] );
            Assert.True( element is long );
            Assert.True( 9223372036854775807L == ((long)element) );

            element = array[8];
            Debug.WriteLine( "Element 8 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[8] );
            Assert.True( element is float );
            Assert.True( 123456.5F == ((float)element) );

            element = array[9];
            Debug.WriteLine( "Element 9 is " + element.GetType() + " value=>" + element.ToString() + " Original=>" + values[9] );
            Assert.True( element is double );
            Assert.True( 123456.5D == ((double)element) );
        }

    }

}