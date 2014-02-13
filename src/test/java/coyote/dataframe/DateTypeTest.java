/**
 * 
 */
package coyote.dataframe;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;

import coyote.util.ByteUtil;

/**
 *
 */
public class DateTypeTest {
	/** The data type under test. */
	static DateType datatype = null;
	static SimpleDateFormat dateFormat = null;
	static byte[] datedata = new byte[8];

	@BeforeClass
	public static void setUpBeforeClass() throws Exception {
		datatype = new DateType();
		dateFormat = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
		datedata[0] = (byte) 0;
		datedata[1] = (byte) 0;
		datedata[2] = (byte) 0;
		datedata[3] = (byte) 191;
		datedata[4] = (byte) 169;
		datedata[5] = (byte) 97;
		datedata[6] = (byte) 245;
		datedata[7] = (byte) 248;
	}

	@AfterClass
	public static void tearDownAfterClass() throws Exception {
		datatype = null;
	}

	@Test
	public void testCheckType() {
		Date date = new Date();
		assertTrue(datatype.checkType(date));
	}

	@Test
	public void testDecode() {
		System.out.println("=================================================================================\r\n");
		System.out.println("Decoding test data:\r\n" + ByteUtil.dump(datedata));
		Object obj = datatype.decode(datedata);
		assertNotNull(obj);
		assertTrue(obj instanceof Date);
		Date date = (Date) obj;
		System.out.println("Decoded as: " + date);
		System.out.println("a long value of: " + date.getTime());
		try {
			Date test = dateFormat.parse("1996/02/01 08:15:23");
			assertTrue(test.equals(date));
		} catch (ParseException e) {
			fail(e.getMessage());
		}
		System.out.println("Test Completed Successfully =====================================================\r\n");
	}

	/**
	 * Test method for
	 * {@link coyote.dataframe.DateType#encode(java.lang.Object)}.
	 */
	@Test
	public void testEncode() {
		System.out.println("=================================================================================\r\n");
		try {
			String dateInString = "1996/02/01 08:15:23";
			Date date = dateFormat.parse(dateInString);
			System.out.println("Encoding date '" + dateInString
					+ "' as long value: " + date.getTime());
			System.out.println("Java date: " + date);
			System.out.println("Expecting an encoding of:\r\n"
					+ ByteUtil.dump(datedata));
			byte[] data = datatype.encode(date);
			assertNotNull(data);
			System.out.println("Encoded as:\r\n" + ByteUtil.dump(data));
			assertTrue("Dat should be 8 bytes in length, is actually "
					+ data.length + " bytes", data.length == 8);
			for (int i = 0; i < datedata.length; i++) {
				assertTrue("element " + i + " should be " + datedata[i]
						+ " but is '" + data[i] + "'", data[i] == datedata[i]);
			}
			System.out.println("Test Completed Successfully =====================================================\r\n");
		} catch (ParseException e) {
			fail(e.getMessage());
		}
	}

	@Test
	public void testGetTypeName() {
		assertTrue(datatype.getTypeName().equals("DAT"));
	}

	@Test
	public void testIsNumeric() {
		assertFalse(datatype.isNumeric());
	}

	@Test
	public void testGetSize() {
		assertTrue(datatype.getSize() == 8);
	}

}
