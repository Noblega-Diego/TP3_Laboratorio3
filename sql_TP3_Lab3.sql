USE ormCiudad;

#Seleccione todos los campos de la tabla city.(mediante *)
SELECT * FROM city;

#Seleccione los campos NAME Y population de la tabla city
SELECT NAME, population FROM city LIMIT 0,10;

#Seleccione los primeros 10 campos name Y population de la tabla city
SELECT NAME FROM city LIMIT 0,10;

#Seleccione los campos name Y population de la tabla city ordenados
#por NAME (ORDER BY)
SELECT NAME, population FROM city ORDER BY NAME;

#Calcule el promedio de población de cada distrito de la tabla city
#ordenados por el nombre del distrito (GROUP BY / ORDER BY)
SELECT District AS "promedio de population", AVG (population) FROM city 
	GROUP BY District ORDER BY District;  
	
#Seleccione CountryCode y la suma SUM() total del campo population
#de la tabla city agrupados por CountryCode (GROUP BY)
SELECT CountryCode, SUM(population) AS "total population" FROM city 
	GROUP BY CountryCode ORDER BY CountryCode;  

#Seleccione los campos name Y population de la tabla city donde
#population sea mayor a 100000 (>)
SELECT NAME, population FROM city WHERE population > 100000;

#Seleccione los campos name Y population de la tabla city donde
#population sea menor a 100000 (>)
SELECT NAME, population FROM city WHERE population < 100000;

#Seleccione los campos name Y population de la tabla city donde
#population sea mayor a 100000 Y menor a 10000000 (> AND < o
#BETWEEN)
SELECT NAME, population FROM city 
	WHERE population BETWEEN 100000 AND  10000000;
#Seleccione los campos name Y countryCode de la tabla city donde el
#countryCode sea igual a NLD
SELECT NAME, CountryCode FROM city 
	WHERE CountryCode = "NLD";

#(11)
#.Seleccione los campos name Y countryCode de la tabla city donde el
#countryCode sea igual a AFG o igual a CAN o igual a MEX (OR)
SELECT NAME, CountryCode FROM city
	WHERE CountryCode = "AFG"
	OR CountryCode = "CAN"
	OR CountryCode = "MEX";

#(12)
#Seleccione los campos name Y countryCode de la tabla city donde
#name comience con p (LIKE P%)
SELECT NAME, CountryCode FROM city 
	WHERE NAME LIKE 'p%';
#(13)	
#Seleccione los campos name Y countryCode de la tabla city donde
#name termine con e (LIKE %P)
SELECT NAME, CountryCode FROM city 
	WHERE NAME LIKE '%e';
	
#(14)
#Seleccione los campos name Y countryCode de la tabla city donde
#name contenga los caracteres ma (LIKE %MA%)
SELECT NAME, countryCode FROM city
	WHERE NAME LIKE '%ma%';
#(15)
#Obtenga el total de poblacion mundial por medio del campo
#pupulation de la tabla city
SELECT SUM(population)  AS "poblacion total" FROM city;


#(16)
#Obtenga el promedio de poblacion mundial por medio del campo
#pupulation de la tabla city
SELECT AVG(population)  AS "poblacion total" FROM city;

#(17)
#Obtenga el total de registros de la tabla city
SELECT COUNT(*) AS total FROM city;

#(18)
#Obtenga el maximo valor de del campo pupulation de la tabla city
SELECT MAX(population) AS total FROM city;

#(19)
#Obtenga el menor valor de del campo pupulation de la tabla city
SELECT MAX(population) AS total FROM city;

#(20)
#Inserte en la tabla city una nueva ciudad llamada TUNUYAN,
#CountryCode ARG, DISTRICT Mendoza, population 110000

INSERT INTO city(NAME, countryCode, district, population)
	VALUES("TUNUYAN", "ARG", "Mendoza", 110000);
#------------------------------------------------------------------
SELECT * FROM country;
SELECT id, NAME FROM city WHERE NAME = "TUNUYAN";
#(21).
#Inserte en la tabla country un nuevo pais llamado mendoza, codigo
#MZA, Y cuya capital sea tunuyan ciudad creada en el punto anterior
INSERT INTO country(NAME, CODE, Capital) 
	VALUES("mendoza","MZA", (SELECT id NAME FROM city WHERE NAME = "TUNUYAN"));
# or
INSERT INTO country(NAME, CODE, Capital) 
	VALUES("mendoza","MZA", 4080);

SELECT * FROM country WHERE NAME = "mendoza";


#(22).Actualizar el nombre de la ciudad TUNUYAN por TUPUNGATO
UPDATE city SET NAME = "TUPUNGATO" WHERE id =  4080;
SELECT NAME FROM city WHERE id = 4080;

#(23).Actualizar el campo languaje de la tabla countrylanguage a espaniol
#donde languaje sea igual a Spanish
SELECT * FROM countryLanguage WHERE LANGUAGE = "espaniol";
UPDATE countryLanguage SET LANGUAGE = "espaniol" WHERE LANGUAGE = "spanish";


#24.Elimine el pais mendoza de la tabla country
SELECT NAME FROM country WHERE NAME = "mendoza";
DELETE country.* FROM country WHERE NAME = 'mendoza';

#25.Elimine la ciudad de TUPUNGATO de la tabla ciudad
DELETE city.* FROM city WHERE NAME = 'tupungato';

#26.Seleccione los campos name, district de la tabla city, el campo NAME
#de la tabla country donde el nombre de la ciudad sea 'Buenos Aires'
SELECT * FROM country;
SELECT * FROM city;
SELECT city.name, city.district, country.name FROM city
	INNER JOIN country ON country.`Capital` = city.`ID` 
	WHERE city.`Name` = 'Buenos Aires';

#(27).Seleccione los campos name, district de la tabla city, el campo NAME
#de la tabla country Y el campo languaje de la tabla countrylanguage
#donde el codigo del pais sea 'ARG' ordenados por district

SELECT city.name AS 'ciudad', city.district AS 'distrito', country.`Name` AS 'pais', cl.language AS 'lenguaje' FROM city
	INNER JOIN country ON country.`Code` = city.`CountryCode`
	INNER JOIN countrylanguage cl ON country.`Code` = cl.`CountryCode` 
	WHERE country.`Code` = 'ARG' ORDER BY city.`District`;

#28.Por medio de la cláusula INNER JOIN seleccione la totalidad de datos
#de las tablas city, country y countrylanguage

SELECT * FROM  country
	JOIN City ON country.`Code` = City.`CountryCode`
	JOIN CountryLanguage cl ON country.`Code` = cl.CountryCode;
	
	
#29.Por medio de la cláusula INNER JOIN seleccione la totalidad de datos
#de las tablas city, country y countrylanguage donde el campo code de
#country sea ‘ARG’
SELECT * FROM  country
	JOIN City ON country.`Code` = City.`CountryCode`
	JOIN CountryLanguage cl ON country.`Code` = cl.CountryCode
	WHERE `Country`.`Code` = "ARG"; 


#(30).Por medio de la cláusula INNER JOIN seleccione la totalidad de datos
#de las tablas city, country y countrylanguage donde el campo
#lenguaje de countrylanguaje sea ‘English’ o ‘Spanish’ o ‘Portuguese’
#O donde el campo Population de country sea mayor a 1 millon
SELECT * FROM  country
	INNER JOIN City ON country.`Code` = City.`CountryCode`
	INNER JOIN CountryLanguage cl ON country.`Code` = cl.CountryCode
	WHERE cl.`Language` = "English" 
	OR cl.`Language` = "Spanish" 
	OR cl.`Language` = "portuguese"
	OR country.`Population` > 1000000; 

#31.Por medio de la cláusula LEFT JOIN seleccione los datos de las tablas
#country y countrylanguage donde el capo IsOfficial de
#countrylanguage sea ‘T’

SELECT * FROM country 
	LEFT JOIN CountryLanguage cl ON cl.`CountryCode` = country.`Code`
	WHERE cl.`IsOfficial` = 'T';

























