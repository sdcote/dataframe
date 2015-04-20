/*
 * 
 */
package coyote.dataframe;

//import static org.junit.Assert.*;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

import org.junit.After;
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;


/**
 * This class models...DataFrameTest
 * 
 * @author Stephan D. Cote'
 */
public class DataFrameTest {

  /**
   * @throws java.lang.Exception
   */
  @BeforeClass
  public static void setUpBeforeClass() throws Exception {}




  /**
   * @throws java.lang.Exception
   */
  @AfterClass
  public static void tearDownAfterClass() throws Exception {}




  /**
   * @throws java.lang.Exception
   */
  @Before
  public void setUp() throws Exception {}




  /**
   * @throws java.lang.Exception
   */
  @After
  public void tearDown() throws Exception {}




  /**
   * Test method for {@link coyote.dataframe.DataFrame#DataFrame()}.
   */
  @Test
  public void testDataFrame() {
    DataFrame frame = new DataFrame();
    assertNotNull( frame );
    assertTrue( frame.getTypeCount() == 18 );
    assertTrue( frame.getFieldCount() == 0 );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#add(java.lang.Object)}.
   */
  @Test
  public void testAddObject() {
    DataFrame frame = new DataFrame();
    assertNotNull( frame );
    assertTrue( frame.getFieldCount() == 0 );

    DataFrame child = new DataFrame();
    frame.add( child );
    assertTrue( frame.getFieldCount() == 1 );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#add(java.lang.String, java.lang.Object)}.
   */
  @Test
  public void testAddStringObject() {
    DataFrame frame = new DataFrame();
    assertNotNull( frame );
    assertTrue( frame.getFieldCount() == 0 );

    DataFrame child = new DataFrame();
    frame.add( "KID", child );
    assertTrue( frame.getFieldCount() == 1 );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#contains(java.lang.String)}.
   */
  @Test
  public void testContains() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getField(int)}.
   */
  @Test
  public void testGetFieldInt() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getFieldCount()}.
   */
  @Test
  public void testGetFieldCount() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getAsString(java.lang.String)}.
   */
  @Test
  public void testGetAsStringString() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#clone()}.
   */
  @Test
  public void testClone() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#DataFrame(java.lang.String, java.lang.Object)}.
   */
  @Test
  public void testDataFrameStringObject() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, java.lang.Object)}.
   */
  @Test
  public void testPutStringObject() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, long)}.
   */
  @Test
  public void testPutStringLong() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, int)}.
   */
  @Test
  public void testPutStringInt() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, short)}.
   */
  @Test
  public void testPutStringShort() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, double)}.
   */
  @Test
  public void testPutStringDouble() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, float)}.
   */
  @Test
  public void testPutStringFloat() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, boolean)}.
   */
  @Test
  public void testPutStringBoolean() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, byte[])}.
   */
  @Test
  public void testPutStringByteArray() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, byte)}.
   */
  @Test
  public void testPutStringByte() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#put(java.lang.String, char)}.
   */
  @Test
  public void testPutStringChar() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#remove(java.lang.String)}.
   */
  @Test
  public void testRemove() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#replace(java.lang.String, java.lang.Object)}.
   */
  @Test
  public void testReplace() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#replaceAll(java.lang.String, java.lang.Object)}.
   */
  @Test
  public void testReplaceAll() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#removeAll(java.lang.String)}.
   */
  @Test
  public void testRemoveAll() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getDigest()}.
   */
  @Test
  public void testGetDigest() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getDigestString()}.
   */
  @Test
  public void testGetDigestString() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getBytes()}.
   */
  @Test
  public void testGetBytes() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getBytes(java.lang.String)}.
   */
  @Test
  public void testGetBytesString() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getFields()}.
   */
  @Test
  public void testGetFields() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getTimestamp()}.
   */
  @Test
  public void testGetTimestamp() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#setFields(java.util.ArrayList)}.
   */
  @Test
  public void testSetFields() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#setTimestamp(long)}.
   */
  @Test
  public void testSetTimestamp() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#dump()}.
   */
  @Test
  public void testDump() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#toXml()}.
   */
  @Test
  public void testToXml() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getPriority()}.
   */
  @Test
  public void testGetPriority() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#setPriority(short)}.
   */
  @Test
  public void testSetPriority() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getPriorityString()}.
   */
  @Test
  public void testGetPriorityString() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#getPriorityString(short)}.
   */
  @Test
  public void testGetPriorityStringShort() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#isModified()}.
   */
  @Test
  public void testIsModified() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#clear()}.
   */
  @Test
  public void testClear() {
    //fail( "Not yet implemented" );
  }




  /**
   * Test method for {@link coyote.dataframe.DataFrame#toString()}.
   */
  @Test
  public void testToString() {
    DataFrame frame1 = new DataFrame();
    frame1.add( "alpha", 1L );
    frame1.add( "beta", 2L );

    DataFrame frame2 = new DataFrame();
    frame2.add( "gamma", 3L );
    frame2.add( "delta", 4L );

    DataFrame frame3 = new DataFrame();
    frame3.add( "epsilon", 5L );
    frame3.add( "zeta", 6L );

    frame2.add( "frame3", frame3 );
    frame1.add( "frame2", frame2 );

    String text = frame1.toString();
    //System.out.println(text);

    assertTrue( text.contains( "alpha" ) );
    assertTrue( text.contains( "beta" ) );
    assertTrue( text.contains( "gamma" ) );
    assertTrue( text.contains( "delta" ) );
    assertTrue( text.contains( "epsilon" ) );
    assertTrue( text.contains( "zeta" ) );
    assertTrue( text.contains( "frame3" ) );
    assertTrue( text.contains( "frame2" ) );
  }




  @Test
  public void testToBoolean() {
    DataFrame frame1 = new DataFrame();
    frame1.add( "alpha", 1L );
    frame1.add( "beta", 0L );
    frame1.add( "gamma", -1L );

    try {
      assertTrue( frame1.getAsBoolean( "alpha" ) );
      assertFalse( frame1.getAsBoolean( "beta" ) );
      assertFalse( frame1.getAsBoolean( "gamma" ) );
    } catch ( Exception e ) {
      fail( e.getMessage() );
    }

    frame1 = new DataFrame();
    frame1.add( "alpha", true );
    frame1.add( "beta", "true" );
    frame1.add( "gamma", "1" );

    try {
      assertTrue( frame1.getAsBoolean( "alpha" ) );
      assertTrue( frame1.getAsBoolean( "beta" ) );
      assertTrue( frame1.getAsBoolean( "gamma" ) );
    } catch ( Exception e ) {
      fail( e.getMessage() );
    }
    frame1 = new DataFrame();
    frame1.add( "alpha", true );
    frame1.add( "beta", "true" );
    frame1.add( "gamma", "1" );

    try {
      assertTrue( frame1.getAsBoolean( "alpha" ) );
      assertTrue( frame1.getAsBoolean( "beta" ) );
      assertTrue( frame1.getAsBoolean( "gamma" ) );
    } catch ( Exception e ) {
      fail( e.getMessage() );
    }
    frame1 = new DataFrame();
    frame1.add( "alpha", false );
    frame1.add( "beta", "false" );
    frame1.add( "gamma", "0" );

    try {
      assertFalse( frame1.getAsBoolean( "alpha" ) );
      assertFalse( frame1.getAsBoolean( "beta" ) );
      assertFalse( frame1.getAsBoolean( "gamma" ) );
    } catch ( Exception e ) {
      fail( e.getMessage() );
    }

  }
}
