package coyote.dataframe;

import coyote.util.ByteUtil;

/** (9) Type code representing an unsigned, 64-bit value in the range of 0 to 18,446,744,073,709,551,615 */
public class U64Type implements FieldType
{
  private static final int _size = 8;

  private final static String _name = "U64";




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
    //return ByteUtil.renderUnsignedLong(( (Long)obj ).longValue());
    return ByteUtil.renderUnsignedInt( ( (Long)obj ).intValue() ); //TODO: FixMe!
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
