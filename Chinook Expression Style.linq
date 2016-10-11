<Query Kind="Expression">
  <Connection>
    <ID>55ecb918-45ca-426c-bd18-f56c606b274b</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

/*
Linq expressions, statements, programs are written uswing C# syntax
*/

//query syntax list all records from entity
from x in Artists
select x

//method syntax list all records from entity
Artists.Select(x => x)

//sort albums by releasedate (most current) by title
from x in Albums
orderby x.ReleaseYear descending, x.Title
select x

//list all albums belonging to artists
//the select is obtaining a subset of attributes from
//the choosen tables
//the new { } is called an anonymous data set
//anonymous datasets are IOrderedQueryable<>
from x in Albums
select new{
			x.Artist.Name,
			x.Title
			}
//list all albums belonging to artists where a condition exists
//find albums released in a particular year
from x in Albums
where x.ReleaseYear == 2008
orderby x.Artist.Name, x.Title
select new{
			x.Artist.Name,
			x.Title
			}