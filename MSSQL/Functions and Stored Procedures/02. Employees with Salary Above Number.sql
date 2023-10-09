CREATE PROC usp_GetEmployeesSalaryAboveNumber @number Decimal(18, 4)
	AS
		SELECT
			FirstName,
			LastName
		FROM Employees
		WHERE Salary >= @number