package coyote.util;

import coyote.dataframe.DataField;
import coyote.dataframe.DataFrame;


public class FrameFormatter
{
  /** Platform specific line separator (default = CRLF) */
  private static final String LINE_FEED = System.getProperty( "line.separator", "\r\n" );

  /*  */
  private static final String NODENAME = "frame";




  public static String toXML( DataFrame frame )
  {
    return toXML( frame, NODENAME );
  }




  public static String toIndentedXML( DataFrame frame )
  {
    return toIndentedXML( frame, 2 );
  }




  public static String prettyPrint( DataFrame frame )
  {
    return prettyPrint( frame, 2 );
  }




  public static String dump( DataFrame frame )
  {
    final int length = frame.getBytes().length;
    final StringBuffer buffer = new StringBuffer();
    buffer.append( "DataFrame of " );
    buffer.append( length );
    buffer.append( " bytes" + LINE_FEED );
    buffer.append( ByteUtil.dump( frame.getBytes(), length ) );
    buffer.append( LINE_FEED );

    return buffer.toString();
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
  private static String toXML( final DataFrame packet, final String name )
  {
    final StringBuffer buffer = new StringBuffer();
    buffer.append( "<" );
    buffer.append( NODENAME );
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
          buffer.append( toXML( (DataFrame)field.getObjectValue(), field.getName() ) );
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
      buffer.append( NODENAME );
      buffer.append( ">" );
    }
    else
    {
      buffer.append( "/>" );
    }

    return buffer.toString();
  }




  private static String toIndentedXML( final DataFrame frame, final int indent )
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

    if( ( frame.getFieldCount() > 0 ) )
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

      for( int x = 0; x < frame.getFieldCount(); x++ )
      {
        final DataField field = frame.getField( x );

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

}
