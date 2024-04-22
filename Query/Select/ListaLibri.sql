SELECT Libri.Id, Libri.Titolo, Autori.NomeAutore,
Libri.Prezzo
FROM
Libri INNER JOIN Autori ON
Libri.IdAutore = Autori.Id;