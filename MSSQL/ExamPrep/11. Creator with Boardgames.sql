CREATE FUNCTION udf_CreatorWithBoardgames(@name NVARCHAR(50))
RETURNS INT
AS
BEGIN
	DECLARE @Total INT = 
	(
		SELECT 
			COUNT(cb.BoardgameId)
		FROM Creators AS c
		JOIN CreatorsBoardgames AS cb ON c.Id = cb.CreatorId
		WHERE c.FirstName = @name
	)
	RETURN @Total
END