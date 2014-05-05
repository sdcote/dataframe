package coyote.dataframe.selector;

import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertTrue;

import java.util.List;

import org.junit.Test;

import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.PropertyFrame;

public class FieldSelectorTest {

  @Test
  public void testFieldSelector()
  {
    FieldSelector selector = new FieldSelector("java.*.>");
    selector= new FieldSelector(">");
  }




  @Test
  public void testSelect()
  {
    PropertyFrame marshaler = new PropertyFrame();
    DataFrame frame = marshaler.marshal( System.getProperties(), true );
    assertNotNull( frame );
    FieldSelector selector = new FieldSelector("java.vm.>");
    List<DataField> results = selector.select( frame );
    assertNotNull(results);
    assertTrue( results.size()>0);
  }

}
