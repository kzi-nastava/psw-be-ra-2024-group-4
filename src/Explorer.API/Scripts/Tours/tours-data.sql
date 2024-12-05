/****** TOURS MODULE ******/

DELETE FROM tours."TourExecution";
DELETE FROM tours."TourReview";
DELETE FROM tours."KeyPoints";
DELETE FROM tours."TourPreferences";
DELETE FROM tours."Tour";
DELETE FROM tours."Positions";
DELETE FROM tours."Equipment";
DELETE FROM tours."Objects";

/* Objects */
INSERT INTO tours."Objects"(
    "Id", "Name", "Description", "Image", "Category", "Longitude", "Latitude", "UserId", "PublicStatus")
VALUES (-1, 'Restoran kod Miloša', 'Tradicionalni srpski restoran poznat po specijalitetima sa roštilja.', 'restoran_milos.jpg', 1, 20.457273, 44.817613, 1001, 0);
INSERT INTO tours."Objects"(
    "Id", "Name", "Description", "Image", "Category", "Longitude", "Latitude", "UserId", "PublicStatus")
VALUES (-2, 'Parking Slavija', 'Javni parking sa preko 200 mesta u centru Beograda.', 'parking_slavija.jpg', 1, 20.467327, 44.799935, 1002, 0);
INSERT INTO tours."Objects"(
    "Id", "Name", "Description", "Image", "Category", "Longitude", "Latitude", "UserId", "PublicStatus")
VALUES (-3, 'WC Kalemegdan', 'Javni toalet na ulazu u park Kalemegdan.', 'wc_kalemegdan.jpg', 1, 20.448921, 44.823292, 1003, 0);


/* Equipment */

INSERT INTO tours."Equipment"(
    "Id", "Name", "Description")
VALUES (-1, 'Voda', 'Količina vode varira od temperature i trajanja ture. Preporuka je da se pije pola litre vode na jedan sat umerena fizičke aktivnosti (npr. hajk u prirodi bez značajnog uspona) po umerenoj vrućini');
INSERT INTO tours."Equipment"(
    "Id", "Name", "Description")
VALUES (-2, 'Štapovi za šetanje', 'Štapovi umanjuju umor nogu, pospešuju aktivnost gornjeg dela tela i pružaju stabilnost na neravnom terenu.');
INSERT INTO tours."Equipment"(
    "Id", "Name", "Description")
VALUES (-3, 'Obična baterijska lampa', 'Baterijska lampa od 200 do 400 lumena.');

/* Positions */

INSERT INTO tours."Positions"(
	"Id", "Longitude", "Latitude", "TouristId")
	VALUES (-1, 41, 21, 1);

INSERT INTO tours."Positions"(
	"Id", "Longitude", "Latitude", "TouristId")
	VALUES (-2, 42, 22, 2);

INSERT INTO tours."Positions"(
	"Id", "Longitude", "Latitude", "TouristId")
	VALUES (-3, 43, 23, 3);

/* Tour */

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
     1001, 
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
     1001, 
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
     1001, 
    15.0,  
    '2024-10-14 23:53:24.948+02',  
    '2024-10-14 23:53:24.948+02',  
    ARRAY[-3, -1]::integer[]   
);
INSERT INTO tours."Tour" (
    "Id", "Name", "Description", "Difficulty", "Tags", "Status", "Price", "UserId", "LengthInKm", "PublishedTime", "ArchiveTime", "EquipmentIds"
)
VALUES (
    -5,  
    'Safari Challenge',
    'PERFECT SAFARI TOUR',
    'Hard',
    ARRAY[2, 3, 5]::integer[], 
    2,
    100.0,  
    1001, 
    15.0,  
    '2024-10-14 23:53:24.948+02',  
    '2024-10-14 23:53:24.948+02',  
    ARRAY[-3, -1]::integer[]
);


/* TourPreferences */

INSERT INTO tours."TourPreferences"(
    "Id", "TouristId", "WeightPreference", "WalkingRating", "BikeRating", "CarRating", "BoatRating", "Tags")
VALUES (1, 1, 2, 2, 2, 2, 2, '{{"creativity"}}');
INSERT INTO tours."TourPreferences"(
    "Id", "TouristId", "WeightPreference", "WalkingRating", "BikeRating", "CarRating", "BoatRating", "Tags")
VALUES (2, 2, 3, 3, 3, 2, 2, '{{"scary"}}');

/* KeyPoints */

INSERT INTO tours."KeyPoints"(
	"Id", "Name", "Longitude", "Latitude", "Description", "Image", "UserId", "TourId", "PublicStatus")
	VALUES (-1, 'Colosseum, Rome', 12.4922, 41.8902, 'It is located just a few hundred meters away from the city center, at the northernmost point of Rome historic center.', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPd_FbzQUocKtjn8O2DFiQIrVQcZQgP5BU1MWzaDSMrw&s', 2, -1, 0);

INSERT INTO tours."KeyPoints"(
	"Id", "Name", "Longitude", "Latitude", "Description", "Image", "UserId", "TourId", "PublicStatus")
	VALUES (-2, 'Chiesa di Sant Ignazio di Loyola, Rome', 12.4797, 41.8992,  'The Church of St. Ignatius of Loyola at Campus Martius is a Latin Catholic titular church, of deaconry rank, dedicated to Ignatius of Loyola, the founder of the Society of Jesus, located in Rome, Italy.', 'https://d1c233nw6edifh.cloudfront.net/wp-content/uploads/sites/60/2018/06/volta-chiesa-sant-ignazio-roma.jpg', 2, -2,0);

INSERT INTO tours."KeyPoints"(
	"Id", "Name", "Longitude", "Latitude", "Description", "Image", "UserId", "TourId", "PublicStatus")
	VALUES (-3, 'Sistine Chapel, Vatican City', 12.4545, 41.9029,  'Sistine Chapel is a large historic chapel and one of the papal facilities located in Vatican City and famous with its amazing frescoes made by Michelangelo and other artists.', 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/Sistina-interno.jpg/640px-Sistina-interno.jpg', 2, -1,0);


/* PublicKeypointRequests */

INSERT INTO tours."KeyPoints"(
	"Id", "Name", "Longitude", "Latitude", "Description", "Image", "UserId", "TourId", "PublicStatus")
	VALUES (-4, 'Sistine Chapel, Vatican City', 12.4545, 41.9029,  'Sistine Chapel is a large historic chapel and one of the papal facilities located in Vatican City and famous with its amazing frescoes made by Michelangelo and other artists.', 'https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/Sistina-interno.jpg/640px-Sistina-interno.jpg', 4, -1,0);
INSERT INTO tours."Objects"(
    "Id", "Name", "Description", "Image", "Category", "Longitude", "Latitude", "UserId", "PublicStatus")
	VALUES (-5, 'WC Kalemegdan', 'Javni toalet na ulazu u park Kalemegdan.', 'wc_kalemegdan.jpg', 1, 20.448921, 44.823292, 1003, 0);

/* TourReviews */

INSERT INTO tours."TourReview"(
	"Id", "IdTour", "IdTourist", "Rating", "Comment", "DateTour", "DateComment", "Images")
	VALUES (-1, 1, 1, 5, 'Mnogo dobra tura.', '2024-10-14 23:53:24.948+02', '2024-10-14 23:53:24.948+02', ARRAY[]::integer[]);
INSERT INTO tours."TourReview"(
	"Id", "IdTour", "IdTourist", "Rating", "Comment", "DateTour", "DateComment", "Images")
	VALUES (-2, 1, 1, 4, 'Dobra tura.', '2024-10-14 23:53:24.948+02', '2024-10-14 23:53:24.948+02', ARRAY[]::integer[]);
INSERT INTO tours."TourReview"(
	"Id", "IdTour", "IdTourist", "Rating", "Comment", "DateTour", "DateComment", "Images")
	VALUES (-3, 1, 1, 1, 'Mnogo losa tura.', '2024-10-14 23:53:24.948+02', '2024-10-14 23:53:24.948+02', ARRAY[]::integer[]);

/* TourExecutions */

INSERT INTO tours."TourExecution"(
    "Id", "TourId", "TouristId", "LocationId", "LastActivity", "Status", "CompletedKeys")
VALUES 
    (-1, -1, -22, -1, '2024-10-14 23:53:24.948+02', 0, 
        '[]');

INSERT INTO tours."TourExecution"(
    "Id", "TourId", "TouristId", "LocationId", "LastActivity", "Status", "CompletedKeys")
VALUES 
    (-2, -1, -21, -2, '2024-10-14 23:53:24.948+02', 1, 
        '[]');

INSERT INTO tours."TourExecution"(
    "Id", "TourId", "TouristId", "LocationId", "LastActivity", "Status", "CompletedKeys")
VALUES 
    (-3, -2, -23, -1, '2024-10-14 23:53:24.948+02', 0, 
        '[]');

INSERT INTO tours."TourExecution"(
    "Id", "TourId", "TouristId", "LocationId", "LastActivity", "Status", "CompletedKeys")
VALUES 
    (-5, -2, -23, -1, '2024-10-14 23:53:24.948+02', 0, 
        '[]');

