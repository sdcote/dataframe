package coyote.dataframe;

import coyote.util.ByteUtil;

/** (11) Type code representing a 32-bit floating point value in the range of ±1.4013e-45 to ±3.4028e+38. */
public class FloatType implements FieldType
{
  private static final int _size = 4;

  private final static String _name = "FLT";




  public boolean checkType( Object obj )
  {
    return obj instanceof Float;
  }




  public Object decode( byte[] value )
  {
    return new Float( ByteUtil.retrieveFloat( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderFloat( ( (Float)obj ).floatValue() );
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
