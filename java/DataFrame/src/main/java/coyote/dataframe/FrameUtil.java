/*
 * DataFrame - a data marshaling toolkit
 * Copyright (C) 2006 Stephan D. Cote' - All rights reserved.
 * 
 * This program and the accompanying materials are made available under the 
 * terms of the Eclipse Public License v1.0  which accompanies this 
 * distribution, and is available at http://www.eclipse.org/legal/epl-v10.html
 *
 * Contributors:
 *   Stephan D. Cote 
 *      - Initial API and implementation
 */
package coyote.dataframe;

import coyote.util.ByteUtil;


/**
 * The FrameUtil class provides a set of useful functions for dealing with
 * frames.
 * 
 * We need to break this class into several classes due to SOLID violations
 * Single Responsibility Principle
 * Liskov Substitution Principle
 * 
 * FrameFormatter - Interface
 * XmlFrameFormatter - Formats a frame into XML (with i18n!)
 * JsonFrameFormatter - Formats a frame into JSON
 * DumpFrameFormatter - Formats DataFrames into ASCII dumps
 * 
 * FrameMarshaler - Interface
 * XmlFrameMarshaler - Converts XML to DataFramew
 * JsonFrameMarshaler - Converts JSON into DataFrames
 */
public class FrameUtil
{
  /** Platform specific line separator (default = CRLF) */
  public static final String LINE_FEED = System.getProperty( "line.separator", "\r\n" );

  public static final String NODENAME = "frame";




  /**
   * 
   */
  private FrameUtil()
  {
    // TODO Auto-generated constructor stub
  }




  public static String toIndentedXML( final DataFrame packet, final int indent )
  {
    String padding = null;
    String nextPadding = null;

    int nextindent = -1;

    if( indent > -1 )
    {
      final char[] pad = new char[indent];
      for( int i = 0; i < indent; pad[i++] = ' ' )
        ;

      padding = new String( pad );
      nextindent = indent + 2;
    }
    else
    {
      padding = new String( "" );
    }

    final StringBuffer xml = new StringBuffer( padding + "<" );

    xml.append( NODENAME );

    if( ( packet.getFieldCount() > 0 ) )
    {

      xml.append( ">" );

      if( indent >= 0 )
      {
        xml.append( LINE_FEED );
      }

      if( nextindent > -1 )
      {
        final char[] pad = new char[nextindent];
        for( int i = 0; i < nextindent; pad[i++] = ' ' )
        {
          ;
        }

        nextPadding = new String( pad );
      }
      else
      {
        nextPadding = new String( "" );
      }

      for( int x = 0; x < packet.getFieldCount(); x++ )
      {
        final DataField field = packet.getField( x );

        {
          xml.append( nextPadding );

          String fname = field.getName();
          xml.append( padding );
          if( fname == null )
          {
            fname = "field" + x;
          }

          xml.append( "<Field " );

          if( field.getName() != null )
          {
            xml.append( "name='" );
            xml.append( field.getName() );
            xml.append( "' " );
          }
          xml.append( "type='" );
          xml.append( field.getTypeName() );
          xml.append( "'>" );
          xml.append( field.getObjectValue().toString() );
          xml.append( "</Field>" );

        }

        if( indent >= 0 )
        {
          xml.append( LINE_FEED );
        }
      }

    }
    else
    {
      xml.append( "/>" );
    }

    return xml.toString();
  }




  /**
   * Method dump
   * 
   * @return
   */
  public static String dump( final DataFrame packet )
  {
    final int length = packet.getBytes().length;
    final StringBuffer buffer = new StringBuffer();
    buffer.append( "DataFrame of " );
    buffer.append( length );
    buffer.append( " bytes" + LINE_FEED );
    buffer.append( ByteUtil.dump( packet.getBytes(), length ) );
    buffer.append( LINE_FEED );

    return buffer.toString();
  }




  /**
   * This dumps the packet in a form of XML adding a name attribute to the
   * packet for additional identification.
   * 
   * <p>
   * This method is called to represent an embedded or child packet with the
   * name of the PacketField being used as the name of the XML node.
   * </p>
   * 
   * @return an XML representation of the packet.
   */
  public static String toXML( final DataFrame packet, final String name )
  {
    final StringBuffer buffer = new StringBuffer();
    buffer.append( "<" );
    buffer.append( FrameUtil.NODENAME );
    if( name != null )
    {
      buffer.append( " name='" );
      buffer.append( name );
      buffer.append( "'" );
    }

    if( packet.getFieldCount() > 0 )
    {
      buffer.append( ">" );
      for( int i = 0; i < packet.getFieldCount(); i++ )
      {
        final DataField field = packet.getField( i );

        if( field.getType() == DataField.FRAMETYPE )
        {
          buffer.append( FrameUtil.toXML( (DataFrame)field.getObjectValue(), field.getName() ) );
        }
        else
        {
          String fname = field.getName();

          if( fname == null )
          {
            fname = "field" + i;
          }

          buffer.append( "<Field " );

          if( field.getName() != null )
          {
            buffer.append( "name='" );
            buffer.append( field.getName() );
            buffer.append( "' " );
          }
          buffer.append( "type='" );
          buffer.append( field.getTypeName() );
          buffer.append( "'>" );
          buffer.append( field.getObjectValue().toString() );
          buffer.append( "</Field>" );
        }
      } // foreach field

      buffer.append( "</" );
      buffer.append( FrameUtil.NODENAME );
      buffer.append( ">" );
    }
    else
    {
      buffer.append( "/>" );
    }

    return buffer.toString();
  }




  public static String prettyPrint( final DataFrame packet, final int indent )
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

    for( int x = 0; x < packet.getFieldCount(); x++ )
    {
      final DataField field = packet.getField( x );
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
        buffer.append( FrameUtil.prettyPrint( (DataFrame)field.getObjectValue(), nextindent ) );
      }
      else
      {
        buffer.append( field.getObjectValue().toString() );
      }

      if( x + 1 < packet.getFieldCount() )
      {
        buffer.append( LINE_FEED );
      }

    }

    return buffer.toString();
  }

  // RFC 4627 data formatting
  //public static String toJSON(final DataFrame packet, final String name)

}
