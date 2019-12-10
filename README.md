# sql2tsv 

Exports SQL Server Table Data in TSV Format

##### Standard usage
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE

sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE > table.txt
```

##### Add filters for WHERE clause 
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE -f "field01 < 1000 AND field02 = 0"
```

##### Query for N records (1000 in example)
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE -m 1000
```

##### Select two columns
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE -c "Field01, Field02"
```

##### Order by a column
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE -o "Field03"
```
