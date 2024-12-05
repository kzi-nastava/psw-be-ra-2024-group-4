/****** BLOG MODULE ******/


DELETE FROM blog."Comments";
DELETE FROM blog."Posts";

/* Post */

INSERT INTO blog."Posts"(
    "Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES
    (-1, 'Exploring the Mountains', 'A journey through the beautiful mountains.', '2024-10-10 12:30:00+00', 'https://example.com/images/mountains.jpg', 0, -11, 0,
    '[{{"Value": 1, "UserId": 2, "CreatedAt": "2024-10-10T12:30:00+00"}}]');

INSERT INTO blog."Posts"(
    "Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES
    (-2, 'City Guide: Paris', 'An insider''s guide to discovering the hidden gems of Paris.', '2024-10-12 09:15:00+00', 'https://example.com/images/paris.jpg', 1, -12, 1,
    '[{{"Value": 1, "UserId": 2, "CreatedAt": "2024-10-10T12:30:00+00"}}]');

INSERT INTO blog."Posts"(
    "Id", "Title", "Description", "CreatedAt", "ImageUrl", "Status", "UserId", "RatingSum", "Ratings")
VALUES
    (-3, 'Discovering Local Cuisine', 'A culinary journey exploring traditional dishes.', '2024-10-11 11:20:00+00', 'https://example.com/images/cuisine.jpg', 2, -13, 3,
    '[{{"Value": 1, "UserId": 2, "CreatedAt": "2024-10-10T12:30:00+00"}}]');

/* Comments */

INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId","Username")
VALUES (-1, 'This is the first comment.', '2024-10-15 08:30:00+00', '2024-10-15 09:45:00+00', -21, -1, 'arijana');
INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId","Username")
VALUES (-2, 'Great post! I really enjoyed reading it.', '2024-10-15 09:15:00+00', '2024-10-15 09:45:00+00', -22, -1, 'anja');
INSERT INTO blog."Comments"(
	"Id", "Text", "CreatedAt", "UpdatedAt", "UserId", "PostId","Username")
VALUES (-3, 'Thanks for sharing this information.', '2024-10-15 10:00:00+00', '2024-10-15 10:45:00+00', -22, -1, 'milan');
