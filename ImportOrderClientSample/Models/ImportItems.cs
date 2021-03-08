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
using System.Text.Json;

namespace Weland.Cs.Api.Sample.ImportClient.Models
{
#pragma warning disable IDE1006 // Naming Styles due to JSON:API
    public class ImportItems : Imports
    {
        [Required]
        public Meta meta { get; set; } = new Meta();
        [Required]
        public IList<ImportItem> data { get; set; } = new List<ImportItem>();

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
            public const string validVersion = "1";

            [Required]
            public string version { get; set; }
        }

        public class ImportItem
        {
            [Required]
            public string type { get; private set; } = "csImportItems";
            [Required]
            [MaxLength(80)]
            public string id { get; set; }
            [Required]
            public ItemAttributes attributes { get; set; } = new ItemAttributes();
        }
        public class ItemAttributes
        {
            [MaxLength(200)]
            public string altItemDescription { get; set; }
            public string comment { get; set; }
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
            [Range(0, 9999999999999999999)]
            public float grossWeight { get; set; }
            [Required]
            [MaxLength(200)]
            public string itemDescription { get; set; }
            [MaxLength(50)]
            public string itemGroupDescription { get; set; }
            [Required]
            [MaxLength(50)]
            public string itemNo { get; set; }
            [Range(0, 9999999999999999999)]
            public float netWeight { get; set; }
            [MaxLength(50)]
            public string serialNo { get; set; }
            [MaxLength(50)]
            public string supplierItemNo { get; set; }
        }
    }
#pragma warning restore IDE1006 // Naming Styles due to JSON:API
}