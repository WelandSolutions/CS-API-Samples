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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Weland.Cs.Api.Sample.ImportClient.Models
{
#pragma warning disable IDE1006 // Naming Styles due to JSON:API
    public class ImportOrders : Imports
    {
        [Required]
        public Meta meta { get; set; } = new Meta();
        [Required]
        public IList<ImportOrder> data { get; set; } = new List<ImportOrder>();

        public int Count()
        {
            return data.Count;
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public class Meta
        {
            [Required]
            public string version { get; set; }
        }

        public class ImportOrder
        {
            [Required]
            public string type { get; private set; } = "csImportOrders";
            [Required]
            [MaxLength(80)]
            public string id { get; set; }
            [Required]
            public Attributes attributes { get; set; } = new Attributes();
        }
        public class Attributes
        {
            [MaxLength(50)]
            public string addressInfo1 { get; set; }
            [MaxLength(50)]
            public string addressInfo2 { get; set; }
            [MaxLength(200)]
            public string altItemDescription { get; set; }
            [MaxLength(50)]
            public string area { get; set; }
            [Required]
            [Range(1, 3)]
            public short assignmentTypeId { get; set; }
            [MaxLength(50)]
            public string batchNo { get; set; }
            [MaxLength(500)]
            public string comment { get; set; }
            [MaxLength(50)]
            public string companyId { get; set; }
            [MaxLength(10)]
            public string countryCode { get; set; }
            [MaxLength(50)]
            public string country { get; set; }
            [MaxLength(50)]
            public string countryOfOrigin { get; set; }
            [MaxLength(30)]
            public string customerField1 { get; set; }
            [MaxLength(30)]
            public string customerField2 { get; set; }
            [MaxLength(30)]
            public string customerField3 { get; set; }
            [MaxLength(30)]
            public string customerField4 { get; set; }
            [MaxLength(30)]
            public string customerField5 { get; set; }
            [MaxLength(30)]
            public string customerField6 { get; set; }
            [MaxLength(30)]
            public string customerField7 { get; set; }
            [MaxLength(10)]
            public string deliveryDay { get; set; }
            [MaxLength(70)]
            public string dangerousGoods { get; set; }
            [MaxLength(50)]
            public string deliveryName { get; set; }
            [MaxLength(50)]
            public string deliveryTerms { get; set; }
            [MaxLength(8)]
            public string deliveryTime { get; set; }
            [MaxLength(50)]
            public string deliveryWay { get; set; }
            [Range(-32768, 32767)]
            public short frequencyId { get; set; }
            [MaxLength(50)]
            public string goodsMark { get; set; }
            [Range(0, 9999999999999999999)]
            public float grossWeight { get; set; }
            [MaxLength(200)]
            public string itemDescription { get; set; }
            [MaxLength(50)]
            public string itemGroupDescription { get; set; }
            [MaxLength(50)]
            public string itemNo { get; set; }
            [MaxLength(50)]
            public string itemNoPallet { get; set; }
            [MaxLength(50)]
            public string itemNoPrimaryPackage { get; set; }
            [MaxLength(50)]
            public string itemNoSecondaryPackage { get; set; }
            [MaxLength(50)]
            public string itemNoTertiaryPackage { get; set; }
            [DefaultValue(false)]
            public bool mergeAllowed { get; set; } = false;
            [Range(0, 9999999999999999999)]
            public float netWeight { get; set; }
            [MaxLength(500)]
            public string orderComment { get; set; }
            [MaxLength(50)]
            public string orderInfo { get; set; }
            [MaxLength(30)]
            public string orderlineNo { get; set; }
            [MaxLength(20)]
            public string orderlineType { get; set; }
            [MaxLength(50)]
            public string orderNo { get; set; }
            [Range(-2147483648, 2147483647)]
            public int orderpoint { get; set; }
            [MaxLength(30)]
            public string owner { get; set; }
            [MaxLength(50)]
            public string ownerName { get; set; }
            [Range(0, 9999999999999999999)]
            public float package { get; set; }
            [Range(0, 9999999999999999999)]
            public float pallet { get; set; }
            [MaxLength(20)]
            public string phoneNo { get; set; }
            [MaxLength(10)]
            public string pickUnit { get; set; }
            [Range(-2147483648, 2147483647)]
            public int priority { get; set; }
            [Range(0, 9999999999999999999)]
            public float quantityPallet { get; set; }
            [Range(0, 9999999999999999999)]
            public float quantityPrimaryPackage { get; set; }
            [Range(0, 9999999999999999999)]
            public float quantitySecondaryPackage { get; set; }
            [Range(0, 9999999999999999999)]
            public float quantityTertiaryPackage { get; set; }
            [MaxLength(30)]
            public string receiver { get; set; }
            [MaxLength(50)]
            public string receiverName { get; set; }
            [MaxLength(50)]
            public string receiverGroupDescription { get; set; }
            [DefaultValue(false)]
            public bool receiverMergeAllowed { get; set; } = false;
            [Range(0, 9999999999999999999)]
            public float requestedQuantity { get; set; }
            [MaxLength(30)]
            public string salesOrderNo { get; set; }
            [MaxLength(30)]
            public string salesOrderlineNo { get; set; }
            [MaxLength(50)]
            public string serialNo { get; set; }
            [MaxLength(50)]
            public string shippingAgent { get; set; }
            [DefaultValue(false)]
            public bool startDirectly { get; set; } = false;
            [MaxLength(30)]
            public string storePosition { get; set; }
            [MaxLength(50)]
            public string supplierItemNo { get; set; }
            [MaxLength(50)]
            public string town { get; set; }
            [MaxLength(50)]
            public string zipCode { get; set; }

        }
    }
#pragma warning restore IDE1006 // Naming Styles due to JSON:API
}
