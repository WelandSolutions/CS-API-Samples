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

using System.Collections.Generic;

namespace Weland.Cs.Api.Sample.ExportOrderServer.Models
{
#pragma warning disable IDE1006 // Naming Styles due to JSON:API
    public class MultiStatusResponse
    {
        public IList<MultiStatusDetails> results { get; set; } = new List<MultiStatusDetails>();
        public const string EmptyAsJson = "{\"results\": []}";
    }

    public class MultiStatusDetails
    {
        /// <value>Order ID</value>
        public string id { get; set; }
        /// <value>HTTP status code</value>
        public int status { get; set; }
    }

#pragma warning restore IDE1006 // Naming Styles due to JSON:API
}
