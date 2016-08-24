using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Coyote.DataFrame;

namespace UnitTest {

    class TestUtil {

        /// <summary>
        /// Make sure the field contains all the properties and expected values
        /// </summary>
        /// <param name="field">the field to check</param>
        /// <param name="expectedType">the expected type</param>
        /// <param name="expectedName">the name of the field, may be null</param>
        internal static void ValidateField( DataField field, short expectedType, string expectedName ) {
            Assert.NotNull( field );

            if ( expectedName != null ) {
                Assert.NotNull( field.Name );
                Assert.True( field.Name.Equals( expectedName ) );
            } else {
                Assert.IsNull( field.Name );
            }
            Assert.AreEqual( field.Type, expectedType );
            Assert.NotNull( field.Value );
            Assert.NotNull( field.ObjectValue );
            Assert.NotNull( field.StringValue );
            Assert.NotNull( field.ToString() );
            Assert.NotNull( field.Bytes );
            Debug.WriteLine( ByteUtil.Dump( field.Bytes ) );
        }

    }

}
