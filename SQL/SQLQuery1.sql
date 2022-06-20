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

DROP TABLE dbo.Circles;
GO

CREATE TABLE Circles(circle Circle);
GO

INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(1,1) r=1'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(2,2) r=2'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(3,3) r=3'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(4,4) r=4'));
GO

SELECT circle.ToString() AS "Okr¹g" FROM dbo.Circles;
GO

