/*
 * 
 */
package coyote.dataframe;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.Date;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import coyote.commons.ByteUtil;


/**
 * This class models...DataFieldTest
 * 
 * @author Stephan D. Cote'
 */
public class DataFieldTest
{

  /**
   * @throws java.lang.Exception
   */
  @BeforeClass
  public static void setUpBeforeClass() throws Exception
  {
  }




  /**
   * @throws java.lang.Exception
   */
  @AfterClass
  public static void tearDownAfterClass() throws Exception
  {
  }




  /**
   * @throws java.lang.Exception
   */
  @Before
  public void setUp() throws Exception
  {
  }




  /**
   * @throws java.lang.Exception
   */
  @After
  public void tearDown() throws Exception
  {
  }




  @Test
  public void testConstructor()
  {
    String nulltag = null;
    Object nullval = null;

    DataField field = new DataField( "" );
    field.getValue();

    field = new DataField( nullval );
    field = new DataField( new Long( 0 ) );

    field = new DataField( new String(), new String() );
    new DataField( nulltag, nullval );

    field = new DataField( 0l );
    field = new DataField( new String(), 0l );
    field = new DataField( nulltag, 0l );

    field = new DataField( 0 );
    field = new DataField( new String(), 0 );
    field = new DataField( nulltag, 0 );

    field = new DataField( (short)0 );
    field = new DataField( new String(), (short)0 );
    field = new DataField( nulltag, (short)0 );

    field = new DataField( new byte[0] );
    field = new DataField( new String(), new byte[0] );
    field = new DataField( nulltag, new byte[0] );

    field = new DataField( (byte[])null );
    field = new DataField( nulltag, (byte[])null );

    field = new DataField( 0f );
    field = new DataField( new String(), 0f );
    field = new DataField( nulltag, 0f );

    field = new DataField( 0d );
    field = new DataField( new String(), 0d );
    field = new DataField( nulltag, 0d );

    field = new DataField( true );
    field = new DataField( new String(), true );
    field = new DataField( nulltag, true );

    field = new DataField( new Date() );
    field = new DataField( new String(), new Date() );
    field = new DataField( null, new Date() );

    try
    {
      field = new DataField( new URI( "" ) );
    }
    catch( IllegalArgumentException e )
    {
      fail( e.getMessage() );
    }
    catch( URISyntaxException e )
    {
      fail( e.getMessage() );
    }
    try
    {
      field = new DataField( new String(), new URI( "" ) );
    }
    catch( IllegalArgumentException e )
    {
      fail( e.getMessage() );
    }
    catch( URISyntaxException e )
    {
      fail( e.getMessage() );
    }

    try
    {
      field = new DataField( nulltag, new URI( "" ) );
    }
    catch( IllegalArgumentException e )
    {
      fail( e.getMessage() );
    }
    catch( URISyntaxException e )
    {
      fail( e.getMessage() );
    }

    //field = new DataField(null); //ambiguous; DataInputStream or Object?

  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.Object)}.
   */
  @Test
  public void testDataFieldObject()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, java.lang.Object)}.
   */
  @Test
  public void testDataFieldStringObject()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(long)}.
   */
  @Test
  public void testDataFieldLong()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, long)}.
   */
  @Test
  public void testDataFieldStringLong()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(int)}.
   */
  @Test
  public void testDataFieldInt()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, int)}.
   */
  @Test
  public void testDataFieldStringInt()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(short)}.
   */
  @Test
  public void testDataFieldShort()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, short)}.
   */
  @Test
  public void testDataFieldStringShort()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(byte[])}.
   */
  @Test
  public void testDataFieldByteArray()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, byte[])}.
   */
  @Test
  public void testDataFieldStringByteArray()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(float)}.
   */
  @Test
  public void testDataFieldFloat()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, float)}.
   */
  @Test
  public void testDataFieldStringFloat()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(double)}.
   */
  @Test
  public void testDataFieldDouble()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, double)}.
   */
  @Test
  public void testDataFieldStringDouble()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(boolean)}.
   */
  @Test
  public void testDataFieldBoolean() {
    DataField field = new DataField( true );
    byte[] data = field.getBytes();
    //System.out.println(ByteUtil.dump( data ));
    assertTrue( data.length==3);
    assertTrue( data[0]==0);
    assertTrue( data[1]==14);
    assertTrue( data[2]==1);
    
 }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, boolean)}.
   */
  @Test
  public void testDataFieldStringBoolean() {
    DataField field = new DataField( "Test", true );
    byte[] data = field.getBytes();
    //System.out.println(ByteUtil.dump( data ));
    assertTrue( data.length==7);
    assertTrue( data[0]==4);
    assertTrue( data[1]==84);
    assertTrue( data[2]==101);
    assertTrue( data[3]==115);
    assertTrue( data[4]==116);
    assertTrue( data[5]==14);
    assertTrue( data[6]==1);
    
    field = new DataField( "Test", false );
    data = field.getBytes();
    //System.out.println(ByteUtil.dump( data ));
    assertTrue( data.length==7);
    assertTrue( data[0]==4);
    assertTrue( data[1]==84);
    assertTrue( data[2]==101);
    assertTrue( data[3]==115);
    assertTrue( data[4]==116);
    assertTrue( data[5]==14);
    assertTrue( data[6]==0);
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.util.Date)}.
   */
  @Test
  public void testDataFieldDate()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, java.util.Date)}.
   */
  @Test
  public void testDataFieldStringDate()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.net.URI)}.
   */
  @Test
  public void testDataFieldURI()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.lang.String, java.net.URI)}.
   */
  @Test
  public void testDataFieldStringURI()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#clone()}.
   */
  @Test
  public void testClone()
  {
    DataField original = new DataField( "Test", 17345 );

    Object copy = original.clone();

    assertNotNull( copy );
    assertTrue( copy instanceof DataField );
    DataField field = (DataField)copy;
    assertTrue( "Test".equals( field.name ) );
    assertTrue( field.type == 7 );
    Object obj = field.getObjectValue();
    assertNotNull( obj );
    assertTrue( obj instanceof Integer );
    assertTrue( ( (Integer)obj ).intValue() == 17345 );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#DataField(java.io.DataInputStream)}.
   */
  @Test
  public void testDataFieldDataInputStream()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getType(java.lang.Object)}.
   */
  @Test
  public void testGetTypeObject()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#encode(java.lang.Object)}.
   */
  @Test
  public void testEncodeObject()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#encode(java.lang.Object, short)}.
   */
  @Test
  public void testEncodeObjectShort()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getTaggedValue(int, byte[])}.
   */
  @Test
  public void testGetTaggedValue()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getType()}.
   */
  @Test
  public void testGetType()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getLength()}.
   */
  @Test
  public void testGetLength()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getValue()}.
   */
  @Test
  public void testGetValue()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getObjectValue()}.
   */
  @Test
  public void testGetObjectValue()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getObjectValue(short, byte[])}.
   */
  @Test
  public void testGetObjectValueShortByteArray()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getStringValue()}.
   */
  @Test
  public void testGetStringValue()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getStringValue(java.lang.String)}.
   */
  @Test
  public void testGetStringValueString()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#write(java.io.DataOutputStream)}.
   */
  @Test
  public void testWrite()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getBytes()}.
   */
  @Test
  public void testGetBytes()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#dumpValue()}.
   */
  @Test
  public void testDumpValue()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#dumpAll()}.
   */
  @Test
  public void testDumpAll()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getName()}.
   */
  @Test
  public void testGetName()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#setName(java.lang.String)}.
   */
  @Test
  public void testSetName()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getTypeName(short)}.
   */
  @Test
  public void testGetTypeNameShort()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getTypeName()}.
   */
  @Test
  public void testGetTypeName()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getAsLong()}.
   */
  @Test
  public void testGetAsLong()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getAsUri()}.
   */
  @Test
  public void testGetAsUri()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#getAsInt()}.
   */
  @Test
  public void testGetAsInt()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#setStringEncoding(java.lang.String)}.
   */
  @Test
  public void testSetStringEncoding()
  {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#isNumeric()}.
   */
  @Test
  public void testIsNumeric()
  {
    DataField subject = new DataField( "Test", 32767 );
    assertTrue( subject.isNumeric() );
    subject = new DataField( "Test", "32767" );
    assertFalse( subject.isNumeric() );
  }




  /**
   * Test method for {@link coyote.dataframe.DataField#toString()}.
   */
  @Test
  public void testToString()
  {
    DataField subject = new DataField( "Test", 32767 );
    String text = subject.toString();
    assertNotNull( text );
    assertTrue( text.length() == 48 );

    // Test truncation of long values
    subject = new DataField( "Test", "01234567890123456789012345678901234567890123456789" );
    text = subject.toString();
    assertNotNull( text );
    assertTrue( text.length() < 170 );
  }

}
