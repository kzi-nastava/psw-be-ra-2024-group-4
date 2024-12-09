INSERT INTO payments."Bundles"(
	"Id", "Name", "Price", "Status", "TourIds", "AuthorId")
	VALUES (-1, 'Divlji zapad', 350.00, 1, ARRAY[1, 2, 3]::integer[], 1);

INSERT INTO payments."Bundles"(
	"Id", "Name", "Price", "Status", "TourIds", "AuthorId")
	VALUES (-2, 'Bliski istok', 250.00, 1, ARRAY[4, 5, 6]::integer[], 1);