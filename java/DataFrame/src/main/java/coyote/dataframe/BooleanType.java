package coyote.dataframe;

import coyote.util.ByteUtil;

/** Type representing a boolean value */
public class BooleanType implements FieldType
{
  private static final int _size = 8;

  private final static String _name = "BOL";




  public boolean checkType( Object obj )
  {
    return obj instanceof Boolean;
  }




  public Object decode( byte[] value )
  {
    return new Boolean( ByteUtil.retrieveBoolean( value, 0 ) );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderBoolean( ( (Boolean)obj ).booleanValue() );
  }




  public String getTypeName()
  {
    return _name;
  }




  public boolean isNumeric()
  {
    return false;
  }




  public int getSize()
  {
    return _size;
  }

}
