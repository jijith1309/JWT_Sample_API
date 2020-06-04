select * from applicationusers
-- delete from applicationusers

select * from PRODUCTS
select * from shoppingcarts
select * from cartitems

select * from orders
select * from orderdetails

INSERT INTO PRODUCTS(category,Name,Price,Description,Quantity,LastUpdatedOn,ProductImagePath)
VALUES('NonVegetarian','Shawarma',200,'Spicy Shawarma',5,NOW(),'https://www.thespruceeats.com/thmb/qWxT_QU0PlqphVPfgMtfQjSKPO0=/1867x1867/smart/filters:no_upscale()/chicken-shawarma-138740297-58260f943df78c6f6acabb5b.jpg');