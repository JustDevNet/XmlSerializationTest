namespace Projekty;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Doc
{
	[Key]
	public Guid Id { get; set; }

	[Column(TypeName = "xml")]
	public string? Document { get; set; }

	public string? Part { get; set; }
}
