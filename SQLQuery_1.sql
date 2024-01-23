CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Login" VARCHAR(200),
    "PasswordHash" VARCHAR(500)
);

CREATE TABLE "UserDetails" (
    "UserId" SERIAL PRIMARY KEY REFERENCES "Users"("Id"),
    "Nickname" VARCHAR(200) NOT NULL,
    "Bio" VARCHAR(500) NOT NULL
);

CREATE TABLE "Books" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL
);

CREATE TABLE "ReservedBooks" (
    "UserId" SERIAL REFERENCES "Users"("Id"),
    "BookId" SERIAL REFERENCES "Books"("Id"),
    "CreatedDate" TIMESTAMP WITH TIME ZONE,
    "UpdatedDate" TIMESTAMP WITH TIME ZONE,
    PRIMARY KEY ("UserId", "BookId")
);

INSERT INTO "Users" ("Login", "PasswordHash")
VALUES ('test-login', '121dasf1212e1fdasfsd');

SELECT * FROM "Users";

INSERT INTO "UserDetails" ("Nickname", "Bio")
VALUES ('test-nickname', 'test description about the user');

SELECT * FROM "UserDetails";

INSERT INTO "Books" ("Name")
VALUES ('test-book');

SELECT * FROM "Books";

INSERT INTO "ReservedBooks" ("UserId", "BookId", "CreatedDate", "UpdatedDate")
VALUES (1,1,CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

SELECT * FROM "ReservedBooks";

SELECT u."Id", ud."Nickname", rb."CreatedDate", b."Name" FROM "Users" u 
INNER JOIN "UserDetails" ud ON ud."UserId" = u."Id"
INNER JOIN "ReservedBooks" rb ON rb."UserId" = u."Id"
INNER JOIN "Books" b ON rb."BookId" = b."Id"
