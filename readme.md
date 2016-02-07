Server API reference
=====================

- Server address - https://currency-dred95.rhcloud.com/


----------
To get the update currency rates you useful script **get_currency.php**

Input parameters:

 1. ***time*** - if specified, then displays the exchange rate after this time.
 2. ***sign*** - currency name, for example **eurusd**.     *not implemented*
 
 output format:
 **int** time from 1.1.1970,**float** bid,**float** ask; 
 <br>
exmple:	1454716790,1.1157,1.116;1454716796,1.1156,1.1159

Error codes:

Code     | Description
-------- | ---
1 		| argument **time** not int
2	    | no one record has > **time**
