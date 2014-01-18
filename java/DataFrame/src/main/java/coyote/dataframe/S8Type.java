package coyote.dataframe;

import coyote.util.ByteUtil;

/** Type representing an signed, 8-bit value in the range of -128 to 127 */
public class S8Type implements FieldType
{
  private static final int _size = 1;

  private final static String _name = "S8";




  public boolean checkType( Object obj )
  {
    return obj instanceof Byte;//TODO: Fix Me!
  }




  public Object decode( byte[] value )
  {
    return new Byte( value[0] );//TODO: Fix Me!
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderUnsignedShort( (Integer)obj );//TODO: Fix Me!
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
