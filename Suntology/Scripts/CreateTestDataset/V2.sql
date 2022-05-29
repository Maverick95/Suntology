CREATE OR ALTER PROCEDURE [dbo.suntology.CreateTestDataset]

	@Members_MiddleManagement_Count		INT,
	@Members_Serf_Count					INT

AS
BEGIN

	DELETE
	FROM [suntology.internalcomms];

	DELETE
	FROM [suntology.member];

	DELETE
	FROM [suntology.caste];

	IF ( OBJECT_ID('[tempdb]..#RAND') IS NOT NULL )
	BEGIN
		DROP TABLE #RAND;
	END;

	CREATE TABLE #RAND
	(
		Rand_1 FLOAT NULL,		Rand_2 FLOAT NULL,			Rand_3 FLOAT NULL,
		Rand_4 FLOAT NULL,		Rand_5 FLOAT NULL,			Rand_6 FLOAT NULL,
		Rand_7 FLOAT NULL,		Rand_8 FLOAT NULL,			Rand_9 FLOAT NULL,
		Rand_10 FLOAT NULL,		Rand_11 FLOAT NULL,			Rand_12 FLOAT NULL
	);

	/* ########## Caste ##########  */

	INSERT INTO		[suntology.caste]
	(	Reference,				DisplayName,								[Rank]		)
	VALUES
	(	'SUN',					'The One True Sun',							1			),
	(	'HINQ',					'Inquisitor',								2			),
	(	'PROP',					'Propaganda Executive',						2			),
	(	'WHIM',					'Non-Sensical Whimsy Executive',			2			),
	(	'INV',					'Invoicing Serf',							3			),
	(	'TEL',					'Telemarketing Serf',						3			),
	(	'CRE',					'"Creative" Serf',							3			);

	/* ########## Member ########## */

	IF (OBJECT_ID('[tempdb]..#DATA') IS NOT NULL)
	BEGIN
		DROP TABLE #MEMBER_DATA;
	END;

	CREATE TABLE #MEMBER_DATA
	(
		Id					INT IDENTITY(1, 1)		NOT NULL		CONSTRAINT PK_Id PRIMARY KEY,
		FormerForename		NVARCHAR(255)			NOT NULL,
		FormerSurname		NVARCHAR(255)			NOT NULL,
		Forename			NVARCHAR(255)			NOT NULL,
		Surname				NVARCHAR(255)			NOT NULL
	);

	/* There can only be one sun lord. */
	/* They have no former name, age, or gender. They have always been. */

	INSERT INTO		[suntology.member]
	(				[Name],					CasteId				)
	SELECT			'The One True Sun',		Id
	FROM			[suntology.caste]
	WHERE			Reference = 'SUN';

	/* Middle-management executives. */

	INSERT INTO #MEMBER_DATA
	(		FormerForename,			FormerSurname,			Forename,			Surname				)
	VALUES
	(		'Steve',				'Johnson',				'Chad',				'Rosindell'			),
	(		'Steph',				'Smith',				'Charlton',			'Gallstones'		),
	(		'Jane',					'Fletcher',				'Hope',				'Raincoat'			),
	(		'Tom',					'Baker',				'Temperance',		'Strongbright'		),
	(		'Peter',				'Hodgson',				'Serenity',			'Moonshaker'		),
	(		'Paul',					'Wright',				'Absolution',		'Dreamsmasher'		),
	(		'James',				'White',				'Power',			'Hopeshiner'		),
	(		'Margaret',				'Black',				'Jesus',			'Mussolini'			),
	(		'Harry',				'Bateman',				'Fertility',		'Mugabe'			),
	(		'Colin',				'Page',					'Pastor',			'Anderson'			),
	(		'Gregory',				'Muntley',				'Moses',			'Stalin'			),
	(		'Malcolm',				'Monkton',				'Eve',				'Chudbringer'		),
	(		'Janice',				'Bridges',				'Boudica',			'Weinstein'			),
	(		'Helen',				'Wood',					'Condoleeza',		'Drake'				),
	(		'Morgan',				'Baron',				'Hillary',			'Spacey'			);

	DELETE FROM #RAND;
	
	DECLARE @lp INT = 0;

	WHILE ( @lp < @Members_MiddleManagement_Count )
	BEGIN

		/*	Reference,		FormerForename,		FormerSurname,		Forename,		Surname,		AssignedGender,		Age			*/

		INSERT INTO #RAND
		(	Rand_1,			Rand_2,				Rand_3,				Rand_4,			Rand_5,			Rand_6,				Rand_7		)
		VALUES
		(	RAND(),			RAND(),				RAND(),				RAND(),			RAND(),			RAND(),				RAND()		);

		SET @lp = @lp + 1;

	END;

	DECLARE @Data_Count INT = ( SELECT COUNT(*) FROM #MEMBER_DATA	);

	WITH RAND_PROC
	AS
	(
		SELECT
		CASE FLOOR(3 * R.Rand_1)
			WHEN 0 THEN 'HINQ'
			WHEN 1 THEN 'PROP'
			WHEN 2 THEN 'WHIM'
		END AS Reference,	
		(
			SELECT TOP 1 D.FormerForename
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_2)
		) AS FormerForename,
		(
			SELECT TOP 1 D.FormerSurname
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_3)
		) AS FormerSurname,
		(
			SELECT TOP 1 D.Forename
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_4)
		) + ' ' +
		(
			SELECT TOP 1 D.Surname
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_5)
		) AS [Name],
		CASE
			WHEN R.Rand_6 < 0.5 THEN 'M' ELSE 'F'
		END AS AssignedGender,
		12 + FLOOR(89 * R.Rand_7) AS Age
		FROM #RAND AS R
	)

	INSERT INTO [suntology.member]
	(
					FormerForename,
					FormerSurname,
					[Name],
					AssignedGender,
					Age,
					CasteId
	)
	SELECT			RP.FormerForename,
					RP.FormerSurname,
					RP.[Name],
					RP.AssignedGender,
					RP.Age,
					C.Id
	FROM			RAND_PROC AS RP
	INNER JOIN		[suntology.caste] AS C
		ON			C.Reference = RP.Reference;

	/* Serfs. */

	TRUNCATE TABLE	#MEMBER_DATA;

	INSERT INTO		#MEMBER_DATA
	(		FormerForename,			FormerSurname,			Forename,			Surname					)
	VALUES
	(		'Steve',				'Johnson',				'You There',		'Nameless'				),
	(		'Steph',				'Smith',				'Hey You',			'Soulless'				),
	(		'Jane',					'Fletcher',				'Mindless',			'A Number'				),
	(		'Tom',					'Baker',				'123',				'56SF'					),
	(		'Peter',				'Hodgson',				'XYZ',				'Number 3'				),
	(		'Paul',					'Wright',				'ABC',				'[Template Name]'		),
	(		'James',				'White',				'789',				'*INSERT NAME*'			),
	(		'Margaret',				'Black',				'XZ12',				'???'					);

	DELETE FROM		#RAND;

	SET				@lp = 0;

	WHILE ( @lp < @Members_Serf_Count )
	BEGIN

		/*	Reference,		FormerForename,		FormerSurname,		Forename,		Surname,		AssignedGender,		Age			*/

		INSERT INTO #RAND
		(	Rand_1,			Rand_2,				Rand_3,				Rand_4,			Rand_5,			Rand_6,				Rand_7		)
		VALUES
		(	RAND(),			RAND(),				RAND(),				RAND(),			RAND(),			RAND(),				RAND()		);

		SET @lp = @lp + 1;

	END;

	SET @Data_Count = ( SELECT COUNT(*) FROM #MEMBER_DATA	);

	WITH RAND_PROC
	AS
	(
		SELECT
		CASE FLOOR(3 * R.Rand_1)
			WHEN 0 THEN 'INV'
			WHEN 1 THEN 'TEL'
			WHEN 2 THEN 'CRE'
		END AS Reference,	
		(
			SELECT TOP 1 D.FormerForename
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_2)
		) AS FormerForename,
		(
			SELECT TOP 1 D.FormerSurname
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_3)
		) AS FormerSurname,
		(
			SELECT TOP 1 D.Forename
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_4)
		) + ' ' +
		(
			SELECT TOP 1 D.Surname
			FROM #MEMBER_DATA AS D WHERE D.Id = CEILING(@Data_Count * R.Rand_5)
		) AS [Name],
		CASE
			WHEN R.Rand_6 < 0.5 THEN 'M' ELSE 'F'
		END AS AssignedGender,
		12 + FLOOR(89 * R.Rand_7) AS Age
		FROM #RAND AS R
	)

	INSERT INTO [suntology.member]
	(
					FormerForename,
					FormerSurname,
					[Name],
					AssignedGender,
					Age,
					CasteId
	)
	SELECT			RP.FormerForename,
					RP.FormerSurname,
					RP.[Name],
					RP.AssignedGender,
					RP.Age,
					C.Id
	FROM			RAND_PROC AS RP
	INNER JOIN		[suntology.caste] AS C
		ON			C.Reference = RP.Reference;

END
