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

namespace Coyote.DataFrame
{

    /// <summary>
    /// This interface defines a data type.
    /// </summary>
    public interface FieldType
    {

        /// <summary>
        /// Determine the size in bytes to be used in representing the value.
        /// <para>A value of 0 means null; negative number means variable length type.</para>
        /// </summary>
        /// <returns>the size of the value to store or read.</returns>
        int Size { get; }

        /// <summary>
        /// Get a simple name for the type to aid in formatting.
        /// </summary>
        /// <returns>short name for the type</returns>
        string TypeName { get; }

        /// <summary>
        /// Flag indicating the data type is numeric.
        /// </summary>
        /// <returns>true if the type is numeric, false otherwise.</returns>
        bool IsNumeric { get; }

        /// <summary>
        /// Check the object if it is the same type.
        /// <para>This is often used in a loop of all the supported field types where this method is called for each with the object trying to be matched.</para>
        /// </summary>
        /// <param name="obj">the object to check</param>
        /// <returns>true if this Field type supports the object, false otherwise</returns>
        bool CheckType(object obj);
        
        /// <summary>
        /// Decode the bytes into an object.
        /// </summary>
        /// <param name="value">the array of bytes to decode</param>
        /// <returns>an object representing the decoded bytes</returns>
        object Decode(byte[] value);
        
        /// <summary>
        /// Encode the given object into a byte array
        /// </summary>
        /// <param name="obj">object to encode</param>
        /// <returns>the wire format of the object</returns>
        byte[] Encode(object obj);
        
        /// <summary>
        /// Get the string value of this data type.
        /// <para>This allows each supported type to customize its own string representation of the values it supports. For example, the string representation can be customized to be formatted in ISO 8601 or the .NET standard long date format.</para>
        /// </summary>
        /// <param name="val">the bytes to decode into a string representation of this type.</param>
        /// <returns>The string representation of the given value according to this type.</returns>
        string StringValue(byte[] val);

    }

}