CREATE TABLE LEAVE_TYPE (ID INT IDENTITY(1,1), LEAVE_TYPE VARCHAR(200), SYS_DATE DATETIME)
INSERT INTO LEAVE_TYPE (LEAVE_TYPE,SYS_DATE)VALUES('BONUS LEAVE',GETDATE())

SELECT LEAVE_TYPE FROM LEAVE_TYPE