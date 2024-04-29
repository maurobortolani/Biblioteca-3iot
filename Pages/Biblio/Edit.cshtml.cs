using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Biblioteca.Pages.Biblio
{
    public class EditModel : PageModel
    {
        public string errorMessage = "";
		public Libri libro = new Libri();
		public void OnGet()
        {            
            libro.Id = Convert.ToInt32(Request.Query["id"]);

			try
            {
                using(SqlConnection conn = new SqlConnection(config.connectionString))
                {
                    conn.Open();
                    string sql = $"SELECT Libri.Id, Libri.Titolo, Libri.DataPub, " +
                        $"Libri.IdAutore, Autori.NomeAutore, " +
                        $"Libri.IdEditore, Editori.NomeEditore, " +
                        $"Libri.Prezzo, Libri.Quantita " +
                        $"FROM " +
                        $"Libri INNER JOIN Autori ON Libri.IdAutore = Autori.Id " +
                        $"INNER JOIN Editori ON Libri.IdEditore = Editori.Id " +
                        $"WHERE Libri.Id = {libro.Id};";

                    using(SqlCommand cmd = new SqlCommand(sql,conn))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            libro.Id = reader.GetInt32(0);
                            libro.Titolo = reader.GetString(1);
                            libro.DataPub = reader.GetDateTime(2);
                            libro.IdAutore = reader.GetInt32(3);
                            libro.NomeAutore = reader.GetString(4);
                            libro.IdEditore = reader.GetInt32(5);
                            libro.NomeEditore = reader.GetString(6);
                            libro.Prezzo = reader.GetDecimal(7);
                            libro.Quantita = reader.GetInt32(8);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {

        }
    }
}
