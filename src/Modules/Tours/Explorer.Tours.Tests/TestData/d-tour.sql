INSERT INTO tours."Tour" (
    "Id", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "UserId", "EquipmentIds", "KeyPointIds"
)
VALUES (
    -1,
    'Amazing City Tour',
    'Explore the historical landmarks of the city with guided insights.',
    'Easy',
    ARRAY[6, 7, 13]::integer[], 
    0,
    0,
    1, 
    ARRAY[]::integer[], 
    ARRAY[]::integer[]
);

INSERT INTO tours."Tour" (
    "Id", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "UserId", "EquipmentIds", "KeyPointIds"
)
VALUES (
    -2,
    'Mountain Adventure',
    'A challenging tour through the high mountains, suitable for experienced hikers.',
    'Hard',
    ARRAY[2, 3, 5]::integer[], 
    0,
    0,
    2, 
    ARRAY[-3, -1]::integer[], 
    ARRAY[]::integer[]
);