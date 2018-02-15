using InvoicePOS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.ViewModels
{
   public class CategoryAutoScr : WpfAutoComplete.ISearchDataProvider
    {
        CategoryModel[] data = null;
        public WpfAutoComplete.SearchResult DoSearch(string searchTerm)
        {
            GetCatagory(1);
            return new WpfAutoComplete.SearchResult
            {
                SearchTerm = searchTerm,
                Results = Category.Where(item => item.Value.ToUpperInvariant().Contains(searchTerm.ToUpperInvariant())).ToDictionary(v => v.Key, v => v.Value)
            };
        }

        public WpfAutoComplete.SearchResult SearchByKey(object Key)
        {
            return new WpfAutoComplete.SearchResult
            {
                SearchTerm = null,
                Results = Category.Where(item => item.Key.ToString() == Key.ToString()).ToDictionary(v => v.Key, v => v.Value)
            };
        }

        //private readonly Dictionary<object, string> dict = new Dictionary<object, string> {
        //    { 1, "The badger knows something"},
        //    { 2, "Your head looks something like a pineapple"},
        //    { 3, "Crazy like a box of green frogs"},
        //    { 4, "The billiard table has green cloth"},
        //    { 5, "The sky is blue"},
        //    { 6, "We're going to need some golf shoes"},
        //    { 7, "This is going straight to the pool room"},
        //    { 8, "We're going to  Bonnie Doon"},
        //    { 9, "Spring forward - Fall back"},
        //    { 10, "Gerry had a plan which involved telling all"},
        //    { 11, "When is the summer coming"},
        //    { 12, "Take you time and tell me what you saw"},
        //    { 13, "All hands on deck"}
        //};
        private readonly Dictionary<object, string> Category = new Dictionary<object, string>();

        public async Task<Dictionary<object, string>> GetCatagory(long comp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(GlobalData.gblApiAdress);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(500000000000);
                HttpResponseMessage response = client.GetAsync("api/CatagoryAPI/GetCatagory?id=" + comp + "").Result;
                if (response.IsSuccessStatusCode)
                {
                    data = JsonConvert.DeserializeObject<CategoryModel[]>(await response.Content.ReadAsStringAsync());
                    int x = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        Category.Add(data[i].CATAGORY_ID, data[i].CATAGORY_NAME);
                            
                       
                    }
                    
                }

                return new Dictionary<object, string>(Category);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
