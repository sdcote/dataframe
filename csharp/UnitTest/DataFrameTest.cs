using NUnit.Framework;
using System;
using Coyote.DataFrame;
using System.Diagnostics;


namespace UnitTest {

    [TestFixture]
    public class DataFrameTest {



        [SetUp]
        public void SetUp() {
        }

        [TearDown]
        public void TearDown() {
        }




        [Test]
        public virtual void testDataFrame() {
            DataFrame frame = new DataFrame();
            Assert.NotNull( frame );
            Assert.True( frame.TypeCount == 18 );
            Assert.True( frame.Size == 0 );
        }




        [Test]
        public virtual void testAddObject() {
            DataFrame frame = new DataFrame();
            Assert.NotNull( frame );
            Assert.True( frame.Size == 0 );

            DataFrame child = new DataFrame();
            frame.Add( child );
            Assert.True( frame.Size == 1 );
        }




        [Test]
        public virtual void testAddStringObject() {
            DataFrame frame = new DataFrame();
            Assert.NotNull( frame );
            Assert.True( frame.Size == 0 );

            DataFrame child = new DataFrame();
            frame.Add( "KID", child );
            Assert.True( frame.Size == 1 );
        }




        [Test]
        public virtual void DataFrameToString() {
            DataFrame frame1 = new DataFrame();
            frame1.Add( "alpha", 1L );
            frame1.Add( "beta", 2L );

            DataFrame frame2 = new DataFrame();
            frame2.Add( "gamma", 3L );
            frame2.Add( "delta", 4L );

            DataFrame frame3 = new DataFrame();
            frame3.Add( "epsilon", 5L );
            frame3.Add( "zeta", 6L );

            frame2.Add( "frame3", frame3 );
            frame1.Add( "frame2", frame2 );

            string text = frame1.ToString();
            Debug.WriteLine( text );
            Debug.WriteLine( JSONMarshaler.ToFormattedString(frame1 ));

            Assert.True( text.Contains( "alpha" ) );
            Assert.True( text.Contains( "beta" ) );
            Assert.True( text.Contains( "gamma" ) );
            Assert.True( text.Contains( "delta" ) );
            Assert.True( text.Contains( "epsilon" ) );
            Assert.True( text.Contains( "zeta" ) );
            Assert.True( text.Contains( "frame3" ) );
            Assert.True( text.Contains( "frame2" ) );
        }

    }

}