using Coyote.DataFrame;
using NUnit.Framework;
using System;
using System.Diagnostics;


namespace DataFrameTests {
    public class DataFieldTest {


        [Test]
        public virtual void DataFieldConstructor() {
            string nulltag = null;
            object nullval = null;

            DataField field = new DataField( "" );
            Assert.NotNull( field.Value );

            field = new DataField( nullval );
            field = new DataField( (long)0 );

            field = new DataField( "", "" );
            new DataField( nulltag, nullval );

            field = new DataField( 0L );
            field = new DataField( "", 0L );
            field = new DataField( nulltag, 0L );

            field = new DataField( 0 );
            field = new DataField( "", 0 );
            field = new DataField( nulltag, 0 );

            field = new DataField( (short)0 );
            field = new DataField( "", (short)0 );
            field = new DataField( nulltag, (short)0 );

            field = new DataField( new sbyte[0] );
            field = new DataField( "", new sbyte[0] );
            field = new DataField( nulltag, new sbyte[0] );

            field = new DataField( (sbyte[])null );
            field = new DataField( nulltag, (sbyte[])null );

            field = new DataField( 0f );
            field = new DataField( "", 0f );
            field = new DataField( nulltag, 0f );

            field = new DataField( 0d );
            field = new DataField( "", 0d );
            field = new DataField( nulltag, 0d );

            field = new DataField( true );
            field = new DataField( "", true );
            field = new DataField( nulltag, true );

            field = new DataField( new DateTime() );
            field = new DataField( "", new DateTime() );
            field = new DataField( null, new DateTime() );


            field = new DataField( new Uri( "s:\\" ) );
            field = new DataField( "", new Uri( "s:\\" ) );
            field = new DataField( nulltag, new Uri( "s:\\" ) );

        }




        [Test]
        public virtual void testDataFieldBoolean() {
            DataField field = new DataField( true );
            byte[] data = field.Bytes;
            // Debug.WriteLine(ByteUtil.dump( data ));
            Assert.True( data.Length == 3 );
            Assert.True( data[0] == 0 );
            Assert.True( data[1] == 14 );
            Assert.True( data[2] == 1 );

        }




        [Test]
        public virtual void testDataFieldStringBoolean() {
            DataField field = new DataField( "Test", true );
            byte[] data = field.Bytes;
            // Debug.WriteLine(ByteUtil.dump( data ));
            Assert.True( data.Length == 7 );
            Assert.True( data[0] == 4 );
            Assert.True( data[1] == 84 );
            Assert.True( data[2] == 101 );
            Assert.True( data[3] == 115 );
            Assert.True( data[4] == 116 );
            Assert.True( data[5] == 14 );
            Assert.True( data[6] == 1 );

            field = new DataField( "Test", false );
            data = field.Bytes;
            // Debug.WriteLine(ByteUtil.dump( data ));
            Assert.True( data.Length == 7 );
            Assert.True( data[0] == 4 );
            Assert.True( data[1] == 84 );
            Assert.True( data[2] == 101 );
            Assert.True( data[3] == 115 );
            Assert.True( data[4] == 116 );
            Assert.True( data[5] == 14 );
            Assert.True( data[6] == 0 );
        }



        [Test]
        public virtual void DataFieldClone() {
            DataField original = new DataField( "Test", 17345 );

            object copy = original.Clone();

            Assert.NotNull( copy );
            Assert.True( copy is DataField );
            DataField field = (DataField)copy;
            Assert.True( "Test".Equals( field.Name ) );
            Assert.True( field.Type == 8 );
            object obj = field.ObjectValue;
            Assert.NotNull( obj );
            Assert.True( obj is int );
            Assert.True( ((int)obj) == 17345 );
        }



        [Test]
        public virtual void DataFieldIsNumeric() {
            DataField subject = new DataField( "Test", 32767 );
            Assert.True( subject.IsNumeric );
            subject = new DataField( "Test", "32767" );
            Assert.False( subject.IsNumeric );
        }




        [Test]
        public virtual void DataFieldToString() {
            DataField subject = new DataField( "Test", 32767 );
            string text = subject.ToString();
            Assert.NotNull( text );
            Assert.True( text.Length >= 48 );

            // Test truncation of long values
            subject = new DataField( "Test", "01234567890123456789012345678901234567890123456789" );
            text = subject.ToString();
            Assert.NotNull( text );
            Assert.True( text.Length < 170 );
        }

    }

}