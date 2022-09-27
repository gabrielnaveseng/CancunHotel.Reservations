CREATE TABLE ReservationsDb.dbo.Reservations (
	SequentialId int IDENTITY(1,1) NOT NULL,
	Id uniqueidentifier NOT NULL,
	CreatedAt datetime NULL,
	UpdatedAt datetime NULL,
	StartReservationPeriod datetime NULL,
	EndReservationPeriod datetime NULL,
	ClientId uniqueidentifier NULL,
	IdcDelete bit NULL,
	CONSTRAINT PK__Reservat PRIMARY KEY (SequentialId),
	CONSTRAINT UQ__Reservat UNIQUE (Id)
);

CREATE UNIQUE NONCLUSTERED INDEX Index__Id ON ReservationsDb.dbo.Reservations (Id);
CREATE NONCLUSTERED INDEX Index_ClientId ON ReservationsDb.dbo.Reservations (ClientId);
CREATE NONCLUSTERED INDEX Index_Dates ON ReservationsDb.dbo.Reservations (StartReservationPeriod, EndReservationPeriod);