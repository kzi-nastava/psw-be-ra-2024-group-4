/****** BLOG MODULE ******/


DELETE FROM blog."Comments";
DELETE FROM blog."Posts";

/* Post */

INSERT INTO blog."Posts"(
	"Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES 
(222, 'Vikend u Beču', 'Šetnja kroz Schönbrunn, muzeje i najukusniju Sacher tortu ikada.', '2025-07-31 14:36:49.578+02', 
 'images/blogs/17dbf350-dea9-4c5c-8bf1-cd8207c1b98e.png', 1, 225, 0, 
 '[]');

INSERT INTO blog."Posts"(
	"Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES 
(223, 'Tura kroz Italiju', 'Od Venecije do Rima – iskustva, saveti i slike koje oduzimaju dah.', '2025-07-31 14:36:49.578+02', 
 'images/blogs/17dbf350-dea9-4c5c-8bf1-cd8207c1b98e.png', 1, 225, 0, 
 '[]');

INSERT INTO blog."Posts"(
	"Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES 
(224, 'Odmor na Zakintosu', 'Plaže, brodovi i sunce – idealan letnji beg iz svakodnevice.', '2025-07-31 14:36:49.578+02', 
 'images/blogs/17dbf350-dea9-4c5c-8bf1-cd8207c1b98e.png', 1, 225, 0, 
 '[]');

INSERT INTO blog."Posts"(
	"Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES 
(225, 'Planinarenje po Alpima', 'Uspon do vrha, noćenje u planinarskim domovima i saveti za opremu.', '2025-07-31 14:36:49.578+02', 
 'images/blogs/17dbf350-dea9-4c5c-8bf1-cd8207c1b98e.png', 1, 225, 0, 
 '[]');



/* Comments */

INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId", "Username")
VALUES 
(222, 'Divne slike iz Beča! Posebno mi se dopala priča o muzeju.', '2025-07-31 14:52:07.478+02', '2025-07-31 14:52:07.478+02', 222, 222, 'turista1');

INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId", "Username")
VALUES 
(223, 'Planiram uskoro da idem u Italiju, ovaj tekst mi je baš pomogao!', '2025-07-31 14:52:07.478+02', '2025-07-31 14:52:07.478+02', 222, 223, 'turista1');

INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId", "Username")
VALUES 
(224, 'Zakintos izgleda prelepo, da li imaš preporuku za smeštaj?', '2025-07-31 14:52:07.478+02', '2025-07-31 14:52:07.478+02', 222, 224, 'turista1');

INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId", "Username")
VALUES 
(225, 'Svaka čast na usponu po Alpima, slike su fantastične!', '2025-07-31 14:52:07.478+02', '2025-07-31 14:52:07.478+02', 222, 225, 'turista1');

