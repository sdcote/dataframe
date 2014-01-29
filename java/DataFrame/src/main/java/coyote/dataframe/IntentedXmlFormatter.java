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

/**
 * @author scote
 * 
 */
public class IntentedXmlFormatter implements FrameFormatter {

	/** Platform specific line separator (default = CRLF) */
	public static final String LINE_FEED = System.getProperty("line.separator",	"\r\n");

	/*  */
	public static final String NODENAME = "frame";

	/*
	 * (non-Javadoc)
	 * 
	 * @see coyote.dataframe.FrameFormatter#format(coyote.dataframe.DataFrame)
	 */
	public String format(DataFrame frame) {
		return toIndentedXML(frame, 2);
	}

	private static String toIndentedXML(final DataFrame frame, final int indent) {
		String padding = null;
		String nextPadding = null;

		int nextindent = -1;

		if (indent > -1) {
			final char[] pad = new char[indent];
			for (int i = 0; i < indent; pad[i++] = ' ')
				;

			padding = new String(pad);
			nextindent = indent + 2;
		} else {
			padding = new String("");
		}

		final StringBuffer xml = new StringBuffer(padding + "<");

		xml.append(NODENAME);

		if ((frame.getFieldCount() > 0)) {

			xml.append(">");

			if (indent >= 0) {
				xml.append(LINE_FEED);
			}

			if (nextindent > -1) {
				final char[] pad = new char[nextindent];
				for (int i = 0; i < nextindent; pad[i++] = ' ') {
					;
				}

				nextPadding = new String(pad);
			} else {
				nextPadding = new String("");
			}

			for (int x = 0; x < frame.getFieldCount(); x++) {
				final DataField field = frame.getField(x);

				{
					xml.append(nextPadding);

					String fname = field.getName();
					xml.append(padding);
					if (fname == null) {
						fname = "field" + x;
					}

					xml.append("<Field ");

					if (field.getName() != null) {
						xml.append("name='");
						xml.append(field.getName());
						xml.append("' ");
					}
					xml.append("type='");
					xml.append(field.getTypeName());
					xml.append("'>");
					xml.append(field.getObjectValue().toString());
					xml.append("</Field>");

				}

				if (indent >= 0) {
					xml.append(LINE_FEED);
				}
			}

		} else {
			xml.append("/>");
		}

		return xml.toString();
	}

}
