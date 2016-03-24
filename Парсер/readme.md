Server API reference
=====================

- Server address - https://currency-dred95.rhcloud.com/


----------
To get the update currency rates you useful script **get_currency.php**

Input parameters:

 1. ***time*** - if specified, then displays the exchange rate after this time. Default = 0.
 2. ***limit*** - if specified, then limit output count. Default = 100.
 3. ***sign*** - currency name, for example **eurusd**.     *not implemented*.
 
 Output format:
 **int** time from 1.1.1970,**float** bid,**float** ask; 
 <br>
exmple:	1454716790,1.1157,1.116;1454716796,1.1156,1.1159

Error codes:

Code     | Description
-------- | ---
1 		| argument **time** not int
2	    | no one record has > **time**
3	    | argument **limit** not int
----------
To set prediction use **upload_prediction.php**

Input parameters:

 1. ***time*** - time prediction.
 2. ***value*** - value prediction.
 3. ***sign*** - currency name, for example **eurusd**.     *not implemented*.
 
 Output format:
 **string** OK; 
 <br>
exmple:	OK

Error codes:

Code     | Description
-------- | ---
1 		| argument **time** null or not int
2	    | argument **value** null or not int
