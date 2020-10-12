DELETE FROM EmployeeProduct
DBCC CHECKIDENT ('EmployeeProduct', RESEED, 1)

DELETE FROM EmployeeOrganizationUnit
DBCC CHECKIDENT ('EmployeeOrganizationUnit', RESEED, 1)

DELETE FROM [UserRole]
DBCC CHECKIDENT ('UserRole', RESEED, 1)

DELETE FROM [User]
DBCC CHECKIDENT ('User', RESEED, 1)

DELETE FROM DocumentDetail
DBCC CHECKIDENT ('DocumentDetail', RESEED, 1)

DELETE FROM DocumentOrganizationUnit
DBCC CHECKIDENT ('DocumentOrganizationUnit', RESEED, 1)

DELETE FROM Document
DBCC CHECKIDENT ('Document', RESEED, 1)

DELETE FROM CustomerCompany
DBCC CHECKIDENT ('CustomerCompany', RESEED, 1)

DELETE FROM Schedule
DBCC CHECKIDENT ('Schedule', RESEED, 1)

DELETE FROM Customer
DBCC CHECKIDENT ('Customer', RESEED, 1)

DELETE FROM ProductOrganizationUnit
DBCC CHECKIDENT ('ProductOrganizationUnit', RESEED, 1)

DELETE FROM Product
DBCC CHECKIDENT ('Product', RESEED, 1)

DELETE FROM OrganizationUnit
DBCC CHECKIDENT ('OrganizationUnit', RESEED, 1)

DELETE FROM License
DBCC CHECKIDENT ('License', RESEED, 1)

DELETE FROM Company

DELETE FROM [File]
DBCC CHECKIDENT ('File', RESEED, 1)


