<Query Kind="Expression">
  <Connection>
    <ID>042e522e-f743-4439-b32b-93aec46ead8b</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//multiple column group
//grouping data placed in a local temp data set for further processing
//.Key allows you to have access the value(s) in your group key(s)
//if you have multiple group columns they MUST be in an anonymous datatype
//to create a DTO type collection you can use .ToList() on the temp data set
//you can have a custom anonymous data collection by using a nested query

//Step A
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice}

//Step B DTO style dataset
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategoryID = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItems = tempdataset.ToList()
	}

//Step C DTO custom style dataset 
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategoryID = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItems = from x in tempdataset
							select new {
										ItemID = x.ItemID,
										FoodDescription = x.Description,
										TimesServed = x.BillItems.Count()
							}
				}