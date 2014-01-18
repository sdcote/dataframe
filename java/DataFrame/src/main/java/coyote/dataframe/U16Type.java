package coyote.dataframe;

import coyote.util.ByteUtil;

/** (5) Type code representing an unsigned, 16-bit value in the range of 0 to 65,535 */
public class U16Type implements FieldType
{
  private static final int _size = 2;

  private final static String _name = "U16";




  public boolean checkType( Object obj )
  {
    return obj instanceof Short;
  }




  public Object decode( byte[] value )
  {
    return new Integer( ByteUtil.retrieveUnsignedShort( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderUnsignedShort( ( (Short)obj ).shortValue() );
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
