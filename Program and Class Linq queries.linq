<Query Kind="Program">
  <Connection>
    <ID>8751cd65-60c5-4775-94a1-1328b8a2efd6</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	// a list of bill counts for all waiters
	//This query will create a flat dataset
	//The columns are native data types (ie int, string,....)
	//One is not concerned with repeated data in a column
	//Instead of using an anonymous datatype (new{.....})
	//we wish to use a defined class definition
	var BestWaiter = from x in Waiters
				select new WaiterBillCounts{
					Name = x.FirstName + " " + x.LastName,
					TCount = x.Bills.Count()
				};
	BestWaiter.Dump();
	var paramMonth = 4;
	var paramYear = 2014;
	var waiterbills = from x in Waiters
					where x.LastName.Contains("k")
					orderby x.LastName, x.FirstName
					select new WaiterBills{
							Name = x.LastName + ", " + x.FirstName,
							TotalBillCount = x.Bills.Count(),
							BillInfo = (from y in x.Bills
										where y.BillItems.Count() > 0
										&& y.BillDate.Month == DateTime.Today.Month - paramMonth
										&& y.BillDate.Year == paramYear
										select new BillItemSummary{
											BillId = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
													}
										).ToList()
								};
	waiterbills.Dump();
}

// Define other methods and classes here
//An example of a POCO class (flat)
public class WaiterBillCounts
{
	//whatever recieving field on your query in your Select
	//appears as a property in this class
	public string Name{get;set;}
	public int TCount{get;set;}
}

public class BillItemSummary
{
	public int BillId{get;set;}
	public DateTime BillDate{get;set;}
	public int? TableID{get;set;}
	public decimal Total{get;set;}
}

//An example of a DTO class (structured)

public class WaiterBills
{
	public string Name{get;set;}
	public int TotalBillCount{get;set;}
	public List<BillItemSummary> BillInfo{get;set;}
}








