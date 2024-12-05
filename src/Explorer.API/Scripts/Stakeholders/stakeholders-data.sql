/****** STAKEHOLDERS MODULE ******/

DELETE FROM stakeholders."Notification";
DELETE FROM tours."KeyPoints";
DELETE FROM stakeholders."Problem";
DELETE FROM stakeholders."ClubJoinRequests";
DELETE FROM stakeholders."ClubInvitations";
DELETE FROM stakeholders."Clubs";
DELETE FROM stakeholders."AppReviews";
DELETE FROM stakeholders."People";
DELETE FROM stakeholders."Users";
DELETE FROM tours."Objects";

/* Users */

INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES (-1, 'admin@gmail.com', '12345678', 0, true),
       (-2, 'korisnik@gmail.com', '12345678', 1, true),
       (-3, 'masa@gmail.com', '12345678', 1, true);

INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES ( 1001, 'autor1@gmail.com', '12345678', 1, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES (-12, 'autor2@gmail.com', '12345678', 1, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES (-13, 'autor3@gmail.com', '12345678', 1, true);

INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES (-21, 'turista1@gmail.com', '12345678', 2, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES (-22, 'turista2@gmail.com', '12345678', 2, true);
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive")
VALUES (-23, 'turista3@gmail.com', '12345678', 2, true);

/* People */
INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ImageUrl", "Biography", "Motto", "Equipment", "Wallet")
VALUES (1001, 1001, 'Ana', 'Anić', 'autor1@gmail.com','https1','KaoJa', 'Samo Jako Bro', ARRAY[1, 2]::integer[], 1000);
INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ImageUrl", "Biography", "Motto", "Equipment", "Wallet")
VALUES 
(-12, -12, 'Lena', 'Lenić', 'autor2@gmail.com', 'profilepic2.jpg', 'Lena loves to write about travel.', 'Explore the world.', ARRAY[1, 2]::integer[], 1000);
INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ImageUrl", "Biography", "Motto", "Equipment", "Wallet")
VALUES 
(-13, -13, 'Sara', 'Sarić', 'autor3@gmail.com', 'profilepic3.jpg', 'Sara is a creative writer.', 'Imagination is key.', ARRAY[1, 2]::integer[], 1000);
INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ImageUrl", "Biography", "Motto", "Equipment", "Wallet")
VALUES 
(-21, -21, 'Pera', 'Perić', 'turista1@gmail.com', 'profilepic4.jpg', 'Pera enjoys discovering new places.', 'Adventure awaits.', ARRAY[1, 2]::integer[], 1000);
INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ImageUrl", "Biography", "Motto", "Equipment", "Wallet")
VALUES 
(-22, -22, 'Mika', 'Mikić', 'turista2@gmail.com', 'profilepic5.jpg', 'Mika is an avid traveler.', 'Travel more, worry less.', ARRAY[1, 2]::integer[], 1000);
INSERT INTO stakeholders."People"(
    "Id", "UserId", "Name", "Surname", "Email", "ImageUrl", "Biography", "Motto", "Equipment", "Wallet")
VALUES 
(-23, -23, 'Steva', 'Stević', 'turista3@gmail.com', 'profilepic6.jpg', 'Steva enjoys nature and hiking.', 'Nature is the best teacher.', ARRAY[1, 2]::integer[], 1000);


/* AppReviews */

INSERT INTO stakeholders."AppReviews"(
    "Id", "UserId", "Grade", "Comment", "CreationTime")
VALUES (-1, -1, 5, 'odlicna aplikacija', '2024-10-14 13:26:04.723');
INSERT INTO stakeholders."AppReviews"(
    "Id", "UserId", "Grade", "Comment", "CreationTime")
VALUES (-2, -2, 4, 'dodati jos funkcionalnosti', '2024-10-12 15:26:04.723');

/* Clubs */

INSERT INTO stakeholders."Clubs"(
    "Id", "Name", "Description", "Image", "UserId", "UserIds")
VALUES 
(-1, 'Mountaineering Society of Vojvodina', 'A group for everyone who wants to try new activities', 'image', -1,ARRAY[21, 23]::integer[]);

INSERT INTO stakeholders."Clubs"(
    "Id", "Name", "Description", "Image", "UserId", "UserIds")
VALUES 
(-2, 'Cycling Across Austria', 'A group for experienced cyclists, ready for long routes through beautiful Austria', 'image2', -11,ARRAY[21, 23]::integer[]);


/* ClubInvitations */

INSERT INTO stakeholders."ClubInvitations"(
    "Id", "ClubId", "MemberId", "Status")
VALUES (-1,-1, 1, 0);
INSERT INTO stakeholders."ClubInvitations"(
    "Id", "ClubId", "MemberId", "Status")
VALUES (-2, -2, 2, 0);
INSERT INTO stakeholders."ClubInvitations"(
    "Id", "ClubId", "MemberId", "Status")
VALUES (-3, -2, 3, 0);  


/* ClubJoinRequests */

INSERT INTO stakeholders."ClubJoinRequests"(
	"Id", "UserId", "ClubId", "Status")
	VALUES (1, 1, 1, 0);
INSERT INTO stakeholders."ClubJoinRequests"(
	"Id", "UserId", "ClubId", "Status")
	VALUES (2, 1, 2, 1);
INSERT INTO stakeholders."ClubJoinRequests"(
	"Id", "UserId", "ClubId", "Status")
	VALUES (3, 2, 1, 2);
INSERT INTO stakeholders."ClubJoinRequests"(
	"Id", "UserId", "ClubId", "Status")
	VALUES (4, 2, 1, 2);


/* Problems */

INSERT INTO stakeholders."Problem"(
    "Id", "UserId", "TourId", "Category", "Description", "Priority", "Time","IsActive","Deadline","Comments")
	VALUES (-2, 2, 2, 'promena dana', 'pomeranje termina sa 3.10 na 4.10', 7, '2024-10-16 12:00:00', true,0,'[]'::jsonb);

INSERT INTO stakeholders."Problem"(
    "Id", "UserId", "TourId", "Category", "Description", "Priority", "Time", "IsActive","Deadline","Comments")
	VALUES (-3, 2, 2, 'promena satnice', 'odlaganje pokreta za 2 sata', 2, '2024-11-16 15:00:00', true,4,'[]'::jsonb);

INSERT INTO stakeholders."Problem"(
    "Id", "UserId", "TourId", "Category", "Description", "Priority", "Time","IsActive","Deadline","Comments")
	VALUES (-4, 1, 1, 'promena trajanja ture', 'skratiti turu za 1 dan', 9, '2024-12-16 11:00:00', true,3,'[]'::jsonb);



/* Notifications */

INSERT INTO stakeholders."Notification"(
    "Id", "Description", "CreationTime", "IsRead", "UserId", "NotificationsType", "ResourceId")
    VALUES (-1, 'E imas novi problemvau', '2024-10-12 15:26:04.723', true, -11, 0, -2);
INSERT INTO stakeholders."Notification"(
    "Id", "Description", "CreationTime", "IsRead", "UserId", "NotificationsType", "ResourceId")
    VALUES (-2, 'E imas novi problem', '2024-10-12 15:26:04.723', true, -11, 0, -3);
INSERT INTO stakeholders."Notification"(
    "Id", "Description", "CreationTime", "IsRead", "UserId", "NotificationsType", "ResourceId")
    VALUES (-3, 'E imas novi problem cao', '2024-10-12 15:26:04.723', false, -11, 0, -4);