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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Weland.Cs.Api.Sample.ImportClient.Models;

namespace Weland.Cs.Api.Sample.ImportClient.Services
{
    public class ImportService
    {
        private const string ContentType = "application/json";
        private const string CsApiBaseUri = "https://localhost:44302/";
        private const string CsApiKey = "myApiKey";
        private const string CsApiImportOrdersRoot = "api/importOrders";
        private const string CsApiImportItemsRoot = "api/importItems";

        private readonly HttpClient client = new HttpClient();

        public ImportService()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-API-KEY", CsApiKey);

        }
        public async Task<ResponseMessage> ImportItemsAsync(IList<ItemDataTable.Item> items)
        {
            var importItems = new ImportItems();
            importItems.meta.version = "1";

            foreach (var item in items)
            {
                var importItem = new ImportItems.ImportItem
                {
                    id = Guid.NewGuid().ToString(),
                };
                importItem.attributes.itemNo = item.ItemNo;
                importItem.attributes.itemDescription = item.ItemDescription;
                importItems.data.Add(importItem);
            }
            return await ImportItemsAsync(importItems);
        }

        public async Task<ResponseMessage> ImportOrdersAsync(IList<OrderDataTable.Order> orders)
        {
            var importOrders = new ImportOrders();
            importOrders.meta.version = "1";

            foreach (var order in orders)
            {
                if (order.Status.Equals("New"))
                {
                    var importOrder = new ImportOrders.ImportOrder
                    {
                        id = Guid.NewGuid().ToString(),
                    };

                    importOrder.attributes.assignmentTypeId = order.AssignmentType;

                    importOrder.attributes.orderNo = order.OrderNo;
                    importOrder.attributes.orderlineNo = order.OrderLineNo;
                    importOrder.attributes.itemNo = order.ItemNo;
                    importOrder.attributes.requestedQuantity = order.Quantity;
                    importOrders.data.Add(importOrder);
                }
            }
            return await ImportOrdersAsync(importOrders);
        }

        private async Task<ResponseMessage> ImportItemsAsync(ImportItems items)
        {
            try
            {
                var relativePath = CsApiImportItemsRoot;
                return await SendRequestAsync(relativePath, items);
            }
            catch (Exception e)
            {
                return new ResponseMessage { StatusCode = 500, Message = $"Item import failed. Could not create URL. Message {e.Message}." };
            }
        }
        private async Task<ResponseMessage> ImportOrdersAsync(ImportOrders orders)
        {
            try
            {
                var relativePath = CsApiImportOrdersRoot;
                return await SendRequestAsync(relativePath, orders);
            }
            catch (Exception e)
            {
                return new ResponseMessage { StatusCode = 500, Message = $"Order import failed. Could not create URL. Message {e.Message}." };
            }
        }
        private async Task<ResponseMessage> SendRequestAsync(string relativePath, Imports imports)
        {
            try
            {
                var importsPayload = imports.Serialize();
                var uri = CreateUri(relativePath);
                var data = new StringContent(importsPayload, Encoding.UTF8, ContentType);
                var response = await client.PostAsync(uri, data);

                if (response.IsSuccessStatusCode)
                {
                    return new ResponseMessage { StatusCode = (int)response.StatusCode, Message = $"Imported {imports.Count()} items successfully." };
                }
                else
                {
                    return new ResponseMessage { StatusCode = (int)response.StatusCode, Message = $"Import failed with status code {response.StatusCode}." };
                }
            }
            catch (Exception e)
            {
                return new ResponseMessage { StatusCode = 500, Message = $"Import failed with message {e.Message}." };
            }
        }
        private Uri CreateUri(string relativePath)
        {
            var baseUri = CsApiBaseUri;
            if (!baseUri.EndsWith('/'))
            {
                baseUri += '/';
            }
            return new Uri(baseUri + relativePath);
        }
    }
}
