using RssFeedReader.Helpers.Interfaces;
using RssFeedReader.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssFeedReader.Helpers;

public class FakeRssFeedHelper : IRssFeedHelper
{
    public async Task<List<Item>?> GetPosts()
    {
        List<Item>? items = new()
        {
            new Item() { Title = "Um titulo para um feed", PubDate = "27/12/2000 18:00:50 GMT" },
            new Item() { Title = "", PubDate = "25/11/2000 18:00:50 GMT" }
        };

        return items;
    }
}
