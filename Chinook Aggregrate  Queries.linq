<Query Kind="Expression">
  <Connection>
    <ID>03e2069a-f1a2-419a-8737-9f7911297712</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//this sample requires a subset of the entity record
//the data needs to be filter for specific select thus a Where is needed
//Using the navigation name on Customer, one can access the
//    associated Employee record
//reminder: this is C# syntax and thus appropriate methods can be used .Equals()
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane")
 	&&	x.SupportRepIdEmployee.LastName.Equals("Peacock")
orderby x.LastName, x.FirstName
select new {
		Name = x.LastName + ", " + x.FirstName,
		City = x.City,
		State = x.State,
		Phone = x.Phone,
		Email = x.Email}
		
//for aggregrates it is best to consider doing parent child direction
//aggregrates are used against collections (multiple records)
//null error could occur if a collection is empty for specific aggregrate(s)
//   such as Sum() thus your may need to filter (Where) certain
//   records from your query

//Aggregrates
//Count() count the number of instances of the collection referenced
//Sum() totals a specific field/expression, thus you will likely need to use
//   a delegate to indicate the collection instance attribute to be used
//Average() Averages a specific field/expression, thus you will likely need to use
//   a delegate to indicate the collection instance attribute to be used
from x in Albums
where x.Tracks.Count() > 0
orderby x.Tracks.Count(), x.Title
select new{
		Title = x.Title,
		TotalTracksforAlbum = x.Tracks.Count(),
		TotalPriceForalbumtracks = x.Tracks.Sum(y => y.UnitPrice),
		AverageTrackLengthA = x.Tracks.Average(y => y.Milliseconds)/1000,
		AverageTrackLengthB = x.Tracks.Average(y => y.Milliseconds/1000)
}

//To get both the albums with tracks and without tracks you can use a .Union()
//In a union you need to ensure cast typing is correct and columns cast types match identically
//Since Average returns a double, the division in the first part of the union needs
//   to be done as Double, therefore the value 1000 (int) is properly set up as a double.
//Note the sorting is as method syntax on the Union
//
// (query1).Union(query2).Union(queryn).OrderBy(first sort).ThenBy(nth sort)
(from x in Albums
	where x.Tracks.Count() > 0
	select new{
		Title = x.Title,
		TotalTracksforAlbum = x.Tracks.Count(),
		TotalPriceForalbumtracks = x.Tracks.Sum(y => y.UnitPrice),
		AverageTrackLengthA = x.Tracks.Average(y => y.Milliseconds)/1000.0,
		AverageTrackLengthB = x.Tracks.Average(y => y.Milliseconds/1000.0)
		}
).Union(
	from x in Albums
	where x.Tracks.Count() == 0
	select new{
			Title = x.Title,
			TotalTracksforAlbum = 0,
			TotalPriceForalbumtracks = 0.00m,
			AverageTrackLengthA = 0.00,
			AverageTrackLengthB = 0.00
		}
).OrderBy(y => y.TotalTracksforAlbum).ThenBy(y => y.Title)