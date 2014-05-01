/*
 *
 */
package coyote.dataframe.marshal;

//import static org.junit.Assert.*;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

import java.util.HashMap;

import org.junit.Test;

import coyote.dataframe.DataFrame;


/**
 * 
 * @author Steve Cote
 */
public class MapFrameTest {

  /**
   * Test method for {@link coyote.dataframe.marshal.MapFrame#marshal(java.util.Map)}.
   */
  @Test
  @SuppressWarnings({ "rawtypes" })
  public void testMarshalMap()
  {
    HashMap data = new HashMap();
    data.put( "One", "One" );  
    data.put( "Two", 2 );

    MapFrame marshaler = new MapFrame();
    DataFrame frame = marshaler.marshal( data );
    assertNotNull( frame );
    assertTrue( frame.getFieldCount() == 2 );
    assertTrue( frame.contains( "One" ) );
  }




  /**
   * Test method for {@link coyote.dataframe.marshal.MapFrame#marshal(coyote.dataframe.DataFrame)}.
   */
  @Test
  public void testMarshalDataFrame()
  {
  }

}
