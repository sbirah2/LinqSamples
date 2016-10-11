<Query Kind="Statements">
  <Connection>
    <ID>8751cd65-60c5-4775-94a1-1328b8a2efd6</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Waiters
var results = from x in Waiters
where x.FirstName.Contains("a")
orderby x.LastName 
select x.LastName + ", " + x.FirstName;
results.Dump();