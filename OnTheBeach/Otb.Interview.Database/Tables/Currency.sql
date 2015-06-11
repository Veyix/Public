create table Currency (
	id					int				identity (1, 1) not null,
	unit				nvarchar(255)	not null,
	conversion_factor	decimal(8, 2)	not null
)