INSERT INTO payments."Sales"(
    "Id", "StartDate", "EndDate", "DiscountPercentage", "AuthorId", "TourIds")
VALUES 
    (-1, '2024-12-30 12:00:00+00', '2024-12-31 12:00:00+00', 10.00, 1, ARRAY[1, 2, 3]::integer[]);
INSERT INTO payments."Sales"(
    "Id", "StartDate", "EndDate", "DiscountPercentage", "AuthorId", "TourIds")
VALUES 
    (-2, '2024-12-12 12:00:00+00', '2024-12-16 12:00:00+00', 15.00, 2, ARRAY[2, 4, 5]::integer[]);
INSERT INTO payments."Sales"(
    "Id", "StartDate", "EndDate", "DiscountPercentage", "AuthorId", "TourIds")
VALUES 
    (-3, '2024-12-10 12:00:00+00', '2024-12-15 12:00:00+00', 20.00, 1, ARRAY[1, 3, 4]::integer[]);
INSERT INTO payments."Sales"(
    "Id", "StartDate", "EndDate", "DiscountPercentage", "AuthorId", "TourIds")
VALUES 
    (-4, '2024-12-19 12:00:00+00', '2024-12-31 12:00:00+00', 25.00, 3, ARRAY[2, 3, 5]::integer[]);
INSERT INTO payments."Sales"(
    "Id", "StartDate", "EndDate", "DiscountPercentage", "AuthorId", "TourIds")
VALUES 
    (-5, '2024-12-10 12:00:00+00', '2024-12-16 12:00:00+00', 30.00, 2, ARRAY[1, 2, 5]::integer[]);
