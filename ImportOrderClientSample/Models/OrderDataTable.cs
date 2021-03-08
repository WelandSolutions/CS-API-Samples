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
using System.Data;

namespace Weland.Cs.Api.Sample.ImportClient.Models
{
    public class OrderDataTable
    {
        private readonly IList<OrderDataTable.Order> _orders = new List<OrderDataTable.Order>();

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }
        public object AsDataSource()
        {
            var orderDataTable = new DataTable();
            DataColumn status = new DataColumn("Status", typeof(string));
            DataColumn assignementType = new DataColumn("AssignmentType", typeof(string));
            DataColumn orderNo = new DataColumn("OrderNo", typeof(string));
            DataColumn orderLineNo = new DataColumn("OrderLineNo", typeof(string));
            DataColumn itemNo = new DataColumn("ItemNo", typeof(string));
            DataColumn quantity = new DataColumn("Quantity", typeof(float));
            orderNo.MaxLength = 50;
            orderLineNo.MaxLength = 30;
            itemNo.MaxLength = 50;

            orderDataTable.Columns.Add(status);
            orderDataTable.Columns.Add(assignementType);
            orderDataTable.Columns.Add(orderNo);
            orderDataTable.Columns.Add(orderLineNo);
            orderDataTable.Columns.Add(itemNo);
            orderDataTable.Columns.Add(quantity);

            foreach (var order in _orders)
            {
                orderDataTable.Rows.Add(order.Status, GetAssignmentTypeText(order.AssignmentType), order.OrderNo, order.OrderLineNo, order.ItemNo, order.Quantity);
            }

            return orderDataTable;
        }
        public IList<Order> GetOrders()
        {
            return _orders;
        }
        public void UpdateStatus(string oldStatus, string newStatus)
        {
            foreach (var order in _orders)
            {
                if (order.Status.Equals(oldStatus))
                {
                    order.Status = newStatus;
                }
            }
        }

        public class Order
        {
            public string Status { get; set; }
            public short AssignmentType { get; set; }
            public string OrderNo { get; set; }
            public string OrderLineNo { get; set; }
            public string ItemNo { get; set; }
            public float Quantity { get; set; }
            public bool IsValid(out ValidationResult result)
            {
                result = new ValidationResult();

                if (AssignmentType < 1 ||
                    AssignmentType > 2 ||
                    string.IsNullOrEmpty(OrderNo) ||
                    OrderNo.Length > 50 ||
                    string.IsNullOrEmpty(OrderLineNo) ||
                    OrderLineNo.Length > 30 ||
                    string.IsNullOrEmpty(ItemNo) ||
                    ItemNo.Length > 50)
                {
                    result.Message = $"Failed to validate order{Environment.NewLine}" +
                        $"Order no is mandatory with max length 50 characters{Environment.NewLine}" +
                        $"Order line no is mandatory with max length 30 characters{Environment.NewLine}" +
                        $"Item no is mandatory with max length 50 characters";
                    return false;
                }
                return true;
            }
        }
        public static short GetAssignmentTypeId(string text)
        {
            return text.Equals("Outbound") ? short.Parse("2") : short.Parse("1");
        }
        public static string GetAssignmentTypeText(int type)
        {
            return type == 2 ? "Outbound" : "Inbound";
        }

    }
    public class ValidationResult
    {
        public string Message { get; set; }
    }
}
