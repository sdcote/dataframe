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

import java.util.List;

import coyote.dataframe.DataFrame;
import coyote.dataframe.marshal.JSONMarshaler;


/**
 * 
 */
public class Stepper {

  public static void main( String[] args ) {
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

  }
}
