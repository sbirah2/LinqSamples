<Query Kind="Expression">
  <Connection>
    <ID>8751cd65-60c5-4775-94a1-1328b8a2efd6</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from x in Items
where x.CurrentPrice > 5.00m
select new{
		x.Description,
		x.Calories
}