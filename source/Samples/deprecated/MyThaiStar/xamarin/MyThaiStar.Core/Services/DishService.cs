using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using MyThaiStar.Core.Business.Dto.DishManagement;
using MyThaiStar.Core.Business.Dto.General;
using MyThaiStar.Core.Configuration;
using MyThaiStar.Core.Domain;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MyThaiStar.Core.Services
{
    public class DishService : ServiceBase<IList<Dish>>
    {
        private HttpClient DefaultHttpClient { get; set; }

        public override async Task<IList<Dish>> SyncDataAsync()
        {

            try
            {
                DefaultHttpClient = GetDefaultClient();
                var url = ApplicationConfig.DishSearchUrl;
                var filter = ((MyThaiStar.Core.FormsApp)Application.Current).GetDishFilter();
                //var opt = new FilterDtoSearchObject{Categories = new CategorySearchDto[0],MinLikes = "", MaxPrice = "",SearchBy = "",  sort = new SortByDto[0]};

                var resultDish = await DefaultHttpClient.PostAsync(new Uri(url), GetStringContentFromObject(filter)).Result.Content.ReadAsStringAsync().ConfigureAwait(false);
                var objtoDeserialize = JsonConvert.DeserializeObject<ResultObjectDto<DishDtoResult>>(resultDish);

                return ConvertToDish(objtoDeserialize.Result);
            }
            catch (Exception ex)
            {
                return new List<Dish>();
            }
        }

        private IList<Dish> ConvertToDish(List<DishDtoResult> dishDtoResult)
        {
            var result = new List<Dish>();

            foreach (var dish in dishDtoResult)
            {
                result.Add(new Dish
                {
                    Categories = dish.categories,
                    Description = dish.dish.description,
                    Extras = dish.extras,
                    IdDish = dish.dish.id,
                    Image = dish.image.content,
                    Name = dish.dish.name,
                    Price = dish.dish.price
                });
            }


            return result;
        }


        private HttpClient GetDefaultClient()
        {
            var handler = new HttpClientHandler { UseCookies = false };
            var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(600) };
            const string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.98 Safari/537.36";
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");

            return client;
        }

        private StringContent GetStringContentFromObject<T>(T objectToSend)
        {
            var content = JsonConvert.SerializeObject(objectToSend);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

    }
}
