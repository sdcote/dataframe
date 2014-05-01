package coyote.dataframe.marshal;

public class FrameMarshalingException extends RuntimeException {

  private static final long serialVersionUID = 651310756998856295L;




  public FrameMarshalingException() {
  }




  public FrameMarshalingException( String msg ) {
    super( msg );
  }




  public FrameMarshalingException( String msg, Throwable t ) {
    super( msg, t );
  }

}