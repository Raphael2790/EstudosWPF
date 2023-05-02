using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace RssFeedReader.Model;

[XmlRoot(ElementName = "item")]
public class Item
{
    [XmlElement(ElementName = "title")]
    public string Title { get; set; }
    [XmlElement(ElementName = "description")]
    public string Description { get; set; }
    [XmlElement(ElementName = "link")]
    public string Link { get; set; }

    [XmlElement(ElementName = "pubDate")]
    private string pubDate;

    public string PubDate 
    {
        get => pubDate;
        set
        {
            pubDate = value;
            PublishedDate = DateTime.ParseExact(pubDate, "ddd, dd MMM yyyy HH:mm:ss GMT", CultureInfo.InvariantCulture);
        } 
    }

    public DateTime PublishedDate { get; set; }
    [XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
    public string Creator { get; set; }
}

[XmlRoot(ElementName = "channel")]
public class Channel
{
    [XmlElement(ElementName = "item")]
    public List<Item> Item { get; set; }
    [XmlElement(ElementName = "link")]
    public string Link { get; set; }
}

[XmlRoot(ElementName = "rss")]
public class FinZenBlog
{
    [XmlElement(ElementName = "channel")]
    public Channel Channel { get; set; }
}
