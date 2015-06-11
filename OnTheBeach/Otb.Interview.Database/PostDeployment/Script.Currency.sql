set identity_insert Currency on

insert into Currency (id, unit, conversion_factor)
values
	(1, 'GBP', 1),
	(2, 'USD', 1.54),
	(3, 'Rocks', 10),
	(4, 'Sweets', 12),
	(5, 'Credits', 8000)

set identity_insert Currency off