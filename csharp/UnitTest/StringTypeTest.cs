using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


using Coyote.DataFrame;


namespace UnitTest
{
    [TestFixture]
    public class StringTypeTest
    {
        [Test]
        public void StringTest()
        {
            StringType type = new StringType();
            Assert.NotNull(type);
            Assert.True(type.Size == -1);

            // test the encoiding
            byte[] data = type.Encode("Hello");
            Assert.AreEqual(data[0], 72);
            Assert.AreEqual(data[1], 101);
            Assert.AreEqual(data[2], 108);
            Assert.AreEqual(data[3], 108);
            Assert.AreEqual(data[4], 111);
        }



        [Test]
        public void StringField()
        {
            string fieldName = "test";
            DataField field = new DataField(fieldName, "hello");
            TestUtil.ValidateField(field, DataField.STRING, fieldName);
            Assert.True(field.Name != null);
        }

    }
}
