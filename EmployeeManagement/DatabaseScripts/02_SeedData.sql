
CREATE OR ALTER FUNCTION fn_GetEmployees(
    @SearchValue NVARCHAR(100) = '',
    @Department NVARCHAR(100) = '',
    @PageNumber INT = 1,
    @PageSize INT = 10
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        e.EmployeeID,
        e.Name,
        d.DepartmentName,
        e.Salary,
        e.DateOfJoining,
        COUNT(*) OVER() AS TotalCount
    FROM Employees e
    INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
    WHERE 
        (ISNULL(@SearchValue, '') = '' OR e.Name LIKE '%' + @SearchValue + '%')
        AND 
        (ISNULL(@Department, '') = '' OR d.DepartmentName = @Department)
    ORDER BY e.EmployeeID
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY
);
GO

SELECT * FROM dbo.fn_GetEmployees('', 'HR', 1, 10);
Select * from dbo.Employees where EmployeeID = 1
GO

--Upsert
CREATE PROCEDURE sp_AddUpdateEmployee
    @EmployeeID INT,
    @Name NVARCHAR(100),
    @DepartmentID INT,
    @Salary DECIMAL(18,2),
    @DateOfJoining DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Departments WHERE DepartmentID = @DepartmentID)
            THROW 50002, 'Invalid Department ID.', 1;
        
        IF @EmployeeID = 0
        BEGIN
            INSERT INTO Employees (
                Name,
                DepartmentID,
                Salary,
                DateOfJoining
            )
            VALUES (
                @Name,
                @DepartmentID,
                @Salary,
                @DateOfJoining
            )
            
            SELECT 'Employee added successfully' AS Result
        END

        ELSE
        BEGIN
            IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID)
                THROW 50003, 'Employee not found.', 1;
                
            UPDATE Employees
            SET Name = @Name,
                DepartmentID = @DepartmentID,
                Salary = @Salary,
                DateOfJoining = @DateOfJoining
            WHERE EmployeeID = @EmployeeID
            
           
            SELECT 'Employee updated successfully' AS Result
        END
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END

EXEC sp_AddUpdateEmployee 
    @EmployeeID = 0,
    @Name = 'John Doe',
    @DepartmentID = 1,
    @Salary = 50000.00,
    @DateOfJoining = '2024-02-06'

GO

--Delete 
CREATE PROCEDURE Sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DELETE FROM Employees WHERE EmployeeID = @EmployeeID
        SELECT 'Employee deleted successfully' AS Result
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

----Department----


CREATE PROCEDURE Sp_CreateDepartment
    @DepartmentName NVARCHAR(100),
    @NewDepartmentID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO Departments (DepartmentName)
        VALUES (@DepartmentName)
        
        SET @NewDepartmentID = SCOPE_IDENTITY()
        
        SELECT 'Department created successfully' AS Result
    END TRY
    BEGIN CATCH
        IF ERROR_NUMBER() = 2627 -- Unique constraint violation
            THROW 50001, 'Department name already exists.', 1;
        ELSE
            THROW;
    END CATCH
END
GO

EXEC Sp_CreateDepartment'Support', 0
