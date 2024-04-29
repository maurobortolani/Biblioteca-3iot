SELECT Libri.Id, Libri.Titolo, Libri.DataPub, 
Libri.IdAutore, Autori.NomeAutore, 
Libri.IdEditore, Editori.NomeEditore,
Libri.Prezzo, Libri.Quantita
FROM 
Libri INNER JOIN Autori ON Libri.IdAutore = Autori.Id
INNER JOIN Editori ON Libri.IdEditore = Editori.Id
WHERE Libri.Id = 3
;