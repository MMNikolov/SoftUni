SELECT
	DepartmentID,
	MIN(Salary) AS MinimumSalary
FROM Employees
WHERE DepartmentID = 2 OR DepartmentID = 5 OR DepartmentID = 7 
	AND HireDate >  '01-01-2000'
GROUP BY DepartmentID