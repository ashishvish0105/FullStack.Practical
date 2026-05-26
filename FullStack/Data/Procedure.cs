//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace FullStack.Data
//{
//    public class Procedure
//    {
//    }
//}


//CREATE PROCEDURE GetProducts
//(
//    @PageNumber INT = 1,
//    @PageSize   INT = 10
//)
//AS
//BEGIN
//    SET NOCOUNT ON;

//--Validate parameters
//    IF(@PageNumber < 1)
//        SET @PageNumber = 1;

//IF(@PageSize < 1)
//        SET @PageSize = 10;

//DECLARE @Offset INT;

//SET @Offset = (@PageNumber - 1) * @PageSize;

//--Paged product list
//    SELECT 
//		prod.inProductId,
//    prod.stName,
//    prod.stDescription,
//    prod.stCategory,
//    prod.dbPrice
//    FROM tblProducts as prod
//    ORDER BY inProductId
//    OFFSET @Offset ROWS
//    FETCH NEXT @PageSize ROWS ONLY;

//--Total count for frontend pagination
//    SELECT 
//        COUNT(1) AS TotalRecords
//    FROM tblProducts;
//END
//GO


//execute GetProducts