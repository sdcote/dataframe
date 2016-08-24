using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class BooleanTypeTest {

        /// The data type under test.
        BooleanType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new BooleanType();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void BooleanType() {
            Assert.True( datatype.CheckType( true ) );
            Assert.True( datatype.CheckType( false ) );
            Assert.False( datatype.CheckType( "" ) );
            Assert.False( datatype.CheckType( 1 ) );
            Assert.False( datatype.CheckType( 0 ) );
            Assert.False( datatype.CheckType( ulong.MaxValue ) );
        }


        [Test]
        public void BooleanTypeName() {
            Assert.True( datatype.TypeName.Equals( "BOL" ) );
        }




        [Test]
        public void BooleanIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public void BooleanSize() {
            Assert.True( datatype.Size == 1 );
        }





        [Test]
        public void BooleanField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, double.MaxValue );
            TestUtil.ValidateField( field, DataField.DOUBLE, fieldName );
        }








        [Test]
        public virtual void testEncode() {
            byte[] result = datatype.Encode( true );
            Assert.NotNull( result );
            Assert.True( result.Length == 1 );
            Assert.True( result[0] == 1 );

            result = datatype.Encode( false );
            Assert.NotNull( result );
            Assert.True( result.Length == 1 );
            Assert.True( result[0] == 0 );
        }



        [Test]
        public virtual void testDecode() {
            byte[] data = new byte[1];
            data[0] = 1;
            object obj = datatype.Decode( data );
            Assert.NotNull( obj );
            Console.WriteLine( obj );
        }




    }

}