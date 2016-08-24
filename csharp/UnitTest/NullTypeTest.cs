using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class NullTypeTest {

        /// The data type under test.
        UndefinedType datatype = null;

        [SetUp]
        public void SetUp() {
            datatype = new UndefinedType();
        }

        [TearDown]
        public void TearDown() {
            datatype = null;
        }


        [Test]
        public void NullType() {
            Assert.True( datatype.CheckType( null ) );

            ulong uvalue = ulong.MaxValue;
            Assert.False( datatype.CheckType( uvalue ) );
        }


        [Test]
        public void NullTypeName() {
            Assert.True( datatype.TypeName.Equals( "UDEF" ) );
        }




        [Test]
        public void NullIsNumeric() {
            Assert.False( datatype.IsNumeric );
        }




        [Test]
        public void NullSize() {
            Assert.True( datatype.Size == 0 );
        }





        [Test]
        public void NullField() {
            string fieldName = "test";
            DataField field = new DataField( fieldName, null );
            Assert.NotNull( field );
            Assert.NotNull( field.Name );
            Assert.True( field.Name.Equals( fieldName ) );
            Assert.AreEqual( field.Type, DataField.UDEF );
            Assert.NotNull( field.Value );
            Assert.NotNull( field.StringValue );
            Assert.NotNull( field.ToString() );
            Assert.NotNull( field.Bytes );
        }










        [Test]
        public virtual void NullCheckType() {
            Assert.True( datatype.CheckType( null ) );
            Assert.False( datatype.CheckType( "" ) );
        }




        [Test]
        public virtual void NullEncode() {
            byte[] result = datatype.Encode( null );
            Assert.NotNull( result );
            Assert.True( result.Length == 0 );
        }



        [Test]
        public virtual void NullDecode() {
            byte[] data = new byte[0];
            object obj = datatype.Decode( data );
            Assert.Null( obj );
        }





    }

}