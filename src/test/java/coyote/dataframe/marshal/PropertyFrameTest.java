package coyote.dataframe.marshal;

import static org.junit.Assert.assertNotNull;

import java.util.Properties;

import org.junit.Test;

import coyote.dataframe.DataFrame;
import coyote.util.FrameFormatter;


public class PropertyFrameTest {

  @Test
  public void testMarshalProperties()
  {
    PropertyFrame marshaler = new PropertyFrame();
    DataFrame frame = marshaler.marshal( System.getProperties() );
    assertNotNull( frame );
    System.out.println( FrameFormatter.prettyPrint( frame ));
  }

}
