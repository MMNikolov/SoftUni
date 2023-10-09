CREATE PROC usp_EmployeesBySalaryLevel @salaryLevel NVARCHAR(15)
AS
	SELECT 
		FirstName,
		LastName
	FROM Employees
	WHERE @salaryLevel = dbo.ufn_GetSalaryLevel(Salary)