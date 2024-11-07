INSERT INTO tours."Tour" (
    "Id", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "UserId", "LengthInKm", "PublishedTime", "ArchiveTime", "EquipmentIds"
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
    5.0,  
    '2024-10-14 23:53:24.948+02', 
    '2024-10-14 23:53:24.948+02',  
    ARRAY[]::integer[] 
);

INSERT INTO tours."Tour" (
    "Id", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "UserId", "LengthInKm", "PublishedTime", "ArchiveTime", "EquipmentIds"
)
VALUES (
    -2,
    'Mountain Adventure',
    'A challenging tour through the high mountains, suitable for experienced hikers.',
    'Hard',
    ARRAY[2, 3, 5]::integer[], 
    1,
    0,  
    2, 
    10.0,  
    '2024-10-14 23:53:24.948+02',  
    '2024-10-14 23:53:24.948+02',  
    ARRAY[-3, -1]::integer[]  
);

INSERT INTO tours."Tour" (
    "Id", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "UserId", "LengthInKm", "PublishedTime", "ArchiveTime", "EquipmentIds"
)
VALUES (
    -4,  
    'Mountain Challenge',
    'An adventurous hike through the mountain trails, perfect for thrill-seekers.',
    'Hard',
    ARRAY[2, 3, 5]::integer[], 
    2,
    100.0,  
    2, 
    15.0,  
    '2024-10-14 23:53:24.948+02',  
    '2024-10-14 23:53:24.948+02',  
    ARRAY[-3, -1]::integer[]   
);

