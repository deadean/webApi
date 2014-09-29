﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using ODataQueryableSample.Models;

namespace ODataQueryableSample.Controllers
{
    /// <summary>
    /// This sample customer controller demonstrates how to create an action which supports
    /// OData style queries using the [EnableQuery] attribute. The difference in this controller
    /// is that we return an HttpResponseMessage instead of <see cref="IEnumerable{T}"/> as is 
    /// the case in the <see cref="CustomersController"/>. This allows up to add extra header
    /// fields, manipulate the status code, etc.
    /// </summary>
    public class ResponseController : ODataController
    {
        private static List<Customer> CustomerList = new List<Customer>
        {  
            new Customer { 
                Id = 11, Name = "Lowest", BirthTime = new DateTime(2001, 1, 1),
                Orders = new List<Order>
                { 
                    new Order { Id = 0 , Quantity = 10 },  
                    new Order { Id = 1 , Quantity = 50 } 
                }
            }, 
            new Customer { 
                Id = 33, Name = "Highest", BirthTime = new DateTime(2002, 2, 2),
                Orders = new List<Order>
                { 
                    new Order { Id = 2 , Quantity = 10 }, 
                    new Order { Id = 3 , Quantity = 5 } 
                }
            }, 
            new Customer { Id = 22, Name = "Middle", BirthTime = new DateTime(2003, 3, 3) }, 
            new Customer { Id = 3, Name = "NewLow", BirthTime = new DateTime(2004, 4, 4) },
        };

        [EnableQuery(AllowedArithmeticOperators=AllowedArithmeticOperators.Add)]
        public HttpResponseMessage Get()
        {
            // Create an HttpResponseMessage and add an HTTP header field
            HttpResponseMessage response = Request.CreateResponse<IEnumerable<Customer>>(HttpStatusCode.OK, CustomerList);
            response.Headers.Add("Sample-Header", "Sample-Value");

            // Return our response. Query composition will still happen on the CustomerList given the Queryable attribute
            return response;
        }

        [HttpDelete]
        [ODataRoute("Response({key})/Orders({relatedKey})/$ref")]
        public HttpResponseMessage DeleteOrdersFromCustomer(int key, int relatedKey)
        {
            var customer = CustomerList.Single(c => c.Id == key);
            var order = customer.Orders.Single(o => o.Id == relatedKey);

            customer.Orders.Remove(order);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            response.Headers.Add("Delete-Ref", "true");

            return response;
        }
    }
}
