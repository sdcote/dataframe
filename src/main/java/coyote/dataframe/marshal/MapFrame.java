package coyote.dataframe.marshal;

import java.util.HashMap;
import java.util.Map;

import coyote.dataframe.DataFrame;


public class MapFrame {

  /**
   * 
   * @param source
   * 
   * @return
   */
  public DataFrame marshal( Map<?, ?> source )
  {
    DataFrame retval = new DataFrame();

    if( source != null ) {

      for( Map.Entry<?, ?> entry : source.entrySet() ) {

        String key = entry.getKey().toString();

        Object value = entry.getValue();

        if( value != null ) {
          if( value instanceof Map ) {
            value = marshal( (Map)value );
          }
          retval.add( key, value );
        }

      }
    }
    return retval;
  }




  /**
   * 
   * @param frame
   * 
   * @return
   */
  @SuppressWarnings("rawtypes")
  public Map<?, ?> marshal( DataFrame frame )
  {
    return new HashMap();
  }
}
