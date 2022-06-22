USE DB2_project;
GO

DROP TABLE IF EXISTS PointsComp;

CREATE TABLE PointsComp (id int IDENTITY(1,1) PRIMARY KEY, myPoint Point, sqlPoint geometry);

INSERT INTO PointsComp (myPoint, sqlPoint) VALUES (CONVERT(Point, '(1; 1)'), geometry::Point(1, 1, 0));
INSERT INTO PointsComp (myPoint, sqlPoint) VALUES (CONVERT(Point, '(2; 2)'), geometry::Point(2, 2, 0));

DECLARE @p1 Point;
DECLARE @p2 geometry;
SET @p1 = (SELECT myPoint FROM PointsComp WHERE id = 1);
SET @p2 = (SELECT sqlPoint FROM PointsComp WHERE id = 1);

-- Porównanie z wbudowanym geometry::Point
SELECT myPoint.ToString() AS "myPoint", sqlPoint.ToString() AS "sqlPoint", myPoint.DistanceFrom(@p1) AS "myDistance", sqlPoint.STDistance(@p2) AS "sqlDistance"
FROM PointsComp;

-- Porównanie z wbudowany geometry CIRCULARSTRING

DROP TABLE IF EXISTS CircleComp;

CREATE TABLE CircleComp (id int IDENTITY(1,1) PRIMARY KEY, myCircle Circle, sqlCircle geometry);

INSERT INTO CircleComp (myCircle, sqlCircle) VALUES (CONVERT (Circle, 'c=(0; 0) r=2'), Convert(geometry, 'CIRCULARSTRING(1 0, 0 1, -1 0, 0 -1, 1 0)'));

SELECT myCircle.ToString() AS "myCircle", sqlCircle.ToString() AS "sqlCircle",
@p1.IsInsideCircle(myCircle) AS "myIsInsideCircle", sqlCircle.STContains(@p2) AS "sqlIsInsideCircle",
myCircle.getSurfaceArea() AS "myCircleArea", sqlCircle.STArea() AS "sqlCircleArea"
FROM CircleComp;

-- Porównanie z wbudowanym geometry POLYGON dla trójk¹ta

DROP TABLE IF EXISTS TriangleComp;

CREATE TABLE TriangleComp (myTriangle Triangle, sqlTriangle geometry);

INSERT INTO TriangleComp (myTriangle, sqlTriangle)
VALUES (CONVERT(Triangle, '(0; 0),(1; 0),(0; 1)'), geometry::STPolyFromText('POLYGON ((0 0, 1 0, 0 1, 0 0))', 0));

SELECT myTriangle.ToString() AS "myTriangle", sqlTriangle.ToString() AS "sqlTriangle",
@p1.IsInsideTriangle(myTriangle) AS "myIsInsideTriangle", sqlTriangle.STContains(@p2) AS "sqlIsInsideTriangle",
myTriangle.getSurfaceArea() AS "myTriangleArea", sqlTriangle.STArea() AS "sqlTriangleArea"
FROM TriangleComp;

-- Porównanie z wbudowanym geometry POLYGON dla czworok¹ta

DROP TABLE IF EXISTS QuadrangleComp;

CREATE TABLE QuadrangleComp (myQuadrangle Quadrangle, sqlQuadrangle geometry);

INSERT INTO QuadrangleComp (myQuadrangle, sqlQuadrangle)
VALUES (CONVERT(Quadrangle, '(0; 0),(1; 0),(1; 1),(0; 1)'), geometry::STPolyFromText('POLYGON ((0 0, 1 0, 1 1, 0 1, 0 0))', 0));

SELECT myQuadrangle.ToString() AS "myQuadrangle", sqlQuadrangle.ToString() AS "sqlQuadrangle",
@p1.IsInsideQuadrangle(myQuadrangle) AS "myIsInsideQuadrangle", sqlQuadrangle.STContains(@p2) AS "sqlIsInsideQuadrangle",
myQuadrangle.getSurfaceArea() AS "myQuadrangleArea", sqlQuadrangle.STArea() AS "sqlQuadrangleArea"
FROM QuadrangleComp;