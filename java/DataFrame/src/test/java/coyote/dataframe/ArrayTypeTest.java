/*
 * Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial API and implementation
 */
package coyote.dataframe;

import static org.junit.Assert.assertTrue;

import org.junit.Test;


/**
 * This class contains unit test for the ArrayType
 * 
 * @author Stephan D. Cote'
 */
public class ArrayTypeTest
{

  /**
   * Test method for {@link coyote.dataframe.ArrayType#checkType(java.lang.Object)}.
   */
  @Test
  public void testCheckType()
  {
    String[] array = new String[3];
    ArrayType subject = new ArrayType();
    assertTrue( subject.checkType( array ) );
  }




  /**
   * Test method for {@link coyote.dataframe.ArrayType#decode(byte[])}.
   */
  @Test
  public void testDecode()
  {
    byte[] data = new byte[12];
    data[0] = 3; // type of string
    data[1] = 0; // first byte of length
    data[2] = 1; // second byte of length
    data[3] = 65; // Latin-1 Capital 'A'
    data[4] = 3;
    data[5] = 0;
    data[6] = 1;
    data[7] = 66;
    data[8] = 3;
    data[9] = 0;
    data[10] = 1;
    data[11] = 67;

    ArrayType subject = new ArrayType();
    Object obj = subject.decode( data );
    assertTrue( obj instanceof Object[] );
    Object[] array = (Object[])obj;
    assertTrue( array.length == 3 );
    Object value1 = array[0];
    Object value2 = array[1];
    Object value3 = array[2];
    assertTrue( value1 instanceof String );
  }




  /**
   * Test method for {@link coyote.dataframe.ArrayType#encode(java.lang.Object)}.
   */
  @Test
  public void testEncode()
  {
    String[] array = new String[3];
    array[0] = "A";
    array[1] = "B";
    array[2] = "C";
    ArrayType subject = new ArrayType();
    byte[] payload = subject.encode( array );
    assertTrue( payload.length == 12 );
  }




  /**
   * Test method for {@link coyote.dataframe.ArrayType#getTypeName()}.
   */
  @Test
  public void testGetTypeName()
  {
    ArrayType subject = new ArrayType();
    assertTrue( subject.getTypeName().equals( "ARY" ) );
  }




  /**
   * Test method for {@link coyote.dataframe.ArrayType#isNumeric()}.
   */
  @Test
  public void testIsNumeric()
  {
    ArrayType subject = new ArrayType();
    org.junit.Assert.assertFalse( subject.isNumeric() );
  }




  /**
   * Test method for {@link coyote.dataframe.ArrayType#getSize()}.
   */
  @Test
  public void testGetSize()
  {
    ArrayType subject = new ArrayType();
    assertTrue( subject.getSize() == -1 );
  }




  @Test
  public void roundTrip()
  {
    byte[] bytes = new byte[1];
    bytes[0] = (byte)255;
    //System.out.println( ByteUtil.dump( bytes ) );

    Object[] values = new Object[10];
    values[0] = "test";
    values[1] = (short)255; //U8 type5
    values[2] = (short)-32768; //S16 type6
    values[3] = 65535; //U16 type7
    values[4] = -2147483648; //S32 type8
    values[5] = 4294967296L; //U32 type9
    values[6] = -9223372036854775808L; //S64 type10
    values[7] = 9223372036854775807L; //U64 type11
    values[8] = 123456.5F; //type12
    values[9] = 123456.5D; //type13

    ArrayType subject = new ArrayType();
    byte[] payload = subject.encode( values );
    assertTrue( payload.length == 61 );
    //System.out.println( coyote.util.ByteUtil.dump( payload ) );

    Object obj = subject.decode( payload );
    assertTrue( obj instanceof Object[] );
    Object[] array = (Object[])obj;
    assertTrue( array.length == 10 );
    Object element = array[0];
    //System.out.println( "Element 0 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[0] );
    assertTrue( element instanceof String );
    assertTrue( "test".equals( (String)element ) );
    element = array[1];
    //System.out.println( "Element 1 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[1] );
    assertTrue( element instanceof Short );
    assertTrue( 255 == ( (Short)element ).shortValue() );
    element = array[2];
    //System.out.println( "Element 2 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[2] );
    assertTrue( element instanceof Short );
    assertTrue( -32768 == ( (Short)element ).shortValue() );
    element = array[3];
    //System.out.println( "Element 3 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[3] );
    assertTrue( element instanceof Integer );
    assertTrue( 65535 == ( (Integer)element ).intValue() );
    element = array[4];
    //System.out.println( "Element 4 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[4] );
    assertTrue( -2147483648 == ( (Integer)element ).intValue() );
    element = array[5];
    //System.out.println( "Element 5 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[5] );
    assertTrue( element instanceof Long );
    assertTrue( 4294967296L == ( (Long)element ).longValue() );
    element = array[6];
    //System.out.println( "Element 6 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[6] );
    assertTrue( element instanceof Long );
    assertTrue( -9223372036854775808L == ( (Long)element ).longValue() );
    element = array[7];
    //System.out.println( "Element 7 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[7] );
    assertTrue( element instanceof Long );
    assertTrue( 9223372036854775807L == ( (Long)element ).longValue() );
    element = array[8];
    //System.out.println( "Element 8 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[8] );
    assertTrue( element instanceof Float );
    assertTrue( 123456.5F == ( (Float)element ).floatValue() );
    element = array[9];
    //System.out.println( "Element 9 is " + element.getClass() + " value=>" + element.toString() + " Original=>" + values[9] );
    assertTrue( element instanceof Double );
    assertTrue( 123456.5D == ( (Double)element ).doubleValue() );
  }
}
