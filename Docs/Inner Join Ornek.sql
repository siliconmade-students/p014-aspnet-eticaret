SELECT p.*, c.Name, b.Name
FROM Products p 
INNER JOIN Categories c ON c.Id=p.CategoryId
INNER JOIN Brands b ON b.Id=p.BrandId
ORDER BY Id DESC