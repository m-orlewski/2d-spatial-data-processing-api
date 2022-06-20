DROP TABLE dbo.Points;
GO

CREATE TABLE Points(point Point);
GO

INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(0,0)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(1,1)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(2,2)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(3,3)'));
GO

SELECT point.ToString() AS "Punkt", point.X as "X", point.Y as "Y" FROM dbo.Points;
GO

