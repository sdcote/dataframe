/*
 * Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the MIT License which accompanies this distribution, and is 
 * available at http://creativecommons.org/licenses/MIT/
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial API and implementation
 */
package coyote.dataframe;

public class PrettyPrintFormatter implements FrameFormatter {

	/** Platform specific line separator (default = CRLF) */
	private static final String LINE_FEED = System.getProperty("line.separator", "\r\n");

	public String format(DataFrame frame) {
		return prettyPrint(frame,2);
	}

	private static String prettyPrint( final DataFrame frame, final int indent )
	  {
	    String padding = null;
	    int nextindent = -1;

	    if( indent > -1 )
	    {
	      final char[] pad = new char[indent];
	      for( int i = 0; i < indent; pad[i++] = ' ' )
	      {
	        ;
	      }

	      padding = new String( pad );
	      nextindent = indent + 2;
	    }
	    else
	    {
	      padding = new String( "" );
	    }

	    final StringBuffer buffer = new StringBuffer();

	    for( int x = 0; x < frame.getFieldCount(); x++ )
	    {
	      final DataField field = frame.getField( x );
	      if( indent > -1 )
	      {
	        buffer.append( padding );
	      }
	      buffer.append( x );
	      buffer.append( ": " );

	      buffer.append( "'" );
	      buffer.append( field.getName() );
	      buffer.append( "' " );
	      buffer.append( field.getTypeName() );
	      buffer.append( "(" );
	      buffer.append( field.getType() );
	      buffer.append( ") " );

	      if( field.getType() == DataField.FRAMETYPE )
	      {
	        buffer.append( LINE_FEED );
	        buffer.append( prettyPrint( (DataFrame)field.getObjectValue(), nextindent ) );
	      }
	      else
	      {
	        buffer.append( field.getObjectValue().toString() );
	      }

	      if( x + 1 < frame.getFieldCount() )
	      {
	        buffer.append( LINE_FEED );
	      }

	    }

	    return buffer.toString();
	  }
	
}
