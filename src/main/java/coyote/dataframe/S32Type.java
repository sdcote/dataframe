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

import coyote.commons.ByteUtil;

/** (8) Type code representing a signed, 32-bit value in the range of -2,147,483,648 to 2,147,483,647 */
public class S32Type implements FieldType
{
  private static final int _size = 4;

  private final static String _name = "S32";




  public boolean checkType( Object obj )
  {
    return obj instanceof Integer;
  }




  public Object decode( byte[] value )
  {
    return new Integer( ByteUtil.retrieveInt( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderInt( ( (Integer)obj ).intValue() );
  }




  public String getTypeName()
  {
    return _name;
  }




  public boolean isNumeric()
  {
    return true;
  }




  public int getSize()
  {
    return _size;
  }

}
