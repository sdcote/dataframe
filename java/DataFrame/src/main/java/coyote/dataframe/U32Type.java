package coyote.dataframe;

import coyote.util.ByteUtil;

/** Type representing an unsigned, 32-bit value in the range of 0 to 4,294,967,295 */
public class U32Type implements FieldType
{
  private static final int _size = 4;

  private final static String _name = "U32";




  public boolean checkType( Object obj )
  {
    return obj instanceof Integer;
  }




  public Object decode( byte[] value )
  {
    return new Long( ByteUtil.retrieveUnsignedInt( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderUnsignedInt( ( (Integer)obj ).intValue() );
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
