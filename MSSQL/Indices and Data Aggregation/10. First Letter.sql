SELECT DISTINCT
	FirstLetter
FROM(
SELECT
	SUBSTRING(FirstName, 1, 1) AS FirstLetter
FROM WizzardDeposits AS wd
WHERE wd.DepositGroup = 'Troll Chest'
GROUP BY FirstName) AS Letter