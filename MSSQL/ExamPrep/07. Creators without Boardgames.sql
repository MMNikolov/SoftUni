SELECT 
	c.Id,
	CONCAT_WS(' ', FirstName, LastName) AS CreatorName,
	c.Email
FROM Creators AS c
LEFT JOIN CreatorsBoardgames AS cb ON c.Id = cb.CreatorId
WHERE cb.CreatorId IS NULL
ORDER BY CreatorName
