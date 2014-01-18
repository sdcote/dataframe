package coyote.dataframe;

import coyote.util.ByteUtil;

/** (10) Type code representing an signed, 64-bit value in the range of -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 */
public class S64Type implements FieldType
{
  private static final int _size = 8;

  private final static String _name = "S64";




  public boolean checkType( Object obj )
  {
    return obj instanceof Long;
  }




  public Object decode( byte[] value )
  {
    return new Long( ByteUtil.retrieveLong( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderLong( ( (Long)obj ).longValue() );
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
