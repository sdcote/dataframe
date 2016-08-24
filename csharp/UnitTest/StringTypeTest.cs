using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


using Coyote.DataFrame;


namespace UnitTest {

    [TestFixture]
    public class StringTypeTest {

        /// The data type under test.
        StringType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new StringType();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }




        [Test]
        public void StringTypeName() {
            Assert.True( datatype.TypeName.Equals( "STR" ) );
        }




        [Test]
        public void StringIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public void StringSize() {
            Assert.True( datatype.Size == -1 );
        }



        
        [Test]
        public void StringType() {
            String value = "hello";
            Assert.True( datatype.CheckType( value ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }





        [Test]
        public void StringField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, "hello" );
            TestUtil.ValidateField( field, DataField.STRING, fieldName );
            Assert.True( field.Name != null );
        }




        [Test]
        public void StringDecode() {
            byte[] data = new byte[5];
            data[0] = 72;
            data[1] = 101;
            data[2] = 108;
            data[3] = 108;
            data[4] = 111;
            object obj = datatype.Decode( data );
            Assert.True( obj is string );
            Assert.True( ((string)obj).Equals( "Hello" ) );
        }




        [Test]
        public void StringEncode() {
            StringType type = new StringType();
            Assert.NotNull( type );
            Assert.True( type.Size == -1 );

            // test the encoiding
            byte[] data = type.Encode( "Hello" );
            Assert.AreEqual( data[0], 72 );
            Assert.AreEqual( data[1], 101 );
            Assert.AreEqual( data[2], 108 );
            Assert.AreEqual( data[3], 108 );
            Assert.AreEqual( data[4], 111 );
        }

    }
}
