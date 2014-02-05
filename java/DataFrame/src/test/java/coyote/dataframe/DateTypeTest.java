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
 * @author scote
 *
 */
public class DateTypeTest
{
  /** The data type under test. */
  static DateType datatype = null;
  static SimpleDateFormat dateFormat = null;




  /**
   * @throws java.lang.Exception
   */
  @BeforeClass
  public static void setUpBeforeClass() throws Exception
  {
    datatype = new DateType();
    dateFormat = new SimpleDateFormat( "yyyy/MM/dd HH:mm:ss" );
  }




  /**
   * @throws java.lang.Exception
   */
  @AfterClass
  public static void tearDownAfterClass() throws Exception
  {
    datatype = null;
  }




  /**
   * Test method for {@link coyote.dataframe.DateType#checkType(java.lang.Object)}.
   */
  @Test
  public void testCheckType()
  {
    Date date = new Date();

    assertTrue( datatype.checkType( date ) );
  }




  /**
   * Test method for {@link coyote.dataframe.DateType#decode(byte[])}.
   */
  @Test
  public void testDecode()
  {
    byte[] data = new byte[8];
    data[0] = (byte)0;
    data[1] = (byte)0;
    data[2] = (byte)0;
    data[3] = (byte)191;
    data[4] = (byte)169;
    data[5] = (byte)97;
    data[6] = (byte)245;
    data[7] = (byte)248;

    Object obj = datatype.decode( data );
    assertNotNull( obj );
    assertTrue( obj instanceof Date );
    Date date = (Date)obj;

    try
    {
      Date test = dateFormat.parse( "1996/02/01 08:15:23" );
      assertTrue( test.equals( date ) );
    }
    catch( ParseException e )
    {
      fail( e.getMessage() );
    }

  }




  /**
   * Test method for {@link coyote.dataframe.DateType#encode(java.lang.Object)}.
   */
  @Test
  public void testEncode()
  {
    try
    {
      String dateInString = "1996/02/01 08:15:23";
      Date date = dateFormat.parse( dateInString );
      byte[] data = datatype.encode( date );
      assertNotNull( data );
      assertTrue( data[0] == (byte)0 );
      assertTrue( data[1] == (byte)0 );
      assertTrue( data[2] == (byte)0 );
      assertTrue( data[3] == (byte)191 );
      assertTrue( data[4] == (byte)169 );
      assertTrue( data[5] == (byte)97 );
      assertTrue( data[6] == (byte)245 );
      assertTrue( data[7] == (byte)248 );

      System.out.println( date );
      long millis = date.getTime();
      System.out.println( millis );
      System.out.println( ByteUtil.dump( data ) );
      System.out.println( ByteUtil.dump( ByteUtil.renderLong( millis ) ) );
    }
    catch( ParseException e )
    {
      fail( e.getMessage() );
    }

  }




  @Test
  public void testGetTypeName()
  {
    assertTrue( datatype.getTypeName().equals( "DAT" ) );
  }




  @Test
  public void testIsNumeric()
  {
    assertFalse( datatype.isNumeric() );
  }




  @Test
  public void testGetSize()
  {
    assertTrue( datatype.getSize() == 8 );
  }

}
