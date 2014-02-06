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
public class XmlFormatter implements FrameFormatter {

	
	/*  */
	private static final String NODENAME = "frame";

	/*
	 * 
	 * @see coyote.dataframe.FrameFormatter#format(coyote.dataframe.DataFrame)
	 */
	public String format(DataFrame frame) {
		return toXML(frame, NODENAME);
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
	private static String toXML(final DataFrame packet, final String name) {
		final StringBuffer buffer = new StringBuffer();
		buffer.append("<");
		buffer.append(NODENAME);
		if (name != null) {
			buffer.append(" name='");
			buffer.append(name);
			buffer.append("'");
		}

		if (packet.getFieldCount() > 0) {
			buffer.append(">");
			for (int i = 0; i < packet.getFieldCount(); i++) {
				final DataField field = packet.getField(i);

				if (field.getType() == DataField.FRAMETYPE) {
					buffer.append(XmlFormatter.toXML(
							(DataFrame) field.getObjectValue(), field.getName()));
				} else {
					String fname = field.getName();

					if (fname == null) {
						fname = "field" + i;
					}

					buffer.append("<Field ");

					if (field.getName() != null) {
						buffer.append("name='");
						buffer.append(field.getName());
						buffer.append("' ");
					}
					buffer.append("type='");
					buffer.append(field.getTypeName());
					buffer.append("'>");
					buffer.append(field.getObjectValue().toString());
					buffer.append("</Field>");
				}
			} // foreach field

			buffer.append("</");
			buffer.append(NODENAME);
			buffer.append(">");
		} else {
			buffer.append("/>");
		}

		return buffer.toString();
	}

}
