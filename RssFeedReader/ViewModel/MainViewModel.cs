using RssFeedReader.Helpers;
using RssFeedReader.Helpers.Interfaces;
using RssFeedReader.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RssFeedReader.ViewModel;

public class MainViewModel
{
    private readonly IRssFeedHelper _rssFeedHelper;
    public ObservableCollection<Item> Items { get; set; }

    public MainViewModel(IRssFeedHelper rssFeedHelper)
    {
        Items = new ObservableCollection<Item>();
        _rssFeedHelper = rssFeedHelper;
        LoadItems();
    }

    public void LoadItems()
    {
        var items = _rssFeedHelper.GetPosts().Result;

        Items.Clear();

        if (items is not null)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}
