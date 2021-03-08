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
using System.ComponentModel.DataAnnotations;

namespace Weland.Cs.Api.Sample.ExportOrderServer.Models
{
#pragma warning disable IDE1006 // Naming Styles due to JSON:API
    public class ExportOrders
    {
        [Required]
        public Meta meta { get; set; }
        [Required]
        public IList<ExportOrder> data { get; set; } = new List<ExportOrder>();

        public class Meta
        {
            /// <value>The body content version</value>
            [Required]
            public string version { get; set; }
        }

        public class ExportOrder
        {
            /// <value>Compact Store Export API will send 'csExportOrders'</value>
            [Required]
            public string type { get; set; }
            [Required]
            [MaxLength(80)]
            /// <value>Unique ID for this order</value>
            public string id { get; set; }
            [Required]
            public Attributes attributes { get; set; }
        }
        public class Attributes
        {

            public short actionCode { get; set; }
            public double actualQuantity { get; set; }
            public short assignmentTypeId { get; set; }
            public string companyId { get; set; }
            public string createdTime { get; set; }
            public string customerValue1 { get; set; }
            public string customerValue2 { get; set; }
            public string customerValue3 { get; set; }
            public string customerValue4 { get; set; }
            public string customerValue5 { get; set; }
            public string customerValue6 { get; set; }
            public string customerValue7 { get; set; }
            public string fromStorePosition { get; set; }
            public string itemNo { get; set; }
            public string orderInfo { get; set; }
            public string orderlineNo { get; set; }
            public string orderNo { get; set; }
            public bool partialDelivery { get; set; }
            public short quantityTypeId { get; set; }
            public string reasonCode { get; set; }
            public double requestedQuantity { get; set; }
            public string salesOrderNo { get; set; }
            public string salesOrderlineNo { get; set; }
            public string toStorePosition { get; set; }
            public short transType { get; set; }
        }
    }
#pragma warning restore IDE1006 // Naming Styles due to JSON:API
}
