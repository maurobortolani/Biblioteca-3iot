using System.Data.Common;

namespace Biblioteca
{
	public class Libri
	{
		public int Id { get; set; }
		public string? Titolo { get; set; }
		public decimal Prezzo { get; set; }
		public int Quantita { get; set; }
		public int IdAutore { get; set; }
		public int IdEditore { get; set; }
		public string? NomeAutore { get; set; }
		public string? NomeEditore { get; set; }
		public DateTime DataPub { get; set; }
	}

	public class config
	{
		public static String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
			"AttachDbFilename=C:\\Users\\bortolanim\\Desktop\\Biblioteca\\biblio.mdf;" +
			"Integrated Security=True;" +
			"Connect Timeout=30";

		
	}
}
