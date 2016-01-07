# Reflection

I wanted to make passing SQL parameters to a stored procedure really easy.  

In the end I created an attribute for each property and used reflection to pass the property value to the DBCommand parameters before inserting into a table.

#Example

Have a look at the DataAttribute class and the example in Program.cs.

# Future

Looking at serializing the response from a database query to a class using reflection and attributes.
