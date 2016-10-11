<Query Kind="Statements">
  <Connection>
    <ID>17bc5634-44bb-48fd-90f5-db9969c64834</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Find the waiter with the most bills

// a) get a list of bill counts by waiter and determine the max
var maxbillcount = (from x in Waiters
					select x.Bills.Count()).Max();
					
// b) using the maxbillcount on the where clause, find
//    the waiter that matches the count
var BestWaiter = from x in Waiters
				where x.Bills.Count() == maxbillcount
				select new{
					Name = x.FirstName + " " + x.LastName
				};
BestWaiter.Dump();
				
//create a dataset that has an unknown number of records
// associate with a data value. 
//A list of all bills associated with the waiter. List all waiters.
//The inner nested query uses the associated Bill records
//  of the currently processing Waiter --> x.Collection
var waiterbills = from x in Waiters
					orderby x.LastName, x.FirstName
					select new {
							Name = x.LastName + ", " + x.FirstName,
							TotalBillCount = x.Bills.Count(),
							BillInfo = (from y in x.Bills
										where y.BillItems.Count() > 0
										select new {
											BillId = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
													}
										)
								};
waiterbills.Dump();
					