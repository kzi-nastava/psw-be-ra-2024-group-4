/****** PAYMENTS MODULE ******/

DELETE FROM payments."PurchaseTokens";
DELETE FROM payments."OrderItems";
DELETE FROM payments."ShoppingCarts";

/* ShoppingCarts */

INSERT INTO payments."ShoppingCarts"(
	"Id", "UserId")
	VALUES (-1, -12);

INSERT INTO payments."ShoppingCarts"(
	"Id", "UserId")
	VALUES (-2, -13);

INSERT INTO payments."ShoppingCarts"(
	"Id", "UserId")
	VALUES (-3, -21);

/* OrderItems */

INSERT INTO payments."OrderItems"(
    "Id", "TourName", "Price", "TourId", "CartId")
VALUES 
    (-1, 'Tour A', 150.00, 1, -1),
    (-2, 'Tour B', 200.00, 2, -1),
    (-3, 'Tour C', 120.00, 3, -2),
    (-4, 'Tour D', 180.00, 4, -2),
    (-5, 'Tour E', 250.00, 5, -3);

/* PurchaseTokens */

INSERT INTO payments."PurchaseTokens"(
	"Id", "CartId", "UserId", "TourId")
	VALUES (-1, -1, -12, -1);

INSERT INTO payments."PurchaseTokens"(
	"Id", "CartId", "UserId", "TourId")
	VALUES (-2, -1, -12, -2);

INSERT INTO payments."PurchaseTokens"(
	"Id", "CartId", "UserId", "TourId")
	VALUES (-3, -2, -13, -3);
