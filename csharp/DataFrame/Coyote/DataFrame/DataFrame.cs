#region license
//
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
//
// Contact Information
//   https://code.google.com/p/linker/
//
#endregion
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace Coyote.DataFrame
{
    
    /// <summary>
    /// A hierarchical unit of data.
    /// </summary>
    /// <remarks>
    /// <para>DataFrame models a unit of data that can be exchanged over a variety of transports for a variety communications needs.</para>
    /// <para>This is a surprisingly efficient transmission scheme as all field values and child frames are stored in their wire format as byte arrays. They are then marshaled only when accessed and are ready for transmission.</para>
    /// <para>The DataFrame class was conceived to implement the Data Transfer Object (DTO) design pattern in distributed applications. Passing a DataFrame as both argument and return values in remote service calls. Using this implementation of a DTO allows for more efficient transfer of data between distributed components, reducing latency, improving throughput and decoupling not only the components of the system, but moving business logic out of the data model.</para>
    /// <para>More recently, this class ha proven to be an effective implementation of the Value Object pattern and has made representing database rows and objects relatively easy to code. It has several features which make this class more feature rich than implementing VOs with Maps or other map-based structures such as properties. Most of the recent upgrades have been directly related to VO implementations.</para>
    /// </remarks>
    public class DataFrame : ICloneable
    {

        #region Attributes

        /// The array of fields this frame holds 
        protected internal List<DataField> fields = new List<DataField>();

        /// Flag indicating the top-level elements of this frame has been changed. 
        protected internal volatile bool modified = false;

        #endregion


        #region Properties

        /// <summary>
        /// Flag indicating this frame was modified since being constructed.
        /// </summary>
        public bool IsModified
        {
            get
            {
                return modified;
            }
        }


        /// <summary>Generate a digest fingerprint for this DataFrame based soley on the wire format of this and all fields contained therein.</summary>
        /// <remarks>
        /// <para>This performs a SHA-1 digest on the payload to help determine a unique identifier for the DataFrame. Note: the digest can be used to help determine equivalance between DataFrames.</para>
        /// </remarks>
        /// <returns>A digest fingerprint of the payload.</returns>
        public byte[] Digest
        {
            get
            {
                return new SHA1CryptoServiceProvider().ComputeHash(this.Bytes);
            }
        }


        /// <summary>
        /// Return the wire format of this frame.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                lock (this)
                {
                    MemoryStream ms = new MemoryStream();
                    BinaryWriter bw = new BinaryWriter(ms);
                    try
                    {
                        for (int i = 0; i < fields.Count; i++)
                        {
                            bw.Write((byte[])((DataField)fields[i]).Bytes);
                        }
                    }
                    catch (IOException)
                    {
                        //Log.Error( e.StackTrace );
                    }

                    return ms.ToArray();
                }
            }
        }


        /// <summary>
        /// Generate a digest fingerprint for this message based solely on the wire format of the payload and returns it as a Hex string.
        /// </summary>
        /// <remarks>
        /// <para>NOTE: This is a very expensive function and should not be used without due consideration.</para>
        /// </remarks>
        public virtual string DigestString
        {
            get
            {
                return ByteUtil.BytesToHex(Digest);
            }
        }


        /// <summary>
        /// Access the number of types supported
        /// </summary>
        public virtual int TypeCount
        {
            get
            {
                return DataField.TypeCount;
            }
        }
        

        /// <summary>
        /// Flag indicating if the frame contains no fields.
        /// </summary>
        public virtual bool IsEmpty
        {
            get
            {
                return fields.Count == 0;
            }
        }
            
        
        /// <summary>
        /// Returns true if all the children are un-named and therefore is considered to be an array.
        /// </summary>
        public virtual bool IsArray
        {
            get
            {
                bool retval = true;
                foreach (DataField field in fields)
                {
                    if (field.name != null)
                        return false;
                }
                return retval;
            }
        }

        /// <summary>
        /// Convenience method to return the number of fields in this frame to make code a little more readable.
        /// </summary>
        public virtual int Size { get { return Field.Count; } }


        #endregion


        #region Indexed Properties

        /// <summary>The collection of fields in this frame.</summary>
        public FieldCollection Field;

        /// <summary>Type allowing the frame to be viewed like an array of fields.</summary>
        public class FieldCollection
        {
            readonly DataFrame frame;  // The containing object

            internal FieldCollection(DataFrame f)
            {
                frame = f;
            }


            /// <summary>Indexer to get and set field values.</summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public DataField this[int index]
            {
                get
                {
                    lock (frame)
                    {
                        if (index >= 0 && index < frame.fields.Count)
                        {
                            return frame.fields[index];
                        }
                        else
                        {
                            throw new IndexOutOfRangeException();
                        }
                    }
                }
                set
                {
                    lock (frame)
                    {
                        if (index >= 0 && index < frame.fields.Count)
                        {
                            frame.fields[index] = value;
                        }
                        else
                        {
                            throw new IndexOutOfRangeException();
                        }
                    }
                }
            }


            /// <summary>Indexer to get and set field values by field name.</summary>
            /// <param name="fname"></param>
            /// <returns></returns>
            public DataField this[string fname]
            {
                get
                {
                    lock (frame)
                    {
                        foreach (DataField field in frame.fields)
                        {
                            if (field.Name.Equals(fname))
                                return field;
                        }
                    }
                    return null; //cfg.fields....
                }
                set
                {
                    lock (frame)
                    {
                        foreach (DataField field in frame.fields)
                        {
                            if (field.Name.Equals(fname))
                            {
                                field.Type = value.Type;
                                Array.Copy(value.Value, field.Value, value.Value.Length);
                            }
                        }
                        frame.fields.Add((DataField)value.Clone());
                    }
                }
            }


            /// <summary>Get the count of fields in the frame.</summary>
            public int Count
            {
                get
                {
                    lock (frame)
                    {
                        return frame.fields.Count;
                    }
                }
            }


            /// <summary>Check to see if the give name is a name of one of the fields.</summary>
            /// <remarks><para>This is a linear search across all the field names returning when the first match is encountered.</para></remarks>
            /// <param name="name">The name to check</param>
            /// <returns>True if there is a field which exactly matches the given name, false otherwise.</returns>
            public bool Contains(string name)
            {
                lock (frame)
                {
                    foreach (DataField field in frame.fields)
                    {
                        if (field.Name.Equals(name))
                            return true;
                    }
                    return false;
                }
            }


            /// <summary>Enumerate over all the fields in this frame; required by foreach loops.</summary>
            /// <returns>An enumerator over all the fields in this frame.</returns>
            public IEnumerator<DataField> GetEnumerator()
            {
                return frame.fields.GetEnumerator();
            }

        }

        #endregion


        #region Constructors

        /// <summary>
        /// Empty constructor for Clone operations
        /// </summary>
        static DataFrame()
        {
        }




        /// <summary>
        /// Construct an empty frame.
        /// </summary>
        public DataFrame()
        {
            Field = new FieldCollection(this);
        }




        /// <summary>
        /// Convenience constructor for a frame wrapping the given field.
        /// </summary>
        /// <param name="field">the field to place in this frame </param>
        public DataFrame(DataField field) : this()
        {
            fields.Add(field);
        }




        /// <summary>
        /// Construct a frame simultaneously populating a data field.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public DataFrame(string name, object value)
            : this()
        {
            Add(name, value);
            modified = false;
        }




        /// <summary>
        /// Construct the frame with the given bytes.
        /// </summary>
        /// <param name="data">The byte array from which to construct the frame.</param>
        public DataFrame(byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                int loc = 0;
                int ploc = 0;
                using (MemoryStream stream = new MemoryStream(data))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            ploc = loc;
                            loc = (int)reader.BaseStream.Position;
                            try
                            {
                                Add(new DataField(reader));
                            }
                            catch (EndOfStreamException eof)
                            {
                                throw new DecodeException("Data underflow adding field", eof, loc, ploc, (fields.Count + 1), (fields.Count > 0) ? fields[fields.Count - 1] : null);
                            }
                            catch (IOException ioe)
                            {
                                throw new DecodeException("Problems decoding field", ioe, loc, ploc, (fields.Count + 1), (fields.Count > 0) ? fields[fields.Count - 1] : null);
                            }
                            catch (DecodeException de)
                            {
                                throw new DecodeException("DF:" + de.Message, de.InnerException, loc, ploc, de.FieldIndex, de.LastField);
                            }
                        }
                    }
                }
            }
        }




        /// <summary>Construct a frame copying the fields from the given frame. Since this is a constructor, the modified flag remains unset.</summary>
        /// <param name="source">The frame from which data is copied.</param>
        public DataFrame(DataFrame source)
            : this()
        {
            Copy(source);
            modified = false;
        }

        #endregion


        #region Methods

        /// <summary>Deep copy all the fields of the given frame into this frame. The result includes setting the modified flag of this frame to true.</summary>
        /// <param name="source">The frame from which data is copied.</param>
        protected void Copy(DataFrame source)
        {
            for (int i = 0; i < source.fields.Count; i++)
            {
                fields.Insert(i, (DataField)((DataField)source.fields[i]).Clone());
            }
        }




        /// <summary>Add the given name-value pair to the frame.</summary>
        /// <param name="name">Name of the field to add; if null, the value will only be accessible by its returned index.</param>
        /// <param name="value">The value to place in the frame.</param>
        /// <returns>The index of the placed value which can be used to later retrieve the value.</returns>
        public int Add(string name, object value)
        {
            modified = true;

            if (value is DataField)
                fields.Add((DataField)value);
            else
                fields.Add(new DataField(name, value));

            return fields.Count - 1;
        }




        /// <summary>Place the (anonymous)object in the frame without a name.</summary>
        /// <param name="obj">Value of the field.</param>
        /// <returns>The index in the list of message fields this value was placed.</returns>
        public int Add(object obj)
        {
            modified = true;

            if (obj is DataField)
            {
                fields.Add((DataField)obj);
            }
            else
            {
                fields.Add(new DataField(obj));
            }

            return fields.Count - 1;
        }




        /// <summary>Place the object in the DataFrame under the given name, overwriting any existing object with the same name.</summary>
        /// <remarks><para>Note this is different from Add(String,Object) in that this method will eliminate duplicate names.</para></remarks>
        /// <param name="name">Name of the field.</param>
        /// <param name="obj">Value of the field.</param>
        /// <returns></returns>
        public int Put(string name, object obj)
        {
            lock (fields)
            {
                if (name != null)
                {
                    for (int i = 0; i < fields.Count; i++)
                    {
                        DataField field = (DataField)fields[i];

                        if ((field.Name != null) && field.Name.Equals(name))
                        {
                            // This is less than optimal! Why can't we do an in-place substitution/overwrite?
                            fields.Insert(i, new DataField(name, obj));
                            fields.RemoveAt(i + 1);
                            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                            modified = true;
                            return i;
                        }
                    }

                    return Add(name, obj);
                }
                else
                {
                    return Add(obj);
                }
            }
        }




        /// <summary>Remove the first occurence of a DataField with the given name.</summary>
        /// <param name="name">The name of the field to remove.</param>
        public DataField Remove(string name)
        {
            if (name != null)
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    DataField field = (DataField)fields[i];

                    if ((field.Name != null) && field.Name.Equals(name))
                    {
                        fields.RemoveAt(i);
                        modified = true;
                        return field;
                    }
                }
            }

            return null;
        }




        /// <summary>Remove all occurences of DataFields with the given name.</summary>
        /// <param name="name">name of the fields to remove.</param>
        /// <returns>A list of all the fields removed. Will never be null but may be empty if no fields with the given name were not found.</returns>
        public List<DataField> RemoveAll(string name)
        {
            modified = true;
            List<DataField> retval = new List<DataField>();

            if (name != null)
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    DataField field = (DataField)fields[i];

                    if ((field.Name != null) && field.Name.Equals(name))
                    {
                        retval.Add(field);
                        fields.RemoveAt(i);
                    }
                }
            }
            return retval;
        }




        /// <summary>
        /// Remove the first field with the given name and add a new field with the given name and new object value.
        /// </summary>
        /// <remarks>
        /// <para>This is equivalent to calling Remove(name); and then Add(name,obj);.</para>
        /// <para>A check of the given type is performed before the remove method is called to throw an exception before removing any data.</para>
        /// </remarks>
        /// <param name="name">Name of the field to replace and then add.</param>
        /// <param name="obj">The value of the object to set in the new field.</param>
        /// <exception cref="ArgumetException">if the given object is not of a supported type.</exception>"
        public void Replace(string name, object obj)
        {
            // check if the type is supported first
            DataField.GetType(obj);
            Remove(name);
            Add(name, obj);
        }




        /// <summary>
        /// Remove all the fields with the given name and add a single new field with the given name and new object value.
        /// </summary>
        /// <remarks>
        /// <para>This is equivalent to calling RemoveAll(name); and then Add(name,obj);.</para>
        /// <para>A check of the given type is performed before the remove method is called to throw an exception before removing any data.</para>
        /// </remarks>
        /// <param name="name">Name of the field to replace and then add.</param>
        /// <param name="obj">The value of the object to set in the new field.</param>
        /// <exception cref="ArgumetException">if the given object is not of a supported type.</exception>"
        public void ReplaceAll(string name, object obj)
        {
            // check if the type is supported first
            DataField.GetType(obj);
            RemoveAll(name);
            Add(name, obj);
        }




        /// <summary>Create a deep-copy of this DataFrame.</summary>
        /// <returns>A copy of this data frame.</returns>
        public virtual object Clone()
        {
            DataFrame retval = new DataFrame();

            // Clone all the fields
            for (int i = 0; i < fields.Count; i++)
            {
                retval.fields.Insert(i, (DataField)((DataField)fields[i]).Clone());
            }
            retval.modified = false;

            return retval;
        }




        /// <summary>
        /// Remove all the fields from this frame.
        /// </summary>
        /// <remarks>
        /// <para>The frame will be empty after this method is called.</para>
        /// </remarks>
        public virtual void Clear()
        {
            fields.Clear();
        }


        

        /// <summary>
        /// This will return a list of unique field names in this data frame.
        /// </summary>
        /// <remarks>
        /// <para>Note that fields are not required to have names. They can be anonymous and accessed by their index in the frame. Therefore it is possible that some fields will be inaccessible by name and will not be represented in the returned list of names.</para>
        /// </remarks>
        /// <returns>a list of field names in this frame.</returns>
        public virtual List<string> GetNames()
        {
            List<string> retval = new List<string>();

            // get a list of unique field names
            HashSet<string> names = new HashSet<string>();

            for (int i = 0; i < fields.Count; names.Add(fields[i++].Name)) ;

            retval.AddRange(names);

            return retval;
        }




        /// <summary>
        /// This is a very simple string representation of this data frame.
        /// </summary>
        /// <returns>Human readable representation of this frame.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder b = new System.Text.StringBuilder();
            if (fields.Count > 0)
            {
                bool isArray = this.IsArray;
                if (isArray)
                    b.Append("[");
                else
                    b.Append("{");

                foreach (DataField field in fields)
                {
                    if (!isArray)
                    {
                        b.Append('"');
                        b.Append(field.Name);
                        b.Append("\":");
                    }

                    if (field.Type == DataField.UDEF)
                    {
                        b.Append("null");
                    }
                    else if (field.Type == DataField.BOOLEANTYPE)
                    {
                        b.Append(field.StringValue.ToLower());
                    }
                    else if (field.IsNumeric)
                    {
                        b.Append(field.StringValue);
                    }
                    else if (field.Type == DataField.STRING)
                    {
                        b.Append('"');
                        b.Append(field.StringValue);
                        b.Append('"');
                    }
                    else if (field.Type == DataField.DATE)
                    {
                        b.Append('"');
                        b.Append(field.StringValue);
                        b.Append('"');
                    }
                    else if (field.Type != DataField.FRAMETYPE)
                    {
                        if (field.ObjectValue != null)
                        {
                            b.Append('"');
                            b.Append(field.ObjectValue.ToString());
                            b.Append('"');
                        }
                    }
                    else
                    {
                        if (field.IsNull)
                        {
                            b.Append("null");
                        }
                        else
                        {
                            b.Append(field.ObjectValue.ToString());
                        }
                    }

                    b.Append(",");
                }
                b.Remove(b.Length - 1, b.Length);

                if (isArray)
                    b.Append("]");
                else
                    b.Append("}");

            }
            else
            {
                b.Append("{}");
            }
            return b.ToString();
        }

        


        /// <summary>
        /// Places all of the fields in the given frame in this frame, overwriting the values with the same name.
        /// </summary>
        /// <remarks>
        /// <para>This is essentially a Put operation for all the fields in the given frame. Contrast this Populate(DataFrame).</para>
        /// </remarks>
        /// <param name="frame">The frame from which the fields are read.</param>
        public virtual void Merge(DataFrame frame)
        {
            foreach (DataField field in frame.Field)
            {
                this.Put(field.Name, field.ObjectValue);
            }
        }




        /// <summary>
        /// Places all of the fields in the given frame in this frame.
        /// </summary>
        /// <remarks>
        /// <para>Overwriting does not occur. This is a straight addition of fields. It is possible that multiple fields with the same name may exist after this operation. Contrast this to Merge(DataFrame).</para>
        /// <para>This is essentially an Add operation for all the fields in the given frame.</para>
        /// </remarks>
        /// <param name="frame">The frame from which the fields are read.</param>
        public virtual void Populate(DataFrame frame)
        {
            foreach (DataField field in frame.Field)
            {
                Add(field);
            }
        }

        #endregion

    }

}