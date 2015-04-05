/*
 * Copyright (c) 2014 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial concept and initial implementation
 */
package coyote.dataframe.marshal.json;

//import static org.junit.Assert.*;
import static org.junit.Assert.assertTrue;

import java.util.List;

import org.junit.Test;

import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.JSONMarshaler;


/**
 * 
 */
public class JSONMarshalTest {

  @Test
  public void testObject() throws Exception {
    String json = "{}";
    System.out.println( json );
    List<DataFrame> results = JSONMarshaler.marshal( json );
    assertTrue( results.size() == 1 );
    DataFrame frame = results.get( 0 );
    System.out.println( frame );
    assertTrue( frame.size() == 0 );
    DataField result = frame.getField( 0 ); // get the JSON data object
    System.out.println( "----------------------------\r\n" );

    json = "{\"one\" : 1}";
    System.out.println( json );
    results = JSONMarshaler.marshal( json );
    assertTrue( results.size() == 1 );
    frame = results.get( 0 );
    System.out.println( frame );
    assertTrue( frame.size() == 1 );
    result = frame.getField( 0 );
    System.out.println( "----------------------------" );

    json = "{\"one\" : 1,\"two\" : 2,\"three\" : 3}";
    System.out.println( json );
    results = JSONMarshaler.marshal( json );
    assertTrue( results.size() == 1 );
    frame = results.get( 0 );
    System.out.println( frame );
    assertTrue( frame.size() == 3 );
    result = frame.getField( 0 );
    System.out.println( "----------------------------\r\n" );

    json = "{}{}{}";
    System.out.println( json );
    results = JSONMarshaler.marshal( json );
    assertTrue( results.size() == 3 );
    frame = results.get( 0 );
    System.out.println( frame );
    System.out.println( "----------------------------\r\n" );
  }



  @Test
  public void testArray() throws Exception {
    String json = "[]";
    List<DataFrame> results = JSONMarshaler.marshal( json );
    assertTrue(results.size()==1);
    DataFrame frame = results.get( 0 );
    System.out.println(frame);
    
    
    json = "[5,3]";
    results =JSONMarshaler.marshal( json ); 
    assertTrue(results.size()==1);
    frame = results.get( 0 );
    System.out.println(frame);
    //assertEquals( "[5]", obj.toString() );

    json = "[5,10,2]";
    results =JSONMarshaler.marshal( json ); 
    assertTrue(results.size()==1);
    frame = results.get( 0 );
    System.out.println(frame);
    //assertEquals( "[5,10,2]", obj.toString() );

    json = "[\"hello\\bworld\\\"abc\\tdef\\\\ghi\\rjkl\\n123\\u4e2d\"]";
    results =JSONMarshaler.marshal( json ); 
    assertTrue(results.size()==1);
    frame = results.get( 0 );
    System.out.println(frame);
    //   assertEquals( "hello\bworld\"abc\tdef\\ghi\rjkl\n123中", ( (List)obj ).get( 0 ).toString() );

    json = "[5,]"; // non-standard
    results =JSONMarshaler.marshal( json ); 
    assertTrue(results.size()==1);
    frame = results.get( 0 );
    System.out.println(frame);
    //assertEquals( "[5,null]", obj.toString() );
    
    json = "[5,,2]"; // non-standard
    results =JSONMarshaler.marshal( json ); 
    assertTrue(results.size()==1);
    frame = results.get( 0 );
    System.out.println(frame);
    //assertEquals( "[5,null,2]", obj.toString() );

  }




  @Test
  public void testRealObject() throws Exception {
    String json = "[{\"message_stats\":{\"deliver_get\":2,\"deliver_get_details\":{\"rate\":0.0},\"get_no_ack\":2,\"get_no_ack_details\":{\"rate\":0.0},\"publish\":2,\"publish_details\":{\"rate\":0.0}},\"messages\":0,\"messages_details\":{\"rate\":0.0},\"messages_ready\":0,\"messages_ready_details\":{\"rate\":0.0},\"messages_unacknowledged\":0,\"messages_unacknowledged_details\":{\"rate\":0.0},\"name\":\"/\",\"tracing\":false}]";
    System.out.println( json );
    List<DataFrame> results =JSONMarshaler.marshal( json ); 
    assertTrue(results.size()==1);
    DataFrame frame = results.get( 0 );
    System.out.println(frame);
    System.out.println( "----------------------------\r\n" );

    //assertTrue(frame.size()==1);
    DataField result = frame.getField( 0 ); // get the JSON data object
  }

  //  public void testx() throws Exception {
  //    String s = "[0,{\"1\":{\"2\":{\"3\":{\"4\":[5,{\"6\":7}]}}}}]";
  //    Object obj = JSONValue.parse( s );
  //    JSONArray array = (JSONArray)obj;
  //    System.out.println( "======the 2nd element of array======" );
  //    System.out.println( array.get( 1 ) );
  //    System.out.println();
  //    assertEquals( "{\"1\":{\"2\":{\"3\":{\"4\":[5,{\"6\":7}]}}}}", array.get( 1 ).toString() );
  //
  //    DataFrame obj2 = (DataFrame)array.get( 1 );
  //    System.out.println( "======field \"1\"==========" );
  //    System.out.println( obj2.getObject( "1" ) );
  //    assertEquals( "{\"2\":{\"3\":{\"4\":[5,{\"6\":7}]}}}", obj2.getObject( "1" ).toString() );
  //  }
  //  

}
