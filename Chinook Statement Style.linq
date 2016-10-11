<Query Kind="Statements">
  <Connection>
    <ID>55ecb918-45ca-426c-bd18-f56c606b274b</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//use of the statement environment allows for C# type commands
//you can have local variables
//you can have multiple statements in your execution
//to display the contents of a variable you will use
//      the LinqPad method .Dump()
var theresults = from x in Albums
where x.ReleaseYear == 2008
orderby x.Artist.Name, x.Title
select new{
			x.Artist.Name,
			x.Title
			};
theresults.Dump();

//list all albums which contains the string "son"
//consider using the method .Contains()
var theList = from x in Albums
where x.Title.Contains("son")
select x;
theList.Dump();