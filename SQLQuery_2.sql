SELECT * FROM "OpenLoops"
WHERE "Note" = 'Test note'

INSERT INTO "OpenLoops" ("Id", "Note", "CreatedDate", "UserId")
VALUES (gen_random_uuid(), 'Test note' || gen_random_uuid(), CURRENT_TIMESTAMP, 1)

INSERT INTO "Users" ("Login", "CreatedDate")
VALUES ('test@mail.ru', CURRENT_TIMESTAMP)

DECLARE i int :=0;
FOR i IN 1..10 LOOP
    SELECT gen_random_uuid()
END LOOP;

DO
$do$
BEGIN
    FOR i in 1..100000 LOOP
        INSERT INTO "OpenLoops" ("Id", "Note", "CreatedDate", "UserId")
        VALUES (gen_random_uuid(), 'Test note ' || gen_random_uuid(), CURRENT_TIMESTAMP, 1);
    END LOOP;
END
$do$;

SELECT COUNT(1) from "OpenLoops" ol

SELECT * FROM "OpenLoops" ol
OFFSET 10000
limit 100

SELECT COUNT(1) FROM "Users" u

SELECT 'test' || 2 || '@mail.ru'

DO
$do$
BEGIN
    FOR i in 1..1000 LOOP
        INSERT INTO "Users" ("Login", "CreatedDate")
        VALUES ('test' || i || '@mail.ru', CURRENT_TIMESTAMP);
    END LOOP;
END
$do$;

DO
$do$
BEGIN
    FOR user_id in 2..5 LOOP
        FOR i in 1..100000 LOOP
            INSERT INTO "OpenLoops" ("Id", "Note", "CreatedDate", "UserId")
            VALUES (gen_random_uuid(), 'Test note ' || gen_random_uuid(), CURRENT_TIMESTAMP, user_id);
        END LOOP;
    END LOOP;
END
$do$;

 SELECT COUNT(1) from "OpenLoops" ol
 WHERE "UserId" = 3

