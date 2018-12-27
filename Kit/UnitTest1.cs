using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Kit
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            int Movies = 0, TV = 0;

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/person/239019/movie_credits?api_key=" + Config.APIKey);
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsAsync<ListCast<Cast>>();
                Movies = r.Cast.Where(p => p.Genre_Ids.Any(q => q == 18)).Count();
            }
            else
            {
                Assert.True(false, "Non success Conection");
            }

            response = await client.GetAsync("https://api.themoviedb.org/3/person/239019/tv_credits?api_key="+ Config.APIKey);
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsAsync<ListCast<Cast>>();
                TV = r.Cast.Where(p => p.Genre_Ids.Any(q => q == 18)).Count();
            }
            else
            {
                Assert.True(false, "Non success Conection");
            }

            Assert.Equal(6, Movies + TV);
        }

        [Theory]
        [InlineData(2015, 5)]
        [InlineData(2016, 0)]
        [InlineData(2017, 2)]
        [InlineData(2018, 1)]
        [InlineData(2019, 2)]
        public async Task TestSeason1(int Year, int MovieNumber)
        {
            int Movies = 0;
            int TV = 0;

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.themoviedb.org/3/person/239019/movie_credits?api_key=" + Config.APIKey);
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsAsync<ListCast<Movie>>();
                Movies = r.Cast.Where(p => p.Release_Date.Year == Year).Count();
            }
            else
            {
                Assert.True(false, "Non success Conection");
            }

            response = await client.GetAsync("https://api.themoviedb.org/3/person/239019/tv_credits?api_key=" + Config.APIKey);
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsAsync<ListCast<Tv>>();
                TV = r.Cast.Where(p => p.First_Air_Date.Year == Year).Count();
            }
            else
            {
                Assert.True(false, "Non success Conection");
            }

            Assert.Equal(MovieNumber, Movies + TV);
        }
    }
}