/**
 * 
 */
package coyote.dataframe;

/**
 * This interface defines a data type
 */
public interface FieldType
{
  /**
   * Check the object if it is the same type.
   * @param obj the object to check
   * @return true if this Field type supports the object, false otherwise
   */
  public boolean checkType( Object obj );




  /**
   * Decode the bytes into an object.
   * @param value the array of bytes to decode
   * @return an object representing
   */
  public Object decode( byte[] value );




  /**
   * Encode the given object into a byte array
   * @param obj object to encode
   * @return the wire format of the object 
   */
  public byte[] encode( Object obj );




  /**
   * Flag indicating the data type is numeric.
   * @return true if the type is numeric, false otherwise.
   */
  public boolean isNumeric();




  /**
   * 0 means null
   * negative number means variable length type.
   * @return the size of the value to store or read.
   */
  public int getSize();




  /**
   * Get a simple name for the type to aid in formatting.
   * @return short name for the type
   */
  public String getTypeName();

}
