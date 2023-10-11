CREATE PROC usp_SearchByCategory(@category NVARCHAR(50))
AS
	SELECT 
		b.[Name],
		b.YearPublished,
		b.Rating,
		c.[Name] AS CategoryName,
		p.[Name] AS PublisherName,
		CONCAT_WS(' ', pr.PlayersMin, 'people') AS MinPlayers,
		CONCAT_WS(' ', pr.PlayersMax, 'people') AS MaxPlayers
	FROM Boardgames AS b
		JOIN Categories AS c ON b.CategoryId = c.Id
		JOIN Publishers AS p ON b.PublisherId = P.Id
		JOIN PlayersRanges AS pr ON b.PlayersRangeId = pr.Id
	WHERE @category = c.[Name]
	ORDER BY p.[Name], b.YearPublished DESC