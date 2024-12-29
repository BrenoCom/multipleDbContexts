DECLARE 
	 @SQL NVARCHAR(MAX) = ''
	 
	 -- PUT THE COMPANY INFO HERE
	,@companyName VARCHAR(200) = 'MY COMPANY'
	 -- YOU NEED TO BE CONNECTD TO THE DB SERVER 
	 -- YOU ARE REFERECING HERE TO WORK WITH THE APPLICATION
	,@otherSoftwareServer VARCHAR(30) = '127.0.0.1'
	,@otherSoftwareDatabase VARCHAR(50) = 'otherSoftwareBRDB' -- IN ONE SERVER WE CAN HAVE MANY DBs
	,@companyCode VARCHAR(5) = 'B1' -- IN ONE DB WE CAN HAVE MANY COMPANYS
	,@companyYear VARCHAR(2) = '24' -- SOME TABLES ARE SEPARETED BY YEAR AND COMPANY
	-- THIS THIS PARAMITER IS FOR DEFININ THAT 
	-- THIS NEW DATABASE IS FOR ANOTHER CLIENT
	-- BUT MANAGE BY THE COMPANY ID BELLOW 
	,@ifFromCompany INT = 0

SET @SQL = '

-- CREATES THE DATABASE
IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = '''+@otherSoftwareDatabase+''')
BEGIN
    CREATE DATABASE '+@otherSoftwareDatabase+'
END
'
EXEC SP_ExecuteSQL @SQL
PRINT 'DATABASE CREATED'

SET @SQL = '
-- Check if the table exists
IF NOT EXISTS (SELECT 1 FROM '+@otherSoftwareDatabase+'.dbo.sysobjects WHERE name=''PL01'+@companyCode+'00'' and xtype=''U'')
BEGIN
    -- Supplier
	CREATE TABLE '+@otherSoftwareDatabase+'.dbo.PL01'+@companyCode+'00
	(
		 PL01001 CHAR(10) NOT NULL -- Supplier Code
		,PL01002 CHAR(200) NOT NULL -- Supplier Name
		,PL01003 CHAR(100) NOT NULL -- Address Line 1
		,PL01004 CHAR(100) NOT NULL -- Address Line 2
		,PL01005 CHAR(100) NOT NULL -- Address Line 3
		-- ... other columns
	)

	-- INSERT DEMO DATA
	INSERT INTO '+@otherSoftwareDatabase+'.dbo.PL01'+@companyCode+'00 
	(PL01001, PL01002, PL01003, PL01004, PL01005)
	VALUES
	 (''0000000001'', ''Diogo e Priscila Consultoria Financeira ME'', ''Rua Mariópolis'', ''Vila Carioca'', ''Guarulhos'')
	,(''0000000002'', ''Thomas e Catarina Gráfica Ltda'', ''Rua Celso Tadeu dos Santos'', ''Jordanópolis'', ''São Bernardo do Campo'')
	,(''0000000003'', ''Débora e Luís Restaurante ME'', ''Rua São Gonçalo'', ''Centro'', ''Taubaté'')
	,(''0000000004'', ''Marcela e Lívia Ferragens ME'', ''Rua Rio Grande do Sul 838'', ''Centro'', ''São Caetano do Sul'')
	,(''0000000005'', ''César e Bárbara Adega ME'', ''Rua Francisco Galassi'', ''Vila Bertini'', ''Americana'')
	,(''0000000006'', ''Fabiana e Esther Corretores Associados ME'', ''Rua Eucário Rebouças de Carvalho'', ''Centro'', ''Taubaté'')
	,(''0000000007'', ''José e Juliana Telecom Ltda'', ''Rua José Pedro Salomão'', ''Conjunto Habitacional São Deocleciano'', ''São José do Rio Preto'')
	,(''0000000008'', ''Cauã e Jorge Corretores Associados Ltda'', ''Praça Milone Romanha'', ''Ayrosa'', ''Osasco'')

END
'
SELECT @SQL
EXEC SP_ExecuteSQL @SQL
PRINT 'SUPPLYER TABLE CREATED'

SET @SQL = '
IF NOT EXISTS (SELECT 1 FROM '+@otherSoftwareDatabase+'.dbo.sysobjects WHERE name=''PL03'+@companyCode+'00'' and xtype=''U'')
BEGIN
	-- Invoices
	CREATE TABLE '+@otherSoftwareDatabase+'.dbo.PL03'+@companyCode+'00
	(
		 PL03001 CHAR(10) NOT NULL -- Supplier Code
		,PL03002 CHAR(25) NOT NULL -- Invoice Number
		,PL03003 CHAR(9) NOT NULL -- TransactioNo
		,PL03004 DATETIME NOT NULL -- Invoice Date
		,PL03005 DATETIME NOT NULL -- BookEntrDate
		,PL03006 DATETIME NOT NULL -- Due Date
		-- ... other columns
	)
	

	-- INSERT DEMO DATA
	INSERT INTO '+@otherSoftwareDatabase+'.dbo.PL03'+@companyCode+'00 
	(PL03001, PL03002, PL03003, PL03004, PL03005, PL03006)
	VALUES
	 (''0000000001'', ''5113544'', ''1223456'', ''2024-12-19'', ''2025-01-10'', ''2025-01-01'')
	,(''0000000002'', ''42432432'', '''', ''2024-12-19'', ''1900-01-01'', ''2025-01-01'')
	,(''0000000003'', ''453231'', '''', ''2024-12-19'', ''1900-01-01'', ''2025-01-15'')
	,(''0000000004'', ''4568453'', '''', ''2024-12-19'', ''1900-01-01'', ''2025-01-01'')
	,(''0000000005'', ''4568453'', ''4561234'', ''2024-12-19'', ''2025-01-10'', ''2025-01-15'')
	,(''0000000006'', ''000041565'', '''', ''2024-12-20'', ''2025-01-10'', ''2025-01-01'')
	,(''0000000007'', ''452312'', ''741852'', ''2025-01-09'', ''2025-01-10'', ''2025-01-10'')
	,(''0000000008'', ''000544562'', '''', ''2024-12-20'', ''1900-01-01'', ''2025-01-10'')
	,(''0000000001'', ''00046434'', '''', ''2024-12-19'', ''1900-01-01'', ''2025-01-01'')
	,(''0000000001'', ''456432'', '''', ''2024-12-19'', ''2025-01-10'', ''2025-01-10'')
	,(''0000000003'', ''4564872'', ''8749852'', ''2024-12-20'', ''2025-01-10'', ''2025-01-01'')
	,(''0000000003'', ''453654321'', '''', ''2024-12-19'', ''1900-01-01'', ''2025-01-01'')
	,(''0000000005'', ''4238420'', ''85271'', ''2024-12-19'', ''2025-01-10'', ''2025-01-10'')
	,(''0000000005'', ''42358464'', '''', ''2024-12-19'', ''2025-01-10'', ''2025-01-01'')
	,(''0000000005'', ''4565421'', '''', ''2024-12-20'', ''1900-01-01'', ''2025-01-10'')
	,(''0000000005'', ''4568465464'', '''', ''2024-12-20'', ''1900-01-01'', ''2025-01-10'')
	

END

'
PRINT 'INVOICE TABLE CREATED'

EXEC SP_ExecuteSQL @SQL
	
-- RUN THIS ONLY ON THE SERVER 
-- WITH THE DB mySoftwareDB

IF ISNULL(@ifFromCompany, 0) = 0
BEGIN
	INSERT INTO mySoftwareDB.dbo.Company
	(name, serverERP, baseERP, codeERP, yearERP, hasIF, serverERPIF, baseERPIF, codeERPIF, yearERPIF)
	VALUES
	(@companyName, @otherSoftwareServer, @otherSoftwareDatabase, @companyCode, @companyYear, 0, '', '', '', '')
END
ELSE 
BEGIN
	UPDATE mySoftwareDB.dbo.Company
	SET
		 hasIF = 1
		,serverERPIF = @otherSoftwareServer
		,baseERPIF = @otherSoftwareDatabase
		,codeERPIF = @companyCode
		,yearERPIF = @companyYear
	WHERE
		idCompany = @ifFromCompany
END



