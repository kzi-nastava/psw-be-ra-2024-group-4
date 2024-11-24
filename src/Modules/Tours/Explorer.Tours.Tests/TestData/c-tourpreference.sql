INSERT INTO tours."TourPreferences"(
    "Id", "TouristId", "WeightPreference", "WalkingRating", "BikeRating", "CarRating", "BoatRating", "Tags")
VALUES (1, 1, 2, 2, 2, 2, 2, ARRAY[0, 1]::integer[]);
INSERT INTO tours."TourPreferences"(
    "Id", "TouristId", "WeightPreference", "WalkingRating", "BikeRating", "CarRating", "BoatRating", "Tags")
VALUES (2, 2, 3, 3, 3, 2, 2, ARRAY[0]::integer[]);