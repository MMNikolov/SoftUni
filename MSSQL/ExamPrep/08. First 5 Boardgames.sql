SELECT TOP 5
	b.[Name],
	b.Rating,
	c.[Name] AS CategoryName
FROM Boardgames AS b
LEFT JOIN Categories AS c ON b.CategoryId = c.Id
LEFT JOIN PlayersRanges AS pr ON b.PlayersRangeId = pr.Id
WHERE Rating > 7 AND b.[Name] LIKE '%a%'
OR Rating > 7.50 AND PlayersMin >= 2 AND PlayersMax <= 5
ORDER BY b.[Name], Rating DESC