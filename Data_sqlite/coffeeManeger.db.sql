BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Product" (
	"ID"	INTEGER,
	"Name"	TEXT,
	"Detail"	TEXT,
	"Image"	TEXT,
	"Price"	INTEGER,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TABLE" (
	"ID"	Integer,
	"Status"	INTEGER,
	"Number"	integer,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "EMPLOYEE" (
	"ID_EMPLOYEE"	integer,
	"Date_of_Birth"	datetime,
	"Phone_Number"	TEXT,
	"Position"	Text,
	"Name"	Text,
	PRIMARY KEY("ID_EMPLOYEE" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Voucher" (
	"ID_Voucher"	integer,
	"Percent"	INTEGER,
	"Expiry_date"	datetime,
	"Begin"	datetime,
	"End"	datetime,
	PRIMARY KEY("ID_Voucher")
);
CREATE TABLE IF NOT EXISTS "CUSTOMER" (
	"ID_CUSTOMER"	integer,
	"Name"	TEXT,
	"Date_of_Birth"	datetime,
	"Phone"	TEXT,
	"Point_Accumulation"	INTEGER,
	PRIMARY KEY("ID_CUSTOMER")
);
CREATE TABLE IF NOT EXISTS "GOODS" (
	"ID_Goods"	INTEGER,
	"Name"	Text,
	"Amount"	INTEGER,
	"Price"	INTEGER,
	"Exprite_date"	datetime,
	PRIMARY KEY("ID_Goods" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "BILL" (
	"ID_Bill"	INTEGER,
	"TOTAL"	INTEGER,
	"ID_EMPLOYEE"	INTEGER,
	"ID_CUSTOMER"	INTEGER,
	"ID_Voucher"	INTEGER,
	"ID_Table"	INTEGER,
	PRIMARY KEY("ID_Bill" AUTOINCREMENT),
	FOREIGN KEY("ID_EMPLOYEE") REFERENCES "EMPLOYEE"("ID_EMPLOYEE"),
	FOREIGN KEY("ID_EMPLOYEE") REFERENCES "Voucher"("ID_Voucher"),
	FOREIGN KEY("ID_CUSTOMER") REFERENCES "CUSTOMER"("ID_CUSTOMER"),
	FOREIGN KEY("ID_EMPLOYEE") REFERENCES "Table"("ID")
);
CREATE TABLE IF NOT EXISTS "Detail_Bill" (
	"ID_Product"	INTEGER,
	"ID_Bill"	INTEGER,
	"Amount"	INTEGER,
	CONSTRAINT "ID_DetailBill" PRIMARY KEY("ID_Product","ID_Bill"),
	FOREIGN KEY("ID_Product") REFERENCES "Product"("ID"),
	FOREIGN KEY("ID_Bill") REFERENCES "BILL"("ID_Bill")
);
COMMIT;
