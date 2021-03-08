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
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Weland.Cs.Api.Sample.ImportClient.Models;
using Weland.Cs.Api.Sample.ImportClient.Services;

namespace Weland.Cs.Api.Sample.ImportClient
{
    public partial class ImportForm : Form
    {
        private readonly ItemDataTable _itemDataTable = new ItemDataTable();
        private readonly OrderDataTable _orderDataTable = new OrderDataTable();
        private readonly ImportService _importService = new ImportService();

        public ImportForm()
        {
            InitializeComponent();
            InitializeOrderDataGrid();
            InitializeItemDataGrid();
        }
        private void InitializeItemDataGrid()
        {
            grdItems.DataSource = _itemDataTable.AsDataSource();
            grdItems.Columns[2].Width = 1500;
        }
        private void InitializeOrderDataGrid()
        {
            cboAssignmentType.SelectedIndex = 0;

            grdOrders.DataSource = _orderDataTable.AsDataSource();
            grdOrders.Columns[4].Width = 500;
        }

        private async void ImportItems_Click(object sender, EventArgs e)
        {
            var newItems = _itemDataTable.GetItems().Where(item => item.Status.Equals("New")).ToList();
            var response = await _importService.ImportItemsAsync(newItems);

            _itemDataTable.UpdateStatus("New", response.IsSuccessStatusCode() ? "Imported" : "Failed");
            grdItems.DataSource = _itemDataTable.AsDataSource();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var item = new ItemDataTable.Item { ItemDescription = txtItemDescription.Text, ItemNo = txtItemNo.Text, Status = "New" };
            if (!item.IsValid(out var validation))
            {
                MessageBox.Show(validation.Message, "Validation Failed");
            }
            else
            {
                _itemDataTable.AddItem(item);
                grdItems.DataSource = _itemDataTable.AsDataSource();
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            var order = new OrderDataTable.Order
            {
                AssignmentType = OrderDataTable.GetAssignmentTypeId(cboAssignmentType.SelectedItem.ToString()),
                ItemNo = txtOrderItemNo.Text,
                OrderLineNo = txtOrderLineNo.Text,
                OrderNo = txtOrderNo.Text,
                Quantity = float.TryParse(txtQuantity.Text, out var floatValue) ? floatValue : 0,
                Status = "New"
            };

            if (!order.IsValid(out var validation))
            {
                MessageBox.Show(validation.Message, "Validation Failed");
            }
            else
            {
                _orderDataTable.AddOrder(order);
                grdOrders.DataSource = _orderDataTable.AsDataSource();
            }
        }

        private async void btnImportOrders_Click(object sender, EventArgs e)
        {
            var newOrders = _orderDataTable.GetOrders().Where(order => order.Status.Equals("New")).ToList();
            var response = await _importService.ImportOrdersAsync(newOrders);
            _orderDataTable.UpdateStatus("New", response.IsSuccessStatusCode() ? "Imported" : "Failed");
            grdOrders.DataSource = _orderDataTable.AsDataSource();
        }
    }

}
