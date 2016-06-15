CREATE TABLE dbo.category (
  category_id int IDENTITY,
  parent_category_id int NULL,
  name varchar(50) NOT NULL,
  keywords varchar(50) NULL,
  CONSTRAINT PK_category_category_id PRIMARY KEY CLUSTERED (category_id)
)
ON [PRIMARY]
GO

CREATE FUNCTION dbo.get_category_with_keywords(@category_id int)
RETURNS TABLE
AS 
RETURN WITH CTE
AS (SELECT
  category_id,
  parent_category_id,
  name,
  keywords
FROM category
WHERE category_id = @category_id

UNION ALL

SELECT
  parent.category_id,
  parent.parent_category_id,
  parent.name,
  parent.keywords
FROM CTE AS child
INNER JOIN category AS parent
  ON child.parent_category_id = parent.category_id
WHERE child.parent_category_id IS NOT NULL AND NOT(child.keywords IS NOT NULL AND parent.Keywords IS NOT NULL)
)
SELECT
  category_id,
  parent_category_id,
  name,
  keywords
FROM CTE
WHERE keywords IS NOT NULL;
GO

CREATE FUNCTION dbo.get_categories_by_level(@level int)
RETURNS TABLE AS 
RETURN WITH CTE
AS (
SELECT
  category_id,
  parent_category_id,
  name,
  keywords,
  level = 1
FROM category
WHERE parent_category_id IS NULL
UNION ALL
SELECT
  child.category_id,
  child.parent_category_id,
  child.name,
  child.keywords,
  level = parent.level + 1
FROM category AS child
INNER JOIN CTE AS parent
  ON child.parent_category_id = parent.category_id
WHERE parent.level < @level
)
SELECT
  category_id,
  parent_category_id,
  name,
  keywords,
  level
FROM CTE
WHERE level = @level
GO

SET IDENTITY_INSERT dbo.category ON
GO
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (100, NULL, N'Business', N'Money')
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (101, 100, N'Accounting', N'Taxes')
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (200, NULL, N'Tutoring', N'Teaching')
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (102, 100, N'Taxation', NULL)
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (201, 200, N'Computer', NULL)
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (103, 101, N'Corporate Tax', NULL)
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (202, 201, N'Operating System', NULL)
INSERT dbo.category(category_id, parent_category_id, name, keywords) VALUES (109, 101, N'Small business Tax', NULL)
GO
SET IDENTITY_INSERT dbo.category OFF
GO

ALTER TABLE dbo.category
  ADD CONSTRAINT FK_category_category_category_id FOREIGN KEY (parent_category_id) REFERENCES dbo.category (category_id)
GO