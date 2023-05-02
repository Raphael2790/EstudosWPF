using RssFeedReader.Helpers.Interfaces;
using RssFeedReader.Model;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RssFeedReader.Helpers;

public class RssFeedHelper : IRssFeedHelper
{
    public async Task<List<Item>?> GetPosts()
    {
        List<Item>? items = new();

        XmlSerializer serializer = new(typeof(FinZenBlog));
        using HttpClient client = new();
        string xml = Encoding.UTF8.GetString(await client.GetByteArrayAsync("https://www.finzen.mx/blog-feed.xml"));
        using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
        {
            var blog = serializer.Deserialize(reader) as FinZenBlog;
            items = blog?.Channel?.Item;
        }

        return items;
    }
}
