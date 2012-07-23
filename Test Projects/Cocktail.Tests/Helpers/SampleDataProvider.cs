//====================================================================================================================
// Copyright (c) 2012 IdeaBlade
//====================================================================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================
// USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
// http://cocktail.ideablade.com/licensing
//====================================================================================================================

using System;
using System.ComponentModel.Composition;
using Test.Model;
using IdeaBlade.EntityModel;
using IdeaBlade.Core;

namespace Cocktail.Tests.Helpers
{
    [Export(typeof(ISampleDataProvider<NorthwindIBEntities>))]
    public class SampleDataProvider : ISampleDataProvider<NorthwindIBEntities>
    {
        #region ISampleDataProvider<NorthwindIBEntities> Members

        void ISampleDataProvider<NorthwindIBEntities>.AddSampleData(NorthwindIBEntities manager)
        {
            AddCustomers(manager);
        }

        #endregion

        public static Guid CreateGuid(int a)
        {
            return new Guid(a, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        private static void AddCustomers(NorthwindIBEntities manager)
        {
            var customer1 = new Customer
                                {
                                    CustomerID = CreateGuid(1),
                                    CompanyName = "Company1",
                                    ContactName = "John Doe",
                                    Address = "SomeAddress",
                                    City = "SomeCity",
                                    PostalCode = "11111"
                                };
            manager.AttachEntity(customer1);

            var customer2 = new Customer
                                {
                                    CustomerID = CreateGuid(2),
                                    CompanyName = "Company2",
                                    ContactName = "Jane Doe",
                                    Address = "SomeAddress",
                                    City = "SomeCity",
                                    PostalCode = "11111"
                                };
            manager.AttachEntity(customer2);
        }
    }
}