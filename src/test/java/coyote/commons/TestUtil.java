package coyote.commons;

import java.io.*;
import java.nio.charset.StandardCharsets;

public class TestUtil {
    public static String ISO8859_1;

    static {
        final String iso = System.getProperty("ISO_8859_1");
        if (iso != null) {
            TestUtil.ISO8859_1 = iso;
        } else {
            new String(new byte[]{(byte) 20}, StandardCharsets.ISO_8859_1);
            TestUtil.ISO8859_1 = "ISO-8859-1";
        }
    }

    /**
     * Read the entire file into memory as an array of bytes.
     *
     * @param file The file to read
     * @return A byte array that contains the contents of the file.
     * @throws IOException If problems occur.
     */
    public static byte[] read(final File file) throws IOException {
        if (file == null) {
            throw new IOException("File reference was null");
        }

        if (file.exists() && file.canRead()) {
            DataInputStream dis = null;
            final byte[] bytes = new byte[new Long(file.length()).intValue()];

            try {
                dis = new DataInputStream(new FileInputStream(file));

                dis.readFully(bytes);

                return bytes;
            } catch (final Exception ignore) {
            } finally {
                // Attempt to close the data input stream
                try {
                    if (dis != null) {
                        dis.close();
                    }
                } catch (final Exception ignore) {
                }
            }
        }

        return new byte[0];
    }


    /**
     * Opens a file, reads it and returns the data as a string and closes the
     * file.
     *
     * @param file - file to open
     * @return String representing the file data
     */
    public static String fileToString(final File file) {
        try {
            final byte[] data = TestUtil.read(file);

            if (data != null) {
                // Attempt to return the string
                try {
                    return new String(data, TestUtil.ISO8859_1);
                } catch (final UnsupportedEncodingException uee) {
                    // Send it back in default encoding
                    return new String(data);
                }
            }
        } catch (final Exception ex) {
        }

        return null;
    }


    /**
     * Opens a file, reads it and returns the data as a string and closes the
     * file.
     *
     * @param fname - file to open
     * @return String representing the file data
     */
    public static String fileToString(final String fname) {
        return TestUtil.fileToString(new File(fname));
    }


}
