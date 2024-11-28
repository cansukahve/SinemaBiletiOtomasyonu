USE SinemaDatabase;
GO

-- Insert sample movies
INSERT INTO Movies (MovieName, ShowTime, Price, PosterPath)
VALUES 
    ('Inception', '2024-01-20 14:00:00', 50.00, 'inception.jpg'),
    ('The Dark Knight', '2024-01-20 16:30:00', 45.00, 'dark_knight.jpg'),
    ('Interstellar', '2024-01-20 19:00:00', 55.00, 'interstellar.jpg'),
    ('The Matrix', '2024-01-20 21:30:00', 40.00, 'matrix.jpg');

-- For each movie, create 50 seats (5 rows x 10 seats)
DECLARE @MovieID INT
DECLARE @Row CHAR(1)
DECLARE @SeatNum INT

DECLARE movie_cursor CURSOR FOR 
SELECT MovieID FROM Movies

OPEN movie_cursor
FETCH NEXT FROM movie_cursor INTO @MovieID

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Row = 'A'
    WHILE ASCII(@Row) <= ASCII('E')
    BEGIN
        SET @SeatNum = 1
        WHILE @SeatNum <= 10
        BEGIN
            INSERT INTO Seats (MovieID, SeatNumber, IsOccupied)
            VALUES (@MovieID, @Row + CAST(@SeatNum AS VARCHAR), 0)
            SET @SeatNum = @SeatNum + 1
        END
        SET @Row = CHAR(ASCII(@Row) + 1)
    END
    FETCH NEXT FROM movie_cursor INTO @MovieID
END

CLOSE movie_cursor
DEALLOCATE movie_cursor
