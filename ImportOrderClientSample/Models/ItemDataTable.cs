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
using System.Windows.Forms;

namespace Weland.Cs.Api.Sample.ImportClient.Models
{
    public class ItemDataTable
    {
        private readonly IList<ItemDataTable.Item> _items = new List<ItemDataTable.Item>();

        public void AddItem(Item item)
        {
            _items.Add(item);
        }
        public object AsDataSource()
        {
            var source = new BindingSource
            {
                DataSource = _items
            };
            return source;
        }
        public IList<Item> GetItems()
        {
            return _items;
        }
        public void UpdateStatus(string oldStatus, string newStatus)
        {
            foreach (var item in _items)
            {
                if (item.Status.Equals(oldStatus))
                {
                    item.Status = newStatus;
                }
            }
        }
        public class Item
        {
            public string Status { get; set; }
            public string ItemNo { get; set; }
            public string ItemDescription { get; set; }

            public bool IsValid(out ValidationResult result)
            {
                result = new ValidationResult();

                if (string.IsNullOrEmpty(ItemNo) ||
                    ItemNo.Length > 50 ||
                    string.IsNullOrEmpty(ItemDescription) ||
                    ItemDescription.Length > 200)
                {
                    result.Message = $"Failed to validate item{Environment.NewLine}" +
                        $"Item no is mandatory with max length 50 characters{Environment.NewLine}" +
                        $"Item description is mandatory with max length 200 characters";

                    return false;
                }
                return true;
            }

        }

    }
}
