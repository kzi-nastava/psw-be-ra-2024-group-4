INSERT INTO payments."OrderItems"(
    "Id", "TourName", "Price", "TourId", "CartId", "IsBundle")
VALUES 
    (-1, 'Tour A', 150.00, 1, -1, FALSE),
    (-2, 'Tour B', 200.00, 2, -1, FALSE),
    (-3, 'Tour C', 120.00, 3, -2, FALSE),
    (-4, 'Tour D', 180.00, 4, -2, FALSE),
    (-5, 'Tour E', 250.00, 5, -3, FALSE);