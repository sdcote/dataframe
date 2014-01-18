package coyote.dataframe;

import coyote.util.ByteUtil;

/** (6) Type code representing an signed, 16-bit value in the range of -32,768 to 32,767 */
public class S16Type implements FieldType
{
  private static final int _size = 2;

  private final static String _name = "S16";




  public boolean checkType( Object obj )
  {
    return obj instanceof Short;//TODO: Fix Me!
  }




  public Object decode( byte[] value )
  {
    return new Short( ByteUtil.retrieveShort( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderUnsignedShort( ( (Short)obj ).shortValue() );//TODO: Fix Me!
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
