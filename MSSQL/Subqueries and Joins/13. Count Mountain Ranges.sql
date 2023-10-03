SELECT 
	mc.CountryCode,
	COUNT(m.MountainRange) AS MountainRanges
FROM MountainsCountries AS mc
	JOIN Mountains AS m ON mc.MountainId = m.Id
WHERE mc.CountryCode = 'BG' OR mc.CountryCode = 'RU' OR mc.CountryCode = 'US'
GROUP BY mc.CountryCode