using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Data.SqlClient;

namespace Biblioteca.Pages.Biblio
{
    public class NewModel : PageModel
    {
        public string errorMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            Libri libro = new Libri();
            libro.Titolo = Request.Form["Titolo"];
            libro.NomeAutore = Request.Form["Autore"];
			libro.NomeEditore = Request.Form["Editore"];
            libro.Prezzo = Convert.ToDecimal(Request.Form["Prezzo"]);
			libro.DataPub = DateTime.ParseExact(Request.Form["DataPub"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            libro.Quantita = Convert.ToInt32(Request.Form["Quantita"]);

			try
			{
				using (SqlConnection conn = new SqlConnection(config.connectionString))
				{
					conn.Open();
					string sql = $"SELECT * FROM Autori WHERE NomeAutore like '%{libro.NomeAutore}%'";

					using (SqlCommand cmd = new SqlCommand(sql, conn))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							reader.Read();
							if (reader.GetInt32(0) != 0)
							{
								// recuperiamo l'id
								libro.IdAutore = reader.GetInt32(0);
							}
							else
							{
								// inserisci il nuovo autore
								string sqlInsert = $"INSERT INTO Autori (NomeAutore) VALUES ({libro.NomeAutore})";
								using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
								{
									cmdInsert.ExecuteNonQuery();
								}
								using(SqlDataReader reader2 = cmd.ExecuteReader()) 
								{
									reader2.Read();
									libro.IdAutore = reader.GetInt32(0);
								}
							}

							// sicuramente possiedo l'id del autore
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			//Response.Redirect("Lista");
		}
    }
}
