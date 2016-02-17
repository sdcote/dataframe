/*
 * Copyright (c) 2015 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial concept and initial implementation
 */
package coyote.dataframe.marshal;

import java.util.Date;
import java.util.List;

import coyote.dataframe.DataFrame;


/**
 * 
 */
public class Stepper {

  public static void main( String[] args ) {
    //json();

    xml();
  }




  static void xml() {

    DataFrame frame = new DataFrame();
    frame.put( "test", "This is a test" );
    frame.put( "another", "This is another" );

    DataFrame nested = new DataFrame();
    nested.put( "inner", "This is some inner data" );
    frame.put( "nested", nested );

    frame.put( "more", "Some more data" );

    DataFrame frame1 = new DataFrame();
    frame1.put( "number", 123 );
    frame1.put( "bool", true );

    DataFrame frame2 = new DataFrame();
    frame2.put( "long", 456l );
    frame2.put( "double", 5.3D );

    DataFrame frame3 = new DataFrame();
    frame3.put( "date", new Date() );

    frame2.put( "Frame3", frame3 );
    frame1.put( "Frame2", frame2 );
    frame.put( "Frame1", frame1 );

    frame.put( "LAST", "The End" );

    // All valid XML has one root, put our data in that root
    DataFrame root = new DataFrame();
    root.put( "root", frame );

    //String xml = XMLMarshaler.marshal( frame );
    String xml = XMLMarshaler.toFormattedString( root );
    System.out.println( xml );

    System.out.println( XMLMarshaler.marshal( frame2 ) );
    
    System.out.println();
    xml = XMLMarshaler.toTypedString( root );
    System.out.println( xml );

    System.out.println();
    xml = XMLMarshaler.toFormattedTypedString( root );
    System.out.println( xml );
  }




  static void json() {
    //String json = "[{\"message_stats\":{\"deliver_get\":2,\"deliver_get_details\":{\"rate\":0.0},\"get_no_ack\":2,\"get_no_ack_details\":{\"rate\":0.0},\"publish\":2,\"publish_details\":{\"rate\":0.0}},\"messages\":0,\"messages_details\":{\"rate\":0.0},\"messages_ready\":0,\"messages_ready_details\":{\"rate\":0.0},\"messages_unacknowledged\":0,\"messages_unacknowledged_details\":{\"rate\":0.0},\"name\":\"/\",\"tracing\":false}]";
    //String json = "[{\"tracing\":false}]";

    String json = "{\"Reader\":{  \"preload\" : true, \"header\" : true } }";

    System.out.println( json );
    List<DataFrame> results = JSONMarshaler.marshal( json );
    DataFrame frame = results.get( 0 );
    System.out.println( "Minimal ----------------------------" );
    String nativetxt = frame.toString();
    String minimal = JSONMarshaler.marshal( frame );
    System.out.println( minimal );

    System.out.println( "Indented ----------------------------" );
    String indented = JSONMarshaler.toFormattedString( frame );
    System.out.println( indented );

    json = JSONMarshaler.marshal( frame );
    System.out.println( json );

  }
}
