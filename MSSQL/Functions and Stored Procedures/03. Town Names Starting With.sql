CREATE PROC usp_GetTownsStartingWith @string NVARCHAR(10)
	AS
		SELECT
			[Name]
		FROM Towns
		WHERE [Name] Like @string + '%'