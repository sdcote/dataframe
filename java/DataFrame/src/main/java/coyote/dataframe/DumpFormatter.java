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

import coyote.util.ByteUtil;

public class DumpFormatter implements FrameFormatter {

	/** Platform specific line separator (default = CRLF) */
	private static final String LINE_FEED = System.getProperty("line.separator", "\r\n");

	public String format(DataFrame frame) {
		final int length = frame.getBytes().length;
		final StringBuffer buffer = new StringBuffer();
		buffer.append("DataFrame of ");
		buffer.append(length);
		buffer.append(" bytes" + LINE_FEED);
		buffer.append(ByteUtil.dump(frame.getBytes(), length));
		buffer.append(LINE_FEED);

		return buffer.toString();
	}

}
