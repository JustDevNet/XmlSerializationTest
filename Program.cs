using Dapper;
using Microsoft.Data.SqlClient;
using Projekty;
using System.Xml;
using System.Xml.Serialization;

void ReadWholeXml()
{
	using var context = new MyContext();
	var result = context.Docs.Select(x => x.Document).FirstOrDefault(x => x != null);

	if (result is not null)
	{
		var serializer = new XmlSerializer(typeof(OrderedItem));
		using StringReader reader = new(result);
		object obj = (OrderedItem)serializer.Deserialize(reader);
	}
}

void ReadTenItems()
{
	string query = @"
            DECLARE @OrderXML XML;
            SET @OrderXML = (SELECT TOP 1 Document FROM Docs WHERE Document is not null); 
            
            ;WITH XMLNAMESPACES('http://www.cpandl.com' as inventory, 'http://www.cohowinery.com' as money)
            SELECT @OrderXML.query('
                for $item in /OrderedItem/InnerItem
                [position() > 1 and position() <=10]
                return $item
            ') AS Result";

	using var connection = new SqlConnection(MyContext.ConnectionString);
	var result = connection.Query<string>(query).FirstOrDefault();
	var serializer = new XmlSerializer(typeof(OrderedItem));
	using StringReader reader = new StringReader($"<OrderedItem xmlns:inventory=\"http://www.cpandl.com\" xmlns:money=\"http://www.cohowinery.com\">{result}</OrderedItem>");
	object obj = (OrderedItem)serializer.Deserialize(reader);
}

void AddNewXml()
{
	var serializer = new XmlSerializer(typeof(OrderedItem));
	OrderedItem orders = new();
	using (Stream stream = new FileStream("./test.xml", FileMode.Open))
	{
		orders = (OrderedItem)serializer.Deserialize(stream);
	}
	var xmlString = "";
	using (var writer = new StringWriter())
	{
		serializer.Serialize(writer, orders);
		xmlString = writer.ToString();
	}
	using var context = new MyContext();
	context.Docs.Add(new Doc
	{
		Id = Guid.NewGuid(),
		Document = xmlString,
	});
	context.SaveChanges();
}

void AddPartial()
{
	var obj = new InnerItem()
	{
		ItemName = "Custom name",
		Description = "Some description",
		UnitPrice = 2,
		Quantity = 10,
		LineTotal = 20
	};

	var serializer = new XmlSerializer(typeof(InnerItem));
	var ns = new XmlSerializerNamespaces();
	ns.Add("", "");
	var settings = new XmlWriterSettings
	{
		OmitXmlDeclaration = true,
		Indent = true,
	};
	using var writer = new StringWriter();
	using var xmlWriter = XmlWriter.Create(writer, settings);
	serializer.Serialize(xmlWriter, obj, ns);
	string xmlString = writer.ToString();
	using var context = new MyContext();
	context.Docs.Add(new Doc
	{
		Id = Guid.NewGuid(),
		Part = xmlString,
	});
	context.SaveChanges();
}

void ReadPartial()
{
	using var context = new MyContext();
	var xmlString = context.Docs.Select(x => x.Part).FirstOrDefault(x => x != null);
	InnerItem item;
	using var stringReader = new StringReader(xmlString);
	var serializer = new XmlSerializer(typeof(InnerItem));
	item = (InnerItem)serializer.Deserialize(stringReader);
}

