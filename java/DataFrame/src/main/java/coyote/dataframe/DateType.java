package coyote.dataframe;

import java.util.Date;

import coyote.util.ByteUtil;


/** Type representing a unsigned 64-bit epoch time in milliseconds */
public class DateType implements FieldType
{
  private static final int _size = 8;

  private final static String _name = "DAT";




  public boolean checkType( Object obj )
  {
    return obj instanceof Date;
  }




  public Object decode( byte[] value )
  {
    return ByteUtil.retrieveDate( value, 0 );
  }




  public byte[] encode( Object obj )
  {
    return ByteUtil.renderDate( (Date)obj );
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
