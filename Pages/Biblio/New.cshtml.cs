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

			//cerco o creo l'autore
			try
			{
				using (SqlConnection conn = new SqlConnection(config.connectionString))
				{
					conn.Open();
					string sqlAutore = $"SELECT * FROM Autori WHERE NomeAutore like '%{libro.NomeAutore}%'";

					using (SqlCommand cmd = new SqlCommand(sqlAutore, conn))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{							
							if (reader.Read()) // L'autore esiste
							{
								// recuperiamo l'id
								libro.IdAutore = reader.GetInt32(0);
							}
							else // l'autore non esiste
							{
								conn.Close();
								// inserisci il nuovo autore
								string sqlInsert = $"INSERT INTO Autori (NomeAutore) VALUES ('{libro.NomeAutore}')";
								conn.Open();
								using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
								{
									cmdInsert.ExecuteNonQuery();									
								}
								conn.Close();
								conn.Open();
								using(SqlDataReader reader2 = cmd.ExecuteReader()) 
								{
									reader2.Read();
									libro.IdAutore = reader2.GetInt32(0);
								}
							}
							// sicuramente possiedo l'id del autore
						}
					}
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			// cerco o creo L'editore
			try
			{
				using (SqlConnection conn = new SqlConnection(config.connectionString))
				{
					conn.Open();
					string sqlEditore = $"SELECT * FROM Editori WHERE NomeEditore like '%{libro.NomeEditore}%'";

					using (SqlCommand cmd = new SqlCommand(sqlEditore, conn))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read()) // L'editore esiste
							{
								// recuperiamo l'id
								libro.IdEditore = reader.GetInt32(0);
							}
							else // l'editore non esiste
							{
								conn.Close();
								// inserisci il nuovo autore
								string sqlInsert = $"INSERT INTO Editori (NomeEditore) VALUES ('{libro.NomeEditore}')";
								conn.Open();
								using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn))
								{
									cmdInsert.ExecuteNonQuery();
								}
								conn.Close();
								conn.Open();
								using (SqlDataReader reader2 = cmd.ExecuteReader())
								{
									reader2.Read();
									libro.IdEditore = reader2.GetInt32(0);
								}
							}
							// sicuramente possiedo l'id dell'editore
						}
					}
					conn.Close();
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			//inserisco il libro
			try
			{
				using (SqlConnection conn = new SqlConnection(config.connectionString))
				{
					conn.Open();
					string sqlInsLibro = $"INSERT INTO Libri " + 
						$"(Titolo, IdAutore, IdEditore, Prezzo) " + 
						$"VALUES " + 
						$"('{libro.Titolo}', {libro.IdAutore}, {libro.IdEditore}, {libro.Prezzo})";

					using (SqlCommand cmdInsert = new SqlCommand(sqlInsLibro, conn))
					{
						cmdInsert.ExecuteNonQuery();
					}

					conn.Close();
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			Response.Redirect("Lista");
		}
    }
}
