# Air_express

## ERD

![air express](./postgres_air_ERD.png)

## Ch:5-> Short Queries and indexes

### Notes

- For **short Queries** ("the querey which use low rows selectivity in which it access low portion of every table) to optimize you must use unique index which is low index selectivity so the optimizer can use index based scan effectivly or even index only scan.

- Is it a best practice to always create an index on a column that has a foreign key
constraint? Not always. An index should only be created if the number of distinct values
is large enough. Remember, indexes with high selectivity are unlikely to be useful. For
example, the flight table has a foreign key constraint on aircraft_code_id. This query selects all
fights between the JFK and ORD airports, between August 14 and 16, 2020. For each flight, we
select the flight number, scheduled departure, aircraft model, and number of passengers:

```sql
SELECT F.FLIGHT_NO,
 F.SCHEDULED_DEPARTURE,
 MODEL,
 COUNT(PASSENGER_ID) PASSENGERS
FROM FLIGHT F
JOIN BOOKING_LEG BL ON BL.FLIGHT_ID = F.FLIGHT_ID
JOIN PASSENGER P ON P.BOOKING_ID = BL.BOOKING_ID
JOIN AIRCRAFT AC ON AC.CODE = F.AIRCRAFT_CODE
WHERE F.DEPARTURE_AIRPORT = 'JFK'
 AND F.SCHEDULED_DEPARTURE BETWEEN '2020-08-14' AND '2020-08-16'
GROUP BY F.FLIGHT_NO,F.SCHEDULED_DEPARTURE,MODEL
```

![list_5-6](notes/list-5-6.png)
so -> Seq Scan on aircraft ac (cost=0.00..1.12 rows=12
The PostgreSQL optimizer accesses table statistics and is able to detect that the size
of the aircraft table is small and index access won’t be efficient

- A **column transformation** occurs when the search criteria are on some modifications of the values in a column. For example, lower(last_name), column transformations affect index use as in the B-Tree index the value of the attribute is compared to the value in the node. The transformed value is not recorded anywhere, so there is nothing to compare it to. Thus, if there is an index on last name  …the following search won’t be able to take advantage of the index:

```sql
SELECT * FROM account WHERE lower(last_name)='daniels';
```

A solution would be to create an (additional) functional index:

```sql
CREATE INDEX account_last_name_lower ON account (lower(last_name));
```

Also the following sql query doesn't use the index_scheduled_departure, Because when the timestamp is converted to a date, a column transformation has been performed.

```sql
SELECT * FROM flight WHERE scheduled_departure ::date BETWEEN '2020-08-17' AND '2020-08-19'
```

- **Excessive Selection Criteria** is Sometimes, when filtering logic is complex and involves attributes from multiple tables,
it is necessary to provide additional, redundant filters to prompt the database engine to use specific indexes or reduce the size of join arguments. This practice is called using excessive selection criteria. The intent is to use this additional filter to preselect a small subset of records from a large table.  For some of these complex criteria, PostgreSQL is able to perform a query rewrite automatically.
For example, This query looks for flights that were more than one hour delayed (of which there should not be many). For all of these delayed flights, the query selects boarding passes issued after the scheduled departure.

```sql
SELECT BP.UPDATE_TS BOARDING_PASS_ISSUED,
 SCHEDULED_DEPARTURE,
 ACTUAL_DEPARTURE,
 STATUS
FROM FLIGHT F
JOIN BOOKING_LEG BL USING (FLIGHT_ID)
JOIN BOARDING_PASS BP USING (BOOKING_LEG_ID)
WHERE BP.UPDATE_TS > SCHEDULED_DEPARTURE + interval '30 minutes'
 AND F.UPDATE_TS >= SCHEDULED_DEPARTURE - interval '1 hour'
```

![page81](notes/list-5-11.png)
the execution plan has full scans of large tables and hash joins, even though all the appropriate indexes on all the tables involved exist.
So here is the caveat: it is not just that a short query requires a small number of rows, but also that the number of rows in the result of any intermediate operation should also be small.
**How can this query be improved?**
the search space is all the flights since the dawn of time—or at least, for the entire time period captured by the database. However, this is an exception report, which most likely is reviewed on a regular cadence, and, likely, the business owner of this report is interested in recent cases since the last review. Earlier exceptions would have already appeared in previous reports and hopefully have been addressed. The next step would be to connect with the business owner of this report and ask whether a report including only the most recent exceptions suits their needs.  If the answer is yes, the excessive selection criterion we just got from business can be applied to the query. Also, we need one more index

```sql
CREATE INDEX boarding_pass_update_ts ON postgres_air.boarding_pass (update_ts);
```

```sql
SELECT bp.update_ts Boarding_pass_issued,
       scheduled_departure,
       actual_departure,
       status
FROM flight f
JOIN booking_leg bl USING (flight_id)
JOIN boarding_pass bp USING (booking_leg_id)
WHERE bp.update_ts  > scheduled_departure + interval '30 minutes'
AND f.update_ts >=scheduled_departure -interval '1 hour'
AND bp.update_ts >='2020-08-16' AND bp.update_ts< '2020-08-20'
```

![page82](notes/list-5-12.png)
