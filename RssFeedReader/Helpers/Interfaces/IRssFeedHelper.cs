using RssFeedReader.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssFeedReader.Helpers.Interfaces;

public interface IRssFeedHelper
{
    Task<List<Item>?> GetPosts();
}
