CREATE DATABASE POS;
GO

USE POS;
GO

CREATE TABLE ProductTable (
    ProductId VARCHAR(5) PRIMARY KEY,    
    ProductName NVARCHAR(100),
	Quantity INT,
    CostPrice DECIMAL(10, 2),
    SellingPrice DECIMAL(10, 2),
    Description NVARCHAR(255),
	Status VARCHAR(20)                    

);
Go



CREATE TABLE EmployeeTable (
    EmployeeId NVARCHAR(10) PRIMARY KEY,
    EmployeeName NVARCHAR(100),
    EmployeeNIC NVARCHAR(12),
    EmployeeTeleNo INT,
    EmployeeAddress NVARCHAR(50),
    EmployeeEmail NVARCHAR(50),
    EmployeeRole NVARCHAR(50),
    EmployeeGender VARCHAR(10),
    EmployeePhoto VARBINARY(MAX),
    EmployeeUserName NVARCHAR(50),
    EmployeePassword NVARCHAR(50),
    EmployeeSalary NVARCHAR(20)
);
GO

CREATE TABLE Sales (
    BillNo INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId NVARCHAR(10),
    SaleDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Total DECIMAL(10, 2),
    Discount DECIMAL(10, 2),
    FOREIGN KEY (EmployeeId) REFERENCES EmployeeTable(EmployeeId)
);


GO

CREATE TABLE Orders (
    BillNo INT,
    ProductId VARCHAR(5),
    Qty INT,
    PRIMARY KEY (BillNo, ProductId),
    FOREIGN KEY (BillNo) REFERENCES Sales(BillNo),  
    FOREIGN KEY (ProductId) REFERENCES ProductTable(ProductId)  
);
GO


CREATE TABLE Loyality (
    PhoneNo INT PRIMARY KEY,    
    Name NVARCHAR(100),         
    Points INT                  
);

GO

INSERT INTO ProductTable (ProductId, ProductName, Quantity, CostPrice, SellingPrice, Description, Status) VALUES
('P1', 'Anchor Full Cream Milk Powder', 30, 1000.00, 1100.00, 'Date Received :- 2025-07-04|Sizes :- 12g, 25g, 100g, 400g, 1kg, 2kg|Categories :- Dairy Products|Expired On :- 2026-07-02', 'Available'),
('P10', 'Himalaya Purifying Neem Facewash 50Ml', 33, 800.00, 850.00, 'Date Received :- 2025-07-15|Sizes :- 15ml, 50ml, 100ml|Categories :- Face Care|Expired On :- 2026-05-20', 'Available'),
('P11', 'Ponds Bright Beauty Serum Cream 35G', 15, 800.00, 820.00, 'Date Received :- 2025-07-01|Sizes :- 7g, 18g, 35g, 50g|Categories :- Personal Care|Expired On :- 2026-05-20', 'Available'),
('P12', 'Kotmale Processed Cheese Wedges Pepper', 25, 500.00, 580.00, 'Date Received :- 2025-07-02|Sizes :- 120g (8 wedges × 15g)|Categories :- Dairy Products|Expired On :- 2026-06-30', 'Available'),
('P13', 'Time Wafer Master Chocolate Cream 65G', 48, 700.00, 750.00, 'Date Received :- 2025-07-15|Sizes :- 35g, 50g, 65g|Categories :- Daily Deals, Biscuits & Cookies|Expired On :- 2026-05-25', 'Available'),
('P14', 'Munchee Onion Biscuits - 170.00 g', 50, 340.00, 380.00, 'Date Received :- 2025-07-08|Sizes :- 85g, 170g, 200g|Categories :- Snacks / Biscuits|Expired On :- 2026-06-03', 'Available'),
('P15', 'Lux Body Soap Multipack - 4.00 pcs', 60, 540.00, 620.00, 'Date Received :- 2025-07-20|Sizes :- 4 pcs × 100g (400g)|Categories :- Personal Care|Expired On :- 2026-05-22', 'Available'),
('P16', 'Motha Rasperry Flavoured Jelly - 100.00 g', 15, 195.00, 230.00, 'Date Received :- 2025-07-16|Sizes :- 80g, 100g|Categories :- Desserts Mix|Expired On :- 2026-05-13', 'OutOfStock'),
('P17', 'Ritzbury Chunky Chocolate - 200.00 g', 35, 400.00, 450.00, 'Date Received :- 2025-07-11|Sizes :- 100g, 200g, 250g|Categories :- Confectionery|Expired On :- 2026-09-22', 'Available'),
('P18', 'Keells Krest Chicken Bockwurst 400G', 20, 1000.00, 1090.00, 'Date Received :- 2025-07-08|Sizes :- 200g, 400g|Categories :- Frozen Processed Meat|Expired On :- 2026-04-22', 'Available'),
('P19', 'Prima Sunrise Tea Bun 65G', 28, 80.00, 100.00, 'Date Received :- 2025-07-10|Sizes :- 65g, 100g|Categories :- Breads & Buns|Expired On :- 2026-06-22', 'Available'),
('P2', 'Quality Street Favourits 283G', 34, 1800.00, 2000.00, 'Date Received :- 2025-07-01|Sizes :- 240g, 283g, 450g|Categories :- Chocolates|Expired On :- 2026-06-25', 'Available'),
('P20', 'Twinfish Oat Choco Orginal 180G', 16, 1100.00, 1250.00, 'Date Received :- 2025-07-22|Sizes :- 180g, 360g|Categories :- Breads & Buns|Expired On :- 2026-05-20', 'OutOfStock'),
('P3', 'Munchee Crevo Savoury Biscuits 215G', 40, 600.00, 680.00, 'Date Received :- 2025-06-25|Sizes :- 80g, 150g, 215g|Categories :- Snacks / Biscuits|Expired On :- 2026-07-01', 'Available'),
('P4', 'Kotmale Fruit N Nut Icecream 1L', 22, 750.00, 820.00, 'Date Received :- 2025-07-01|Sizes :- 1L, 1.3L|Categories :- Ice Cream|Expired On :- 2026-07-02', 'Available'),
('P5', 'Elephant House Karutha Kolumban Ice Cream 900Ml', 18, 700.00, 790.00, 'Date Received :- 2025-07-04|Sizes :- 500ml, 900ml, 1L|Categories :- Ice Cream|Expired On :- 2026-07-08', 'OutOfStock'),
('P6', 'Nescafe 3In1 Original 18Gx28S 504G', 26, 2250.00, 2950.00, 'Date Received :- 2025-07-10|Sizes :- 252g , 504g|Categories :- Coffee Mix|Expired On :- 2026-06-30', 'Available'),
('P7', 'Nestle Milo Nuggets Mocha 25G', 32, 500.00, 550.00, 'Date Received :- 2025-07-10|Sizes :- 25g, 80g|Categories :- Coffee Mix|Expired On :- 2026-08-03', 'Available'),
('P8', 'Sweetzone Double Fruits 180G', 45, 700.00, 725.00, 'Date Received :- 2025-07-12|Sizes :- 100g, 180g, 300g|Categories :- Daily Deals, Candies & Sweets|Expired On :- 2026-08-03', 'Available'),
('P9', 'Prima Kottu Mee Koream Ramen Seafood 116G', 28, 280.00, 300.00, 'Date Received :- 2025-07-11|Sizes :- 70g, 85g, 116g|Categories :- Noodles And Pastas|Expired On :- 2026-06-03', 'Available');

GO