/* ========================================================================
* MIT License
*
* Copyright (c) 2021 Weland Solutions AB
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*
* ======================================================================*/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using Weland.Cs.Api.Sample.ExportOrderServer.Models;

namespace Weland.Cs.Api.Sample.ExportOrderServer.Controllers
{
    /// <summary>
    /// This controller implements a sample Compact Store Export API server
    /// </summary>
    [ApiController]
    [Route("api/export/sample")]
    public class ExportOrdersController : ControllerBase
    {
        private static readonly IList<ExportOrders> Orders = new List<ExportOrders>();

        /// <summary>
        /// This method shows a sample implementation of the export order post method required by the Compact Store Export API.
        /// 
        /// Minimum requirement for this method is:
        /// - HTTP method POST
        /// - Content-Type = application/json
        /// - HTTP response code = 2xx (for successful execution)
        /// 
        /// To report if any of the orders included failed to be exported the multi status response (207) can be used. You can 
        /// report only the failing orders in the multi status response body or you can report all orders with status = 2xx for 
        /// the ones that succeeded
        /// 
        /// Compact Store Export API can be configured to supply additional (customized) headers in the request. In this example we validate 
        /// that the header X-API-KEY is present and has a particular value. This is not mandatory.
        /// 
        /// </summary>
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(MultiStatusResponse), StatusCodes.Status207MultiStatus)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Post([ApiKeyHeader] string apiKey, [FromBody] ExportOrders exportOrders)
        {
            if (!IsAuthenticated(apiKey)) return Unauthorized();

            if (!TrySaveOrders(exportOrders, out var multiStatusResponse))
            {
                return StatusCode((int)HttpStatusCode.MultiStatus, multiStatusResponse);
            }

            return NoContent();
        }

        /// <summary>
        /// This method is implementing an interface where the user can get the result of previous exports. The interface is 
        /// added for test purpose and is not required by the Compact Store Export API
        /// </summary>
        [HttpGet]
        public ActionResult<IList<ExportOrders>> Get()
        {
            return Ok(Orders);
        }

        private static bool IsAuthenticated(string providedApiKey)
        {
            if (string.IsNullOrWhiteSpace(providedApiKey))
            {
                return false;
            }

            return providedApiKey == ApiKeyHeaderAttribute.ApiKeyHeaderValue;
        }
        private static bool TrySaveOrders(ExportOrders exportOrders, out MultiStatusResponse multiStatusResponse)
        {
            var storedOrders = new ExportOrders();
            multiStatusResponse = new MultiStatusResponse();

            foreach (var order in exportOrders.data)
            {
                if (IsValid(order))
                {
                    storedOrders.data.Add(order);
                }
                else
                {
                    multiStatusResponse.results.Add(new MultiStatusDetails
                    {
                        id = order.id,
                        status = 400
                    });
                }
            }
            Orders.Add(storedOrders);

            return multiStatusResponse.results.Count == 0;
        }
        private static bool IsValid(ExportOrders.ExportOrder order)
        {
            return order.attributes is {assignmentTypeId: 2};
        }

        public class ApiKeyHeaderAttribute : FromHeaderAttribute
        {
            private const string ApiKeyHeaderName = "X-API-KEY";
            internal const string ApiKeyHeaderValue = "MySecretKey";

            public ApiKeyHeaderAttribute()
            {
                Name = ApiKeyHeaderName;
            }
        }

    }
}
