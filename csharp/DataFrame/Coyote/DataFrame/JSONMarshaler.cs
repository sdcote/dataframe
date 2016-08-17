#region license
// Copyright (c) 2006 Stephan D. Cote' - All rights reserved.
// 
// This program and the accompanying materials are made available under the 
// terms of the MIT License which accompanies this distribution, and is 
// available at http://creativecommons.org/licenses/MIT/
// *
// Contributors:
//   Stephan D. Cote 
//      - Initial API and implementation
#endregion
using System;
using System.IO;
using System.Collections.Generic;
using Coyote.DataFrame.JSON;

namespace Coyote.DataFrame
{

    ///
    // * 
    // 
    public class JSONMarshaler
    {
        private const string NULL = "null";
        private const string TRUE = "true";
        private const string FALSE = "false";




        //  *
        //   * Marshal the given JSON into a dataframe.
        //   * 
        //   * @param json
        //   * 
        //   * @return Data frame containing the JSON represented data
        //   

        public static List<DataFrame> Marshal(string json)
        {
            List<DataFrame> retval = null;

            try
            {
                retval = new JsonFrameParser(json).Parse();
            }
            catch (Exception e)
            {
                throw new MarshalException("Could not marshal JSON to DataFrame: " + e.Message, e);
            }

            return retval;
        }




        //  *
        //   * Generate a JSON string from the given data frame.
        //   * 
        //   * @param frame The frame to marshal
        //   * 
        //   * @return A JSON formatted string which can be marshaled back into a frame
        //   
        public static string Marshal(DataFrame frame)
        {
            return write(frame, JsonWriterConfig.MINIMAL);
        }




        //  *
        //   * Generate a nicely formatted (and indented) JSON string from the given data frame.
        //   * 
        //   * @param frame The frame to marshal
        //   * 
        //   * @return A JSON formatted string which can be marshaled back into a frame
        //   
        public static string ToFormattedString(DataFrame frame)
        {
            return write(frame, JsonWriterConfig.FORMATTED);
        }




        /// </summary>
        /// Write the given frame using the given JSON writer.
        /// </summary>
        /// <param name="frame">The DataFrame to be written</param>
        /// <param name="config">The configuration of a JSON writer to use when writing the data</param>
        /// <returns>A JSON string representing the results of writing the data. It may contain error text if an I/O exception occurred</returns>
        private static string write(DataFrame frame, JsonWriterConfig config)
        {

            // create string writer
             StringWriter sw = new StringWriter();
             JsonWriter writer = config.createWriter(sw);

            try
            {
                writeFrame(frame, writer);
                sw.Flush();
            }
            catch (IOException e)
            {
                return "[\"" + e.Message + "\"]";
            }
            return sw.ToString();
        }




        /// <summary>
        /// Write the given frame using the given JSON writer. This is expected to be called recursively.
        /// </summary>
        /// <param name="frame">The DataFrame to be written</param>
        /// <param name="writer">The instance of the JSON writer responsible for writing the data</param>
        private static void writeFrame(DataFrame frame, JsonWriter writer)
        {

            if (frame == null || writer == null)
            {
                return;
            }

            if (frame.Size > 0)
            {
                bool isArray = frame.IsArray;
                if (isArray)
                    writer.writeArrayOpen();
                else
                    writer.writeObjectOpen();

                DataField field = null;
                for (int i = 0; i < frame.Size; i++)
                {
                    field = frame.Field[i];

                    if (!isArray)
                    {
                        if (field.Name != null)
                        {
                            writer.writeString(field.Name);
                        }
                        else
                        {
                            writer.writeString("");
                        }
                        writer.writeMemberSeparator();
                    }

                    if (field.Type == DataField.UDEF)
                    {
                        writer.writeLiteral(NULL);
                    }
                    else if (field.Type == DataField.BOOLEANTYPE)
                    {
                        if (TRUE.Equals(field.StringValue, StringComparison.OrdinalIgnoreCase))
                        {
                            writer.writeLiteral(TRUE);
                        }
                        else
                        {
                            writer.writeLiteral(FALSE);
                        }
                    }
                    else if (field.IsNumeric)
                    {
                        writer.writeNumber(field.StringValue);
                    }
                    else if (field.Type == DataField.FRAMETYPE)
                    {
                        writeFrame((DataFrame)field.ObjectValue, writer);
                    }
                    else
                    {
                        object obj = field.ObjectValue;
                        if (obj != null)
                        {
                            writer.writeString(obj.ToString());
                        }
                        else
                        {
                            writer.writeLiteral(NULL);
                        }
                    }
                    if (i + 1 < frame.Size)
                    {
                        writer.writeObjectSeparator();
                    }
                }

                if (isArray)
                    writer.writeArrayClose();
                else
                    writer.writeObjectClose();

            }
            else
            {
                writer.writeObjectOpen();
                writer.writeObjectClose();
            }

            return;
        }
    }

}