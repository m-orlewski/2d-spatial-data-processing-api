USE DB2_project;
GO

DROP TABLE IF EXISTS dbo.Points;
DROP TABLE IF EXISTS dbo.Circles;
DROP TABLE IF EXISTS dbo.Triangles;
DROP TABLE IF EXISTS dbo.Quadrangles;
GO

CREATE TABLE Points(point Point);
CREATE TABLE Circles(circle Circle);
CREATE TABLE Triangles(triangle Triangle);
CREATE TABLE Quadrangles(quadrangle Quadrangle);
GO

INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(0,0)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(1,1)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(2,2)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(3,3)'));
GO

INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(1,1) r=1'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(2,2) r=2'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(3,3) r=3'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(4,4) r=4'));
GO

INSERT INTO dbo.Triangles (triangle) VALUES (CONVERT(Triangle, '(0,0),(1,1),(2,2)'));
INSERT INTO dbo.Triangles (triangle) VALUES (CONVERT(Triangle, '(3,3),(4,4),(2,2)'));
GO

INSERT INTO dbo.Quadrangles (quadrangle) VALUES (CONVERT(Quadrangle, '(0,0),(1,1),(2,2),(3,3)'));
INSERT INTO dbo.Quadrangles (quadrangle) VALUES (CONVERT(Quadrangle, '(0,0),(1,1),(2,2),(4,4)'));
GO


SELECT point.ToString() AS "Punkt", point.X as "X", point.Y as "Y" FROM dbo.Points;
GO

SELECT circle.ToString() AS "Okr¹g" FROM dbo.Circles;
GO

SELECT triangle.ToString() AS "Trójk¹t" FROM dbo.Triangles;
GO

SELECT quadrangle.ToString() AS "Czworok¹t" FROM dbo.Quadrangles;
GO
