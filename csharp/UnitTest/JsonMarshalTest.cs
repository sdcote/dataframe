﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Coyote.DataFrame;

namespace UnitTest {
    [TestFixture]
    public class JsonMarshalTest {


        [Test]
        public void MarshalJSON() {
            try {
                string json = "{}";
                Debug.WriteLine( json );
                List<DataFrame> results = JSONMarshaler.Marshal( json );
                Assert.True( results.Count == 1 );
                DataFrame frame = results[0];
                Debug.WriteLine( frame );
                Assert.True( frame.Size == 0 );
                Assert.True( frame.Field.Count == 0 );
                try {
                    DataField rslt = frame.Field[0];
                    Assert.Fail( "Should have thrown IndexOutOfRangeException on empty dataframe" );
                } catch ( IndexOutOfRangeException ) { }
                Debug.WriteLine( "----------------------------\r\n" );


                json = "{\"one\" : 1}";
                Debug.WriteLine( json );
                results = JSONMarshaler.Marshal( json );
                Assert.True( results.Count == 1 );
                frame = results[0];
                Debug.WriteLine( frame );
                Assert.True( frame.Size == 1 );
                DataField result = frame.Field[0];
                Debug.WriteLine( "----------------------------\r\n" );

                json = "{\"one\" : 1,\"two\" : 2,\"three\" : 3}";
                Debug.WriteLine( json );
                results = JSONMarshaler.Marshal( json );
                Assert.True( results.Count == 1 );
                frame = results[0];
                Debug.WriteLine( frame );
                Assert.True( frame.Size == 3 );
                result = frame.Field[0];
                Debug.WriteLine( "----------------------------\r\n" );

                json = "{}{}{}";
                Debug.WriteLine( json );
                results = JSONMarshaler.Marshal( json );
                Assert.True( results.Count == 3 );
                frame = results[0];
                Debug.WriteLine( frame );
                Debug.WriteLine( "----------------------------\r\n" );


            } catch ( MarshalException e ) {
                Assert.Fail( e.Message );
            }
        }




        [Test]
        public void MarshalJSONArray() {
            string json = "[]";
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];
            Debug.WriteLine( frame );
            
            json = "[5,3]";
            results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            frame = results[0];
            Debug.WriteLine( frame );
            //assertEquals( "[5]", obj.toString() );

            json = "[5,10,2]";
            results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            frame = results[0];
            Debug.WriteLine( frame );
            //assertEquals( "[5,10,2]", obj.toString() );

            json = "[\"hello\\bworld\\\"abc\\tdef\\\\ghi\\rjkl\\n123\\u4e2d\"]";
            results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            frame = results[0];
            Debug.WriteLine( frame );
            //   assertEquals( "hello\bworld\"abc\tdef\\ghi\rjkl\n123ä¸­", ( (List)obj ).get( 0 ).toString() );

            json = "[5,]"; // non-standard
            results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            frame = results[0];
            Debug.WriteLine( frame );
            //assertEquals( "[5,null]", obj.toString() );

            json = "[5,,2]"; // non-standard
            results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            frame = results[0];
            Debug.WriteLine( frame );
            //assertEquals( "[5,null,2]", obj.toString() );

        }




        [Test]
        public void TestRealJSON() {
            //String json = "[{\"message_stats\":{\"deliver_get\":2,\"deliver_get_details\":{\"rate\":0.0},\"get_no_ack\":2,\"get_no_ack_details\":{\"rate\":0.0},\"publish\":2,\"publish_details\":{\"rate\":0.0}},\"messages\":0,\"messages_details\":{\"rate\":0.0},\"messages_ready\":0,\"messages_ready_details\":{\"rate\":0.0},\"messages_unacknowledged\":0,\"messages_unacknowledged_details\":{\"rate\":0.0},\"name\":\"/\",\"tracing\":false}]";
            String json = "{ \"skills\" : \"\", \"upon_approval\" : \"proceed\", \"location\" : \"\", \"expected_start\" : \"\", \"reopen_count\" : \"0\", \"close_notes\" : \"\", \"impact\" : \"3\", \"urgency\" : \"3\", \"correlation_id\" : \"\", \"sys_tags\" : \"\", \"sys_domain\" : { \"link\" : \"https://nwdevelopment.service-now.com:443/api/now/table/sys_user_group/global\", \"value\" : \"global\" }, \"description\" : \"\", \"group_list\" : \"\", \"priority\" : \"5\", \"sys_mod_count\" : \"0\", \"work_notes_list\" : \"\", \"follow_up\" : \"\", \"closed_at\" : \"\", \"sla_due\" : \"\", \"sys_updated_on\" : \"2015-04-08 21:12:53\", \"parent\" : \"\", \"work_end\" : \"\", \"number\" : \"INC0010032\", \"closed_by\" : \"\", \"work_start\" : \"\", \"calendar_stc\" : \"\", \"business_duration\" : \"\", \"category\" : \"inquiry\", \"incident_state\" : \"1\", \"activity_due\" : \"\", \"correlation_display\" : \"\", \"company\" : \"\", \"active\" : \"true\", \"due_date\" : \"\", \"assignment_group\" : \"\", \"caller_id\" : \"\", \"knowledge\" : \"false\", \"made_sla\" : \"true\", \"comments_and_work_notes\" : \"\", \"parent_incident\" : \"\", \"state\" : \"1\", \"user_input\" : \"\", \"sys_created_on\" : \"2015-04-08 21:12:53\", \"approval_set\" : \"\", \"reassignment_count\" : \"0\", \"rfc\" : \"\", \"child_incidents\" : \"0\", \"opened_at\" : \"2015-04-08 21:12:53\", \"short_description\" : \"Test with java post\", \"order\" : \"\", \"sys_updated_by\" : \"cotes7\", \"resolved_by\" : \"\", \"notify\" : \"1\", \"upon_reject\" : \"cancel\", \"approval_history\" : \"\", \"problem_id\" : \"\", \"work_notes\" : \"\", \"calendar_duration\" : \"\", \"close_code\" : \"\", \"sys_id\" : \"905040750f9f7100085d6509b1050e7d\", \"approval\" : \"not requested\", \"caused_by\" : \"\", \"severity\" : \"3\", \"sys_created_by\" : \"cotes7\", \"assigned_to\" : \"\", \"resolved_at\" : \"\", \"business_stc\" : \"\", \"cmdb_ci\" : \"\", \"opened_by\" : { \"link\" : \"https://nwdevelopment.service-now.com:443/api/now/table/sys_user/a0b8c4490f093500a5eee478b1050ebe\", \"value\" : \"a0b8c4490f093500a5eee478b1050ebe\" }, \"subcategory\" : \"\", \"sys_class_name\" : \"incident\", \"watch_list\" : \"\", \"time_worked\" : \"\", \"contact_type\" : \"phone\", \"escalation\" : \"0\", \"comments\" : \"\" }";

            Debug.WriteLine( json );
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];
            Debug.WriteLine( frame );

            String formatted = JSONMarshaler.ToFormattedString( frame );
            Debug.WriteLine( formatted );
            Debug.WriteLine( "----------------------------\r\n" );

            //Assert.True(frame.Size==1);
            DataField result = frame.Field[0]; // get the JSON data object
        }




        [Test]
        public void MarshalJSONDouble() {
            string json = "{\"double\":123.456}";
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];

            DataField field = frame.Field["double"];
            Assert.NotNull( field );
            Assert.True( field.Type == DataField.DOUBLE );
            Debug.WriteLine( frame );
        }




        [Test]
        public void MarshalJSONBoolean() {
            string json = "{\"boolean\":true}";
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];

            DataField field = frame.Field["boolean"];
            Assert.NotNull( field );
            Assert.True( field.Type == DataField.BOOLEAN );
            Debug.WriteLine( frame );
        }




        [Test]
        public void MarshalJSONLong() {
            string json = "{\"long\":123}";
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];

            DataField field = frame.Field["long"];
            Assert.NotNull( field );
            Assert.True( field.Type == DataField.S64 );
            Debug.WriteLine( frame );
        }




        [Test]
        public void MarshalJSONNegative() {
            string json = "{\"long\":-123}";
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];

            DataField field = frame.Field["long"];
            Assert.NotNull( field );
            Assert.True( field.Type == DataField.S64 );
            Assert.True( ((long)field.ObjectValue) == -123 );
            Debug.WriteLine( frame );
        }



        [Test]
        public void MarshalJSONNest() {
            string json = "[0,{\"1\":{\"2\":{\"3\":{\"4\":[5,{\"6\":7}]}}}}]";
            List<DataFrame> results = JSONMarshaler.Marshal( json );
            Assert.True( results.Count == 1 );
            DataFrame frame = results[0];
            Debug.WriteLine( frame );
            // start traversing into the nested frames to validate proper parsing
        }

        [Test]
        public void MarshalJSONError() {
            string json = "{\"one\":1,\"two\":2,}";
            try {
                List<DataFrame> results = JSONMarshaler.Marshal( json );
                Assert.Fail();
            } catch ( MarshalException e ) {
                Debug.WriteLine( e );
                e.GetBaseException();
                //testc for the position and location of the error
                //Expected attribute name at line:1 column:18 offset:17 char: '}'
            }
        }

    }
}
