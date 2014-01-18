/**
 * 
 */
package coyote.dataframe;

/**
 * @author Steve.Cote
 *
 */
public class NullType implements FieldType
{

  private static final byte[] NULLVALUE = new byte[0];

  private final static String _name = "BYTE";




  /** 
   * zero size implies null type
   * @see coyote.dataframe.FieldType#getSize()
   */
  public int getSize()
  {
    return 0;
  }




  /**
   * @see coyote.dataframe.FieldType#checkType(java.lang.Object)
   */
  public boolean checkType( Object obj )
  {
    if( obj == null )
      return true;
    else
      return false;
  }




  public byte[] encode( Object obj )
  {
    return NULLVALUE;
  }




  public Object decode( byte[] value )
  {
    return null;
  }




  public String getTypeName()
  {
    return _name;
  }




  public boolean isNumeric()
  {
    return false;
  }

}
