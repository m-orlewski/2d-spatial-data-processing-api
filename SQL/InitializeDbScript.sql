USE DB2_project;
GO

DROP TABLE IF EXISTS dbo.Points;
DROP TABLE IF EXISTS dbo.Circles;
DROP TABLE IF EXISTS dbo.Triangles;
DROP TABLE IF EXISTS dbo.Quadrangles;
GO

CREATE TABLE Points(id int IDENTITY(1,1) PRIMARY KEY, point Point);
CREATE TABLE Circles(id int IDENTITY(1,1) PRIMARY KEY, circle Circle);
CREATE TABLE Triangles(id int IDENTITY(1,1) PRIMARY KEY, triangle Triangle);
CREATE TABLE Quadrangles(id int IDENTITY(1,1) PRIMARY KEY, quadrangle Quadrangle);
GO

INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(0; 0)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(0.5; 0.5)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(1; 1)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(2; 2)'));
INSERT INTO dbo.Points (point) VALUES (CONVERT(Point, '(3; 3)'));
GO

INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(1; 1) r=1'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(2; 2) r=2,5'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(3; 3) r=3'));
INSERT INTO dbo.Circles (circle) VALUES (CONVERT(Circle, 'c=(4; 4) r=-2')); -- Validation failed - exception
GO

INSERT INTO dbo.Triangles (triangle) VALUES (CONVERT(Triangle, '(0; 0),(1; 0),(1; 1)'));
INSERT INTO dbo.Triangles (triangle) VALUES (CONVERT(Triangle, '(1; 0),(1; 1),(0; 1)'));
INSERT INTO dbo.Triangles (triangle) VALUES (CONVERT(Triangle, '(0; 0),(1; 1),(2; 2)')); -- Validation failed - exception
GO

INSERT INTO dbo.Quadrangles (quadrangle) VALUES (CONVERT(Quadrangle, '(0; 0),(1; 0),(1; 1),(0; 1)'));
INSERT INTO dbo.Quadrangles (quadrangle) VALUES (CONVERT(Quadrangle, '(0,0),(1,1),(2,2),(4,4)')); -- Validation failed - exception
GO