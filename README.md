# sql2tsv 

Exports SQL Server Table Data in TSV Format

Usage:
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE

sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE > table.txt
```

Filters for WHERE clause can be specified with **-f** parameter:
```batch
sql2tsv.exe -u USERID -p PASSWORD -i INSTANCE -d DATABASE -t TABLE -f "field01 < 1000 AND field02 = 0"
```