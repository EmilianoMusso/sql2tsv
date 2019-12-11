# sql2tsv 

Exports SQL Server Table Data in TSV Format

### Command line parameters

```
  -u, --userid        Required.
  -p, --password      Required.
  -d, --database      Required.
  -s, --server        Required.
  -t, --table         Required.
  -f, --filter
  -o, --order
  -m, --maxrecords    (Default: 9999999)
  -c, --columns       (Default: *)
  -q, --query         (Default: )
  -h, --hasheaders    (Default: 1)
  -x, --separator     (Default:         )
  -l, --filllength    (Default: 0)
```

### Sample usages

##### Standard 
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
