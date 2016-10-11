<Query Kind="Statements">
  <Connection>
    <ID>55ecb918-45ca-426c-bd18-f56c606b274b</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//a statement has a receiving variable which is set
//by the result of a query

//when you need to use multiple steps
//to solve a problem, switch your Language
//choice to either Statement(s) or Program

var maxcount = (from x in MediaTypes
				select x.Tracks.Count()).Max();

//to display the contents of a variable in LinqPad
// you use the method .Dump()
maxcount.Dump();

//to filter data you can use the Where clause
//uses a previously creae variable value in
// a following statement
var mediatypecounts = from x in MediaTypes
				where x.Tracks.Count() == maxcount 
				select new{
					Name = x.Name,
					TrackCount = x.Tracks.Count()
				};
mediatypecounts.Dump();

//Can this set of statements be written as one complete query?
//the answer: possibly; and in this case yes
//In this example maxcount could be exchanged for the query that
//  actually created the value in the first place
//This subsitution query is a nested query (subquery)
//The nested query needs its on instance indentifier
var mediatypecountsNested = from x in MediaTypes
				where x.Tracks.Count() == (from y in MediaTypes
										select y.Tracks.Count()).Max() 
				select new{
					Name = x.Name,
					TrackCount = x.Tracks.Count()
				};
mediatypecountsNested.Dump();

//using a method syntax to determine the count value for the where expression.
//this demonstrates that queries can be constructed using
//  both query syntax and method syntax
var mediatypecountsMethod = from x in MediaTypes
				where x.Tracks.Count() == 
						MediaTypes.Select(y => y.Tracks.Count()).Max() 
				select new{
					Name = x.Name,
					TrackCount = x.Tracks.Count()
				};
mediatypecountsMethod.Dump();