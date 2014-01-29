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
    //fail( "Not yet implemented" );
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
    System.out.println( coyote.util.ByteUtil.dump( payload ) );
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

}
