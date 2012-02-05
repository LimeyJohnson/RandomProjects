connect 'jdbc:derby://localhost:1527/myDB;create=true;user=me;password=mine';
insert into restaurants values (4, 'Grande Burrito', 'Oakland');
update restaurants set city = 'Ukiah' where name = 'Irifunes';
select * from restaurants;
disconnect;
exit;