/**
 * 
 */
package coyote.dataframe;

/**
 * @author Steve.Cote
 *
 */
public class ByteArrayType implements FieldType
{
  /** negative size indicates a variable length value is to be expected. */
  private static final int _size = -1;

  private final static String _name = "BYTE";




  public boolean checkType( Object obj )
  {
    return ( obj instanceof byte[] );
  }




  public byte[] encode( Object obj )
  {
    return (byte[])obj;
  }




  public Object decode( byte[] value )
  {
    return value;
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
