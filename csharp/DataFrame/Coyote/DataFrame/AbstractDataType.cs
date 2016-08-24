#region license
// Linkage - a multi-broker .NET messaging API
// Copyright (c) 2004, 2011 Stephan D. Cote
//
// All rights reserved. This program and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html
//
// Contributors:
//   Stephan D. Cote 
//      - Initial API and implementation
#endregion
using System;

namespace Coyote.DataFrame {

    /// <summary>
    /// This type contains convenience accessor methods for data contained in this ADT.
    /// </summary>
    /// <remarks>
    /// <para>The logic in this class has been separated from the DataFrame as it is not strictly required for the DataFrame to operate. This logic offers additional convenience methods to abstract data access even further.</para>
    /// </remarks>
    class AbstractDataType : DataFrame {


        #region Convenience Accessors


        /// <summary></summary>
        /// <param name="name"></param>
        /// <returns>The value of the named field as a frame or null if the named value does not exist.</returns>
        /// <exception cref="Exception"> if the named value cannot be converted to a frame; (e.g. the value is a scalar type).</exception>
        public DataFrame GetAsFrame( string name ) {
            if ( Contains( name ) ) {
                DataField field = Field[name];
                if ( field.IsFrame ) {
                    DataFrame rf = field.ObjectValue as DataFrame;
                    return rf;
                } else {
                    DataFrame rt = new DataFrame();
                    rt.Put( name, field );
                    return rt;
                }
            } else {
                return null;
            }
        }




        /// <summary></summary>
        /// <param name="name"></param>
        /// <returns>The value of the named field as a string or null if the named value does not exist.</returns>
        public string GetAsString( string name ) {
            if ( Contains( name ) ) {
                DataField field = Field[name];
                if ( field.IsFrame ) {
                    DataFrame rf = field.ObjectValue as DataFrame;
                    return rf.ToString();
                } else
                    return field.StringValue;
            } else {
                return null;
            }
        }




        /// <summary></summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If either the name argument or the field value is null</exception>
        /// <exception cref="FormatException">If the value could not be converted</exception>
        public long GetAsLong( string name ) {
            if ( name == null ) throw new ArgumentNullException( "field name is null" );

            if ( Contains( name ) ) {
                DataField field = Field[name];
                switch ( field.Type ) {
                    case DataField.S8:
                    case DataField.U8:
                    case DataField.S16:
                    case DataField.U16:
                    case DataField.S32:
                    case DataField.U32:
                    case DataField.S64:
                        return (long)field.ObjectValue;
                    case DataField.DATE:
                        return ByteUtil.RetrieveLong( field.Value, 0 );
                    case DataField.BOOLEAN:
                        if ( ((bool)field.ObjectValue) ) return 1L;
                        else return 0L;
                    case DataField.STRING:
                        return Int64.Parse( field.StringValue );
                    case DataField.BYTEARRAY:
                        return ByteUtil.RetrieveLong( field.Value, 0 );
                    default:
                        break;
                }
            } else {
                throw new ArgumentNullException( "field value is null" );
            }

            throw new FormatException( "Type conversion not possible" );
        }




        /// <summary></summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If either the name argument or the field value is null</exception>
        /// <exception cref="FormatException">If the value could not be converted</exception>
        public byte[] GetAsByteArray( string name ) {
            if ( name == null ) throw new ArgumentNullException( "field name is null" );

            if ( Contains( name ) )
                return Field[name].Value;
            else
                throw new ArgumentNullException( "field value is null" );

        }




        /// <summary></summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">If either the name argument or the field value is null</exception>
        /// <exception cref="FormatException">If the value could not be converted</exception>
        public DateTime GetAsDate( string name ) {
            if ( name == null ) throw new ArgumentNullException( "field name is null" );

            if ( Contains( name ) ) {
                DataField field = Field[name];
                switch ( field.Type ) {
                    case DataField.S64:
                        return new DateTime( ((long)field.ObjectValue * 10000) + new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc ).Ticks, DateTimeKind.Utc );
                    case DataField.DATE:
                        return (DateTime)field.ObjectValue;
                    default:
                        break;
                }
            } else {
                throw new ArgumentNullException( "field value is null" );
            }

            throw new FormatException( "Type conversion not possible" );
        }


        # endregion


    }
}
