/**
 * 
 */
package coyote.dataframe;

/**
 *
 */
public class FrameType implements FieldType
{

  /** negative size indicates a variable length value is to be expected. */
  private static final int _size = -1;

  private final static String _name = "FRAME";




  /**
   * @see coyote.dataframe.FieldType#checkType(java.lang.Object)
   */
  public boolean checkType( Object obj )
  {
    return ( obj instanceof DataFrame );
  }




  /**
   * @see coyote.dataframe.FieldType#decode(byte[])
   */
  public Object decode( byte[] value )
  {
    try
    {
      return new DataFrame( value );
    }
    catch( final Exception e )
    {
      e.printStackTrace();
      return new DataFrame();
    }
  }




  /**
   * @see coyote.dataframe.FieldType#encode(java.lang.Object)
   */
  public byte[] encode( Object obj )
  {
    return ( (DataFrame)obj ).getBytes();
  }




  /**
   * @see coyote.dataframe.FieldType#isNumeric()
   */
  public boolean isNumeric()
  {
    return false;
  }




  /**
   * @see coyote.dataframe.FieldType#getSize()
   */
  public int getSize()
  {
    return _size;
  }




  /**
   * @see coyote.dataframe.FieldType#getTypeName()
   */
  public String getTypeName()
  {
    return _name;
  }

}
