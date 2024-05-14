namespace Projekty;
using System.Collections.Generic;
using System.Xml.Serialization;

public class OrderedItem
{
	[XmlElement(ElementName = "InnerItem")]
	public List<InnerItem> Items { get; set; }
}

public class InnerItem
{
	[XmlElement(Namespace = "http://www.cpandl.com")]
	public string ItemName;
	[XmlElement(Namespace = "http://www.cpandl.com")]
	public string Description;
	[XmlElement(Namespace = "http://www.cohowinery.com")]
	public decimal UnitPrice;
	[XmlElement(Namespace = "http://www.cpandl.com")]
	public int Quantity;
	[XmlElement(Namespace = "http://www.cohowinery.com")]
	public decimal LineTotal;
}